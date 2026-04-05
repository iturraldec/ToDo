using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace Api.Middlewares;
public class ExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;

  public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(context, ex);
    }
  }
  private static Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    var code = HttpStatusCode.InternalServerError; // 500 por defecto
    var result = exception.Message;

    // Aquí mapeamos nuestras excepciones de dominio a códigos HTTP
    if (exception is AlreadyExistsException) code = HttpStatusCode.Conflict; // 409
    if (exception is NotFoundException) code = HttpStatusCode.NotFound;     // 404
    if (exception is ArgumentException) code = HttpStatusCode.BadRequest;   // 400

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)code;

    return context.Response.WriteAsync(JsonSerializer.Serialize(new { 
        error = result, 
        status = (int)code 
    }));
  }
}