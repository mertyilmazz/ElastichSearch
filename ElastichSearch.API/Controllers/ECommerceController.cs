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

        [HttpPost]
        public async Task<IActionResult> TermsQuery([FromBody] List<string> customerFirstNameList)
        {
            var result = await _eCommerceService.TermsQueryByCustomerFullName(customerFirstNameList);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PrefixQuery(string customerFullName)
        {
            var result = await _eCommerceService.PrefixQuery(customerFullName);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> RangeQuery(double fromPrice, double toPrice)
        {
            var result = await _eCommerceService.RangeQuery(fromPrice, toPrice);
            return Ok(result);
        }
    }
}
