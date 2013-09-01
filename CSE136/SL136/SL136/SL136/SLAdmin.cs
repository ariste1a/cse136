using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BL;
using POCO;

namespace SL136
{
    class SLAdmin
    {
        public static string InsertAdmin(Admin a, ref List<string> err)
        {
            return BLAdmin.InsertAdmin(a, ref err);
        }

        public static void UpdateAdmin(Admin a, ref List<string> err)
        {
            BLAdmin.UpdateAdmin(a, ref err);
        }

        public static void DeleteAdmin(string id, ref List<string> err)
        {
            BLAdmin.DeleteAdmin(id, ref err);
        }

        public static void GetAdmin(string email, ref List<string> err)
        {
            BLAdmin.GetAdmin(email, ref err);
        }
    }
}
