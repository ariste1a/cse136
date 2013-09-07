using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc;
using web136.Models.Schedule;
using web136.Models.Student;
using web136.Models.Course;
namespace web136.Hubs
{
    public class classHub : Hub
    {
        public void login(string user)
        {
            Clients.All.getMessage("User " + user + " has logged in");
        }
        public void postMessage(string message)
        {
            Clients.All.getMessage(message);
        }
        public void addClass(string student_id, int schedule_id)
        {
            int classSize = StudentClientService.Enroll(student_id, schedule_id);
            Clients.All.getMessage(student_id + " has enrolled in " + schedule_id);
            Clients.All.updateSchedule(classSize, schedule_id); 
        }
        public void removeClass(string student_id, int schedule_id)
        {
            int classSize = StudentClientService.Drop(student_id, schedule_id);
            Clients.All.getMessage(student_id + " has dropped " + schedule_id);
            Clients.All.updateSchedule(classSize, schedule_id); 
        }
       
        public void addCourse(string title, string description)
        {
            PLCourse course = new PLCourse();
            course.title = title;
            course.description = description;
            CourseClientService.InsertCourse(course);
            Clients.All.getMessage(title + " has been created"); 
        }

        public void addSchedule(int course_id, string year, string session, string schedule_day_id, string schedule_time_id, int quota, string type, string quarter, int instructor_id)
        {
            SLSchedule.Schedule schedule = new SLSchedule.Schedule();
            schedule.course.id = course_id.ToString();
            schedule.year = year;
            schedule.session = session;
            schedule.day = schedule_day_id;
            schedule.time = schedule_time_id;
            schedule.instructor= instructor_id.ToString(); 
            schedule.quota = quota;
            schedule.type = type;
            schedule.quarter = quarter;
            ScheduleClientService.addSchedule(schedule); 
            Clients.All.getMessage("A new Schedule has been created");
            Clients.All.addSchedule(schedule);
        }

    }
}