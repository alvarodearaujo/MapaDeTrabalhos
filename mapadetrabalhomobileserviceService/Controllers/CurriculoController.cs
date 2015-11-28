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
    public class CurriculoController : TableController<Curriculo>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mapadetrabalhomobileserviceContext context = new mapadetrabalhomobileserviceContext();
            DomainManager = new EntityDomainManager<Curriculo>(context, Request, Services);
        }

        // GET tables/Curriculo
        public IQueryable<Curriculo> GetAllCurriculo()
        {
            return Query(); 
        }

        // GET tables/Curriculo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Curriculo> GetCurriculo(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Curriculo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Curriculo> PatchCurriculo(string id, Delta<Curriculo> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Curriculo
        public async Task<IHttpActionResult> PostCurriculo(Curriculo item)
        {
            Curriculo current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Curriculo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCurriculo(string id)
        {
             return DeleteAsync(id);
        }

    }
}