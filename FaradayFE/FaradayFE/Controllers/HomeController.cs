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
        private static List<LocationModel> locationList = new List<LocationModel>(); //Contains all information about and location
        private static List<string> locationsListDTO = new List<string>();

        //Car
        private static CarModel carModel = new CarModel();
        private static List<CarModel> carListDB = new List<CarModel>();              //Contains all information about and CarModel
        private static List<string> carsListDTO = new List<string>();

        //Booking 
        private static LocationModel selectedPickupLocations;     //Used for create Booking  -  Information sent from view
        private static LocationModel selectedDropOffLocations;    //Used for create Booking  -  Information sent from view
        private static CarModel selectedCar = new CarModel();     //Used for create Booking  -  Information sent from view
        private static CustomerModel customer;                    //Used for create Booking  -  Information sent from view
        private static string pickupDate;                         //Used for create Booking  -  Information sent from view
        private static string dropOffDate;                        //Used for create Booking  -  Information sent from view
        private static bool isCannceled = false;                  //Used for create Booking  -  Information sent from view

        //Cancel
        public static string test;
        private static BookingModel booking;
        private static List<BookingModel> bookingList;
        private static BookingModel selectedBookingToCancel;

        //Hvis man laver en controller som tager variabler som input der stemmer overenst men ^^ name="" ^^ i cshtlm (viewet) kan man få det til helt 
        //automatisk at hente både input, selecteditems, li elementer - ja, faktisk alt, så længe der er angivet en name=""
        public async Task<IActionResult> getCustomerDetails(string selectedDate, string firstname, string driverlicens,  string lastname, string gender, string cityList, string citydropoff, string carList)
         {
            Service service = new Service();
            
            foreach (var location in locationList)
            {
                if (location.Address == cityList)
                {
                    selectedPickupLocations = location;
                }
            }
            
            foreach (var location in locationList)
            {
                if (location.Address == citydropoff)
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
            customer = new CustomerModel() { FirstName = firstname,  LastName = lastname, DriversLicense = driverlicens, Gender = gender };

            CustomerId customerId = await service.Client().CreateCustomerModelAsync(customer);
            customer.Id = customerId.Id;

            pickupDate = DateTime.Today.ToString();
            dropOffDate = selectedDate;

            //LocationModel testLocationspickup = new LocationModel() { Id = 1560, City = "Rønne", Country = "Denmark", Address = "kildevej", Zipcode = "4000" };     Test objekt. 
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
            BookingId bookingId = await service.Client().CreateBookingModelAsync(bookingModel);
            
            ViewData["bookingComplete"] = bookingModel;

            return RedirectToAction("CreateBooking");
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

        private static string _selectedCancelBooking;
        public async Task<IActionResult> getSelectedCancelBooking(string selectedCancelBooking)  // id   model   n avn 
        {
            Service service = new Service();
            //Getting the whole booking 
            string[] test = selectedCancelBooking.Split(" ");
            var selectedItemId = Convert.ToInt32(test[2]);
            foreach (var booking in bookingList)
            {
                if(booking.Id == selectedItemId)
                {
                    selectedBookingToCancel = booking;
                    var requestAllBookings = await service.Client().CancelBookingModelAsync(selectedBookingToCancel);
                }
            }
            return RedirectToAction("CancelBooking");
        }

        public async Task<IActionResult> CancelBooking()
        {
            ViewBag.Message = "Cancel booking";
            Service service = new Service();
            booking = new BookingModel();
            bookingList = new List<BookingModel>();
           // List<string> bookingListDTO = new List<string>();

            using (var requestAllBookings = service.Client().GetAllActiveBookings(_emptyRequest))
            {
                while (await requestAllBookings.ResponseStream.MoveNext())
                {
                    booking = requestAllBookings.ResponseStream.Current;
                    bookingList.Add(booking);
                    //bookingListDTO.Add(booking.Car.Model);
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
            ViewData["location"] = locationList;  //Sends the list of data to the view. 
            return View();
        }


        public static async Task<List<CarModel>> GetCarModels()
        {
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
