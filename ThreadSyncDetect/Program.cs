using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSyncDetect
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Processing");

            Console.WriteLine("MAIN ThreadId = " + AppDomain.GetCurrentThreadId());

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //StartFileOperations();

            //StartDeadlockOperation();

            SingleCoreOperation();

            //StartMultiCoreOperation();

            //StartMultiCoreOperationDataShare();

            //sw.Stop();
            //Console.WriteLine("Total time elapsed: " + (sw.ElapsedMilliseconds / 1000).ToString() + " sec");

            Console.WriteLine("End Processing");

            Console.ReadKey();
        }

        public static void StartFileOperations()
        {
            try
            {
                ThreadStart readThrdRef = new ThreadStart(SalesFileOperation.ReadCityRevenueFile);
                Thread readThread = new Thread(readThrdRef);

                ThreadStart writeThreadRef = new ThreadStart(SalesFileOperation.WriteCityRevenueFile);
                Thread writeThread = new Thread(writeThreadRef);

                readThread.Start();
                writeThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Raised: " + ex.Message);
            }
        }


        public static void StartDeadlockOperation()
        {
            try
            {
                ThreadStart readThrdRef = new ThreadStart(ThreadDeadlock.ProcessCityRevenueData);
                Thread readThread = new Thread(readThrdRef);

                ThreadStart writeThreadRef = new ThreadStart(ThreadDeadlock.ProcessMonthRevenueData);
                Thread writeThread = new Thread(writeThreadRef);

                readThread.Start();
                writeThread.Start();

                Console.WriteLine("Both the Threads Started");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Raised: " + ex.Message);
            }
        }

        public static void SingleCoreOperation()
        {

            try
            {
                MultiCoreProcessing.FindNewUsersForMarketForMonth("en-GB");
                MultiCoreProcessing.FindNewUsersForMarketForMonth("en-AU");
                MultiCoreProcessing.FindNewUsersForMarketForMonth("en-CA");
                MultiCoreProcessing.FindNewUsersForMarketForMonth("de-DE");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Raised: " + ex.Message);
            }

        }

        public static void StartMultiCoreOperation()
        {
            Console.WriteLine("Start Time: " + DateTime.Now.ToLongTimeString());

            try
            {
                Thread enGBThread = new Thread(() => MultiCoreProcessing.FindNewUsersForMarketForMonth("en-GB"));
                Thread enAUThread = new Thread(() => MultiCoreProcessing.FindNewUsersForMarketForMonth("en-AU"));
                Thread enCAThread = new Thread(() => MultiCoreProcessing.FindNewUsersForMarketForMonth("en-CA"));
                Thread deDEThread = new Thread(() => MultiCoreProcessing.FindNewUsersForMarketForMonth("de-DE"));


                enGBThread.Start();
                enAUThread.Start();
                enCAThread.Start();
                deDEThread.Start();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Raised: " + ex.Message);
            }

            Console.WriteLine("End Time: " + DateTime.Now.ToLongTimeString());

        }

        public static void StartMultiCoreOperationDataShare()
        {
            Console.WriteLine("Start Time: " + DateTime.Now.ToLongTimeString());

            try
            {
                MultiCoreProcessing.ReadFileDataToList();

                //ThreadStart enGBThrdRef = new ThreadStart(MultiCoreProcessing.FindNewUsersForMarketForMonth);
                Thread enGBThread = new Thread(() => MultiCoreProcessing.FindNewUsersForMarketForMonthList("en-GB"));
                Thread enAUThread = new Thread(() => MultiCoreProcessing.FindNewUsersForMarketForMonthList("en-AU"));
                Thread enCAThread = new Thread(() => MultiCoreProcessing.FindNewUsersForMarketForMonthList("en-CA"));
                Thread deDEThread = new Thread(() => MultiCoreProcessing.FindNewUsersForMarketForMonthList("de-DE"));



                enGBThread.Start();
                enAUThread.Start();
                enCAThread.Start();
                deDEThread.Start();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Raised: " + ex.Message);
            }
            
            Console.WriteLine("End Time: " + DateTime.Now.ToLongTimeString());

        }
    }
}
