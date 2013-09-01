using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;
using DAL;


namespace BL
{
    public class BLEnrollment
    {
        public static string InsertEnrollment(Enrollment e, ref List<string> errors)
        {
            //need to check here if there's enough space in the class. 
            Schedule schedule = DALSchedule.GetSchedule(Convert.ToInt32(e.schedule_id), ref errors);
            if (schedule.enrollments + 1 > schedule.quota)
            {
                errors.Add("Lecture" + schedule.course.title + " is full"); 
                return "-1";
            } 
            string id = DALEnrollment.InsertEnrollment(e, ref errors);
            if (id != null)
            {
                return id;
            }
            return "-1";
        }

        public static Enrollment GetEnrollment(string student_id, string schedule_id, ref List<string> errors)
        {
            if (student_id == null)
            {
                errors.Add("Invalid student ID");
            }
            if (schedule_id == null)
            {
                errors.Add("Invalid Schedule ID");
            }
            if (errors.Count > 0)
                return null;
            return DALEnrollment.GetEnrollmentGrade(student_id, schedule_id, ref errors);
        }

       
        public static void UpdateEnrollment(Enrollment en, ref List<string> errors)
        {
            if (en == null)
            {
                errors.Add("Invalid Enrollment"); 
            }
            if (errors.Count > 0)
                return;
            DALEnrollment.UpdateEnrollment(en, ref errors);
        }
         

        public static void DeleteEnrollmentSchedule(string student_id, string schedule_id, ref List<string> errors)
        {
            if (student_id == null || schedule_id == null)
            {
                errors.Add("Invalid Arguments");
            }
            DALEnrollment.DeleteEnrollmentSchedule(student_id, schedule_id, ref errors);
            if (errors.Count > 0)
                return;
        }


    }
}
