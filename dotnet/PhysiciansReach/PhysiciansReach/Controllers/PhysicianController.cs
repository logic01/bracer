using Microsoft.AspNetCore.Mvc;
using PhysiciansReach.Models;
using PR.Business.Interfaces;
using System.Collections.Generic;

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

        [HttpGet]
        public ActionResult<List<PhysicianModel>> Get()
        {
            return _business.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<PhysicianModel> Get(int id)
        {
            return _business.Get(id);
        }
        [HttpPost]
        public ActionResult<PhysicianModel> Post([FromBody] PhysicianModel physician)
        {
            return _business.Create(physician);
        }
        [HttpPut("{id}")]
        public ActionResult<PhysicianModel> Put(int id, [FromBody] PhysicianModel physician)
        {
            return _business.Update(physician);
        }
    }
}
