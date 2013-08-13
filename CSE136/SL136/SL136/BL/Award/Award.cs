using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BL.Award
{
  public class Award
  {
    private IRanking rank;

    public Award(IRanking ranking)
    {
      this.rank = ranking;
    }

    public void AwardScholarship(string student_id)
    {
      int score = this.rank.GetRankScore(student_id);

      if (score > 70)
      {
        // put some money in student's account
      }
    }
  }
}