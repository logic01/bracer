using Microsoft.AspNetCore.Mvc;
using PhysiciansReach.Models;
using PR.Business.Interfaces;

namespace PhysiciansReach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicianController : ControllerBase
    {
        private readonly IPhysicianBusiness _business;

        public PhysicianController(IPhysicianBusiness business)
        {
            _business = business;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<PhysicianModel> Get(int id)
        {
            return _business.Get(id);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<PhysicianModel> Post([FromBody] PhysicianModel physician)
        {
            return _business.Create(physician);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<PhysicianModel> Put(int id, [FromBody] PhysicianModel physician)
        {
            return _business.Update(physician);
        }
    }
}
