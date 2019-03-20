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
        public ActionResult<IEnumerable<UserAccountModel>> Get()
        {
            return new UserAccountModel[] { new UserAccountModel(), new UserAccountModel() };
        }

        // GET api/values/5
        [HttpGet("{userId}")]
        public ActionResult<UserAccountModel> Get(int userId)
        {
            return _business.Get(userId);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<UserAccountModel> Post([FromBody] UserAccountModel value)
        {
            return _business.Create(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<UserAccountModel> Put(int id, [FromBody] UserAccountModel value)
        {
            return _business.Update(value);
        }
    }
}
