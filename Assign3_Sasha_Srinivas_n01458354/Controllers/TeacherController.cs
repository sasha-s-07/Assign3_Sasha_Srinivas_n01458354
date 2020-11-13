using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assign3_Sasha_Srinivas_n01458354.Models;

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
        
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        /// <summary>
        /// View teacher information based on their Id
        /// </summary>
        /// <example>GET Teacher/Show/{id}</example>
        /// <returns>
        /// TeacherFName, TeacherLname, EmployeeNumber based on TeacherId
        /// </returns>

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }


    }
}