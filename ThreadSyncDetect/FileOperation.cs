using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSyncDetect
{
    class SalesFileOperation
    {
        private static Object thisLock = new Object();  

        public static void ReadCityRevenueFile()
        {
            Console.WriteLine("Entering Function ReadCityRevenueFile");
            Console.WriteLine("ReadCityRevenueFile ThreadId = " + AppDomain.GetCurrentThreadId());

            Dictionary<string, UInt64> dictCityRevenueData = new Dictionary<string, UInt64>();

            string file = @"./data/CityRevenueJan17.txt";

            try
            {
                lock (thisLock)
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        UInt64 cityRevenue = 0;

                        reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (!string.IsNullOrEmpty(line))
                            {
                                Thread.Sleep(1000);
                                string[] cityData = line.Split('\t');

                                // Line format is City Area Item    Date SalePrice
                                // Delhi    Saket   Radio   12-01-2017  580
                                if (cityData.Length != 5)
                                {
                                    //throw new Exception("Invalid Record");
                                    Console.WriteLine("Invalid Record");
                                    continue;
                                }

                                // Check if Dictionary has entry for this Market
                                if (dictCityRevenueData.TryGetValue(cityData[0], out cityRevenue))
                                {

                                    dictCityRevenueData[cityData[0]] = cityRevenue + Convert.ToUInt64(cityData[4]);
                                }
                                else
                                {
                                    dictCityRevenueData.Add(cityData[0], Convert.ToUInt64(cityData[4]));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Raised: " + ex.Message);
            }
        }

        public static void WriteCityRevenueFile()
        {
            Console.WriteLine("Entering Function WriteCityRevenueFile");
            Console.WriteLine("WriteCityRevenueFile ThreadId = " + AppDomain.GetCurrentThreadId());

            Thread.Sleep(2000);

            string file = @"./data/CityRevenueJan17.txt";

            try
            {
                lock (thisLock)
                {

                    Console.WriteLine("Inside the lock of WriteCityRevenueFile");
                    //using (StreamReader reader = new StreamReader(file))
                    //{

                    //}

                    Thread.Sleep(2000);

                    Console.WriteLine("Leaving the lock of WriteCityRevenueFile");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Raised: " + ex.Message);
            }

            Console.WriteLine("Leaving Function WriteCityRevenueFile");

        }
    }
}
