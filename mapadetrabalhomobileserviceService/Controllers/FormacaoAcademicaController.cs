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
    public class FormacaoAcademicaController : TableController<FormacaoAcademica>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mapadetrabalhomobileserviceContext context = new mapadetrabalhomobileserviceContext();
            DomainManager = new EntityDomainManager<FormacaoAcademica>(context, Request, Services);
        }

        // GET tables/FormacaoAcademica
        public IQueryable<FormacaoAcademica> GetAllFormacaoAcademica()
        {
            return Query(); 
        }

        // GET tables/FormacaoAcademica/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<FormacaoAcademica> GetFormacaoAcademica(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/FormacaoAcademica/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<FormacaoAcademica> PatchFormacaoAcademica(string id, Delta<FormacaoAcademica> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/FormacaoAcademica
        public async Task<IHttpActionResult> PostFormacaoAcademica(FormacaoAcademica item)
        {
            FormacaoAcademica current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/FormacaoAcademica/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFormacaoAcademica(string id)
        {
             return DeleteAsync(id);
        }

    }
}