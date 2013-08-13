using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web136.Models.Schedule
{
  public class PLSchedule
  {
    [Required]
    [DisplayName("Schedule ID")]
    public int schedule_id { get; set; }

    [Required]
    [DisplayName("Year")]
    public string year { get; set; }

    [Required]
    [DisplayName("Quarter")]
    public string quarter { get; set; }

    [Required]
    [DisplayName("Session")]
    public string session { get; set; }

    [Required]
    [DisplayName("Course Title")]
    public string course_title { get; set; }

    [Required]
    [DisplayName("Description")]
    public string course_description { get; set; }
  }
}