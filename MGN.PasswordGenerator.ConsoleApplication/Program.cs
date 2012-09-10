using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGN.PasswordGenerator.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(PasswordGenerator.Model.PasswordGenerator.GeneratePassword(12));
            }
        }
    }
}
