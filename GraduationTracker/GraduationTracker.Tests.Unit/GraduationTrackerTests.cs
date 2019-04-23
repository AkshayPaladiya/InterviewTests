using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        /// Using strongly typed to do more stable code at compile time.

        private Diploma _diploma;
        private Student[] _students;
        private GraduationTracker _graduationTracker;

        public GraduationTrackerTests()
        { 
            ReadData();
            _graduationTracker = new GraduationTracker();
        }

        /// <summary>
        /// Get datas from repository.
        /// </summary>
        private void ReadData()
        {
            _diploma = Repository.GetDiploma(1);
            _students = Repository.GetStudents();

        }

        private List<Tuple<bool, STANDING>> GraduateList()
        {
            var graduated = new List<Tuple<bool, STANDING>>();

            foreach (var student in _students)
            {
                graduated.Add(_graduationTracker.HasGraduated(_diploma, student));
            }

            return graduated;
        }

        [TestMethod]
        public void TestHasCredits()
        {
            var graduated = GraduateList();

            /// True if any data get without knowing graduate or not.
            Assert.IsTrue(graduated.Any());
        }

        /// <summary>
        /// Has Any Student Graduate
        /// </summary>
        [TestMethod]
        public void TestHasAnyStudentGraduate()
        {
            var graduated = GraduateList();

            /// True if graduate data found in response with true value
            Assert.IsTrue(graduated.Any(response => response.Item1 == true));
        }

        /// <summary>
        /// Has Any Student Under Graduate
        /// </summary>
        [TestMethod]
        public void TestHasAnyStudentUnderGraduate()
        {
            var graduated = GraduateList();

            /// True if graduate data found in response with false value
            Assert.IsTrue(graduated.Any(response => response.Item1 == false));
        }
    }
}
