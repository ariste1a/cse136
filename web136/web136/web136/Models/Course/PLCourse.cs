using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web136.Models.Course
{
  public class PLCourse
  {
      [Required]
      [DisplayName("Session")]
      public string id { get; set; }

      [Required]
      [DisplayName("Course Title")]
      public string title { get; set; }

      [Required]
      [DisplayName("Description")]
      public string description { get; set; }
  }
}