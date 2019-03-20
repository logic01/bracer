using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhysiciansReach.Models;
using PR.Business.Interfaces;

namespace PhysiciansReach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBusiness _business;
        public UsersController(IUserBusiness business)
        {
            _business = business;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            return new UserModel[] { new UserModel(), new UserModel() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<UserModel> Get(int id)
        {
            return new UserModel();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] UserModel value)
        {
            _business.SaveUser();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserModel value)
        {
        }
    }
}
