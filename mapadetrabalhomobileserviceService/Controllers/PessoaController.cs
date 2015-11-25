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
    public class PessoaController : TableController<Pessoa>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            mapadetrabalhomobileserviceContext context = new mapadetrabalhomobileserviceContext();
            DomainManager = new EntityDomainManager<Pessoa>(context, Request, Services);
        }

        // GET tables/Pessoa
        public IQueryable<Pessoa> GetAllPessoa()
        {
            return Query(); 
        }

        // GET tables/Pessoa/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Pessoa> GetPessoa(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Pessoa/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Pessoa> PatchPessoa(string id, Delta<Pessoa> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Pessoa
        public async Task<IHttpActionResult> PostPessoa(Pessoa item)
        {
            Pessoa current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Pessoa/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePessoa(string id)
        {
             return DeleteAsync(id);
        }

    }
}