using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class HomeController : BaseController
{
    public HomeController()
    {
    }
    public ActionResult Index()
    {
        return Redirect(Url.Action("index", "messages"));
    }
}