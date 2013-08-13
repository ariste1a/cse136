using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web136.Models.Logon;

namespace web136.Controllers
{
  public class LoginController : Controller
  {
    //
    // GET: /Login
    // Display login 
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Index(FormCollection collection)
    {
      try
      {
        string email = collection["email"];
        string password = collection["Password"];

        PLLogon logon = LogonClientService.Validate(email, password);
      //  Console.writeLine(logon);

        if (logon.Role.Equals("invalid"))
        {
          // ViewBag is a way to pass info from controller to the view page.
          ViewBag.message = "Invalid login";
        }
        else
        {
          Session["role"] = logon.Role;
          Session["id"] = logon.ID;
          Session["email"] = email;

          if (Session["role"].Equals("admin"))
          {
            return RedirectToAction("Index", "Admin");
          }
          else if (Session["role"].Equals("staff"))
          {
            return RedirectToAction("Index", "Student");
          }
          else if (Session["role"].Equals("student"))
          {
            return RedirectToAction("Index", "Schedule");
          }
        }
      }
      catch
      {
        return View("Index");
      }
      return View("Index");
    }

    public ActionResult LogOff()
    {
      Session["role"] = null;
      Session["id"] = null;
      Session["email"] = null;
      return RedirectToAction("Index", "Home");
    }
  }
}
