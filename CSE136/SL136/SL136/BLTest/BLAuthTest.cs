using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POCO;
using BL;

namespace BLTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class BLAuthTest
    {
        public BLAuthTest()
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

        [TestMethod]
        public void InsertStudentErrorTest()
        {
            List<string> errors = new List<string>();

            BLStudent.InsertStudent(null, ref errors);
            Assert.AreEqual(1, errors.Count);

            errors = new List<string>();

            Student student = new Student();
            student.id = "";
            BLStudent.InsertStudent(student, ref errors);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void EmailErrorTest()
        {
            
        }

        [TestMethod]
        public void PasswordErrorTest()
        {
            
        }

        [TestMethod]
        public void SucessTest()
        {
           

        }

    }
}
