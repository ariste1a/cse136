using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using DAL;

namespace BL
{
  public static class BLSchedule
  {
    public static List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
    {
      return (DALSchedule.GetScheduleList(year, quarter, ref errors));
    }

    public static Boolean deleteScheduleDay(string day_id)
    {
        return DALSchedule.deleteScheduleDay(day_id);
    }
    
    public static Boolean deleteScheduleTime(string time_id)
    {
        return DALSchedule.deleteScheduleTime(time_id);
    }

    public static Boolean deleteStudentSchedule(string student_id, string schedule_id)
    {
        return DALSchedule.deleteStudentSchedule(student_id, schedule_id);
    }

    public static Quota GetQuota(string class_id, ref List<string> errors)
    {
        return DALSchedule.GetQuota(class_id, ref errors);
    }

    public static Schedule GetSchedule(int schedule_id, ref List<string> errors)
    {
        return DALSchedule.GetSchedule(schedule_id, ref errors);
    }

    public static void UpdateSchedule(Schedule s, ref List<string> errors)
    {
        DALSchedule.UpdateSchedule(s, ref errors);
    }

    public static int InsertSchedule(Schedule schedule, ref List<string> errors)
    {
        return DALSchedule.InsertSchedule(schedule, ref errors); 
    }

    public static void DeleteSchedule(int id, ref List<string> errors)
    {
        DALSchedule.DeleteSchedule(id, ref errors);
    }

  }
}
