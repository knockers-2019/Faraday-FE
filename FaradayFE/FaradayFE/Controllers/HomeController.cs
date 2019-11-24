using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FaradayFE.Models;
using FaradayFE.Content;
using FaradayGrpcServer;
using FaradayFE.protobufferrepo;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FaradayFE.Controllers
{
    public class HomeController : Controller
    {
        private static EmptyRequest _emptyRequest = new EmptyRequest();
        //Location
        private LocationModel location = new LocationModel();
        private List<LocationModel> locationList = new List<LocationModel>(); //Contains all information 
        private List<string> locationsListDTO = new List<string>();
        //Car
        private static CarModel carModel = new CarModel();
        private static List<CarModel> carList = new List<CarModel>();
        private static List<string> carsListDTO = new List<string>();

        private static CustomerModel customer;
        private static string dropofDate;


        public IActionResult getCustomerDetails(string selectedDate, string name, string phone, string email)
        {
            dropofDate = selectedDate;
            customer = new CustomerModel() { FirstName = name, LastName = phone, DriversLicense = email };
            return Redirect("CreateBooking");
            
        }

        //private readonly ILogger<HomeController> _logger;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController()
        {

        }
     

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public IActionResult CancelBooking()
        {
            ViewBag.Message = "Cancel booking";
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Our contact page.";
            return View();
        }


       
        public async Task<IActionResult> CreateBooking()
        {
            DummyBackend dummyBackend = new DummyBackend();
            Service service = new Service();
            using (var requestAllLocations = service.Client().GetAllLocationModels(_emptyRequest))
            {
                while (await requestAllLocations.ResponseStream.MoveNext())
                {
                    location = requestAllLocations.ResponseStream.Current;
                    locationList.Add(location);
                    locationsListDTO.Add(location.City);
                }
            }
            ViewData["location"] = locationsListDTO;  //Sends the list of data to the view. 
            return View();
        }


       
        public static async Task<List<CarModel>> GetCarModels()
        {
            DummyBackend dummyBackend = new DummyBackend();
            Service service = new Service();

            using (var requestAllLocations = service.Client().GetAllCarModels(_emptyRequest))
            {
                while (await requestAllLocations.ResponseStream.MoveNext())
                {
                    carModel = requestAllLocations.ResponseStream.Current;
                    carList.Add(carModel);
                    carsListDTO.Add(carModel.Brand);
                }
            }
            return carList;  //Sends the list of data to the view. 
        }

         public async Task<List<string>> GetSomething214123()
        {
            DummyBackend dummyBackend = new DummyBackend();
            Service service = new Service();

            EmptyRequest emptyRequest = new EmptyRequest();
            CarModel carModel = new CarModel();
            List<CarModel> carList = new List<CarModel>();
            List<string> carsListDTO = new List<string>();

            //var opt = element
            //CarModel strDDLValue = new Select
            //foreach (var item in carList
            //{
            //    if (item.)
            //}
            //    customer 
            //string dropOfDate
            string dropOfDate = Request.Form["selectedDate"];
            string dropOfLocation = Request.Form["selectedDate"];
            bool isCannceled = false;
            //pickupdate 
            //    pickuplocation

            BookingModel bookingModel = new BookingModel() { };

            

            using (var requestAllLocations = service.Client().GetAllCarModels(emptyRequest))
            {
                while (await requestAllLocations.ResponseStream.MoveNext())
                {
                    carModel = requestAllLocations.ResponseStream.Current;
                    carList.Add(carModel);
                    carsListDTO.Add(carModel.Brand);
                }
            }
            return carsListDTO;  //Sends the list of data to the view. 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
