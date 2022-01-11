namespace Server
{
    class Program
    {
        static void Main()
        {
            ServerLauncher server = new ServerLauncher();

            try
            {
                server.ServerStart();

                Console.Write("============================================\n");
                Console.Write("\tServer started\n");
                Console.Write("============================================\n");

                while (server.RunLoop)
                {
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape && Console.ReadKey(true).Key == ConsoleKey.Delete)
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(1000);
                }

                Console.Write("============================================\n");
                Console.Write("\tServer Closed\n");
                Console.Write("============================================\n");

                server.Dispose();

            }catch(Exception e)
            {
                Console.Write(e.ToString());
            }
        }
    }
}