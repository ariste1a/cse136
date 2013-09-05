using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web136.Models.Admin;

namespace web136.Controllers
{
  public class AdminController : Controller
  {
    //
    // GET: /Admin/
    public ActionResult Index()
    {
      return View();
    }

    // GET: /Admin/Edit
    public ActionResult Edit()
    {
      return View();
    }

    public ActionResult Manage()
    {
        return View();
    }

    public ActionResult MakeCourse()
    {
        return View();
    }

    // Get /Admin/GetAdminInfo -- call by AdminModel.js through ajax
    public string GetAdminInfo(string AdminId)
    {
      // this would normally call a service to obtain the info.  But hard-coding it for now
      // to demo this GetAdminInfo() can be called by $ajax in AdminModel.js
      PLAdmin adminInfo = new PLAdmin();

      if (AdminId == "1")
      {
        adminInfo.FirstName = "Isaac";
        adminInfo.LastName = "Chu";
      }
      else
      {
        // use wife's name
        adminInfo.FirstName = "Becky";
        adminInfo.LastName = "Zhang";
      }
      string adminInfoJson = new JavaScriptSerializer().Serialize(adminInfo);
      return adminInfoJson;
    }

    [HttpPost]
    public string UPdateAdminInfo(PLAdmin adminInfo)
    {
      return "Update admin info successfully";
    }

  }
}
