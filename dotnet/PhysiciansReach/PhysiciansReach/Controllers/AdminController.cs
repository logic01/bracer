using Microsoft.AspNetCore.Mvc;
using PhysiciansReach.Models;
using PR.Business.Interfaces;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusiness _business;

        public AdminController(IAdminBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        public ActionResult<List<AdminModel>> Get()
        {
            return _business.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<AdminModel> Get(int id)
        {
            return _business.Get(id);
        }

        [HttpPost]
        public ActionResult<AdminModel> Post([FromBody] AdminModel admin)
        {
            return _business.Create(admin);
        }

        [HttpPut("{id}")]
        public ActionResult<AdminModel> Put(int id, [FromBody] AdminModel admin)
        {
            return _business.Update(admin);
        }
    }
}
