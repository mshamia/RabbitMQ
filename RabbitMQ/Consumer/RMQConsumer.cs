using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RMQLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RabbitMQ.Consumer
{
    public class RMQConsumer
    {
        //نحتاج في حال في خطأ يبقى شغال الكنسيومر
        public void Consume()
        {
            int elapsedCounts = 0;

            if (elapsedCounts >= 1200)
            {

                if (RMQConnection.connection.IsOpen)
                {
                    RMQConnection.connection.Close();

                }
                elapsedCounts = 0;
            }

            var channel = RMQConnection.connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            var ConsumerTag = "";

            channel.BasicQos(0, 1, true);
            consumer.Received += async (ch, ea) =>
           {
               try
               {
                   elapsedCounts++;


                   var body = ea.Body.ToArray();

                   var messageData = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(body));

                   TestClient.TestClient testClient = new TestClient.TestClient();
                   channel.BasicAck(ea.DeliveryTag, false);
                   await testClient.SendSMS(messageData);


                   //TODO:
                   //Failure Status
                   //send to dead letter queue
                   //resend to the same queue

               }

               catch (Exception ex)
               {

                   //if(!channel.IsClosed)
                   //{
                   //    channel.BasicAck(ea.DeliveryTag, false);

                   //}
                   if (elapsedCounts >= 1200)
                   {

                       if (RMQConnection.connection.IsOpen)
                       {
                           //RMQConnection.connection.Close() = new TimeSpan(1);
                           RMQConnection.connection.Close();
                           await StopAsync(new CancellationToken());

                       }
                       elapsedCounts = 0;
                   }
                   //if (elapsedCounts >= 1200)
                   //{
                   //    var s = RMQConnection.connection.IsOpen;
                   //    if (RMQConnection.connection.IsOpen)
                   //        RMQConnection.connection.Close();

                   //    if (channel.IsOpen)
                   //    {
                          
                   //        channel.BasicCancel(ConsumerTag);
                   //        channel.Close();
                   //        await StopAsync(new CancellationToken());

                   //    }
                   //}
               }

           };
            ConsumerTag = channel.BasicConsume("Que.Mshamia", false, consumer);


        }

        System.Timers.Timer _timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Consume();

                // create exchange , queue and bind queue with exchange
                if (_timer != null)
                    _timer.Dispose();

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                //retry to connect 
                _timer = new System.Timers.Timer();
                _timer.Interval = 1;
                _timer.Elapsed += _timer_Elapsed;
                _timer.Start();
                return Task.CompletedTask;
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            StartAsync(cancellationToken);
            return Task.CompletedTask;
        }
        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            StartAsync(new CancellationToken());
        }



    }
}