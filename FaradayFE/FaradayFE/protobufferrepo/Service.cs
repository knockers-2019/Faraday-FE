using FaradayGrpcServer;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FaradayGrpcServer;

namespace FaradayFE.protobufferrepo
{
    public class Service
    {

        private Bookings.BookingsClient client;
        public Service()
        {
            //http://80.198.94.195:5001
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new Bookings.BookingsClient(channel);

        }

        /// <summary>
        /// Property to use the Proto methods. 
        /// </summary>
        public Bookings.BookingsClient Client()
        {
            return client;
        }
    }
}