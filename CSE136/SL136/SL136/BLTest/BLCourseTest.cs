using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POCO;
using BL;

namespace BLTest
{
    class BLCourseTest
    {
        public BLCourseTest()
        {
            // y d we even got these l0l
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

        [TestMethod]
        public void BLInsertCourseErrorTest()
        {
            List<string> errors = new List<string>();

            BLCourse.InsertCourse(null, ref errors);
            Assert.AreEqual(1, errors.Count);

            errors = new List<string>();

            Student student = new Student();
            student.id = "";
            BLStudent.InsertStudent(student, ref errors);
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void BLCourseCRUDTest()
        {
            Course c = new Course();
            c.id = Guid.NewGuid().ToString().Substring(0, 20);
            c.title = "Introduction to Facepalm";
            c.level = CourseLevel.lower;
            c.description = "50 derps to every herp!";

            List<string> errors = new List<string>();

            BLCourse.InsertCourse(c, ref errors);

            Assert.AreEqual(0, errors.Count);

            Course fetchedCourse = BLCourse.GetCourse(c.id, ref errors);

            Assert.AreEqual(0, errors.Count);

            Assert.AreEqual(c.id, fetchedCourse.id);
            Assert.AreEqual(c.title, fetchedCourse.title);
            Assert.AreEqual(c.level, fetchedCourse.level);
            Assert.AreEqual(c.description, fetchedCourse.description);

            c.title = "banana";

            BLCourse.UpdateCourse(c, ref errors);

            Assert.AreEqual(0, errors.Count);

            fetchedCourse = BLCourse.GetCourse(c.id, ref errors);

            Assert.AreEqual(c.id, fetchedCourse.id);
            Assert.AreEqual(c.title, fetchedCourse.title);
            Assert.AreEqual(c.level, fetchedCourse.level);
            Assert.AreEqual(c.description, fetchedCourse.description);

            BLCourse.DeleteCourse(c.id, ref errors);
            Assert.AreEqual(0, errors.Count);

            Course nullCourse = BLCourse.GetCourse(c.id, ref errors);
            Assert.AreEqual(0, errors.Count);

            Assert.AreEqual(null, nullCourse);
        }

    }
}
