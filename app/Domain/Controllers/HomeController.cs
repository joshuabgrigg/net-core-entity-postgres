using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class HomeController : BaseController
{
    public HomeController()
    {
    }

    [Route("/")]
    public ActionResult Index()
    {
        return Redirect(Url.Action("", "messages"));
    }
}