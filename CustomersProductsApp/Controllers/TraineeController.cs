using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using TraineesApp.Models;
using TraineesApp.Repositories;

namespace TraineesApp.Controllers
{
    public class TraineeController : Controller
    {
        private readonly ITraineeRepository _traineeRepository;
        private readonly ITrackRepository _trackRepository;

        public TraineeController(ITraineeRepository traineeRepository, ITrackRepository trackRepository)
        {
            _traineeRepository = traineeRepository;
            _trackRepository = trackRepository;
        }

        [Route("trainees")]
        [Authorize(Roles = "Member,Administrator")]
        public ActionResult Index()
        {

            var tracks = _trackRepository.GetAll();
            SelectList selectLists = new SelectList(tracks, "Id", "Name");
            ViewBag.SelectList = selectLists;
            var trainees = _traineeRepository.GetAllTraineesWithTrack();
            return View(trainees);
        }
        [HttpPost]
        [Authorize(Roles = "Member,Administrator")]
        public ActionResult Index(int TrackId)
        {
            var tracks = _trackRepository.GetAll().ToList();
            if (TrackId == 0)
            {
                return RedirectToAction("Index");
            }
            if (TrackId == -1)
            {
                var trainees = _traineeRepository.GetAllTraineesWithTrack().Where(t => t.Track == null);
                SelectList selectLists = new SelectList(tracks, "Id", "Name", TrackId);
                ViewBag.SelectList = selectLists;

                return View(trainees);

            }
            else
            {
                var trainees = _traineeRepository.GetAllTraineesWithTrack().Where(t => TrackId == t.TrackId);
                SelectList selectLists = new SelectList(tracks, "Id", "Name", TrackId);
                ViewBag.SelectList = selectLists;

                return View(trainees);
            }

        }

        [Route("trainees/{id:int:min(1)}")]
        [Authorize(Roles = "Member,Administrator")]
        public ActionResult Details(int id)
        {
            var track = _traineeRepository.GetTraineeByIdWithTrack(id);
            return View(track);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var tracks = _trackRepository.GetAll();
            SelectList selectLists = new SelectList(tracks, "Id", "Name");
            ViewBag.tracks = selectLists;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(Trainee trainee)
        {

            if (trainee.TrackId == 0)
                trainee.TrackId = null;
            if (ModelState.IsValid)
            {
                _traineeRepository.Add(trainee);
                return RedirectToAction("Index");
            }
            else
            {
                var tracks = _trackRepository.GetAll();
                SelectList selectLists = new SelectList(tracks, "Id", "Name", trainee.TrackId);
                ViewBag.tracks = selectLists;
                return View();
            }
        }

        [Route("trainees/edit/{id:int:min(1)}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var tracks = _trackRepository.GetAll();
            SelectList selectLists = new SelectList(tracks, "Id", "Name");
            ViewBag.tracks = selectLists;
            var trainee = _traineeRepository.GetTraineeByIdWithTrack(id);
            return View(trainee);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Trainee trainee)
        {
            if (trainee.TrackId == 0)
                trainee.TrackId = null;
            if (ModelState.IsValid)
            {
                _traineeRepository.Update(trainee);
                return RedirectToAction("Index");
            }
            else
            {
                var tracks = _trackRepository.GetAll();
                SelectList selectLists = new SelectList(tracks, "Id", "Name");
                ViewBag.tracks = selectLists;
                var t = _traineeRepository.GetTraineeByIdWithTrack(trainee.Id);

                return View(t);
            }
        }

        [Route("trainees/delete/{id:int:min(1)}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            _traineeRepository.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
