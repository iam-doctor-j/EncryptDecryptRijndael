using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Encrypt/n2.Decrypt");
            string opt = Console.ReadLine();
            string str = "";
            switch(opt)
            {
                case "1":
                    str = Console.ReadLine();
                    Console.WriteLine(new EncryptDecrypt().Encrypt(str));
                    break;
                case "2":
                    str = Console.ReadLine();
                    Console.WriteLine(new EncryptDecrypt().Decrypt(str));
                    break;
            }
        }
    }
}
