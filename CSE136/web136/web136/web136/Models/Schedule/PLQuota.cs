using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web136.Models.Schedule
{
  public class PLQuota
  {
    [Required]
    [DisplayName("Schedule ID")]
    public string schedule_id { get; set; }

    [Required]
    [DisplayName("Course Title")]
    public string course_title { get; set; }

    [Required]
    [DisplayName("Students Enrolled")]
    public int students_enrolled { get; set; }

    [Required]    
    [DisplayName("Max Students")]    
    public int max_students { get; set; }
  }
}