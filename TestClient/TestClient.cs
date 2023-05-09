using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    public class TestClient
    {
        public async Task<bool> SendSMS(object input)
        {
            if (Convert.ToInt64(input) % 2 == 0)
                throw new Exception("Error");

            return true;
        }
    }
}
