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
  public class DALAdminTest
  {
    public DALAdminTest()
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
    public void CRUDAdminTest()
    {
      Admin admin = new Admin();
      admin.email = "test@ucsd.edu";
      admin.password = "test"; 

      List<string> errors = new List<string>();

      string id = DALAdmin.InsertAdmin(admin, ref errors);
      admin.id = id; 
      //Assert.AreEqual(0, errors.Count);
      //System.Diagnostics.Debug.Write("asdf"); 
      Admin verifyAdmin = DALAdmin.GetAdminDetail(admin.email, ref errors);

      Assert.AreEqual(0, errors.Count);
      Assert.AreEqual(admin.id, verifyAdmin.id);
      Assert.AreEqual(admin.email, verifyAdmin.email);
      Assert.AreEqual(admin.password, verifyAdmin.password);

      Admin admin2 = new Admin();       
      admin2.email = "last2";
      admin2.password = "test";

      DALAdmin.DeleteAdmin(admin.id, ref errors);
      Admin verifyEmptyCourse = DALAdmin.GetAdminDetail(admin.email, ref errors);
      Assert.AreEqual(0, errors.Count);
      Assert.AreEqual(null, verifyEmptyCourse);
    }
  }
}
