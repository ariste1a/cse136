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
  // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SLSchedule" in both code and config file together.
  public class SLSchedule : ISLSchedule
  {
    public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
    {
      return BLSchedule.GetScheduleList(year, quarter, ref errors);
    }

    public Boolean DeleteStudentSchedule(string student_id, string schedule_id, ref List<string> errors)
    {
        return BLSchedule.deleteStudentSchedule(student_id, schedule_id, ref errors);
    }

    public Quota GetQuota(string class_id, ref List<string> errors)
    {
        return BLSchedule.GetQuota(class_id, ref errors);
    }

    public Schedule GetSchedule(int id, ref List<string> errors)
    {
        return BLSchedule.GetSchedule(id, ref errors);
    }

    public void UpdateSchedule(Schedule s, ref List<string> errors)
    {
        BLSchedule.UpdateSchedule(s, ref errors);
    }

    public int InsertSchedule(Schedule s, ref List<string> errors)
    {
        return BLSchedule.InsertSchedule(s, ref errors);
    }

    public void DeleteSchedule(int id, ref List<string> errors)
    {
       BLSchedule.DeleteSchedule(id, ref errors);
    }

    public List<Schedule> GetDiscussions(int class_id, ref List<string> errors)
    {
        return BLDiscussion.GetDiscussions(class_id, ref errors);
    }
    public string createDiscussion(int lecture_id, string session, int day, int time, int instructor, int quota, ref List<String> errors)
    {
        return BLDiscussion.createDiscussion(lecture_id, session, day, time, instructor, quota, ref errors);
    }
    public Boolean removeDiscussion(string discussion_id, ref List<String> errors)
    {
        return BLDiscussion.removeDiscussion(discussion_id, ref errors);
    }
  }
}
