using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POCO;
using BL;

namespace BLTest
{
    class BLEnrollmentTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void BLEnrollmentErrorTest()
        {
            List<string> er = new List<string>();

            BLEnrollment.GetEnrollment(null, null, ref er);
            Assert.AreEqual(1, er.Count);
        }

        [TestMethod]
        public void BLEnrollmentCRUDTest()
        {
            Enrollment e = new Enrollment();
            e.student_id = Guid.NewGuid().ToString().Substring(0, 20);
            e.schedule_id = "25";
            e.grade = "F";

            List<string> errors = new List<string>();

            BLEnrollment.InsertEnrollment(e, ref errors);

            Assert.AreEqual(0, errors.Count);

            Enrollment fetchedEnrollment = BLEnrollment.GetEnrollment(e.student_id, e.schedule_id, ref errors);

            Assert.AreEqual(0, errors.Count);

            Assert.AreEqual(e.student_id, fetchedEnrollment.student_id);
            Assert.AreEqual(e.schedule_id, fetchedEnrollment.schedule_id);
            Assert.AreEqual(e.grade, fetchedEnrollment.grade);

            e.grade = "A";

            BLEnrollment.UpdateEnrollment(e, ref errors);

            Assert.AreEqual(0, errors.Count);

            fetchedEnrollment = BLEnrollment.GetEnrollment(e.student_id, e.schedule_id, ref errors);

            Assert.AreEqual(e.student_id, fetchedEnrollment.student_id);
            Assert.AreEqual(e.schedule_id, fetchedEnrollment.schedule_id);
            Assert.AreEqual(e.grade, fetchedEnrollment.grade); ;

            BLEnrollment.DeleteEnrollmentSchedule(e.student_id, e.schedule_id , ref errors);
            Assert.AreEqual(0, errors.Count);

            Enrollment nullEnrollment = BLEnrollment.GetEnrollment(e.student_id, e.schedule_id, ref errors);
            Assert.AreEqual(0, errors.Count);

            Assert.AreEqual(null, nullEnrollment);
        }
    }
}
