using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.Web.Models;

namespace SchoolProSite.Web.Controllers
{
    public class OnsiteCourseController : Controller
    {
        private readonly IDaoOnsiteCourse daoOnsiteCourse;
        private readonly IDaoCourse daoCourse;

        public OnsiteCourseController(IDaoOnsiteCourse daoOnsiteCourse, IDaoCourse daoCourse)
        {
            this.daoOnsiteCourse = daoOnsiteCourse;
            this.daoCourse = daoCourse;
        }

        // GET: OnsiteCourseController
        public ActionResult Index()
        {
            var onsiteCourses = this.daoOnsiteCourse.GetOnsiteCourses().Select(cd => new Models.OnsiteCourseModel(cd));

            return View(onsiteCourses);
        }

        // GET: OnsiteCourseController/Details/5
        public ActionResult Details(int id)
        {
            var onsiteCourse = this.daoOnsiteCourse.GetOnsiteCourse(id);

            OnsiteCourseModel onsiteCourseModel = new OnsiteCourseModel(onsiteCourse);

            return View(onsiteCourseModel);
        }

        // GET: OnsiteCourseController/Create
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

        // POST: OnsiteCourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OnsiteCourseModel onsiteCourseModel)
        {
            try
            {
                this.daoOnsiteCourse.SaveOnsiteCourse(new DAL.Entities.OnsiteCourse()
                {

                    CourseID = onsiteCourseModel.CourseID,
                    Location = onsiteCourseModel.Location,
                    Days = onsiteCourseModel.Days,
                    Time = onsiteCourseModel.Time

                });


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OnsiteCourseController/Edit/5
        public ActionResult Edit(int id)
        {
            var onsiteCourse = this.daoOnsiteCourse.GetOnsiteCourse(id);

            var modelOnsiteCourse   = new Models.OnsiteCourseModel()
            {
                CourseID = onsiteCourse.CourseID,   
                CourseName = onsiteCourse.CourseName,   
                Location = onsiteCourse.Location,
                Days    = onsiteCourse.Days,
                Time = onsiteCourse.Time
            };

            return View(modelOnsiteCourse);
        }

        // POST: OnsiteCourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OnsiteCourseModel onsiteCourseModel)
        {
            try
            {
                this.daoOnsiteCourse.UpdateOnsiteCourse(new DAL.Entities.OnsiteCourse()
                {

                    CourseID = onsiteCourseModel.CourseID,
                    Days = onsiteCourseModel.Days,
                    Location = onsiteCourseModel.Location,
                    Time = onsiteCourseModel.Time

                }); 

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OnsiteCourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OnsiteCourseController/Delete/5
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
