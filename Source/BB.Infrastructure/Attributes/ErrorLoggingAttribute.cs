namespace BB.Infrastructure.Attributes
{
  using Microsoft.AspNetCore.Mvc.Filters;
  using Serilog;

  public class ErrorLoggingAttribute : ExceptionFilterAttribute
  {
    public override void OnException(ExceptionContext context)
    {
      base.OnException(context);

      Log.Logger.Error(context.Exception, context.Exception.Message);
    }
  }
}
