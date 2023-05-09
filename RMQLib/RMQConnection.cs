using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQLib
{
   public static class RMQConnection
    {
        public static ConnectionFactory conFac = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "mshamia",
            Password = "12345",
            RequestedHeartbeat = new TimeSpan(0, 1, 60),
            AutomaticRecoveryEnabled = true,

        };
        public static IConnection _connection;
        public static IConnection connection
        {
            get
            {
                if (_connection == null || !_connection.IsOpen)
                {
                    _connection = conFac.CreateConnection("Publisher");

                }
                return _connection;
            }
        }
    }
}
