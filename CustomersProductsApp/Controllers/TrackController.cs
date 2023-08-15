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

		public ActionResult Index()
		{
			var tracks = _trackRepository.GetAll();
			return View(tracks);
		}

		public ActionResult Details(int id)
		{
			var track = _trackRepository.GetById(id);
			return View(track);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
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

		public ActionResult Edit(int id)
		{
			var track = _trackRepository.GetById(id);
			return View(track);
		}

		[HttpPost]
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

		public ActionResult Delete(int id)
		{
			_trackRepository.Delete(id);
			return RedirectToAction("Index");
		}

	}
}
