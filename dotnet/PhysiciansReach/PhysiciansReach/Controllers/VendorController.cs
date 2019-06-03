using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Constants.Enums;
using PR.Models;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorBusiness _business;
        private readonly ILoggingBusiness _logging;

        public VendorController(IVendorBusiness business, ILoggingBusiness logging)
        {
            _business = business;
            _logging = logging;
        }

        [HttpGet]
        public ActionResult<List<VendorModel>> Get()
        {
            _logging.Log(LogSeverity.Info, "Get All Vendor");
            return _business.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<VendorModel> Get(int id)
        {
            _logging.Log(LogSeverity.Info, "Get Vendor");
            return _business.Get(id);
        }

        [HttpPost]
        public ActionResult<VendorModel> Post([FromBody] VendorModel vendor)
        {
            _logging.Log(LogSeverity.Info, "Post Vendor");
            return _business.Create(vendor);
        }

        [HttpPut("{id}")]
        public ActionResult<VendorModel> Put(int id, [FromBody] VendorModel vendor)
        {
            _logging.Log(LogSeverity.Info, "Put Vendor");

            vendor.VendorId = id;

            return _business.Update(vendor);
        }
    }
}
