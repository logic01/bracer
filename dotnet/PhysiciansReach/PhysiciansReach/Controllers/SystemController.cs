using Microsoft.AspNetCore.Mvc;
using PR.Models;
using System;
using System.Reflection;

namespace PR.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get()
        {
            var assembly = Assembly.GetEntryAssembly();
            var statusModel = new SystemModel();

            statusModel.SystemTime = DateTime.Now;
            statusModel.Title = assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
            statusModel.Description = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
            statusModel.Version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            statusModel.FullName = Assembly.GetEntryAssembly().FullName;

            return Ok(statusModel);
        }

    }
}