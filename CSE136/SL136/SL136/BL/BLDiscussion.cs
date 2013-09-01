using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;
using DAL;

namespace BL
{
    public class BLDiscussion
    {
        public static List<Schedule> GetDiscussions(int class_id, ref List<string> errors)
        {
            if (class_id <= 0)
            {
                errors.Add("Invalid Discussion ID");
            }
            if (errors.Count > 0)
                return null;
            return DALDiscussion.GetDiscussions(class_id, ref errors); 
        }
        public static string createDiscussion(int lecture_id, string session, int day, int time, int instructor, int quota, ref List<String> errors)
        {
            int diTotalQuota = 0; 
            if (lecture_id <=0 || session ==null || day <=0 || time <= 0 || quota <=0)
            {
                errors.Add("Invalid Argument(s)"); 
            }
            Schedule course_instance = DALSchedule.GetSchedule(lecture_id, ref errors);
            //get all dicussions for this particular lecture
            List<Schedule> dicussions = DALDiscussion.GetDiscussions(Convert.ToInt32(course_instance.course.id), ref errors);
            //adds up each dicussion's quota
            foreach (Schedule di in dicussions)
            {
                diTotalQuota += di.quota;
            }

            //check if this dicussion would violate class size 
            if (quota + diTotalQuota > course_instance.quota)
            {
                errors.Add("Discussion class size greater than Lecture class size");
                return "-1";
            }

            string id = createDiscussion(lecture_id, session, day, time, instructor, quota, ref errors);
            if (errors.Count > 0)
                return null;
            return id; 
        }

        public static Boolean removeDiscussion(string discussion_id, ref List<String> errors)
        {
            if (DALDiscussion.GetDiscussions(Convert.ToInt32(discussion_id), ref errors).Count <= 0 )
            {
                errors.Add("Invalid Discussion ID"); 
            }
            if (discussion_id == null)
            {
                errors.Add("Invalid Discussion ID");
            }
            if (errors.Count > 0)
                return false;
            return removeDiscussion( discussion_id, ref errors);
        }
    }
}
