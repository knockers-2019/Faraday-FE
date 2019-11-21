using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaradayFE.Content
{
    public class DummyBackend
    {
        private List<Car> carlist = new List<Car>() {
        new Car("1", "AB67523", Car.CarType.A), new Car("2", "AE519283", Car.CarType.A), new Car("3", "BC92831", Car.CarType.A), new Car("4", "BC6751", Car.CarType.A),
        new Car("5", "BC12351", Car.CarType.B), new Car("6", "TH21643", Car.CarType.B), new Car("7", "TH21641", Car.CarType.B), new Car("8", "TH21621", Car.CarType.B),
        new Car("9", "TH21721", Car.CarType.C), new Car("10", "KH92831", Car.CarType.C), new Car("111", "KH92831", Car.CarType.C), new Car("12", "EG95482", Car.CarType.C),
        new Car("13", "EG95451", Car.CarType.D),new Car("14", "EG95421", Car.CarType.D)};

        private List<string> placelist = new List<string>()
        {
            "Roskilde", "København", "Farum", "Gundsømagle"
        };


        public DummyBackend()
        {

        }

        public List<Car> Carlist
        {
            get { return carlist; }
            set { carlist = value; }
        }

        public List<string> Placelist
        {
            get { return placelist; }
            set { placelist = value; }
        }
    }
}