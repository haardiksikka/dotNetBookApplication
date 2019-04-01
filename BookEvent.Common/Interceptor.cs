using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEvent.Common
{
    public class Interceptor
    {
        public void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(string message, Exception ex)
        {
            Console.WriteLine(message, ex.StackTrace);
        }
    }
}
