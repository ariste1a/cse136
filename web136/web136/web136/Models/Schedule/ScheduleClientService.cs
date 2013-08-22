using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web136.Models.Schedule
{
  public static class ScheduleClientService
  {
    public static List<PLSchedule> GetScheduleList(string year, string quarter)
    {
      List<PLSchedule> scheduleList = new List<PLSchedule>();

      SLSchedule.ISLSchedule client = new SLSchedule.SLScheduleClient();

      string[] errors = new string[0];
      SLSchedule.GetScheduleListRequest request = new SLSchedule.GetScheduleListRequest(year, quarter, errors);
      SLSchedule.GetScheduleListResponse response = client.GetScheduleList(request);
      SLSchedule.Schedule[] schedulesLoaded = response.GetScheduleListResult;

      if (schedulesLoaded != null)
      {
        foreach (SLSchedule.Schedule s in schedulesLoaded)
        {
          PLSchedule schedule = DTO_to_PL(s);
          scheduleList.Add(schedule);
        }
      }
      return scheduleList;
    }

    private static PLSchedule DTO_to_PL(SLSchedule.Schedule s)
    {
      PLSchedule mySchedule = new PLSchedule();

      mySchedule.schedule_id = s.id;
      mySchedule.year = s.year;
      mySchedule.quarter = s.quarter;
      mySchedule.session = s.session;
      mySchedule.course_title = s.course.title;
      mySchedule.course_description = s.course.description;
      mySchedule.quota = s.quota;
      mySchedule.day = s.day;
      mySchedule.time = s.time;
      mySchedule.type = s.type;
       
      return mySchedule;
    }
  }
}