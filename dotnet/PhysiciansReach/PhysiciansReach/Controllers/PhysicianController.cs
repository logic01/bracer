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
    public class PhysicianController : ControllerBase
    {
        private readonly IPhysicianBusiness _business;
        private readonly ILoggingBusiness _logging;

        public PhysicianController(IPhysicianBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }


        [HttpGet]
        public ActionResult<List<PhysicianModel>> Get([FromQuery]int[] ids)
        {
            try
            {
                if (ids.Length == 0)
                {
                    return _business.GetAll();
                }
                else
                {
                    return _business.Get(ids);
                }
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<PhysicianModel> Get(int id)
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
        public ActionResult<PhysicianModel> Post([FromBody] PhysicianModel physician)
        {
            try
            {
                physician.UserAccount.Type = AccountType.Physician;
                return _business.Create(physician);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PhysicianModel> Put(int id, [FromBody] PhysicianModel physician)
        {
            try
            {
                physician.UserAccount.Type = AccountType.Physician;
                physician.UserAccount.UserAccountId = id;
                return _business.Update(physician);
            }
            catch (Exception ex)
            {
                _logging.Log(LogSeverity.Error, ex.ToString());
                throw;
            }
        }
    }
}
