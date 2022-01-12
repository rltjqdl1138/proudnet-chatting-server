using ChattingCommon;
using Nettention.Proud;

namespace ChattingServer
{
    public class ServerLauncher
    {
        public bool RunLoop;
        public static readonly NetServer NetServer = new NetServer();
        private readonly Nettention.Proud.ThreadPool _netWorkerThreadPool = new Nettention.Proud.ThreadPool(8);
        private readonly Nettention.Proud.ThreadPool _userWorkerThreadPool = new Nettention.Proud.ThreadPool(8);

        Handler Handler = new Handler();
        process.CommonProcess Process = new process.CommonProcess();

        public void InitializeStub()
        {
            Process.InitStub();
        }
        public void InitializeHandler()
        {
            NetServer.ConnectionRequestHandler = Handler.ConnectionRequestHandler;
            NetServer.ClientHackSuspectedHandler = Handler.ClientHackSuspectedHandler;
            NetServer.ClientJoinHandler = Handler.ClientJoinHandler;
            NetServer.ClientLeaveHandler = Handler.ClientLeaveHandler;
            NetServer.ErrorHandler = Handler.ErrorHandler;
            NetServer.WarningHandler = Handler.WarningHandler;
            NetServer.ExceptionHandler = Handler.ExceptionHandler;
            NetServer.InformationHandler = Handler.InformationHandler;
            NetServer.NoRmiProcessedHandler = Handler.NoRmiProcessedHandler;
            NetServer.P2PGroupJoinMemberAckCompleteHandler = Handler.P2PGroupJoinMemberAckCompleteHandler;
            NetServer.TickHandler = Handler.TickHandler;
            NetServer.UserWorkerThreadBeginHandler = Handler.UserWorkerThreadBeginHandler;
            NetServer.UserWorkerThreadEndHandler = Handler.UserWorkerThreadEndHandler;
        }
        public void InitialzieServerParameter()
        {
            var parameter = new StartServerParameter();
            parameter.protocolVersion = new Nettention.Proud.Guid(Vars.m_Version);
            parameter.tcpPorts.Add(Vars.m_serverPort);
            NetServer.Start(parameter);
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