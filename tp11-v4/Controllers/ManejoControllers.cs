// ManejoControllers.cs
using Microsoft.AspNetCore.Http;
namespace tp10.Controllers;
public class ManejoController
{
    public bool IsLogged(HttpContext httpContext)
    {
        if (httpContext.Session != null && (httpContext.Session.GetString("NivelDeAcceso") == "admin" || httpContext.Session.GetString("NivelDeAcceso") == "simple"))
            return true;

        return false;
    }

    public bool IsAdmin(HttpContext httpContext)
    {
        if (httpContext.Session != null && httpContext.Session.GetString("NivelDeAcceso") == "admin")
            return true;

        return false;
    }
}
