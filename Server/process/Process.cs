using Nettention.Proud;

namespace Server.process
{
    internal class CommonProcess
    {
        static S2C.Proxy S2CProxy = new S2C.Proxy();
        static C2S.Stub C2SStub = new C2S.Stub();

        public void InitStub()
        {
            C2SStub.Chat = Chat;

            ServerLauncher.NetServer.AttachProxy(S2CProxy);
            ServerLauncher.NetServer.AttachStub(C2SStub);
        }
        static public bool Chat(HostID remote, RmiContext rmiContext, string str)
        {
            return true;
        }
        public void SystemChat(string str)
        {
            S2CProxy.SystemChat(ServerLauncher.NetServer.GetClientHostIDs(), RmiContext.ReliableSend, str);
        }

    }
}