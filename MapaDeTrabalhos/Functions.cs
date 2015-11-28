using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace MapaDeTrabalhos
{
    class Functions
    {
        public static async Task<byte[]> ConvertStorageFileToArrayBytes(StorageFile file)
        {
            byte[] fileBytes = null;
            using (IRandomAccessStreamWithContentType stream = await file.OpenReadAsync())
            {
                fileBytes = new byte[stream.Size];
                using (DataReader reader = new DataReader(stream))
                {
                    await reader.LoadAsync((uint)stream.Size);
                    reader.ReadBytes(fileBytes);
                }
            }

            // return to azure bytes
            return fileBytes;
        }

        public static BitmapImage ConvertArrayBytesToImage(byte[] arquivo)
        {
            BitmapImage image = null;
            using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(arquivo);
                    writer.StoreAsync().GetResults();
                }

                image = new BitmapImage();
                image.SetSource(ms);
            }

            return image;
        }
    }
}
