﻿using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusiness _business;
        private readonly ILoggingBusiness _logging;

        public AdminController(IAdminBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }

        [HttpGet]
        public ActionResult<List<AdminModel>> Get()
        {
            _logging.Log(LogSeverity.Info, "Get All Admin");
            return _business.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<AdminModel> Get(int id)
        {
            _logging.Log(LogSeverity.Info, "Get Admin");
            return _business.Get(id);
        }

        [HttpPost]
        public ActionResult<AdminModel> Post([FromBody] AdminModel admin)
        {
            admin.UserAccount.Type = AccountType.Admin;
            _logging.Log(LogSeverity.Info, "Post Admin");

            return _business.Create(admin);

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AdminModel admin)
        {
            _logging.Log(LogSeverity.Info, "Put Admin");

            admin.UserAccount.Type = AccountType.Admin;
            admin.UserAccount.UserAccountId = id;

            var adminId = _business.Update(admin);

            return CreatedAtAction("Post", new { adminId });
        }
    }
}
