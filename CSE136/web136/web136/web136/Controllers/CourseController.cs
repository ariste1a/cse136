﻿using System;
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
      /*
    public ActionResult InsertCourse(string title, string description)
    {
        PLCourse p = new PLCourse();
        p.title = title;
        p.description = description;
        p.level = null;
        CourseClientService.InsertCourse(p);

        return Content("great success!");
    }*/  
      /*
    public ActionResult InsertCourse(string title, string description)
    {
        PLCourse course = new PLCourse();
        course.title = title;
        course.description = description;
        //course.title = "dummy";
        //course.description = "dummy2"; 
        return Content(CourseClientService.InsertCourse(course));
        //return Content(course.title + ' ' + course.description);
    }*/ 
          
    public ActionResult InsertCourse(string title, string description)
    {
        PLCourse course = new PLCourse();
        course.title = title;
        course.description = description;
        //course.title = "dummy";
        //course.description = "dummy2"; 
        return Content(CourseClientService.InsertCourse(course));
        //return Content(course.title + ' ' + course.description);
    }
       
  }
}
