using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puplisher
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int i = 0;
                while (true)
                {
                    Console.WriteLine(i++);

                    RMQPublisher rMQPublisher = new RMQPublisher();
                    rMQPublisher.GenericPushMessage();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
