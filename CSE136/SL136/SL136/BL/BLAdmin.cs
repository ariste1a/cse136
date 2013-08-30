using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;
using DAL; 

namespace BL
{
    class BLAdmin
    {
        public static string InsertAdmin(Admin a, ref List<string> errors)
        {
            string ret_val = DALAdmin.InsertAdmin(a, errors);
            return ret_val != null ? ret_val : "-1";
        }

        public static void UpdateAdmin(Adming a, ref List<string> errors)
        {
            DALAdmin.UpdateAdmin(a, ref errors);
        }

        public static void DeleteAdmin(string id, ref List<string> errrors)
        {
            DALAdmin.DeleteAdmin(id, ref errors);
        }

        public static Admin GetAdmin(string email, ref List<string> errors)
        {
            return DALAdmin.GetAdmin(email, ref errors);
        }

    }
}
