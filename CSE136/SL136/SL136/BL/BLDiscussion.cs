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
            List<Schedule> discussions = DALDiscussion.GetDiscussions(lecture_id, ref errors);
            //adds up each dicussion's quota
            if (discussions != null)
            {
                foreach (Schedule di in discussions)
                {
                    diTotalQuota += di.quota;
                }
            }
            //check if this dicussion would violate class size 
            if (quota + diTotalQuota > course_instance.quota)
            {
                errors.Add("Discussion class size greater than Lecture class size");                
            }
            if (errors.Count > 0)
                return null;
            string id = DALDiscussion.createDiscussion(lecture_id, session, day, time, instructor, quota, ref errors);            
            return id; 
        }

        public static Boolean removeDiscussion(string discussion_id, ref List<String> errors)
        {
            if (discussion_id == null)
            {
                errors.Add("Invalid Discussion ID");
            }
            return DALDiscussion.removeDiscussion(discussion_id, ref errors);
        }
    }
}
