using Nettention.Proud;

namespace ChattingServer.process
{
    internal class CommonProcess
    {
        static S2C.Proxy S2CProxy = new S2C.Proxy();
        static C2S.Stub C2SStub = new C2S.Stub();

        public void InitStub()
        {
            ServerLauncher.NetServer.AttachProxy(S2CProxy);
            ServerLauncher.NetServer.AttachStub(C2SStub);
        }

    }
}