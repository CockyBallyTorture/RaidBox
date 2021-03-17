using Konsole;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using WebSocket4Net;
using static System.ConsoleColor;

namespace RaidBoxConsoleGui
{
    class Program
    {

        public static string Message;

        private static readonly Window window = new(89, 49);
        
        static void WS(string Message, IConsole status)
        {


            string token = System.IO.File.ReadAllText("token.txt");

            WebSocket client = new("wss://zoom.raidforums.com/socket.io/?token=" + token + "&EIO=3&transport=websocket");
            Thread TI = new(() => TextInput(Message, status, client));
            TI.Start();

            client.Closed += (s, ee) => ServerDisconnected(s, ee, status);
            client.MessageReceived += (s, ee) => MessageReceived(s, ee, client, status, Message);

            static void PingerTimer(WebSocket client)
            {
                System.Timers.Timer aTimer = new()
                {
                    // Hook up the Elapsed event for the timer. 
                    Interval = 25000,
                    AutoReset = true,
                    Enabled = true

                };
                aTimer.Elapsed += (s, ee) => Pinger(s, ee, client);
            }
            static void Pinger(Object source, System.Timers.ElapsedEventArgs e, WebSocket client)
            {
                client.Send("2");
            }
            Thread PingShit = new(() => PingerTimer(client));
            client.Opened += (s, ee) => ServerConnected(s, ee, status, PingShit);


            //<i class=\"rf_i rf_god\"></i><span class=\"rf_godx\" style=\"color: #4b019a;\"> uhuhhq</span>"
            static void MessageReceived(object sender, MessageReceivedEventArgs args, WebSocket client, IConsole status, string message1)
            {


                if (args.Message == "40")
                {
                    status.WriteLine("sending : " + "40/member");
                    client.Send("40/member");


                }
                if (args.Message == "42/member,[\u0022ckusr\u0022,\u0022ok\u0022]")
                {
                    status.WriteLine("sending : " + "42/member,0[\u0022add user\u0022]");
                    client.Send("42/member,0[\u0022add user\u0022]");

                }
                if (args.Message.Contains("[\u0022login\u0022,{\u0022usernames\u0022:"))
                {
                    status.WriteLine("sending : " + "42/member,[\u0022getoldmsg\u0022,{\u0022ns\u0022:\u002250\u0022}]");
                    client.Send("42/member,[\u0022getoldmsg\u0022,{\u0022ns\u0022:\u002250\u0022}]");

                }
                if (args.Message == "3")
                {
                    
                }
                if (args.Message.Contains("42/member,[\u0022load old msgs\u0022,"))
                {
                    try
                    {
                        for (int i = 50; i > 0; i--)
                        {
                            string[] MessageSendera1 = args.Message.Split("\u0022,\u0022nick\u0022:\u0022");

                            string[] MessageRFa1 = args.Message.Split("\u0022msg\u0022:\u0022");
                            string[] MessageContenta = MessageRFa1[i].Split("\u0022,\u0022nickto");
                            if (MessageSendera1[i].Contains("rf_godx rf_sparkles"))
                            {
                                string[] RfPart1 = MessageSendera1[i].Split("</span>");
                                string[] RfPart2 = RfPart1[0].Split("\\\u0022>");
                                status.WriteLine("[#RF] " + RfPart2[2] + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[i].Contains("rf_elite"))
                            {
                                string[] RfElite1 = MessageSendera1[i].Split("</span>");
                                string[] RfElite2 = RfElite1[0].Split("\\\u0022rf_elite\\\u0022>");
                                status.WriteLine("[#RF] " + RfElite2[1] + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[i].Contains("rf_member"))
                            {
                                string[] Rfmember1 = MessageSendera1[i].Split("</span>");
                                string[] Rfmember2 = Rfmember1[0].Split("\\\u0022rf_member\\\u0022>");
                                status.WriteLine("[#RF] " + Rfmember2[1] + " : " + MessageContenta[0]);
                                //<span class=\"rf_member\">skullandpwnz</span>"
                            }
                            else if (MessageSendera1[i].Contains("rf_godx"))
                            {
                                string[] Rfgodx1 = MessageSendera1[i].Split("</span>");
                                string[] Rfgodx2 = Rfgodx1[0].Split("\\\u0022>");
                                status.WriteLine("[#RF] " + Rfgodx2[2] + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[i].Contains("rf_noob"))
                            {
                                string[] RfNoob1 = MessageSendera1[i].Split("</span>");
                                string[] RfNoob2 = RfNoob1[0].Split("\\\u0022rf_noob\\\u0022>");
                                status.WriteLine("[#RF] " + RfNoob2[1] + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[i].Contains("rf_leet"))
                            {
                                //<span class=\"rf_leet\">Mannix</span>"
                                string[] RfLeet1 = MessageSendera1[i].Split("</span>");
                                string[] RfLeet2 = RfLeet1[0].Split("\\\u0022rf_leet\\\u0022>");
                                status.WriteLine("[#RF] " + RfLeet2[1] + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[i].Contains("\\\u0022>rf_i rf_mvp\\\u0022>"))
                            {
                                string[] RfMvp1 = MessageSendera1[i].Split("</span>");
                                string[] RfMvp2 = RfMvp1[0].Split("\\\u0022rf_i rf_mvp\\\u0022>");

                                status.WriteLine("[#RF] " + RfMvp2[1] + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[i].Contains("\\\u0022rf_i rf_vip\\\u0022"))
                            {
                                string[] RfVip1 = MessageSendera1[i].Split("</span>");
                                string[] RfVip2 = RfVip1[0].Split("\\\u0022rf_i rf_vip\\\u0022>");
                                status.WriteLine("[#RF] " + RfVip2[1] + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[i].Contains("\\\u0022rf_owner\\\u0022"))
                            {
                                string[] RfOwner1 = MessageSendera1[i].Split("<b>");
                                string[] RfOwner2 = RfOwner1[1].Split("</b>");
                                status.WriteLine("[#RF] " + RfOwner2[0] + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[i].Contains("\\\u0022rf_mod\\\u0022"))
                            {
                                string[] Rfmod1 = MessageSendera1[i].Split("</span>");
                                string[] Rfmod2 = Rfmod1[0].Split("\\\u0022rf_mod\\\u0022>");
                                status.WriteLine("[#RF] " + Rfmod2[1] + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[i].Contains("\\\u0022rf_i rf_god\\\u0022"))
                            {
                                if (MessageSendera1[i].Contains("title=\\\u0022pacino\\\u0022"))
                                {
                                    status.WriteLine("[#RF] " + "Pacino" + " : " + MessageContenta[0]);
                                }
                                else if (MessageSendera1[i].Contains("title=\\\u0022Event Horizon\\\u0022/>"))
                                {
                                    status.WriteLine("[#RF] " + "Event Horizon" + " : " + MessageContenta[0]);
                                }
                                else if (MessageSendera1[i].Contains("<b>"))
                                {
                                    string[] RfBshit1 = MessageSendera1[i].Split("<b>");
                                    string[] RfBshit2 = RfBshit1[1].Split("</b>");
                                    status.WriteLine("[#RF] " + RfBshit2[0] + " : " + MessageContenta[0]);
                                }
                                else if (MessageSendera1[i].Contains("</span>\u0022,\u0022edt"))
                                {
                                    string[] RfGod1 = MessageSendera1[i].Split("</span>");
                                    string[] RfGod2 = RfGod1[0].Split("\\\u0022>");
                                    status.WriteLine("[#RF] " + RfGod2[1] + " : " + MessageContenta[0]);
                                }

                            }
                        }
                    }
                    catch
                    {

                    }

                }
                try
                {
                    if (args.Message.Contains("[\u0022message\u0022,{\u0022_id\u0022:"))
                    {
                        //<i class=\"rf_i rf_god\"></i><span class=\"rf_godx rf_sparkles\" style=\"color:#0││0b8ae;\">Tony Montana</span>","edt":"0","edtusr":"0","type":"shout","created":"2021-03-││16T22: 20:11.914Z","__v":0}]
                        //< i class=\"rf_m\"></i><span class=\"rf_mod\">Burpingjimmy_Bot</span>"
                        //│<i class=\"rf_a\"></i><span class=\"rf_owner\" style=\"color: #ecf0f1; text-shadow: 2px│2px 6px #b30000;\"<b>moot : Bro lowkey
                        //<i class=\"rf_i rf_god\"></i><span class=\"rf_godx\" style=\"color: #8AACB8;\"> verking│</ span > ","edt":"0","edtusr":"0","type":"shout","created":"2021 - 03 - 16T17: 33:30.815Z","__│v":0}]
                        //42/member,["message",{"_id":"6050eb1fc593d2001577ba6a","msg":"our federal agents are perplexed","nickto":"0","uid":"121875619","gid":"129","colorsht":"","bold":"NaN","font":"NaN","size":"NaN","avatar":"./uploads/avatars/avatar_121875619.png?dateline=1593489879","uidto":"0","suid":"121875619,0","nick":"<i class=\"rf_i rf_god\"></i><span class=\"rf_godx\" style=\"color: #671d9d;\"> Scientia</span>","edt":"0","edtusr":"0","type":"shout","created":"2021-03-16T17:30:07.693Z","__v":0}]
                        //42/member,["message",{"_id":"6050e4bdc593d2001577b9b2","msg":"cbt","nickto":"0","uid":"121570296","gid":"129","colorsht":"","bold":"NaN","font":"NaN","size":"NaN","avatar":"./uploads/avatars/avatar_121570296.png?dateline=1605552198","uidto":"0","suid":"121570296,0","nick":"<i class=\"rf_i rf_god\"></i><img class=\"rf_img\" src=\"//cdn.raidforums.com/i/RF_ZI5U.gif\" title=\"pacino\"/>","edt":"0","edtusr":"0","type":"shout","created":"2021-03-16T17:02:53.153Z","__v":0}]
                        //42/member,["message",{"_id":"6050d611c593d2001577b8f5","msg":"Prob his self","nickto":"0","uid":"122012173","gid":"2","colorsht":"","bold":"NaN","font":"NaN","size":"NaN","avatar":"","uidto":"0","suid":"122012173,0","nick":"<span class=\"rf_noob\">Ammi</span>","edt":"0","edtusr":"0","type":"shout","created":"2021-03-16T16:00:17.827Z","__v":0}]
                        //42/member,["message",{"_id":"6050d36fc593d2001577b8ca","msg":"sup fatties","nickto":"0","uid":"121421457","gid":"129","colorsht":"","bold":"NaN","font":"NaN","size":"NaN","avatar":"./uploads/avatars/avatar_121421457.png?dateline=1614648071","uidto":"0","suid":"121421457,0","nick":"<span class=\"rf_i rf_god\">grangus</span>","edt":"0","edtusr":"0","type":"shout","created":"2021-03-16T15:49:03.359Z","__v":0}]
                        //42/member,["message",{"_id":"6050ca27c593d2001577b7d8","msg":"Php is super super easy lol ","nickto":"0","uid":"121928153","gid":"129","colorsht":"","bold":"NaN","font":"NaN","size":"NaN","avatar":"./uploads/avatars/avatar_121928153.gif?dateline=1609224451","uidto":"0","suid":"121928153,0","nick":"<i class=\"rf_i rf_god\"></i><span style=\"color: #ffd1dc; text-shadow: 2px 2px 5px #000000;\"><b>pompompurin</b></span>","edt":"0","edtusr":"0","type":"shout","created":"2021-03-16T15:09:27.554Z","__v":0}]
                        string[] MessageSendera1 = args.Message.Split("\u0022,\u0022nick\u0022:\u0022");

                        string[] MessageRFa1 = args.Message.Split("\u0022msg\u0022:\u0022");
                        string[] MessageContenta = MessageRFa1[1].Split("\u0022,\u0022nickto");
                        if (MessageSendera1[1].Contains("rf_godx rf_sparkles"))
                        {
                            string[] RfPart1 = MessageSendera1[1].Split("</span>");
                            string[] RfPart2 = RfPart1[0].Split("\\\u0022>");
                            status.WriteLine("[#RF] " + RfPart2[2] + " : " + MessageContenta[0]);
                        }
                        else if (MessageSendera1[1].Contains("rf_elite"))
                        {
                            string[] RfElite1 = MessageSendera1[1].Split("</span>");
                            string[] RfElite2 = RfElite1[0].Split("\\\u0022rf_elite\\\u0022>");
                            status.WriteLine("[#RF] " + RfElite2[1] + " : " + MessageContenta[0]);
                        }
                        else if (MessageSendera1[1].Contains("rf_member"))
                        {
                            string[] Rfmember1 = MessageSendera1[1].Split("</span>");
                            string[] Rfmember2 = Rfmember1[0].Split("\\\u0022rf_member\\\u0022>");
                            status.WriteLine("[#RF] " + Rfmember2[1] + " : " + MessageContenta[0]);
                            //<span class=\"rf_member\">skullandpwnz</span>"
                        }
                        else if (MessageSendera1[1].Contains("rf_godx"))
                        {
                            string[] Rfgodx1 = MessageSendera1[1].Split("</span>");
                            string[] Rfgodx2 = Rfgodx1[0].Split("\\\u0022>");
                            status.WriteLine("[#RF] " + Rfgodx2[2] + " : " + MessageContenta[0]);
                        }
                        else if (MessageSendera1[1].Contains("rf_noob"))
                        {
                            string[] RfNoob1 = MessageSendera1[1].Split("</span>");
                            string[] RfNoob2 = RfNoob1[0].Split("\\\u0022rf_noob\\\u0022>");
                            status.WriteLine("[#RF] " + RfNoob2[1] + " : " + MessageContenta[0]);
                        }
                        else if (MessageSendera1[1].Contains("rf_leet"))
                        {
                            //<span class=\"rf_leet\">Mannix</span>"
                            string[] RfLeet1 = MessageSendera1[1].Split("</span>");
                            string[] RfLeet2 = RfLeet1[0].Split("\\\u0022rf_leet\\\u0022>");
                            status.WriteLine("[#RF] " + RfLeet2[1] + " : " + MessageContenta[0]);
                        }
                        else if (MessageSendera1[1].Contains("rf_i rf_mvp"))
                        {
                            string[] RfMvp1 = MessageSendera1[1].Split("</span>");
                            string[] RfMvp2 = RfMvp1[0].Split("\\\u0022rf_i rf_mvp\\\u0022>");

                            status.WriteLine("[#RF] " + RfMvp2[1] + " : " + MessageContenta[0]);
                        }
                        else if (MessageSendera1[1].Contains("\\\u0022rf_i rf_vip\\\u0022"))
                        {
                            string[] RfVip1 = MessageSendera1[1].Split("</span>");
                            string[] RfVip2 = RfVip1[0].Split("\\\u0022rf_i rf_vip\\\u0022>");
                            status.WriteLine("[#RF] " + RfVip2[1] + " : " + MessageContenta[0]);
                        }
                        else if (MessageSendera1[1].Contains("\\\u0022rf_owner\\\u0022"))
                        {
                            string[] RfOwner1 = MessageSendera1[1].Split("<b>");
                            string[] RfOwner2 = RfOwner1[1].Split("</b>");
                            status.WriteLine("[#RF] " + RfOwner2[0] + " : " + MessageContenta[0]);
                        }
                        else if (MessageSendera1[1].Contains("\\\u0022rf_mod\\\u0022"))
                        {
                            string[] Rfmod1 = MessageSendera1[1].Split("</span>");
                            string[] Rfmod2 = Rfmod1[0].Split("\\\u0022rf_mod\\\u0022>");
                            status.WriteLine("[#RF] " + Rfmod2[1] + " : " + MessageContenta[0]);
                        }
                        else if (MessageSendera1[1].Contains("\\\u0022rf_i rf_god\\\u0022"))
                        {
                            if (MessageSendera1[1].Contains("title=\\\u0022pacino\\\u0022"))
                            {
                                status.WriteLine("[#RF] " + "Pacino" + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[1].Contains("title=\\\u0022Event Horizon\\\u0022/>"))
                            {
                                status.WriteLine("[#RF] " + "Event Horizon" + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[1].Contains("<b>"))
                            {
                                string[] RfBshit1 = MessageSendera1[1].Split("<b>");
                                string[] RfBshit2 = RfBshit1[1].Split("</b>");
                                status.WriteLine("[#RF] " + RfBshit2[0] + " : " + MessageContenta[0]);
                            }
                            else if (MessageSendera1[1].Contains("</span>\u0022,\u0022edt"))
                            {
                                string[] RfGod1 = MessageSendera1[1].Split("</span>");
                                string[] RfGod2 = RfGod1[0].Split("\\\u0022>");
                                status.WriteLine("[#RF] " + RfGod2[1] + " : " + MessageContenta[0]);
                            }

                        }

                    }
                }
                catch
                {

                }
                //if (args.Message.Contains("\u0022type\u0022:\u0022pmshout\u0022"))
                //{
//
                //}
                //For later pms and shit
                //42/member,["message",{"msg":"pm test ","nickto":"<span class=\"rf_i rf_god\">LulzSecurity</span>","colorsht":"","font":"","size":"","bold":"","uidto":121439412,"type":"pmshout"}]
                //42/member,["message",{"_id":"60513a73c593d2001577c2f9","msg":"hi cbt","nickto":"<span class=\"rf_i rf_mvp\">CBT</span>","uid":"121439412","gid":"129","colorsht":"","bold":"NaN","font":"NaN","size":"NaN","avatar":"./uploads/avatars/avatar_121439412.jpg?dateline=1532486193","uidto":"121924816","suid":"121439412,121924816","nick":"<span class=\"rf_i rf_god\">LulzSecurity</span>","edt":"0","edtusr":"0","type":"pmshout","created":"2021-03-16T23:08:35.847Z","__v":0}]
            }
            static void ServerConnected(object sender, EventArgs args, IConsole status, Thread PingShit)
            {
                status.WriteLine(Green, "Server connected");
                PingShit.Start();

            }
            static void ServerDisconnected(object sender, EventArgs args, IConsole status)
            {
                status.WriteLine(Red, "Server disconnected");

            }
            static void TextInput(string Message, IConsole status, WebSocket client)
            {



                while (true)
                {

                    Console.SetCursorPosition(1, 47);
                    Console.WriteLine("Say : ");
                    Console.SetCursorPosition(7, 47);
                    Message = Console.ReadLine();
                    Console.SetCursorPosition(7, 47);
                    for (int i = 7; i <= 7 + Message.Length; i++)
                    {
                        Console.Write(" ");
                    }

                    if (Message == "/help")
                    {
                        status.WriteLine(White, Message);
                        status.WriteLine(Yellow, "/quit = Exits the Programm Safely." + Environment.NewLine + "/pm Username = to private Message someone (Todo)");
                    }
                    else if (Message == "/Help")
                    {
                        status.WriteLine(White, Message);
                        status.WriteLine(Yellow, "/quit = Exits the Programm Safely." + Environment.NewLine + "/pm Username = to private Message someone (Todo)");
                    }
                    else if (Message == "/HELP")
                    {
                        status.WriteLine(White, Message);
                        status.WriteLine(Yellow, "/quit = Exits the Programm Safely." + Environment.NewLine + "/pm Username = to private Message someone (Todo)");
                    }
                    else if (Message == "/h")
                    {
                        status.WriteLine(White, Message);
                        status.WriteLine(Yellow, "/quit = Exits the Programm Safely." + Environment.NewLine + "/pm Username = to private Message someone (Todo)");
                    }
                    else if (Message == "/H")
                    {
                        status.WriteLine(White, Message);
                        status.WriteLine(Yellow, "/quit = Exits the Programm Safely." + Environment.NewLine + "/pm Username = to private Message someone (Todo)");
                    }
                    else if (Message == "/quit")
                    {
                        status.WriteLine(White, Message);
                        System.Environment.Exit(1);
                    }
                    else if (Message == "/Quit")
                    {
                        status.WriteLine(White, Message);
                        System.Environment.Exit(1);
                    }
                    else if (Message == "/QUIT")
                    {
                        status.WriteLine(White, Message);
                        System.Environment.Exit(1);
                    }
                    else if (Message == "/q")
                    {
                        status.WriteLine(White, Message);
                        System.Environment.Exit(1);
                    }
                    else if (Message == "/Q")
                    {
                        status.WriteLine(White, Message);
                        System.Environment.Exit(1);
                    }
                    else if (Message == "/Connect")
                    {
                        status.WriteLine(White, Message);
                        try
                        {
                            client.OpenAsync();
                        }
                        catch
                        {
                            client.OpenAsync();
                        }
                    }
                    else if (Message == "/connect")
                    {
                        status.WriteLine(White, Message);
                        try
                        {
                            client.OpenAsync();
                        }
                        catch
                        {
                            client.OpenAsync();
                        }
                    }
                    else if (Message == "/CONNECT")
                    {
                        status.WriteLine(White, Message);
                        try
                        {
                            client.OpenAsync();
                        }
                        catch
                        {
                            client.OpenAsync();
                        }
                    }
                    else
                    {

                        client.Send("42/member,[\u0022message\u0022,{\u0022msg\u0022:\u0022" + Message.ToString() + "\u0022,\u0022nickto\u0022:\u00220\u0022, \u0022uidto\u0022:0,\u0022colorsht\u0022:\u0022\u0022,\u0022font\u0022:\u0022\u0022,\u0022size\u0022:\u0022\u0022,\u0022bold\u0022:\u0022\u0022,\u0022type\u0022:\u0022shout\u0022}]");
                    }
                }


            }
        }
        public static void TextInput(IConsole status)
        {

            Thread wsh = new(() =>
            {
                WS(Message, status);
            });

            while (true)
            {

                Console.SetCursorPosition(1, 47);
                Console.WriteLine("Say : ");
                Console.SetCursorPosition(7, 47);
                Message = Console.ReadLine().ToString();
                Console.SetCursorPosition(7, 47);
                for (int i = 7; i <= 7 + Message.Length; i++)
                {
                    Console.Write(" ");
                }

                if (Message == "/help")
                {
                    status.WriteLine(White, Message);
                    status.WriteLine(Yellow, "/quit = Exits the Programm Safely." + Environment.NewLine + "/pm Username = to private Message someone (Todo)");
                }
                else if (Message == "/Help")
                {
                    status.WriteLine(White, Message);
                    status.WriteLine(Yellow, "/quit = Exits the Programm Safely." + Environment.NewLine + "/pm Username = to private Message someone (Todo)");
                }
                else if (Message == "/HELP")
                {
                    status.WriteLine(White, Message);
                    status.WriteLine(Yellow, "/quit = Exits the Programm Safely." + Environment.NewLine + "/pm Username = to private Message someone (Todo)");
                }
                else if (Message == "/h")
                {
                    status.WriteLine(White, Message);
                    status.WriteLine(Yellow, "/quit = Exits the Programm Safely." + Environment.NewLine + "/pm Username = to private Message someone (Todo)");
                }
                else if (Message == "/H")
                {
                    status.WriteLine(White, Message);
                    status.WriteLine(Yellow, "/quit = Exits the Programm Safely." + Environment.NewLine + "/pm Username = to private Message someone (Todo)");
                }
                else if (Message == "/quit")
                {
                    status.WriteLine(White, Message);
                    System.Environment.Exit(1);
                }
                else if (Message == "/Quit")
                {
                    status.WriteLine(White, Message);
                    System.Environment.Exit(1);
                }
                else if (Message == "/QUIT")
                {
                    status.WriteLine(White, Message);
                    System.Environment.Exit(1);
                }
                else if (Message == "/q")
                {
                    status.WriteLine(White, Message);
                    System.Environment.Exit(1);
                }
                else if (Message == "/Q")
                {
                    status.WriteLine(White, Message);
                    System.Environment.Exit(1);
                }
                else if (Message == "/Connect")
                {
                    status.WriteLine(White, Message);
                    try
                    {
                        wsh.Start();
                    }
                    catch
                    {
                        wsh.Start();
                    }
                }
                else if (Message == "/connect")
                {
                    status.WriteLine(White, Message);
                    try
                    {
                        wsh.Start();
                    }
                    catch
                    {
                        wsh.Start();
                    }
                }
                else if (Message == "/CONNECT")
                {
                    status.WriteLine(White, Message);
                    try
                    {
                        wsh.Start();
                    }
                    catch
                    {
                        wsh.Start();
                    }
                }
                else
                {

                    //client.Send("42/member,[\u0022message\u0022,{\u0022msg\u0022:\u0022"+ Message.ToString() +"\u0022,\u0022nickto\u0022:\u00220\u0022, \u0022uidto\u0022:0,\u0022colorsht\u0022:\u0022\u0022,\u0022font\u0022:\u0022\u0022,\u0022size\u0022:\u0022\u0022,\u0022bold\u0022:\u0022\u0022,\u0022type\u0022:\u0022shout\u0022}]");
                }
            }


        }


        static void Titlebar()
        {
            string IpUrl = "https://ipv4.apimon.de";

            try
            {
                var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })
                {
                    BaseAddress = new Uri(IpUrl)
                };
                HttpResponseMessage response = client.GetAsync(IpUrl).Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;

                Console.Title = "RaidBox V.0.2" + " Public ipv4 : " + result;
            }
            catch
            {
                Console.Title = "RaidBox V.0.2" + " Public ipv4 : " + "Check Your Connection";
            }
        }
        //maybe later
        //private static void AddText(FileStream fs, string value)
        //{
        //    byte[] info = new UTF8Encoding(true).GetBytes(value);
        //    fs.Write(info, 0, info.Length);
        //}
        static void Main()
        {

            Thread TB = new(Titlebar);
            TB.Start();

            var console = new ConcurrentWriter();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.SetWindowSize(90, 50);
                Console.SetBufferSize(90, 50);
            }
            IConsole Shoutbox = window.OpenBox("RaidForums Shoutbox", 89, 46);
            IConsole PM = window.OpenBox("", 89, 3);
            Shoutbox.WriteLine(Blue, "Made With Love By CBT");
            Shoutbox.WriteLine(Blue, "TODO : make Receiving PMs less Buggy = Improve the Filter make it look into more Details, if some names are Buggy just pm me anywhere youd like, 2. (Somewhere in the Future) Rewrite the Engine to Accept Images,");
            Shoutbox.WriteLine(Yellow, "Create a text file named \u0022token.txt\u0022 in the same Directory as this Application");
            Shoutbox.WriteLine(Yellow, "Enter your Shoutbox token in \u0022token.txt\u0022");
            Shoutbox.WriteLine(Yellow, "/h,/help,/Help,/HELP for available Commands");
            

            Thread WSH = new(() => WS(Message, Shoutbox));
            WSH.Start();
            while (true)
            {
            }
        }
    }
}
