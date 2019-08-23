using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PR.Business.Interfaces;
using PR.Models;
using System.Collections.Generic;

namespace PhysiciansReach.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
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
            return _business.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<VendorModel> Get(int id)
        {
            return _business.Get(id);
        }

        [HttpPost]
        public ActionResult<VendorModel> Post([FromBody] VendorModel vendor)
        {
            return _business.Create(vendor);
        }

        [HttpPut("{id}")]
        public ActionResult<VendorModel> Put(int id, [FromBody] VendorModel vendor)
        {
            vendor.VendorId = id;

            return _business.Update(vendor);
        }
    }
}
