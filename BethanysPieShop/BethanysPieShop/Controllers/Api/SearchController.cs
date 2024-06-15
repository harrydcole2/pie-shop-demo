using Microsoft.AspNetCore.Http;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    [Route("api/[controller]")] //attribute based routing; auto-searched and tabled by the framework; [controller] is a token that will be replaced by the name of the controller class without the Controller suffix
    [ApiController] //optional but recommended; adds some default behaviors to the controller
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allPies = _pieRepository.AllPies;
            return Ok(allPies);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if(!_pieRepository.AllPies.Any(p => p.PieId == id))
            {
                return NotFound();
            }
            return Ok(_pieRepository.AllPies.Where(p=> p.PieId == id));
        }

        [HttpPost]
        public IActionResult SearchPies([FromBody] string searchQuery)
        {
            IEnumerable<Pie> pies = new List<Pie>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                pies = _pieRepository.SearchPies(searchQuery);
            }

            return new JsonResult(pies);
        }
    }
}
