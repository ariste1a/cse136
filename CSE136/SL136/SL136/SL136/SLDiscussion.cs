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
    public class SLDiscussion
    {
        public static List<Schedule> GetDiscussions(int class_id, ref List<string> errors)
        {
            return BLDiscussion.GetDiscussions(class_id, ref errors);
        }
        public static string createDiscussion(int lecture_id, string session, int day, int time, int instructor, int quota, ref List<String> errors)
        {
            return BLDiscussion.createDiscussion(lecture_id, session, day, time, instructor, quota, ref errors);            
        }
        public static Boolean removeDiscussion(string discussion_id, ref List<String> errors)
        {
            return BLDiscussion.removeDiscussion(discussion_id, ref errors);
        }
    }
}
