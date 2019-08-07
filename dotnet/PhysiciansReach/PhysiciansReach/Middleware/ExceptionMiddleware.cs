using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PR.Business.Interfaces;
using PR.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PR.Api.Filters
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILoggingBusiness logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, logger, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ILoggingBusiness logger, Exception ex)
        {
            var error = new ErrorModel
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            };

            //  logger.Log(LogSeverity.Error, ex.Message, ex.StackTrace);

            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)error.StatusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
