using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web136.Models.Course;

namespace web136.Controllers
{
  public class CourseController : Controller
  {
    //
    // GET: /Course/
    public ActionResult GetCourseList()
    {
      List<PLCourse> courseList = CourseClientService.GetCourseList();
      JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
      string courseListJson = jsonSerialiser.Serialize(courseList);

      // return the JSON string
      return Content(courseListJson);
    }
  }
}
