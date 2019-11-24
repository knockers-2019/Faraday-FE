using FaradayFE.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaradayFE.Models
{
    public class Booking
    {
        private Car car;
        private string customer;
        private string pickUpDate;

        public Booking()
        {
                
        }
        public Booking(Car car, string customer, string pickUpDate)
        {
            this.car = car;
            this.customer = customer;
            this.pickUpDate = pickUpDate;
        }

        public Car Car
        {
            get { return car; }
            set { car = value; }
        }
        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public string PickUpDate
        {
            get { return pickUpDate; }
            set { pickUpDate = value; }
        }
    }
}
