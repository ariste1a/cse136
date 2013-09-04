using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;
using DAL;


namespace BL
{
    public class BLStaff
    {
        public static string InsertStaff(Staff s, ref List<string> err)
        {
            string ret_id = DALStaff.InsertStaff(s, ref err);
            return ret_id != null ? ret_id : "-1"; 
        }

        public static void UpdateStaff(Staff s, ref List<string> err)
        {
            if (s == null)
            {
                err.Add("Invalid Staff");
            }
            if (err.Count > 0)
                return;
            DALStaff.UpdateStaff(s, ref err);
        }

        public static void DeleteStaff(string id, ref List<string> err)
        {
            if (id == null)
            {
                err.Add("Invalid Staff ID");
            }
            if (err.Count > 0)
                return;

            DALStaff.DeleteStaff(id, ref err);
        }

        public static Staff GetStaffDetails(string email, ref List<string> err)
        {
            if (email == null)
            {
                err.Add("Invalid Staff ID");
            }
            if (err.Count > 0)
                return null;
            return DALStaff.GetStaffDetail(email, ref err);
        }
      
    }
}
