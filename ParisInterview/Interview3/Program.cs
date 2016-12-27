namespace Interview3
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("1 = " + RomanNumeral.GetValue("I"));
            Console.WriteLine("3 = " + RomanNumeral.GetValue("III"));
            Console.WriteLine("4 = " + RomanNumeral.GetValue("IV"));
            Console.WriteLine("9 = " + RomanNumeral.GetValue("IX"));
            Console.WriteLine("37 = " + RomanNumeral.GetValue("XXXVII"));*/

            HotelFinder hotelFinder = new HotelFinder();
            List<Hotel> hotelsInArea = hotelFinder.FindHotelsNearLocation(new Point {X = 0.0, Y = 0.0}, 10.0);
            
            Console.WriteLine("Results:");
            foreach (Hotel hotel in hotelsInArea)
            {
                Console.WriteLine(string.Format("{0} at ({1}, {2})", hotel.Name, hotel.Location.X, hotel.Location.Y));
            }

            Console.ReadLine();
        }
    }
}