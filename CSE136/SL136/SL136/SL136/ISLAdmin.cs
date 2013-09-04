using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using POCO;
using BL;
namespace SL136
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISLCourse" in both code and config file together.
    [ServiceContract]
    public interface ISLAdmin
    {
        [OperationContract]
        string InsertAdmin(Admin a, ref List<string> err);

        [OperationContract]
        void UpdateAdmin(Admin a, ref List<string> err);

        [OperationContract]
        void DeleteAdmin(string id, ref List<string> err);

        [OperationContract]
        void GetAdmin(string email, ref List<string> err);
    }

}
