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
    public class DALStaffTest
    {
        public DALStaffTest()
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
        public void InsertStaffTest()
        {
            Staff staff = new Staff();
            staff.email = "test@ucsd.edu";
            staff.password = "test";

            List<string> errors = new List<string>();
            staff.id = DALStaff.InsertStaff(staff, ref errors);

            //Assert.AreEqual(0, errors.Count);

            Staff verifyStaff = DALStaff.GetStaffDetail(staff.email, ref errors);

            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(staff.id, verifyStaff.id);
            Assert.AreEqual(staff.email, verifyStaff.email);
            Assert.AreEqual(staff.password, verifyStaff.password);

            Staff staff2 = new Staff();
            staff2.email = "last2";
            staff2.password = "test";
            /*
            DALAdmin.Updateadmin(admin2, ref errors);

            verifyAdmin = DALAdmin.GetadminDetail(admin2.id, ref errors);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(admin2.first_name, verifyadmin.first_name);
            Assert.AreEqual(admin2.last_name, verifyadmin.last_name);
      
            List<Schedule> scheduleList = DALSchedule.GetScheduleList("", "", ref errors);
            Assert.AreEqual(0, errors.Count);

            // enroll all available scheduled courses for this admin
            for (int i = 0; i < scheduleList.Count; i++)
            {
              DALAdmin.EnrollSchedule(admin.id, scheduleList[i].id, ref errors);
              Assert.AreEqual(0, errors.Count);
            }

            // drop all available scheduled courses for this admin
            for (int i = 0; i < scheduleList.Count; i++)
            {
              DALAdmin.DropEnrolledSchedule(admin.id, scheduleList[i].id, ref errors);
              Assert.AreEqual(0, errors.Count);
            }

            DALAdmin.Deleteadmin(admin.id, ref errors);

            admin verifyEmptyadmin = DALadmin.GetadminDetail(admin.id, ref errors);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(null, verifyEmptyadmin);
            */
        }
    }
}
