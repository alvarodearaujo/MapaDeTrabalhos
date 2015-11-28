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
    public class IdiomaController : TableController<Idioma>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mapadetrabalhomobileserviceContext context = new mapadetrabalhomobileserviceContext();
            DomainManager = new EntityDomainManager<Idioma>(context, Request, Services);
        }

        // GET tables/Idioma
        public IQueryable<Idioma> GetAllIdioma()
        {
            return Query(); 
        }

        // GET tables/Idioma/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Idioma> GetIdioma(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Idioma/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Idioma> PatchIdioma(string id, Delta<Idioma> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Idioma
        public async Task<IHttpActionResult> PostIdioma(Idioma item)
        {
            Idioma current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Idioma/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteIdioma(string id)
        {
             return DeleteAsync(id);
        }

    }
}