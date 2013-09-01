using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;
using DAL; 

namespace BL
{
    public class BLAdmin
    {
        public static string InsertAdmin(Admin a, ref List<string> errors)
        {
            string ret_val = DALAdmin.InsertAdmin(a, ref errors);
            return ret_val != null ? ret_val : "-1";
        }

        public static void UpdateAdmin(Admin a, ref List<string> errors)
        {
            Admin b = DALAdmin.GetAdminDetail(a.email, ref errors);
            if (b == null)
            {
                errors.Add("Invalid email"); 
            }
            if (a == null)
            {
                errors.Add("Invalid admin object"); 
            }
            if (errors.Count > 0)
                return;
            DALAdmin.UpdateAdmin(a, ref errors);
        }

        public static void DeleteAdmin(string id, ref List<string> errors)
        {
            if (id == null)
            {
                errors.Add("Invalid Admin ID"); 
            }
            if (errors.Count > 0)
                return;
            DALAdmin.DeleteAdmin(id, ref errors);
        }

        public static Admin GetAdmin(string email, ref List<string> errors)
        {
            if (email == null)
            {
                errors.Add("invalid admin email"); 
            }
            if (errors.Count > 0)
                return null;

            return DALAdmin.GetAdminDetail(email, ref errors);
        }

    }
}
