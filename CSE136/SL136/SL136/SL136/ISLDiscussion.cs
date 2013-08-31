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
    [ServiceContract]
    public interface ISLDiscussion
    {
        [OperationContract]
        public static List<Schedule> GetDiscussions(int class_id, ref List<string> errors);

        [OperationContract]
        public static string createDiscussion(int lecture_id, string session, int day, int time, int instructor, int quota, ref List<String> errors);

        [OperationContract]
        public static Boolean removeDiscussion(string discussion_id, ref List<String> errors);        
    }
}
