using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TraineesApp.Models;
using TraineesApp.Repositories;

namespace TraineesApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITrackRepository _trackRepository;

        public CourseController(ICourseRepository courseRepository, ITrackRepository trackRepository)
        {
            _courseRepository = courseRepository;
            _trackRepository = trackRepository;
        }

        [Route("courses")]
        [Authorize(Roles = "Member,Administrator")]
        public ActionResult Index()
        {
            var courses = _courseRepository.GetAllCoursesWithTrack();
            return View(courses);
        }

        [Route("courses/{id:int:min(1)}")]
        [Authorize(Roles = "Member,Administrator")]
        public ActionResult Details(int id)
        {
            var course = _courseRepository.GetCourseWithTrack(id);
            return View(course);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var tracks = _trackRepository.GetAll();
            SelectList selectLists = new SelectList(tracks, "Id", "Name");
            ViewBag.tracks = selectLists;
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (course.TrackId == -1)
                ModelState.AddModelError("TrackId", "You have to choose a track");

            if (ModelState.IsValid)
            {
                _courseRepository.Add(course);
                return RedirectToAction("Index");
            }
            else
            {
                var tracks = _trackRepository.GetAll();
                SelectList selectLists = new SelectList(tracks, "Id", "Name");
                ViewBag.tracks = selectLists;
                return View();
            }



        }

        [Route("courses/edit/{id:int:min(1)}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var tracks = _trackRepository.GetAll();
            SelectList selectLists = new SelectList(tracks, "Id", "Name");
            ViewBag.tracks = selectLists;
            var track = _courseRepository.GetById(id);
            return View(track);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseRepository.Update(course);
                return RedirectToAction("Index");
            }
            else
            {
                var tracks = _trackRepository.GetAll();
                SelectList selectLists = new SelectList(tracks, "Id", "Name");
                ViewBag.tracks = selectLists;
                var track = _courseRepository.GetById(course.Id);
                return View(track);
            }
        }

        [Route("courses/delete/{id:int:min(1)}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            _courseRepository.Delete(id);
            return RedirectToAction("Index");
        }



    }
}
