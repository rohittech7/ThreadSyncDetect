using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSyncDetect
{
    class MultiCoreProcessing
    {
        private static List<string> clientMarketList = null;

        public static void ReadFileDataToList()
        {
            clientMarketList = new List<string>();
            string file = @"H:\HBI\BingIntSearch\Analysis_Data\Bing for Partners\Test\Mar-Dec16-ClientID.txt";

            using (StreamReader reader = new StreamReader(file))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        clientMarketList.Add(line);
                    }
                }
            }
        }

        public static void FindNewUsersForMarketForMonth(string market)
        {
            FindNewUsersForMarketForMonth(3, market);
            FindNewUsersForMarketForMonth(4, market);
            FindNewUsersForMarketForMonth(5, market);
            FindNewUsersForMarketForMonth(6, market);
            FindNewUsersForMarketForMonth(7, market);
            FindNewUsersForMarketForMonth(8, market);
            FindNewUsersForMarketForMonth(9, market);
            FindNewUsersForMarketForMonth(10, market);
            FindNewUsersForMarketForMonth(11, market);
            FindNewUsersForMarketForMonth(12, market);

        }

        public static void FindNewUsersForMarketForMonthList(string market)
        {
            FindNewUsersForMarketForMonthList(3, market);
            FindNewUsersForMarketForMonthList(4, market);
            FindNewUsersForMarketForMonthList(5, market);
            FindNewUsersForMarketForMonthList(6, market);
            FindNewUsersForMarketForMonthList(7, market);
            FindNewUsersForMarketForMonthList(8, market);
            FindNewUsersForMarketForMonthList(9, market);
            FindNewUsersForMarketForMonthList(10, market);
            FindNewUsersForMarketForMonthList(11, market);
            FindNewUsersForMarketForMonthList(12, market);

        }

        static void FindNewUsersForMarketForMonthList(int currentMonth, string market)
        {
            Dictionary<string, int> clientIDBeforeDict = new Dictionary<string, int>();
            Dictionary<string, int> clientIDNewDict = new Dictionary<string, int>();

            int month = 0;
            foreach (string line in clientMarketList)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] bfpData = line.Split('\t');

                    if (!bfpData[2].Equals(market))
                        continue;

                    month = Convert.ToInt32(bfpData[1].Substring(0, bfpData[1].IndexOf('/')));
                    if (month < currentMonth)
                    {
                        if (!clientIDBeforeDict.ContainsKey(bfpData[0]))
                        {
                            clientIDBeforeDict.Add(bfpData[0], 1);

                        }
                    }
                    else if (month == currentMonth)
                    {
                        if (!clientIDNewDict.ContainsKey(bfpData[0]))
                        {
                            clientIDNewDict.Add(bfpData[0], 1);
                        }
                    }
                }
            }

            int newUserCount = 0;
            foreach (KeyValuePair<string, int> item in clientIDNewDict)
            {
                if (!clientIDBeforeDict.ContainsKey(item.Key))
                {
                    newUserCount++;
                }
            }

            Console.WriteLine(market + "#" + GetMonth(currentMonth) + "#" + clientIDNewDict.Count + "#" + newUserCount);
        }


        static void FindNewUsersForMarketForMonth(int currentMonth, string market)
        {
            Dictionary<string, int> clientIDBeforeDict = new Dictionary<string, int>();
            Dictionary<string, int> clientIDNewDict = new Dictionary<string, int>();

            string file = @"H:\HBI\BingIntSearch\Analysis_Data\Bing for Partners\Test\Mar-Dec16-ClientID.txt";


            int month = 0;
            using (StreamReader reader = new StreamReader(file))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] bfpData = line.Split('\t');

                        if (!bfpData[2].Equals(market))
                            continue;

                        month = Convert.ToInt32(bfpData[1].Substring(0, bfpData[1].IndexOf('/')));
                        if (month < currentMonth)
                        {
                            if (!clientIDBeforeDict.ContainsKey(bfpData[0]))
                            {
                                clientIDBeforeDict.Add(bfpData[0], 1);

                            }
                        }
                        else if (month == currentMonth)
                        {
                            if (!clientIDNewDict.ContainsKey(bfpData[0]))
                            {
                                clientIDNewDict.Add(bfpData[0], 1);
                            }
                        }
                    }
                }
            }

            int newUserCount = 0;
            foreach (KeyValuePair<string, int> item in clientIDNewDict)
            {
                if (!clientIDBeforeDict.ContainsKey(item.Key))
                {
                    //Console.WriteLine(item.Key);
                    newUserCount++;
                }
            }

            //Console.WriteLine("Market : " + market);
            //Console.WriteLine("Month : " + currentMonth);
            //Console.WriteLine("Month Unique Users: " + clientIDNewDict.Count);
            //Console.WriteLine("Month New Users: " + newUserCount);
            Console.WriteLine(market + "#" + GetMonth(currentMonth) + "#" + clientIDNewDict.Count + "#" + newUserCount);

            /*
            string outFile = @"H:\HBI\BingIntSearch\Data_Mining\Tasks\Query Groups\Top 20K - fr-FR - Jul16-Aggregate.tsv";
            using (StreamWriter writer = new StreamWriter(outFile))
            {
                foreach (KeyValuePair<string, QueryData> item in queryURLDict)
                {
                    writer.WriteLine(item.Value.query + "\t" + item.Key + "\t" + item.Value.dsqValue + "\t" + item.Value.Category);
                }
            }
             * */
        }

        public static string GetMonth(int number)
        {
            switch (number)
            {
                case 3: return "Mar";
                case 4: return "Apr";
                case 5: return "May";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Aug";
                case 9: return "Sep";
                case 10: return "Oct";
                case 11: return "Nov";
                case 12: return "Dec";
            }

            return "";
        }
    }
}
