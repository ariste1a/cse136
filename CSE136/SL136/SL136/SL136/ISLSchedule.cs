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
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISLSchedule" in both code and config file together.
  [ServiceContract]
  public interface ISLSchedule
  {
    [OperationContract]
    List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors);

    [OperationContract]
    Boolean DeleteStudentSchedule(string student_id, string schedule_id, ref List<string> errors);

    [OperationContract]
    Quota GetQuota(string class_id, ref List<string> errors);

    [OperationContract]
    Schedule GetSchedule(int id, ref List<string> errors);

    [OperationContract]
    void UpdateSchedule(Schedule s, ref List<string> errors);
    
    [OperationContract]
    int InsertSchedule(Schedule s, ref List<string> errors);

    [OperationContract]
    void DeleteSchedule(int id, ref List<string> errors);

    [OperationContract]
    List<Schedule> GetDiscussions(int class_id, ref List<string> errors);

    [OperationContract]
    string createDiscussion(int lecture_id, string session, int day, int time, int instructor, int quota, ref List<String> errors);

    [OperationContract]
    Boolean removeDiscussion(string discussion_id, ref List<String> errors);   
  }
}
