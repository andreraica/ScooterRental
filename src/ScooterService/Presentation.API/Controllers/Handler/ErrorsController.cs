namespace ScooterRental.Presentation.WebAPI.Controllers.Handle
{
    using System;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public string Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; 
            var code = 500; 

            if (exception is ApplicationException) code = 409;

            Response.StatusCode = code; 

            return exception.Message; 
        }
    }
}
