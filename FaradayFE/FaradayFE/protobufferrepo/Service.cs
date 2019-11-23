﻿using FaradayGrpcServer;
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

        private Bookings.BookingsClient client;
        public Service()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var bookingClient = new Bookings.BookingsClient(channel);
        }

        /// <summary>
        /// Property to use the Proto methods. 
        /// </summary>
        public Bookings.BookingsClient Client
        {
            get { return client; }
            set { client = value; }
        }
    }
}