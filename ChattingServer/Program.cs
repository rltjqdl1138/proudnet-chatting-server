using Nettention.Proud;
using ChattingCommon;

namespace ChattingServer
{

    class Program
    {
        // RMI stub instance
        // For details, check client source code first.
        static C2S.Stub g_Stub = new C2S.Stub();
        static S2C.Proxy g_Proxy = new S2C.Proxy();

        static void InitStub()
        {
            g_Stub.Chat = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, string str) =>
            {
                Console.Write("[Server] Chat :");
                Console.Write(" {0}\n", str);
                return true;
            };

        }

        internal static void StartServer(NetServer server, Nettention.Proud.StartServerParameter param)
        {
            if ((server == null) || (param == null))
            {
                throw new NullReferenceException();
            }

            try
            {
                server.Start(param);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Failed to start server~!!" + ex.ToString());
            }

            Console.WriteLine("Succeed to start server~!!\n");
        }

        static void Main()
        {

            // Network server instance.
            NetServer srv = new NetServer();

            // set a routine which is executed when a client is joining.
            // clientInfo has the client info including its HostID.
            srv.ClientJoinHandler = (clientInfo) =>
            {
                Console.Write("Client {0} connected.\n", clientInfo.hostID);
            };

            // set a routine for client leave event.
            srv.ClientLeaveHandler = (clientInfo, errorInfo, comment) =>
            {
                Console.Write("Client {0} disconnected.\n", clientInfo.hostID);
            };

            InitStub();

            // Associate RMI proxy and stub instances to network object.
            srv.AttachStub(g_Stub);
            srv.AttachProxy(g_Proxy);

            var p1 = new StartServerParameter();
            p1.protocolVersion = new Nettention.Proud.Guid(Vars.m_Version); // This must be the same to the client.
            p1.tcpPorts.Add(Vars.m_serverPort); // TCP listening endpoint

            try
            {
                /* Starts the server.
                This function throws an exception on failure.
                Note: As we specify nothing for threading model,
                RMI function by message receive and event callbacks are
                called in a separate thread pool.
                You can change the thread model. Check out the help pages for details. */
                srv.Start(p1);
            }
            catch (Exception e)
            {
                Console.Write("Server start failed: {0}\n", e.ToString());
                return;
            }

            Console.Write("Server started. Enterable commands:\n");
            Console.Write("1: Creates a P2P group where all clients join.\n");
            Console.Write("2: Sends a message to P2P group members.\n");
            Console.Write("q: Quit.\n");

            while (true)
            {

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape && Console.ReadKey(true).Key == ConsoleKey.Delete)
                {
                    break;
                }
                System.Threading.Thread.Sleep(1000);
            }

            srv.Stop();
        }
    }
}