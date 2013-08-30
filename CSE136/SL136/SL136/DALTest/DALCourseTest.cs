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
    public class DALCourseTest
    {
        public DALCourseTest()
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
        public void CRUDCourseTest()
        {
            Course course = new Course();
            course.title = "CSE 8999";
            course.level = CourseLevel.upper;
            course.description = "Not over 9000";
            List<string> errors = new List<string>();

            string id = DALCourse.InsertCourse(course, ref errors);
            course.id = id;             
            Course verifyCourse = DALCourse.GetCourse(course.id, ref errors); 

            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(course.id, verifyCourse.id);
            Assert.AreEqual(course.title, verifyCourse.title);
            Assert.AreEqual(course.description, verifyCourse.description);
            Assert.AreEqual(course.level, verifyCourse.level);

            course.title = "CSE 1000";
            course.level = CourseLevel.grad;
            course.description = "Now Over 9000!";

            DALCourse.UpdateCourse(course, ref errors);
            verifyCourse = DALCourse.GetCourse(course.id, ref errors); 

            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(course.id, verifyCourse.id);
            Assert.AreEqual(course.id, verifyCourse.id);
            Assert.AreEqual(course.description, verifyCourse.description);
            Assert.AreEqual(course.level, verifyCourse.level);

            DALCourse.DeleteCourse(course.id, ref errors);
            Course verifyEmptyCourse = DALCourse.GetCourse(course.id, ref errors);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(null, verifyEmptyCourse);
        }
    }
}
