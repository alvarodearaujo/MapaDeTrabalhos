using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using mapadetrabalhomobileserviceService.Models;
using mapadetrabalhomobileserviceService.DataObjects;

namespace mapadetrabalhomobileserviceService.Controllers
{
    public class EnderecoController : TableController<Endereco>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mapadetrabalhomobileserviceContext context = new mapadetrabalhomobileserviceContext();
            DomainManager = new EntityDomainManager<Endereco>(context, Request, Services);
        }

        // GET tables/Endereco
        public IQueryable<Endereco> GetAllEndereco()
        {
            return Query(); 
        }

        // GET tables/Endereco/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Endereco> GetEndereco(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Endereco/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Endereco> PatchEndereco(string id, Delta<Endereco> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Endereco
        public async Task<IHttpActionResult> PostEndereco(Endereco item)
        {
            Endereco current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Endereco/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEndereco(string id)
        {
             return DeleteAsync(id);
        }

    }
}