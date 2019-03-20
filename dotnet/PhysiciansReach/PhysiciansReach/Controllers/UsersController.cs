using Microsoft.AspNetCore.Mvc;
using PhysiciansReach.Models;
using PR.Business.Interfaces;
using System.Collections.Generic;

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
        [HttpGet("{userId}")]
        public ActionResult<UserModel> Get(int userId)
        {
            return _business.Get(userId);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<UserModel> Post([FromBody] UserModel value)
        {
            return _business.Create(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<UserModel> Put(int id, [FromBody] UserModel value)
        {
            return _business.Update(value);
        }
    }
}
