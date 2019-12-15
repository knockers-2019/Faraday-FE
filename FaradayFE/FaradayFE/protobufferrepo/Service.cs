using FaradayGrpcServer;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FaradayGrpcServer;
using System.Net.Http;

namespace FaradayFE.protobufferrepo
{
    public class Service
    {
        private Bookings.BookingsClient client;
        public Service()
        {
            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpClientHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);

            var channel = GrpcChannel.ForAddress("https://80.198.94.195:5001",
                new GrpcChannelOptions { HttpClient = httpClient });

            client = new Bookings.BookingsClient(channel);
        }

        public Bookings.BookingsClient Client()
        {
            return client;
        }
    }
}