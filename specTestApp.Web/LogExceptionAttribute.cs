using System.Web.Mvc;
using Newtonsoft.Json;

namespace specTestApp.Web
{
    public class LogExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext exceptionContext)
        {
            Logger.Error(JsonConvert.SerializeObject(exceptionContext.Exception));
        }
    }
}