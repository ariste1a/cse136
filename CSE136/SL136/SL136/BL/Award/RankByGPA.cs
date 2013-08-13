using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BL.Award
{
  public class RankByGPA : IRanking
  {
    public int GetRankScore(string id)
    {
      // retrieve gpa data from database and calculate a ranking between 1-100
      // hardcode for now.
      return 100;
    }
  }
}