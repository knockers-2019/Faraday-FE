using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaradayFE.Protofil
{
    public class Service 
    {
        public Service()
        {
            Channel channel = new Channel("localhost", ChannelCredentials.Insecure);
            var client = new Booking.BookingClient(channel);

        }
    }
}