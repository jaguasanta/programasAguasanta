using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.Web.Models;

namespace SchoolProSite.Web.Controllers
{
    public class StudentGradeController : Controller
    {

        private readonly IDaoStudentGrade daoStudentGrade;
        private readonly IDaoCourse daoCourse;
        private readonly IDaoStudent daoStudent;

        public StudentGradeController(IDaoStudentGrade daoStudentGrade, IDaoCourse daoCourse, 
                                      IDaoStudent daoStudent)
        {
            this.daoStudentGrade = daoStudentGrade;
            this.daoCourse = daoCourse;
            this.daoStudent = daoStudent;
        }

        // GET: StudentGradeController
        public ActionResult Index()
        {
            var studentGrades = this.daoStudentGrade.GetStudentGrades().Select(cd => new Models.StudentGradeModel(cd));

            return View(studentGrades);
        }

        // GET: StudentGradeController/Details/5
        public ActionResult Details(int id)
        {
            var studentGrade = this.daoStudentGrade.GetStudentGrade(id);

            StudentGradeModel studentGradeModel = new StudentGradeModel(studentGrade);

            return View(studentGradeModel);
        }

        // GET: StudentGradeController/Create
        public ActionResult Create()
        {
            var courseList = this.daoCourse.GetCourses()
                                                   .Select(cd => new CourseList()
                                                   {
                                                       CourseID = cd.CourseID,
                                                       Title = cd.Title

                                                   })
                                                   .ToList();

            ViewData["Course"] = new SelectList(courseList, "CourseID", "Title");

            var studentList = this.daoStudent.GetStudents()
                                                   .Select(cd => new StudentList()
                                                   {
                                                       Id = cd.Id,
                                                       FirstName = cd.FirstName,
                                                       LastName = cd.LastName


                                                   })
                                                   .ToList();

            ViewData["Student"] = new SelectList(studentList, "Id", "LastName");

            return View();
        }
            
        // POST: StudentGradeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentGradeModel studentGradeModel)
        {
            try
            {
                this.daoStudentGrade.SaveStudentGrade(new DAL.Entities.StudentGrade()
                {
                    CourseID = studentGradeModel.CourseID,
                    StudentID = studentGradeModel.StudentID,
                    Grade = studentGradeModel.Grade
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentGradeController/Edit/5
        public ActionResult Edit(int id)
        {
            var studentGrade = this.daoStudentGrade.GetStudentGrade(id);

            StudentGradeModel studentGradeModel = new StudentGradeModel(studentGrade);

            var courseList = this.daoCourse.GetCourses()
                                                   .Select(cd => new CourseList()
                                                   {
                                                       CourseID = cd.CourseID,
                                                       Title = cd.Title

                                                   })
                                                   .ToList();

            ViewData["Course"] = new SelectList(courseList, "CourseID", "Title");

            var studentList = this.daoStudent.GetStudents()
                                                   .Select(cd => new StudentList()
                                                   {
                                                       Id = cd.Id,
                                                       FirstName = cd.FirstName,
                                                       LastName = cd.LastName
                                                      

                                                   })
                                                   .ToList();

            ViewData["Student"] = new SelectList(studentList, "Id", "LastName");

            return View(studentGradeModel);
        }

        // POST: StudentGradeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentGradeModel studentGradeModel)
        {
            try
            {
                this.daoStudentGrade.UpdateStudentGrade(new DAL.Entities.StudentGrade()
                {

                    EnrollmentID = studentGradeModel.EnrollmentID,  
                    CourseID = studentGradeModel.CourseID,
                    StudentID = studentGradeModel.StudentID,
                    Grade = studentGradeModel.Grade
                    

                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentGradeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentGradeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
