using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RabbitMQ.Controllers
{
    public class RMQConsumeController : Controller
    {
        // GET: RMQConsume
        public ActionResult Index()
        {
            MqTest();
            return View();
        }

        public void MqTest()
        {
            System.Diagnostics.Debug.Write("test begin:");
            //RMQConsumer.Consume();
         }
     }
}