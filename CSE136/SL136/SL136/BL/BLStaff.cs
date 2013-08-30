using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO;
using DAL;


namespace BL
{
    class BLStaff
    {
        public static string InsertStaff(Staff s, ref List<string> err)
        {
            string ret_id = DALStaff.InsertStaff(s, ref err);
            return ret_id != null ? ret_id : "-1"; 
        }

        public static void UpdateStaff(Staff s, ref List<string> err)
        {
            DALStaff.UpdateStaff(s, ref err);
        }

        public static void DeleteStaff(string id, ref List<string> err)
        {
            DALStaff.DeleteStaff(id, ref err);
        }

        public static Staff GetStaffDetails(string email, ref List<string> err)
        {
            return DALStaff.GetStaffDetail(email, ref err);
        }
    }
}
