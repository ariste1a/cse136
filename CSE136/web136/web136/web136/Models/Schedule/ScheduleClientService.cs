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

    public static Boolean addSchedule(SLSchedule.Schedule s)
    {
        try
        {
            SLSchedule.ISLSchedule client = new SLSchedule.SLScheduleClient();
            String[] errors = new string[0];
            SLSchedule.InsertScheduleRequest req = new SLSchedule.InsertScheduleRequest(s, errors);
            SLSchedule.InsertScheduleResponse res = client.InsertSchedule(req);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public static Boolean addDiscussion(int lecture_id, string session, int day, int time, int instructor, int quota)
    {
        try
        {
            SLSchedule.ISLSchedule client = new SLSchedule.SLScheduleClient();
            String[] errors = new string[0];
            SLSchedule.createDiscussionRequest req = new SLSchedule.createDiscussionRequest
                (lecture_id, session, day, time, instructor, quota, errors);
            SLSchedule.createDiscussionResponse res = client.createDiscussion(req);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public static void deleteSchedule(int id)
    {
        SLSchedule.ISLSchedule client = new SLSchedule.SLScheduleClient();
        String[] errors = new string[0];
        SLSchedule.DeleteScheduleRequest request = new SLSchedule.DeleteScheduleRequest(id, errors);
        SLSchedule.DeleteScheduleResponse res = client.DeleteSchedule(request);
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
      mySchedule.enrollment = s.enrollments;
       
      return mySchedule;
    }

    private static PLQuota DTO_to_PL(SLSchedule.Quota s)
    {
        PLQuota PLQuota = new PLQuota();
        PLQuota.course_title = s.course_title;
        PLQuota.max_students = s.max_students;
        PLQuota.schedule_id = s.schedule_id;
        PLQuota.students_enrolled = s.students_enrolled;
        return PLQuota;
    }

    public static PLQuota GetScheduleQuota(string id)
    {
        SLSchedule.ISLSchedule client = new SLSchedule.SLScheduleClient();    
        string[] errors = new string[0];
        SLSchedule.GetQuotaRequest request = new SLSchedule.GetQuotaRequest(id, errors);
        SLSchedule.GetQuotaResponse response = client.GetQuota(request);
        SLSchedule.Quota q= response.GetQuotaResult;
        PLQuota PLQuota = DTO_to_PL(q); 
        return PLQuota; 
    }

    public static List<PLSchedule> GetDiscussionList(int id)
    {
        List<PLSchedule> scheduleList = new List<PLSchedule>();

        SLSchedule.ISLSchedule client = new SLSchedule.SLScheduleClient();

        string[] errors = new string[0];
        SLSchedule.GetDiscussionsRequest request = new SLSchedule.GetDiscussionsRequest(id, errors);
        SLSchedule.GetDiscussionsResponse response = client.GetDiscussions(request);
        SLSchedule.Schedule[] discussionsLoaded = response.GetDiscussionsResult;

        if (discussionsLoaded != null)
        {
            foreach (SLSchedule.Schedule s in discussionsLoaded)
            {
                PLSchedule schedule = DTO_to_PL(s);
                scheduleList.Add(schedule);
            }
        }
        return scheduleList;
    }
    

   

  }
}