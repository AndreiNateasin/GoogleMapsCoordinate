using System.Collections.Generic;
using System.Web.Mvc;
using GoogleMapsCoordonates.Models;
using GoogleMapsCoordonates.Models.Home;
using GoogleMapsCoordonates.Repositories;
using GoogleMapsCoordonates.Helpers;
namespace GoogleMapsCoordonates.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoutesRepository _repository;
        private const int NumberOfMarkersAvailable = 23;

        public HomeController()
        {
            _repository = new RoutesRepository();
        }

        public ActionResult Index()
        {
            var routes = _repository.GetIMEIs();

            return View(routes);
        }

        public ActionResult MapDisplay(long imei)
        {
            var coordinatesByIMEI = _repository.GetCoordinatesByIMEI(imei).TruncateCoordinates(NumberOfMarkersAvailable);
            return View(coordinatesByIMEI);
        }

        public ActionResult About() 
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Delete(int id)
        {
            var coordonate = _repository.GetCoordinatesByIMEI(id);

            return View(coordonate);
        }


        [HttpPost]
        public ActionResult Delete(int id, string confirmationButton)
        {
            _repository.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new RouteInput());
        }

        [HttpPost]
        public ActionResult Create(RouteInput routeInput)
        {
            _repository.Add(new Coordinate
                {
                    latitude = routeInput.ToLat,
                    longitude = routeInput.ToLong
                });

            return RedirectToAction("Index");
        }
    }
}
