using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using mapadetrabalhomobileserviceService.DataObjects;
using mapadetrabalhomobileserviceService.Models;

namespace mapadetrabalhomobileserviceService.Controllers
{
    public class AnuncioController : TableController<Anuncio>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mapadetrabalhomobileserviceContext context = new mapadetrabalhomobileserviceContext();
            DomainManager = new EntityDomainManager<Anuncio>(context, Request, Services);
        }

        // GET tables/Anuncio
        public IQueryable<Anuncio> GetAllAnuncio()
        {
            return Query(); 
        }

        // GET tables/Anuncio/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Anuncio> GetAnuncio(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Anuncio/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Anuncio> PatchAnuncio(string id, Delta<Anuncio> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Anuncio
        public async Task<IHttpActionResult> PostAnuncio(Anuncio item)
        {
            Anuncio current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Anuncio/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAnuncio(string id)
        {
             return DeleteAsync(id);
        }

    }
}