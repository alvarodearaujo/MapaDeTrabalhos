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
    public class ExperienciaProfissionalController : TableController<ExperienciaProfissional>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mapadetrabalhomobileserviceContext context = new mapadetrabalhomobileserviceContext();
            DomainManager = new EntityDomainManager<ExperienciaProfissional>(context, Request, Services);
        }

        // GET tables/ExperienciaProfissional
        public IQueryable<ExperienciaProfissional> GetAllExperienciaProfissional()
        {
            return Query(); 
        }

        // GET tables/ExperienciaProfissional/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ExperienciaProfissional> GetExperienciaProfissional(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ExperienciaProfissional/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ExperienciaProfissional> PatchExperienciaProfissional(string id, Delta<ExperienciaProfissional> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/ExperienciaProfissional
        public async Task<IHttpActionResult> PostExperienciaProfissional(ExperienciaProfissional item)
        {
            ExperienciaProfissional current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ExperienciaProfissional/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteExperienciaProfissional(string id)
        {
             return DeleteAsync(id);
        }

    }
}