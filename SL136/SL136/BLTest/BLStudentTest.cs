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
  public class BLStudentTest
  {
    public BLStudentTest()
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
    public void StudentErrorTest()
    {
      List<string> errors = new List<string>();

      BLStudent.GetStudent(null, ref errors);
      Assert.AreEqual(1, errors.Count);
    }

    [TestMethod]
    public void DeleteStudentErrorTest()
    {
      List<string> errors = new List<string>();

      BLStudent.DeleteStudent(null, ref errors);
      Assert.AreEqual(1, errors.Count);
    }

    [TestMethod]
    public void StudentInsertAndSelectTest()
    {
      Student student = new Student();
      student.first_name = "first";
      student.last_name = " last";
      student.id = Guid.NewGuid().ToString().Substring(0, 20);
      student.ssn = "888991234";
      student.email = "myemail@ucsd.edu";
      student.password = "pass1234";
      student.shoe_size = 0;
      student.weight = 0;

      List<string> errors = new List<string>();
      BLStudent.InsertStudent(student, ref errors);

      Assert.AreEqual(0, errors.Count);

      Student verifyStudent = BLStudent.GetStudent(student.id, ref errors);

      Assert.AreEqual(0, errors.Count);
      Assert.AreEqual(student.first_name, verifyStudent.first_name);
      Assert.AreEqual(student.last_name, verifyStudent.last_name);
      Assert.AreEqual(student.id, verifyStudent.id);
      Assert.AreEqual(student.ssn, verifyStudent.ssn);
      Assert.AreEqual(student.email, verifyStudent.email);
      Assert.AreEqual(student.shoe_size, verifyStudent.shoe_size);
      Assert.AreEqual(student.weight, verifyStudent.weight);

      List<Schedule> scheduleList = BLSchedule.GetScheduleList("", "", ref errors);
      Assert.AreEqual(0, errors.Count);

      // enroll all available scheduled courses for this student
      for (int i = 0; i < scheduleList.Count; i++)
      {
        BLStudent.EnrollSchedule(student.id, scheduleList[i].id, ref errors);
        Assert.AreEqual(0, errors.Count);
      }

      // drop all available scheduled courses for this student
      for (int i = 0; i < scheduleList.Count; i++)
      {
        BLStudent.DropEnrolledSchedule(student.id, scheduleList[i].id, ref errors);
        Assert.AreEqual(0, errors.Count);
      }

      BLStudent.DeleteStudent(student.id, ref errors);

      Student verifyEmptyStudent = BLStudent.GetStudent(student.id, ref errors);
      Assert.AreEqual(0, errors.Count);
      Assert.AreEqual(null, verifyEmptyStudent);

    }

  }
}
