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
            string id = DALEnrollment.InsertEnrollment(e, ref errors);
            if (id != null)
            {
                return id;
            }
            return "-1";
        }

        public static Enrollment GetEnrollment(string student_id, string schedule_id, ref List<string> errors)
        {
            return DALEnrollment.GetEnrollmentGrade(student_id, schedule_id, ref errors);
        }

       
        public static void UpdateEnrollment(Enrollment en, ref List<string> errors)
        {
            DALEnrollment.UpdateEnrollment(en, ref errors);
        }
         

        public static void DeleteEnrollmentSchedule(string student_id, string schedule_id, ref List<string> errors)
        {
            DALEnrollment.DeleteEnrollmentSchedule(student_id, schedule_id, ref errors); 
        }


    }
}
