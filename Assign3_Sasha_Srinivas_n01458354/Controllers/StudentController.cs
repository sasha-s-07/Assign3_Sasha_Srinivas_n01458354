using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assign3_Sasha_Srinivas_n01458354.Models;

namespace Assign3_Sasha_Srinivas_n01458354.Controllers
{
    public class StudentController : Controller
    {
        
        /// <summary>
        /// View a list of all students
        /// </summary>
        /// <example>GET Student/List</example>
        /// <returns>
        /// Returns a list of all students (first name and last name)
        /// </returns>
        // GET: /Student/List
        public ActionResult List()
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents();
            return View(Students);
        }
        /// <summary>
        /// View student information based on their Id
        /// </summary>
        /// <example>GET Student/Show/{id}</example>
        /// <returns>
        /// StudentFName, StudentLname, StudentNumber based on StudentId
        /// </returns>
        //GET : /Student/Show/{id}
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }


    }
}