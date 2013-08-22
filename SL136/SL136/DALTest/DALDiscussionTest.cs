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
        public void DiscussionsUnitTest()
        {
            List<String> errors = new List<String>();
            Assert.IsTrue( DAL.DALDiscussion.createDiscussion(100, 118, ref errors));
            Assert.IsTrue(errors.Count==0);
            List<Schedule> discussions = DAL.DALDiscussion.GetDiscussions(100, ref errors);
            Boolean check = false;
            foreach (Schedule o in discussions)
            {
                if (o.id == 118) { check = true; }
            }
            Assert.IsTrue(check);
            // need to get course and verify, but oh well. 
            Assert.IsTrue(DAL.DALDiscussion.removeDiscussion(100, 118, ref errors));
            Assert.IsTrue(errors.Count == 0);
            // same
            discussions = DAL.DALDiscussion.GetDiscussions(100, ref errors);
            foreach (Schedule o in discussions)
            {
                Assert.AreNotEqual(118, o.id);
            }
        }
    }   // class
}       // namespace
