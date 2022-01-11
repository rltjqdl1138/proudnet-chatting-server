using Common;
using Nettention.Proud;

namespace Server
{
    public class ServerLauncher
    {
        public bool RunLoop;
        public static readonly NetServer NetServer = new NetServer();
        private readonly Nettention.Proud.ThreadPool _netWorkerThreadPool = new Nettention.Proud.ThreadPool(8);
        private readonly Nettention.Proud.ThreadPool _userWorkerThreadPool = new Nettention.Proud.ThreadPool(8);

        Handler Handler = new Handler();

        public void InitializeStub()
        {
            process.CommonProcess.InitStub();
        }
        public void InitializeHandler()
        {

        }
        public void InitialzieServerParameter()
        {

        }
        public void ServerStart()
        {
            InitializeHandler();
            InitializeStub();
            InitialzieServerParameter();
            RunLoop = true;
        }
        public void Dispose()
        {
            NetServer.Dispose();
        }
    }
}