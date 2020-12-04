using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assign3_Sasha_Srinivas_n01458354.Models;
using System.Diagnostics;

namespace Assign3_Sasha_Srinivas_n01458354.Controllers
{
    public class TeacherController : Controller
    {
        
        /// <summary>
        /// View a list of all teachers
        /// </summary>
        /// <example>GET Teacher/List</example>
        /// <returns>
        /// Returns a list of all Teachers (first name and last name)
        /// </returns>
        
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        /// <summary>
        /// View teacher information based on their Id
        /// </summary>
        /// <example>GET Teacher/Show/{id}</example>
        /// <returns>
        /// returns a student object based on TeacherId
        /// </returns>

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        /// <summary>
        /// Confirm deletion of a teacher based on their ID
        /// </summary>
        /// <example>GET Teacher/DeleteConfirm/{id}</example>
        /// <returns>
        /// returns a page confirming the deletion of Teacher
        /// </returns>

        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
        /// <summary>
        /// Delete teacher based on their ID
        /// </summary>
        /// <example>POST Teacher/Delete/{id}</example>
        /// <returns>
        /// deletes teacher based on their ID
        /// </returns>
       
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        /// <summary>
        /// displays form to add a new teacher to the database
        /// </summary>
        /// <example>GET /Teacher/New</example>
        /// <returns>
        /// returns a form to add new teacher information
        /// </returns>
        
        public ActionResult New()
        {
            return View();
        }
        /// <summary>
        /// sends new teacher information to the database
        /// </summary>
        /// <example>POST : /Teacher/Create</example>
        /// <returns>
        /// adds information to the database
        /// </returns>

        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber)
        {
            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);

            Teacher NewTeacher = new Teacher();

            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
    }
}