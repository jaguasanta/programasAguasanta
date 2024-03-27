using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.Web.Models;

namespace SchoolProSite.Web.Controllers
{
    public class OnlineCourseController : Controller
    {
        private readonly IDaoOnlineCourse daoOnlineCourse;
        private readonly IDaoCourse daoCourse;

        public OnlineCourseController(IDaoOnlineCourse daoOnlineCourse, IDaoCourse daoCourse)
        {
            this.daoOnlineCourse = daoOnlineCourse;
            this.daoCourse = daoCourse;
        }

        // GET: OnlineCourseController
        public ActionResult Index()
        {
            var onlineCourses = this.daoOnlineCourse.GetOnlineCourses().Select(cd => new Models.OnlineCourseModel(cd));

            return View(onlineCourses);
        }

        // GET: OnlineCourseController/Details/5
        public ActionResult Details(int id)
        {
            var onlineCourse = this.daoOnlineCourse.GetOnlineCourse(id);

            OnlineCourseModel onlieCourseModel = new OnlineCourseModel(onlineCourse);

            return View(onlieCourseModel);
        }

        // GET: OnlineCourseController/Create
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

            return View();
        }

        // POST: OnlineCourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OnlineCourseModel onlineCourseModel)
        {
            try
            {
                this.daoOnlineCourse.SaveOnlineCourse(new DAL.Entities.OnlineCourse()
                {

                    CourseID = onlineCourseModel.CourseID,
                    URL = onlineCourseModel.URL

                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OnlineCourseController/Edit/5
        public ActionResult Edit(int id)
        {
            var onlineCourse = this.daoOnlineCourse.GetOnlineCourse(id);

            var modelOnlineCourse = new Models.OnlineCourseModel()
            {

                CourseID=onlineCourse.CourseID,
                URL=onlineCourse.URL
               
            };

            return View(modelOnlineCourse);

        }

        
        // POST: OnlineCourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OnlineCourseModel onlineCourseModel)
        {
            try
            {
                this.daoOnlineCourse.UpdateOnlineCourse(new DAL.Entities.OnlineCourse()
                {

                    CourseID = onlineCourseModel.CourseID,
                    URL = onlineCourseModel.URL
                    
                }); ;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OnlineCourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OnlineCourseController/Delete/5
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
