using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web136.Controllers
{
  public class HomeController : Controller
  {
    //
    // GET: /Home/
    // Display the homepage 
    public ActionResult Index()
    {
      return View();
    }
  }
}
