using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FaradayFE.Content
{
    public class Car
    {
        public enum CarType { A, B, C, D, E, F }
        /// <summary>
        /// The mode class of a car
        /// </summary>
        /// <param name="carId">The Id of the car, makes the car unique, and mainly used for convience.</param>
        /// <param name="licensePlate">A unique identifier, which is used for references in the system.</param>

        private string carId;
        private string licensePlate;
        private CarType carType;


        ///Empty constructor
        public Car()
        {

        }

        public Car(string carId, string licensePlate, CarType carType)
        {
            this.CarId = carId;
            this.LicensePlate = licensePlate;
            this.CarType1 = carType;
        }

        ///Getters and setters
        public string CarId { get => carId; set => carId = value; }
        public string LicensePlate { get => licensePlate; set => licensePlate = value; }
        public CarType CarType1 { get => carType; set => carType = value; }
    }
}