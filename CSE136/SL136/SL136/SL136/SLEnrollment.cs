using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using POCO;
using BL;

namespace SL136
{
    public class SLEnrollment
    {
        public static string InsertEnrollment(Enrollment e, ref List<string> err)
        {
            return BLEnrollment.InsertEnrollment(e, ref err);
        }

        public static Enrollment GetEnrollment(string student_id, string schedule_id, ref List<string> err)
        {
            return BLEnrollment.GetEnrollment(student_id, schedule_id, ref err);
        }

        public static void UpdateEnrollment(Enrollment e, ref List<string> err)
        {
            BLEnrollment.UpdateEnrollment(e, ref err);
        }

        public static void DeleteEnrollment(string student_id, string schedule_id, ref List<string> err)
        {
            BLEnrollment.DeleteEnrollmentSchedule(student_id, schedule_id, ref err);
        }
    }
}
