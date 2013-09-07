﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL;
using POCO;
using System.Diagnostics;
namespace BLTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class BLScheduleTest
    {
        public BLScheduleTest()
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
        public void BLScheduleCRUDTest()
        {
            Schedule schedule = new Schedule();
            List<string> errors = new List<string>();
            Course course = BLCourse.GetCourse("1", ref errors);
            schedule.year = "2014";
            schedule.quarter = "Spring";
            schedule.session = "A00";
            schedule.course = course;
            schedule.quota = 45;
            schedule.time = "1";
            schedule.instructor = "1";
            schedule.day = "1";
            schedule.type = "LE";

            int id = BLSchedule.InsertSchedule(schedule, ref errors);
            schedule.id = id;
            Schedule verifySchedule = BLSchedule.GetSchedule(schedule.id, ref errors);

            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(schedule.id, verifySchedule.id);
            Assert.AreEqual(schedule.quarter, verifySchedule.quarter);
            Assert.AreEqual(schedule.session, verifySchedule.session);
            Assert.AreEqual(schedule.course.id, verifySchedule.course.id);
            Assert.AreEqual(schedule.course.title, verifySchedule.course.title);
            Assert.AreEqual(schedule.course.title, verifySchedule.course.title);
            Assert.AreEqual(schedule.course.description, verifySchedule.course.description);
            Assert.AreEqual("8am-9am", verifySchedule.time);
            Assert.AreEqual(schedule.year, verifySchedule.year);
            Assert.AreEqual(schedule.instructor, verifySchedule.instructor);
            Assert.AreEqual("Mon/Wed", verifySchedule.day);
            Assert.AreEqual(schedule.type, verifySchedule.type);

            schedule.time = "2";
            schedule.year = "5";
            schedule.session = "B01";
            schedule.quota = 100;

            BLSchedule.UpdateSchedule(schedule, ref errors);

            verifySchedule = BLSchedule.GetSchedule(schedule.id, ref errors);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(schedule.id, verifySchedule.id);
            Assert.AreEqual(schedule.quarter, verifySchedule.quarter);
            Assert.AreEqual(schedule.session, verifySchedule.session);
            Assert.AreEqual(schedule.course.id, verifySchedule.course.id);
            Assert.AreEqual(schedule.course.title, verifySchedule.course.title);
            Assert.AreEqual(schedule.course.title, verifySchedule.course.title);
            Assert.AreEqual(schedule.course.description, verifySchedule.course.description);
            Assert.AreEqual("8am-9:30am", verifySchedule.time);
            Assert.AreEqual(schedule.year, verifySchedule.year);
            Assert.AreEqual(schedule.instructor, verifySchedule.instructor);
            Assert.AreEqual("Mon/Wed", verifySchedule.day);
            Assert.AreEqual(schedule.type, verifySchedule.type);

            BLSchedule.DeleteSchedule(schedule.id, ref errors);
            Schedule verifyEmptyCourse = BLSchedule.GetSchedule(schedule.id, ref errors);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(null, verifyEmptyCourse);

            //NEED TO PUT IN CHECKS FOR THIS
        }
    }
}