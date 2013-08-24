using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc;
using web136.Models.Student;

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
            StudentClientService.Enroll(student_id, schedule_id);
            Clients.All.getMessage(student_id + " has enrolled in " + schedule_id);
        }
        public void removeClass(string student_id, int schedule_id)
        {
            StudentClientService.Drop(student_id, schedule_id);
            Clients.All.getMessage(student_id + " has dropped " + schedule_id);
        }
    }
}