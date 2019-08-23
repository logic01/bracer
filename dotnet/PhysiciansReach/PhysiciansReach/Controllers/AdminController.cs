using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
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
            try
            {
                return _business.Get();
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<AdminModel> Get(int id)
        {
            try
            {
                return _business.Get(id);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpPost]
        public ActionResult<AdminModel> Post([FromBody] AdminModel admin)
        {
            try
            {
                admin.UserAccount.Type = AccountType.Admin;

                return _business.Create(admin);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] AdminModel admin)
        {
            try
            {
                admin.UserAccount.Type = AccountType.Admin;
                admin.UserAccount.UserAccountId = id;

                var adminId = _business.Update(admin);

                return CreatedAtAction("Post", new { adminId });
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }
    }
}
