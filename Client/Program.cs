using System;
using System.Threading;
using Nettention.Proud;
using Common;

namespace Client
{
    class Program
    {
        static object g_critSec = new object();
        static NetClient netClient = new NetClient();
        static S2C.Stub S2CStub = new S2C.Stub();
        static C2S.Proxy C2SProxy = new C2S.Proxy();
        static bool isConnected = false;
        static bool keepWorkerThread = true;

        static void InitializeStub()
        {
            S2CStub.NotifyChat = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, String str) =>
            {
                lock (g_critSec)
                {
                    Console.Write("{0}\n", str);
                }

                return true;
            };

        }
        static void InitializeHandler()
        {
            netClient.JoinServerCompleteHandler = (info, replyFromServer) =>
            {
                // as here is running in 2nd thread, lock is needed for console print.
                lock (g_critSec)
                {
                    if (info.errorType == ErrorType.Ok)
                    {
                        Console.Write("Succeed to connect server. Allocated hostID={0}\n", netClient.GetLocalHostID());
                        isConnected = true;
                    }
                    else
                    {
                        // connection failure.
                        Console.Write("Failed to connect server.\n");
                        Console.WriteLine("errorType = {0}, detailType = {1}, comment = {2}", info.errorType, info.detailType, info.comment);
                    }
                }
            };

            // set a routine for network disconnection.
            netClient.LeaveServerHandler = (errorInfo) =>
            {
                // lock is needed as above.
                lock (g_critSec)
                {
                    // show why disconnected.
                    Console.Write("OnLeaveServer: {0}\n", errorInfo.comment);

                    // let main loop exit
                    isConnected = false;
                    keepWorkerThread = false;
                }
            };

            // set a routine for P2P member join (P2P available)
            netClient.P2PMemberJoinHandler = (memberHostID, groupHostID, memberCount, customField) =>
            {
                // lock is needed as above.
                lock (g_critSec)
                {
                    // memberHostID = P2P connected client ID
                    // groupHostID = P2P group ID where the P2P peer is in.
                    Console.Write("[Client] P2P member {0} joined group {1}.\n", memberHostID, groupHostID);

                }
            };

            // called when a P2P member left.
            netClient.P2PMemberLeaveHandler = (memberHostID, groupHostID, memberCount) =>
            {
                Console.Write("[Client] P2P member {0} left group {1}.\n", memberHostID, groupHostID);
            };

        }
        static void initializeClient()
        {
            netClient.AttachStub(S2CStub);
            netClient.AttachProxy(C2SProxy);
        }
        static void InitializeClientParameter()
        {
            NetConnectionParam cp = new NetConnectionParam();
            cp.protocolVersion.Set(Vars.m_Version);
            cp.serverIP = "localhost";
            cp.serverPort = (ushort)Vars.m_serverPort;
            netClient.Connect(cp);
        }
        static void Main(string[] args)
        {
            InitializeHandler();
            initializeClient();
            InitializeStub();
            InitializeClientParameter();


            Thread workerThread = new Thread(() =>
            {
                while (keepWorkerThread)
                {
                    Thread.Sleep(10);
                    netClient.FrameMove();
                }
            });
            workerThread.Start();


            while (keepWorkerThread)
            {
                // get user input
                string userInput = Console.ReadLine();
                if(userInput == null)
                {
                    continue;
                }
                if (userInput == "q")
                {
                    keepWorkerThread = false;
                }
                else
                {
                    if (isConnected)
                    {
                        lock (g_critSec)
                        {
                            C2SProxy.Chat(HostID.HostID_Server, RmiContext.ReliableSend, userInput);
                        }
                    }
                    else
                    {
                        Console.Write("Not yet connected.\n");
                    }
                }
            }
            workerThread.Join();
            netClient.Disconnect();
        }
    }
}