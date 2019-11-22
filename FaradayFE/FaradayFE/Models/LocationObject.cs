using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaradayFE.Models
{
    public class LocationObject
    {
        int Id = 1;
        string Address = "2";
        string Zipcode = "3";
        string City = "4";
        string Country = "5";

        public LocationObject()
        {

        }

        public LocationObject(int Id, string Addres, string Zipcode, string City, string Country)
        {
            this.Id = Id;
            this.Address = Addres;
            this.Zipcode = Zipcode;
            this.City = City;
            this.Country = Country;
        }

        private int myVar;

        public int getId
        {
            get { return Id; }
            set { Id = value; }
        }
        public string getAddress
        {
            get { return Address; }
            set { Address = value; }
        }
        public string getZipcode
        {
            get { return Zipcode; }
            set { Zipcode = value; }
        }
        public string getCity
        {
            get { return City; }
            set { City = value; }
        }
        public string getCountry
        {
            get { return Country; }
            set { Country = value; }
        }

    }
}
