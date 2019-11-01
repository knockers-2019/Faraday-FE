using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FaradayContract;

namespace FaradayFE
{
    public class TestClasse : FaradayContract.IBooking
    {
        public void CancelBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public Booking CreateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetBookings(string driversLicense)
        {
            throw new NotImplementedException();
        }

        public List<string> MakeNewBooking()
        {
            throw new NotImplementedException();
        }

        public void RegisterCar(Car car)
        {
            throw new NotImplementedException();
        }

        public void RegisterCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public List<Car> RegisterPickup(string place, DateTime time)
        {
            throw new NotImplementedException();
        }

        public List<Car> ShowAvailableCars(string place, DateTime time)
        {
            throw new NotImplementedException();
        }
    }
}