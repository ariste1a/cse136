using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;
using DAL;

namespace BL
{
    public class Discussion
    {
        public static List<Schedule> GetDiscussions(int class_id, ref List<string> errors)
        {
            return DALDiscussion.GetDiscussions(class_id, ref errors); 
        }
        public static string createDiscussion(int lecture_id, string session, int day, int time, int instructor, int quota, ref List<String> errors)
        {
            string id = createDiscussion(lecture_id, session, day, time, instructor, quota, ref errors);
            return id; 
        }
        public static Boolean removeDiscussion(string discussion_id, ref List<String> errors)
        {
            return removeDiscussion( discussion_id, ref errors);
        }
    }
}
