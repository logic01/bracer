using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PhysiciansReach.Controllers
{
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult Post(IFormCollection collection)
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(int id)
        {
            return Ok();
        }


    }
}