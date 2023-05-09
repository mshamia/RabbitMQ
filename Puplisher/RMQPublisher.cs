using RabbitMQ.Client;
using RMQLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Puplisher
{
  public   class RMQPublisher
    {
        private static int elapsedCounts = 0;
        //private static ConnectionFactory conFac = new ConnectionFactory()
        //{
        //    HostName = "localhost",
        //    UserName = "mshamia",
        //    Password = "12345",
        //    RequestedHeartbeat = new TimeSpan(0, 1, 60),
        //    AutomaticRecoveryEnabled = true,

        //};
        //private static IConnection _connection;
        //private static IConnection connection
        //{
        //    get
        //    {
        //        if (_connection == null || !_connection.IsOpen)
        //        {
        //            _connection = conFac.CreateConnection("Publisher");
                   
        //        }
        //        return _connection;
        //    }
        //}
        public void GenericPushMessage()
        {
            DateTime RabbitMQStartDate = DateTime.Now;
            elapsedCounts++;
            if (elapsedCounts > 1200)
            {
                
                if (RMQConnection.connection.IsOpen)
                    RMQConnection.connection.Close();
                elapsedCounts = 0;
            }
            //if (!connection.IsOpen)
            //{
            //    connection = conFac.CreateConnection("Producer");
            //}
            var iModel = RMQConnection.connection.CreateModel();
            var msgProperties = iModel.CreateBasicProperties();
            msgProperties.Persistent = true;

            //msgProperties.Headers = new Dictionary<string, object>();
            //msgProperties.Headers.Add("x-delay", 70000);
            var message = DateTime.Now.Ticks.ToString();
            var messageSerialzed = Newtonsoft.Json.JsonConvert.SerializeObject(message, (Newtonsoft.Json.Formatting)Formatting.Indented);
 

            //iModel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            //iModel.QueueDeclare(queueName, false, false, false, null);
            //iModel.QueueBind(queueName, exchangeName, routingKey, null);
            //iModel.BasicPublish("", "", msgProperties, Encoding.UTF8.GetBytes(messageSerialzed));

            iModel.BasicPublish("Ex.Mshamia", "#", msgProperties, Encoding.UTF8.GetBytes(messageSerialzed));


 




        }

    }
}
