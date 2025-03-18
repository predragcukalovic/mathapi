using Papi.GameServer.Utils.Logging;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Papi.GameServer.Math.Api.Exceptions
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        #region Public Methods

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var msg = new
            {
                Uri = actionExecutedContext.Request.Method.Method + " Uri: " + actionExecutedContext.Request.RequestUri,
                actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                Arguments = actionExecutedContext.ActionContext.ActionArguments
            };

            Logger.LogError(actionExecutedContext.Exception, "Global exception filter: {@GlobalExceptionMessage}", msg);
            actionExecutedContext.Response =
                actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        #endregion
    }
}