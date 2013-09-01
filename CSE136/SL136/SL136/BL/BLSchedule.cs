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
        if (year == null || quarter==null)
        {
            errors.Add("Invalid year or quarter");
        }
      return (DALSchedule.GetScheduleList(year, quarter, ref errors));
    }

    public static Boolean deleteScheduleDay(string day_id)
    {
        if (day_id == null)
        {
            
        }
        return DALSchedule.deleteScheduleDay(day_id);
    }
    
    public static Boolean deleteScheduleTime(string time_id)
    {

        return DALSchedule.deleteScheduleTime(time_id);
    }

    public static Boolean deleteStudentSchedule(string student_id, string schedule_id, ref List<string> errors)
    {
        if (student_id == null || schedule_id == null)
        {
            errors.Add("Invalid Student ID or Schedule ID");
            return false; 
        }

        return DALSchedule.deleteStudentSchedule(student_id, schedule_id, ref errors);
    }

    public static Quota GetQuota(string class_id, ref List<string> errors)
    {
        if (class_id == null)
        {
            errors.Add("Invalid Class ID");
        }
        return DALSchedule.GetQuota(class_id, ref errors);
    }

    public static Schedule GetSchedule(int schedule_id, ref List<string> errors)
    {
        if (schedule_id <=0 )
        {
            errors.Add("Invalid Schedule ID"); 
        } 
        return DALSchedule.GetSchedule(schedule_id, ref errors);
    }

    public static void UpdateSchedule(Schedule s, ref List<string> errors)
    {
        if (s == null)
        {
            errors.Add("Invalid Schedule");
        }
        if (errors.Count > 0)
            return;
        DALSchedule.UpdateSchedule(s, ref errors);
    }

    public static int InsertSchedule(Schedule schedule, ref List<string> errors)
    {
        if (schedule == null)
        {
            errors.Add("Invalid Schedule");
        }
        if (errors.Count > 0)
            return -1; 
        return DALSchedule.InsertSchedule(schedule, ref errors); 
    }

    public static void DeleteSchedule(int id, ref List<string> errors)
    {
        if (id <= 0)
        {
            errors.Add("Invalid Schedule ID");
        }
        if (errors.Count > 0)
            return;
        DALSchedule.DeleteSchedule(id, ref errors);
    }

  }
}
