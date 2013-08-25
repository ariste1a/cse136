using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using POCO;
using System.Diagnostics;
namespace DALTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class DALCombineTests
    {
        public DALCombineTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        ///A test for Insertadmin
        ///</summary>
        [TestMethod]
        public void DALCombineTest()
        {
            //CREATE THE CLASS INSTANCE/COURSE SCHEDULE
            Schedule schedule = new Schedule();
            List<string> errors = new List<string>();
            Course course = DALCourse.GetCourse("1", ref errors);
            schedule.year = "2014";
            schedule.quarter = "Spring";
            schedule.session = "A00";
            schedule.course = course;
            schedule.quota = "45";
            schedule.time = "1";
            schedule.instructor = "1";
            schedule.day = "1";
            schedule.type = "LE";

            int id = DALSchedule.InsertSchedule(schedule, ref errors);
            schedule.id = id;
            Schedule verifySchedule = DALSchedule.GetSchedule(schedule.id, ref errors);

            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(schedule.id, verifySchedule.id);
            Assert.AreEqual(schedule.quarter, verifySchedule.quarter);
            Assert.AreEqual(schedule.session, verifySchedule.session);
            Assert.AreEqual(schedule.course.id, verifySchedule.course.id);

            
            string classid = DAL.DALDiscussion.createDiscussion(schedule.id, "A00", 1, 1, 1, 50, ref errors);
            Assert.IsTrue(errors.Count == 0);
            List<Schedule> discussions = DAL.DALDiscussion.GetDiscussions(schedule.id, ref errors);
            Boolean check = false;
            foreach (Schedule o in discussions)
            {
                if (o.id.ToString() == classid) { check = true; }
            }
            Assert.IsTrue(check);

            //enroll the student. 
            DALStudent.EnrollSchedule("A000001", schedule.id, ref errors);
            DALStudent.EnrollSchedule("A000001", Convert.ToInt32(classid), ref errors);
            List<string> scheduleList = DALStudent.GetStudentSchedule("A000001", ref errors);
            Assert.AreEqual(scheduleList.Count, 2);
            Assert.AreEqual(scheduleList[0], schedule.id.ToString());
            Assert.AreEqual(scheduleList[1], classid);

            DALSchedule.DeleteSchedule(schedule.id, ref errors);
            Schedule verifyEmptyCourse = DALSchedule.GetSchedule(schedule.id, ref errors);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(null, verifyEmptyCourse);

            Assert.IsTrue(DAL.DALDiscussion.removeDiscussion(classid, ref errors));
            Assert.IsTrue(errors.Count == 0);
            // same
            discussions = DAL.DALDiscussion.GetDiscussions(100, ref errors);
            foreach (Schedule o in discussions)
            {
                Assert.AreNotEqual(classid, o.id.ToString());
            }                
        }
    }
}
