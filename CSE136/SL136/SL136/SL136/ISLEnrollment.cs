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
    [ServiceContract]
    public interface ISLEnrollment
    {
        [OperationContract]
        string InsertEnrollment(Enrollment e, ref List<string> err);
       
        [OperationContract]
         void UpdateEnrollment(Enrollment e, ref List<string> err);

        [OperationContract]
         void DeleteEnrollment(string student_id, string schedule_id, ref List<string> err);
     
    }
}
