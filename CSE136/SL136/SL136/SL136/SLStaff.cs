﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using POCO;
using BL;

namespace SL136
{
    class SLStaff
    {
        public static string InsertStaff(Staff s, ref List<string> err)
        {
            return BLStaff.InsertStaff(s, ref err);
        }

        public static string UpdateStaff(Staff s, ref List<string> err)
        {
            return BLStaff.UpdateStaff(s, ref err);
        }

        public static string DeleteStaff(string id, ref List<string> err)
        {
            return BLStaff.DeleteStaff(id, ref err);
        }

        public static Staff GetStaffDetails(string email, ref List<string> err)
        {
            return BLStaff.GetStaffDetails(email, ref err)
        }
    }
}
