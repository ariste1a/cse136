using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web136.Models.Student;
using System.Web.Script.Serialization;

namespace web136.Controllers
{
  public class StudentController : Controller
  {
    public string GetNumStudentsTotal()
    {
      List<PLStudent> list = StudentClientService.GetStudentList();
      return list.Count.ToString();
    }

    public JsonResult GetSampleStudent()
    {
      List<PLStudent> list = StudentClientService.GetStudentList();

      return this.Json(list[0], JsonRequestBehavior.AllowGet);
    }


    public ActionResult Index()
    {
      List<PLStudent> list = StudentClientService.GetStudentList();
      ViewBag.breadCrumbData = "Student List";

      return View("List", list);
    }

    //
    // GET: /Student/Create
    public ActionResult Create()
    {
      if (HttpContext != null)
      {
        UrlHelper url = new UrlHelper(HttpContext.Request.RequestContext);
        ViewBag.breadCrumbData = "<a href='" + url.Action("Index", "Student") + "'>Student List</a>";
        ViewBag.breadCrumbData += " > Create";
      }
      PLStudent student = new PLStudent();
      return View("Create", student);
    }

    //
    // POST: /Student/Create
    [HttpPost]
    public ActionResult Create(FormCollection collection)
    {
      try
      {
        PLStudent student = new PLStudent();
        student.ID = collection["ID"];
        student.FirstName = collection["FirstName"];
        student.LastName = collection["LastName"];
        student.SSN = collection["SSN"];
        student.EmailAddress = collection["EmailAddress"];
        student.Password = collection["Password"];
        StudentClientService.CreateStudent(student);
        return RedirectToAction("Index");
      }
      catch (Exception e)
      {
        Console.Write(e.ToString());
        return View();
      }
    }

    // GET: /Student/GetStudentScheudle
    public ActionResult GetStudentSchedule(string id)
    {
        List<string> schedule_ids = StudentClientService.GetStudentSchedule(id);
        JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
        string ScheduleListJson = jsonSerialiser.Serialize(schedule_ids);
        return Content(ScheduleListJson);
    }

    //
    // GET: /Student/Create
    public ActionResult Edit(string id)
    {
      if (HttpContext != null)
      {
        UrlHelper url = new UrlHelper(HttpContext.Request.RequestContext);
        ViewBag.breadCrumbData = "<a href='" + url.Action("Index", "Student") + "'>Student List</a>";
        ViewBag.breadCrumbData += " > Edit";
      }

      PLStudent student = StudentClientService.GetStudentDetail(id);
      return View("Edit", student);
    }

    //
    // POST: /Student/Edit/
    [HttpPost]
    public ActionResult Edit(string id, FormCollection collection)
    {
      try
      {
        PLStudent student = new PLStudent();
        student.ID = collection["ID"];
        student.FirstName = collection["FirstName"];
        student.LastName = collection["LastName"];
        student.SSN = collection["SSN"];
        student.EmailAddress = collection["EmailAddress"];
        student.Password = collection["Password"];
        StudentClientService.UpdateStudent(student);
        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
    }

    //
    // GET: /Student/Delete/5

    public ActionResult Delete(string id)
    {
      try
      {
        bool success = StudentClientService.Delete(id);

        if (success)
          return RedirectToAction("Index");

        return RedirectToAction("Error");
      }
      catch
      {
        return View();
      }
    }

    //
    // GET: /Student/Enroll/100
    public ActionResult Enroll(int schedule_id)
    {
      string student_id = Session["id"].ToString();

      if (HttpContext != null)
      {
        UrlHelper url = new UrlHelper(HttpContext.Request.RequestContext);
        ViewBag.breadCrumbData = "<a href='" + url.Action("Index", "Student") + "'>Student List</a>";
        ViewBag.breadCrumbData += " > Details";
      }
      StudentClientService.Enroll(student_id, schedule_id);

      PLStudent student = StudentClientService.GetStudentDetail(student_id);
      return RedirectToAction("Index", "Schedule", student);

    }

    //
    // GET: /Student/Drop/100
    public ActionResult Drop(int schedule_id)
    {
      string student_id = Session["id"].ToString();

      if (HttpContext != null)
      {
        UrlHelper url = new UrlHelper(HttpContext.Request.RequestContext);
        ViewBag.breadCrumbData = "<a href='" + url.Action("Index", "Student") + "'>Student List</a>";
        ViewBag.breadCrumbData += " > Details";
      }
      StudentClientService.Drop(student_id, schedule_id);

      PLStudent student = StudentClientService.GetStudentDetail(student_id);
      return RedirectToAction("Index", "Schedule", student);
    }

    public ActionResult Error()
    {
      return View("Error");
    }

  }
}
