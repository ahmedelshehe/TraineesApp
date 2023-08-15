using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public ActionResult Index()
        {

            var tracks = _trackRepository.GetAll();
            SelectList selectLists = new SelectList(tracks, "Id", "Name");
            ViewBag.SelectList = selectLists;
            var trainees = _traineeRepository.GetAllTraineesWithTrack();
            return View(trainees);
        }
        [HttpPost]
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
        public ActionResult Details(int id)
        {
            var track = _traineeRepository.GetTraineeByIdWithTrack(id);
            return View(track);
        }

        public ActionResult Create()
        {
            var tracks = _trackRepository.GetAll();
            SelectList selectLists = new SelectList(tracks, "Id", "Name");
            ViewBag.tracks = selectLists;
            return View();
        }

        [HttpPost]
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

        public ActionResult Edit(int id)
        {
            var tracks = _trackRepository.GetAll();
            SelectList selectLists = new SelectList(tracks, "Id", "Name");
            ViewBag.tracks = selectLists;
            var trainee = _traineeRepository.GetTraineeByIdWithTrack(id);
            return View(trainee);
        }

        [HttpPost]
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

        public ActionResult Delete(int id)
        {
            _traineeRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
