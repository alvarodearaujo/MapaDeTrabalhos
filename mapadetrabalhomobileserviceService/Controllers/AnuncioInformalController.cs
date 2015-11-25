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
    public class AnuncioInformalController : TableController<AnuncioInformal>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mapadetrabalhomobileserviceContext context = new mapadetrabalhomobileserviceContext();
            DomainManager = new EntityDomainManager<AnuncioInformal>(context, Request, Services);
        }

        // GET tables/AnuncioInformal
        public IQueryable<AnuncioInformal> GetAllAnuncioInformal()
        {
            return Query(); 
        }

        // GET tables/AnuncioInformal/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<AnuncioInformal> GetAnuncioInformal(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/AnuncioInformal/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<AnuncioInformal> PatchAnuncioInformal(string id, Delta<AnuncioInformal> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/AnuncioInformal
        public async Task<IHttpActionResult> PostAnuncioInformal(AnuncioInformal item)
        {
            AnuncioInformal current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/AnuncioInformal/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAnuncioInformal(string id)
        {
             return DeleteAsync(id);
        }

    }
}