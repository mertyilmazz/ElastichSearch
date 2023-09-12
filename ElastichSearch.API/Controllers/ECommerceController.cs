using ElastichSearch.API.Services;
using ElasticSearch.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly ECommerceService _eCommerceService;


        public ECommerceController(ECommerceService eCommerceService)
        {
            _eCommerceService = eCommerceService;
        }

        [HttpGet]
        public async Task<IActionResult> TermQuery(string customerName)
        {
            var result = await _eCommerceService.TermQueryByCustomerFullName(customerName);
            return Ok(result);
        }
    }
}
