using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineesApp.Models;
using TraineesApp.Repositories;

namespace TraineesApp.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackRepository _trackRepository;

        public TrackController(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        [Authorize(Roles = "Member,Administrator")]
        public ActionResult Index()
        {
            var tracks = _trackRepository.GetAll();
            return View(tracks);
        }

        [Route("tracks/{id:int:min(1)}")]
        [Authorize(Roles = "Member,Administrator")]
        public ActionResult Details(int id)
        {
            var track = _trackRepository.GetById(id);
            return View(track);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(Track track)
        {
            if (ModelState.IsValid)
            {
                _trackRepository.Add(track);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Route("tracks/edit/{id:int:min(1)}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var track = _trackRepository.GetById(id);
            return View(track);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Track track)
        {
            if (ModelState.IsValid)
            {
                _trackRepository.Update(track);
                return RedirectToAction("Index");
            }
            else
            {
                var t = _trackRepository.GetById(track.Id);
                return View(t);
            }
        }

        [Route("tracks/delete/{id:int:min(1)}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            _trackRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
