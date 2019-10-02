using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DatabaseContext db = new DatabaseContext()) { }
        }
    }
}
