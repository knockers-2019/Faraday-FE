using Faraday_BE_gRPC;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaradayFE.protobufferrepo
{
    public class Service
    {

        private Booking.BookingClient client;
        public Service()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var bookingClient = new Booking.BookingClient(channel);
        }

        /// <summary>
        /// Property to use the Proto methods. 
        /// </summary>
        public Booking.BookingClient Client
        {
            get { return client; }
            set { client = value; }
        }
    }
}