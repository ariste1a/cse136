using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BL.Award
{
    public class Resolver
    {
        public IRanking ChooseRanking(string studentType)
        {
            if (studentType == "grad")
            {
                return new RankByInnovation();
            }
            return new RankByGPA();
        }
    }
}