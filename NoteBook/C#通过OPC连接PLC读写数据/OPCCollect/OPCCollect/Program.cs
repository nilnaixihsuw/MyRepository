using opc;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OPCCollect
{
    class Program
    {
        static void Main(string[] args)
        {
            Util.InitDatabase();
            opcCollect.AddGroups();
            opcCollect.AddItems();
            Console.WriteLine("数据采集中...");
            //while (true)
            {
                opcCollect.group1.SyncRead(1, 78, opcCollect.group1.GetServerhandles(opcCollect.items1), out Array result1, out Array errors1, out object qualities1, out object timeStamps1);
                for(int i=1;i<=78;i++)
                {
                    Console.WriteLine(result1.GetValue(i)+" "+qualities1+" "+timeStamps1);
                }
                opcCollect.group2.SyncRead(1, 78, opcCollect.group2.GetServerhandles(opcCollect.items2), out Array result2, out Array errors2, out object qualities2, out object timeStamps2);
                for (int i = 1; i <= 78; i++)
                {
                    Console.WriteLine(result2.GetValue(i) + " "+ qualities2 + " " + timeStamps2);
                }
                opcCollect.group3.SyncRead(1, 78, opcCollect.group3.GetServerhandles(opcCollect.items3), out Array result3, out Array errors3, out object qualities3, out object timeStamps3);
                for (int i = 1; i <= 78; i++)
                {
                    Console.WriteLine(result3.GetValue(i) + " " + qualities3 + " " + timeStamps3);
                }
                opcCollect.group4.SyncRead(1, 78, opcCollect.group4.GetServerhandles(opcCollect.items4), out Array result4, out Array errors4, out object qualities4, out object timeStamps4);
                for (int i = 1; i <= 78; i++)
                {
                    Console.WriteLine(result4.GetValue(i) + " " + qualities4 + " " + timeStamps4);
                }
                opcCollect.group5.SyncRead(1, 78, opcCollect.group5.GetServerhandles(opcCollect.items5), out Array result5, out Array errors5, out object qualities5, out object timeStamps5);
                for (int i = 1; i <= 78; i++)
                {
                    Console.WriteLine(result5.GetValue(i) + " " + qualities5 + " " + timeStamps5);
                }
                opcCollect.group6.SyncRead(1, 78, opcCollect.group6.GetServerhandles(opcCollect.items6), out Array result6, out Array errors6, out object qualities6, out object timeStamps6);
                for (int i = 1; i <= 78; i++)
                {
                    Console.WriteLine(result6.GetValue(i) + " " + qualities6 + " " + timeStamps6);
                }
                opcCollect.group7.SyncRead(1, 30, opcCollect.group7.GetServerhandles(opcCollect.items7), out Array result7, out Array errors7, out object qualities7, out object timeStamps7);
                for (int i = 1; i <= 30; i++)
                {
                    Console.WriteLine(result7.GetValue(i) + " " + qualities7 + " " + timeStamps7);
                }
                opcCollect.group8.SyncRead(1, 30, opcCollect.group8.GetServerhandles(opcCollect.items8), out Array result8, out Array errors8, out object qualities8, out object timeStamps8);
                for (int i = 1; i <= 30; i++)
                {
                    Console.WriteLine(result8.GetValue(i) + " " + qualities8 + " " + timeStamps8);
                }
                opcCollect.group9.SyncRead(1, 30, opcCollect.group9.GetServerhandles(opcCollect.items9), out Array result9, out Array errors9, out object qualities9, out object timeStamps9);
                for (int i = 1; i <= 30; i++)
                {
                    Console.WriteLine(result9.GetValue(i) + " " + qualities9 + " " + timeStamps9);
                }
                opcCollect.group10.SyncRead(1, 30, opcCollect.group10.GetServerhandles(opcCollect.items10), out Array result10, out Array errors10, out object qualities10, out object timeStamps10);
                for (int i = 1; i <= 30; i++)
                {
                    Console.WriteLine(result10.GetValue(i) + " " + qualities10 + " " + timeStamps10);
                }
                opcCollect.group11.SyncRead(1, 30, opcCollect.group11.GetServerhandles(opcCollect.items11), out Array result11, out Array errors11, out object qualities11, out object timeStamps11);
                for (int i = 1; i <= 30; i++)
                {
                    Console.WriteLine(result11.GetValue(i) + " " + qualities11 + " " + timeStamps11);
                }
                opcCollect.group12.SyncRead(1, 30, opcCollect.group12.GetServerhandles(opcCollect.items12), out Array result12, out Array errors12, out object qualities12, out object timeStamps12);
                for (int i = 1; i <= 30; i++)
                {
                    Console.WriteLine(result12.GetValue(i) + " " + qualities12 + " " + timeStamps12);
                }
                opcCollect.group13.SyncRead(1, 30, opcCollect.group13.GetServerhandles(opcCollect.items13), out Array result13, out Array errors13, out object qualities13, out object timeStamps13);
                for (int i = 1; i <= 30; i++)
                {
                    Console.WriteLine(result13.GetValue(i) + " " + qualities13 + " " + timeStamps13);
                }
                opcCollect.group14.SyncRead(1, 4, opcCollect.group14.GetServerhandles(opcCollect.items14), out Array result14, out Array errors14, out object qualities14, out object timeStamps14);
                for (int i = 1; i <= 4; i++)
                {
                    Console.WriteLine(result14.GetValue(i) + " " + qualities14 + " " + timeStamps14);
                }
                opcCollect.group15.SyncRead(1, 4, opcCollect.group15.GetServerhandles(opcCollect.items15), out Array result15, out Array errors15, out object qualities15, out object timeStamps15);
                for (int i = 1; i <= 4; i++)
                {
                    Console.WriteLine(result15.GetValue(i) + " " + qualities15 + " " + timeStamps15);
                }
                opcCollect.group16.SyncRead(1, 40, opcCollect.group16.GetServerhandles(opcCollect.items16), out Array result16, out Array errors16, out object qualities16, out object timeStamps16);
                for (int i = 1; i <= 40; i++)
                {
                    Console.WriteLine(result16.GetValue(i) + " " + qualities16 + " " + timeStamps16);
                }
                opcCollect.group17.SyncRead(1, 40, opcCollect.group17.GetServerhandles(opcCollect.items17), out Array result17, out Array errors17, out object qualities17, out object timeStamps17);
                for (int i = 1; i <= 40; i++)
                {
                    Console.WriteLine(result17.GetValue(i) + " " + qualities17 + " " + timeStamps17);
                }
                opcCollect.group18.SyncRead(1, 40, opcCollect.group18.GetServerhandles(opcCollect.items18), out Array result18, out Array errors18, out object qualities18, out object timeStamps18);
                for (int i = 1; i <= 40; i++)
                {
                    Console.WriteLine(result18.GetValue(i) + " " + qualities18 + " " + timeStamps18);
                }
                opcCollect.group19.SyncRead(1, 133, opcCollect.group19.GetServerhandles(opcCollect.items19), out Array result19, out Array errors19, out object qualities19, out object timeStamps19);
                for (int i = 1; i <= 133; i++)
                {
                    Console.WriteLine(result19.GetValue(i) + " " + qualities19 + " " + timeStamps19);
                }
                opcCollect.group20.SyncRead(1, 132, opcCollect.group20.GetServerhandles(opcCollect.items20), out Array result20, out Array errors20, out object qualities20, out object timeStamps20);
                for (int i = 1; i <= 132; i++)
                {
                    Console.WriteLine(result20.GetValue(i) + " " + qualities20 + " " + timeStamps20);
                }
                opcCollect.group21.SyncRead(1, 136, opcCollect.group21.GetServerhandles(opcCollect.items21), out Array result21, out Array errors21, out object qualities21, out object timeStamps21);
                for (int i = 1; i <= 136; i++)
                {
                    Console.WriteLine(result21.GetValue(i) + " " + qualities21 + " " + timeStamps21);
                }
                opcCollect.group22.SyncRead(1, 135, opcCollect.group22.GetServerhandles(opcCollect.items22), out Array result22, out Array errors22, out object qualities22, out object timeStamps22);
                for (int i = 1; i <= 135; i++)
                {
                    Console.WriteLine(result22.GetValue(i) + " " + qualities22 + " " + timeStamps22);
                }
                Console.WriteLine("_______________________");
            }
            //Timer timer = new Timer(TimerMethod, null, 0, 1000);
            Console.ReadKey();
        }
        public static int counter = 0;
        public static bool state = false;
        public static OPCCollect opcCollect= new OPCCollect();

        public static void TimerMethod(object o)
        {
            if(state)
            {
                return;
            }
            switch (counter % 22)
            {
                case 0: opcCollect.group1.AsyncRead(78, opcCollect.group1.GetServerhandles(opcCollect.items1), out Array Errors1, 1, out int cancelID1); break;
                case 1: opcCollect.group2.AsyncRead(78, opcCollect.group2.GetServerhandles(opcCollect.items2), out Array Errors2, 1, out int cancelID2); break;
                case 2: opcCollect.group3.AsyncRead(78, opcCollect.group3.GetServerhandles(opcCollect.items3), out Array Errors3, 1, out int cancelID3); break;
                case 3: opcCollect.group4.AsyncRead(78, opcCollect.group4.GetServerhandles(opcCollect.items4), out Array Errors4, 1, out int cancelID4); break;
                case 4: opcCollect.group5.AsyncRead(78, opcCollect.group5.GetServerhandles(opcCollect.items5), out Array Errors5, 1, out int cancelID5); break;
                case 5: opcCollect.group6.AsyncRead(78, opcCollect.group6.GetServerhandles(opcCollect.items6), out Array Errors6, 1, out int cancelID6); break;
                case 6: opcCollect.group7.AsyncRead(30, opcCollect.group7.GetServerhandles(opcCollect.items7), out Array Errors7, 1, out int cancelID7); break;
                case 7: opcCollect.group8.AsyncRead(30, opcCollect.group8.GetServerhandles(opcCollect.items8), out Array Errors8, 1, out int cancelID8); break;
                case 8: opcCollect.group9.AsyncRead(30, opcCollect.group9.GetServerhandles(opcCollect.items9), out Array Errors9, 1, out int cancelID9); break;
                case 9: opcCollect.group10.AsyncRead(30, opcCollect.group10.GetServerhandles(opcCollect.items10), out Array Errors10, 1, out int cancelID10); break;
                case 10: opcCollect.group11.AsyncRead(30, opcCollect.group11.GetServerhandles(opcCollect.items11), out Array Errors11, 1, out int cancelID11); break;
                case 11: opcCollect.group12.AsyncRead(30, opcCollect.group12.GetServerhandles(opcCollect.items12), out Array Errors12, 1, out int cancelID12); break;
                case 12: opcCollect.group13.AsyncRead(30, opcCollect.group13.GetServerhandles(opcCollect.items13), out Array Errors13, 1, out int cancelID13); break;
                case 13: opcCollect.group14.AsyncRead(4, opcCollect.group14.GetServerhandles(opcCollect.items14), out Array Errors14, 1, out int cancelID14); break;
                case 14: opcCollect.group15.AsyncRead(4, opcCollect.group15.GetServerhandles(opcCollect.items15), out Array Errors15, 1, out int cancelID15); break;
                case 15: opcCollect.group16.AsyncRead(40, opcCollect.group16.GetServerhandles(opcCollect.items16), out Array Errors16, 1, out int cancelID16); break;
                case 16: opcCollect.group17.AsyncRead(40, opcCollect.group17.GetServerhandles(opcCollect.items17), out Array Errors17, 1, out int cancelID17); break;
                case 17: opcCollect.group18.AsyncRead(40, opcCollect.group18.GetServerhandles(opcCollect.items18), out Array Errors18, 1, out int cancelID18); break;
                case 18: opcCollect.group19.AsyncRead(133, opcCollect.group19.GetServerhandles(opcCollect.items19), out Array Errors19, 1, out int cancelID19); break;
                case 19: opcCollect.group20.AsyncRead(132, opcCollect.group20.GetServerhandles(opcCollect.items20), out Array Errors20, 1, out int cancelID20); break;
                case 20: opcCollect.group21.AsyncRead(136, opcCollect.group21.GetServerhandles(opcCollect.items21), out Array Errors21, 1, out int cancelID21); break;
                case 21: opcCollect.group22.AsyncRead(135, opcCollect.group22.GetServerhandles(opcCollect.items22), out Array Errors22, 1, out int cancelID22); break;
            }
            //同步读取：opcCollect.group1.SyncRead(1, 78, opcCollect.group1.GetServerhandles(opcCollect.items1), out Array result, out Array errors, out object qualities, out object timeStamps);
            //读取某一项：opcCollect.items1[0].Read(1, out object value, out object Qualities, out object TimeStamps);
        }
    }
}
