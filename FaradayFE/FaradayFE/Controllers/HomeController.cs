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
using System.Globalization;

namespace FaradayFE.Controllers
{
    public class HomeController : Controller
    {
        private static EmptyRequest _emptyRequest = new EmptyRequest();
        //Location
        private static LocationModel location = new LocationModel();
        private static List<LocationModel> locationList = new List<LocationModel>(); //Contains all information 
        private static List<string> locationsListDTO = new List<string>();
        //Car
        private static CarModel carModel = new CarModel();
        private static List<CarModel> carListDB = new List<CarModel>();
        private static List<string> carsListDTO = new List<string>();

        //Booking 
        private static LocationModel selectedPickupLocations;    //Used for create Booking  - Information sent from view
        private static LocationModel selectedDropOffLocations;    //Used for create Booking  - Information sent from view
        private static CarModel selectedCar = new CarModel();                  //Used for create Booking  - Information sent from view
        private static CustomerModel customer;                                 //Used for create Booking  - Information sent from view
        private static string pickupDate;
        private static string dropOffDate;  //Used for create Booking  - Information sent from view
        private static bool isCannceled = false;

        //private readonly List<LocationModel> _locations;  //preloaded from database and stored
        //public int SelectedLocationsId { get; set; }
        //string citySelected;
        //string carSelected;


        //Hvis man laver en controller som tager variabler som input der stemmer overenst men ^^ name="" ^^ i cshtlm (viewet) kan man få det til helt 
        //automatisk at hente både input, selecteditems, li elementer - ja, faktisk alt, så længe der er angivet en name=""
        public async void getCustomerDetails(string selectedDate, string name, string phone, string email, string cityList, string citydropoff, string carList)
        {
            Service service = new Service();


            //pickupLocations.City = cityList;
            foreach (var location in locationList)
            {
                if (location.City == cityList)
                {
                    selectedPickupLocations = location;
                }
            }
            //dropOffLocations.City =  "Rønne";            //Hard coded, need to modify view.
            foreach (var location in locationList)
            {
                if (location.City == citydropoff)
                {
                    selectedDropOffLocations = location;
                }
            }
            foreach (var car in carListDB)
            {
                if (car.Brand == carList)
                {
                    selectedCar = car;
                }
            }
            customer = new CustomerModel() { FirstName = name, LastName = phone, DriversLicense = email };
            pickupDate = DateTime.Today.ToString();
            dropOffDate = selectedDate;


            //{ booking.PickupLocation.Id}
            //', '{ booking.DropoffLocation.Id}
            //', '{ booking.Car.Id}
            //', '{ booking.Customer.Id}
            //', '{ booking.PickupDate}
            //', '{ booking.DropoffDate} 

            BookingModel bookingModel = new BookingModel()
            {
                PickupLocation = selectedPickupLocations,
                DropoffLocation = selectedDropOffLocations,
                Car = selectedCar,
                Customer = customer,
                PickupDate = pickupDate,
                DropoffDate = dropOffDate,
                IsCancelled = isCannceled
            };

            var id = await service.Client().CreateBookingModelAsync(bookingModel);
        }

        //message BookingModel {
        //    int32 id = 1;
        //    LocationModel pickup_location = 2;
        //    LocationModel dropoff_location = 3;
        //    CarModel car = 4;
        //    CustomerModel customer = 5;
        //    string pickup_date = 6;
        //    string dropoff_date = 7;
        //    bool is_cancelled = 8;
        //}




        //public async Task<List<string>> GetSomething214123()
        //{
        //    DummyBackend dummyBackend = new DummyBackend();
        //    Service service = new Service();

        //    EmptyRequest emptyRequest = new EmptyRequest();
        //    CarModel carModel = new CarModel();
        //    List<CarModel> carList = new List<CarModel>();
        //    List<string> carsListDTO = new List<string>();

        //    //var opt = element
        //    //CarModel strDDLValue = new Select
        //    //foreach (var item in carList
        //    //{
        //    //    if (item.)
        //    //}
        //    //    customer 
        //    //string dropOfDate
        //    string dropOfDate = Request.Form["selectedDate"];
        //    string dropOfLocation = Request.Form["selectedDate"];
        //    bool isCannceled = false;
        //    //pickupdate 
        //    //    pickuplocation

        //    return View();
        //}

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

        public async Task<IActionResult> CancelBooking()
        {
            ViewBag.Message = "Cancel booking";
            DummyBackend dummyBackend = new DummyBackend();
            Service service = new Service();
            BookingModel booking = new BookingModel();
            List<BookingModel> bookingList = new List<BookingModel>();
            List<string> bookingListDTO = new List<string>();

            using (var requestAllBookings = service.Client().GetAllBookingModels(_emptyRequest))
            {
                while (await requestAllBookings.ResponseStream.MoveNext())
                {
                    booking = requestAllBookings.ResponseStream.Current;
                    bookingList.Add(booking);
                    bookingListDTO.Add(booking.Car.Model);
                }
            }
            ViewData["booking"] = bookingList;  //Sends the list of data to the view.

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
                    carListDB.Add(carModel);
                    carsListDTO.Add(carModel.Brand);
                }
            }
            return carListDB;  //Sends the list of data to the view. 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
