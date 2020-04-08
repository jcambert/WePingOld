using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var spidauth = "FFTT";
            //var tm =DateTime.Now.ToString("yyyyMMddhhmmssfff", CultureInfo.CreateSpecificCulture("fr-FR"));//MMddHHMMSSmmm
            string tm = "20150611140022081";

            var ccle = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(spidauth));
            var clee = BitConverter.ToString(ccle)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            //Console.WriteLine(GetHash(tm,ccle));

            Console.WriteLine(clee);

            Console.WriteLine(GetHash(tm, clee));


            Console.ReadLine();
        }

        public static String GetHash(String text, String key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            UTF8Encoding encoding = new UTF8Encoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA1 hash = new HMACSHA1(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
