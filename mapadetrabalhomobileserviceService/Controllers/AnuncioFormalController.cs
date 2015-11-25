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
    public class AnuncioFormalController : TableController<AnuncioFormal>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mapadetrabalhomobileserviceContext context = new mapadetrabalhomobileserviceContext();
            DomainManager = new EntityDomainManager<AnuncioFormal>(context, Request, Services);
        }

        // GET tables/AnuncioFormal
        public IQueryable<AnuncioFormal> GetAllAnuncioFormal()
        {
            return Query(); 
        }

        // GET tables/AnuncioFormal/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<AnuncioFormal> GetAnuncioFormal(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/AnuncioFormal/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<AnuncioFormal> PatchAnuncioFormal(string id, Delta<AnuncioFormal> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/AnuncioFormal
        public async Task<IHttpActionResult> PostAnuncioFormal(AnuncioFormal item)
        {
            AnuncioFormal current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/AnuncioFormal/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAnuncioFormal(string id)
        {
             return DeleteAsync(id);
        }

    }
}