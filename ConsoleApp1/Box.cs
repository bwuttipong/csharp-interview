using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Box<T>
    {
        public T Content { get; set; }

        public void LogContent()
        {
            Console.WriteLine($"Box contains: {Content}");
        }
    }
}