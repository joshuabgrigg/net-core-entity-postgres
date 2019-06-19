using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{

    protected BaseController()
    {
    }
}