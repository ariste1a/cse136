using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POCO;
using BL;

namespace BLTest
{
    [TestClass]
    class BLAdminTest
    {
        public BLAdminTest()
        {
            // constructa rogic
        }

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
        public void AdminErrorTest()
        {
            List<string> er = new List<string>();

            BLAdmin.GetAdmin(null, ref er);
            Assert.AreEqual(1, er.Count);
        }

        [TestMethod]
        public void AdminCRUDTest()
        {
            Admin a = new Admin();
            a.id = Guid.NewGuid().ToString().Substring(0, 20);
            a.email = "pooplord@spiderman.com";
            a.password = "password";

            List<string> errors = new List<string>(); 

            BLAdmin.InsertAdmin(a, ref errors);

            Assert.AreEqual(0, errors.Count);

            Admin fetchedAdmin = BLAdmin.GetAdmin(a.email, ref errors);

            Assert.AreEqual(0, errors.Count);

            Assert.AreEqual(a.id, fetchedAdmin.id); 
            Assert.AreEqual(a.email, fetchedAdmin.email);
            Assert.AreEqual(a.password, fetchedAdmin.password);

            a.password = "banana";

            BLAdmin.UpdateAdmin(a, ref errors);

            Assert.AreEqual(0, errors.Count); 

            fetchedAdmin = BLAdmin.GetAdmin(a.email, ref errors);

            Assert.AreEqual(a.id, fetchedAdmin.id); 
            Assert.AreEqual(a.email, fetchedAdmin.email);
            Assert.AreEqual(a.password, fetchedAdmin.password);

            BLAdmin.DeleteAdmin(a.id, ref errors); 
            Assert.AreEqual(0, errors.Count); 

            Admin nullAdmin = BLAdmin.GetAdmin(a.email, ref errors);
            Assert.AreEqual(0, errors.Count); 

            Assert.AreEqual(null, nullAdmin); 
    }
}
