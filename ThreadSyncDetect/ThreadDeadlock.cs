using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadSyncDetect
{
    class ThreadDeadlock
    {
        private static Object cityRevenueDataLock = new Object();  
        private static Object monthRevenueDataLock = new Object();  

        public static void ProcessCityRevenueData()
        {
            Console.WriteLine("Entering Function ProcessCityRevenueData");
            Console.WriteLine("ProcessCityRevenueData ThreadId = " + AppDomain.GetCurrentThreadId());

            string [] cities = {"Delhi", "Mumbai", "Calcutta" , "Chennai"};

            try
            {
                lock(cityRevenueDataLock)
                {
                    // Read data from City Revenue File
                    // Do data processing and Calculation

                    foreach(string city in cities)
                    {
                        Console.WriteLine(city);
                        Thread.Sleep(1000);
                    }

                    lock(monthRevenueDataLock)
                    {
                        // Write Data to Month Revenue File
                        Console.WriteLine("ProcessCityRevenueData: Inside the Month Revenue Data Lock");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Raised: " + ex.Message);
            }

            Console.WriteLine("Leaving Function ProcessCityRevenueData");
        }

        public static void ProcessMonthRevenueData()
        {
            Console.WriteLine("Entering Function ProcessMonthRevenueData");
            Console.WriteLine("ProcessMonthRevenueData ThreadId = " + AppDomain.GetCurrentThreadId());

            string [] months = {"Jan", "Feb", "Mar" , "Apr"};

            try
            {
                lock(monthRevenueDataLock)
                {
                    // Read data from Month Revenue File
                    // Do data processing and Calculation

                    foreach(string month in months)
                    {
                        Console.WriteLine(month);
                        Thread.Sleep(1000);
                    }


                    lock(cityRevenueDataLock)
                    {
                        // Write Data to City Revenue File
                        Console.WriteLine("ProcessMonthRevenueData: Inside the City Revenue Data Lock");
                    }
                    System.Threading.Monitor.Exit(cityRevenueDataLock);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Raised: " + ex.Message);
            }

            Console.WriteLine("Leaving Function ProcessMonthRevenueData");
        }
    }
}
