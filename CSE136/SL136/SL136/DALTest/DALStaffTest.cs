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
        public void CRUDStaffTest()
        {
            Staff staff = new Staff();
            staff.email = "lolmamajamas@email.com";
            staff.password = "simpsons";
            List<string> errors = new List<string>();

            string id = DALStaff.InsertStaff(staff, ref errors);
            staff.id = id;
            Staff verifyStaff = DALStaff.GetStaffDetail(staff.email, ref errors);

            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(staff.id, verifyStaff.id);
            Assert.AreEqual(staff.email, verifyStaff.email);
            Assert.AreEqual(staff.password, verifyStaff.password);

            staff.email = "herpderp";
            staff.password = "DOYOULIKEWAFFLES";

            DALStaff.DeleteStaff(staff.id, ref errors);
            Staff verifyEmptyStaff = DALStaff.GetStaffDetail(staff.id, ref errors);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(null, verifyEmptyStaff);

            //DALStaff.UpdateStaff(staff, ref errors);
            //verifyStaff = DALStaff.GetStaffDetail(staff.email, ref errors);

            //Assert.AreEqual(0, errors.Count);
            //Assert.AreEqual(staff.id, verifyStaff.id);
            //Assert.AreEqual(staff.email, verifyStaff.email);
            //Assert.AreEqual(staff.password, verifyStaff.password);
        }
    }
}
