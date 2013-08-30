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
    [TestClass]
    public class DALDiscussionTest
    {
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
        public void GetDiscussionsTest()
        {

            List<String> errors = new List<String>();
            List<Schedule> discussions = DAL.DALDiscussion.GetDiscussions(100, ref errors);
            foreach (Schedule o in discussions)
            {
                /// TestContext.WriteLine(o.course.title);
                foreach (var property in typeof(Schedule).GetProperties())
                    Assert.IsNotNull(property.GetValue(o));
            }
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void CRUDDiscussionsTest()
        {
            List<String> errors = new List<String>();
            //need to redo this to pass in a discussion object instead. 
            string classid =DAL.DALDiscussion.createDiscussion(100, "A01", 1,1,1,50, ref errors);
            // Assert.IsTrue(classid!="0");
            // TestContext.WriteLine(classid);
            // TestContext.WriteLine(errors[0].ToString());
            Assert.IsTrue(errors.Count==0);
            List<Schedule> discussions = DAL.DALDiscussion.GetDiscussions(100, ref errors);
            Boolean check = false;
            foreach (Schedule o in discussions)
            {
                if (o.id.ToString() == classid) { check = true; }
            }
            Assert.IsTrue(check);
            // need to get course and verify, but oh well. 
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
