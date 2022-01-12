using Nettention.Proud;
using ChattingCommon;
namespace ChattingServer.process
{
    internal class CommonProcess
    {
        static S2C.Proxy S2CProxy = new S2C.Proxy();
        static C2S.Stub C2SStub = new C2S.Stub();

        public void InitStub()
        {
            C2SStub.Chat = Chat;
            C2SStub.Login = Login;

            ServerLauncher.NetServer.AttachProxy(S2CProxy);
            ServerLauncher.NetServer.AttachStub(C2SStub);
        }
        // Chat 함수 로직 작성
        static public bool Chat(HostID remote, RmiContext rmiContext, int UserID, string str)
        {
            ServerLauncher.UserList.TryGetValue(UserID, out User user);
            Console.WriteLine("{0}: {1}",user.UserName, str);
            S2CProxy.NotifyChat(ServerLauncher.NetServer.GetClientHostIDs(), rmiContext, user.UserName, str);
            return true;
        }
        static public bool Login(HostID remote, RmiContext rmiContext, string UserName)
        {
            string message = string.Format("{0} entered.", UserName);
            User user = new User(UserName, remote);

            // 유저 등록
            ServerLauncher.UserList.TryAdd(user.UserID, user);
            
            S2CProxy.ResponseLogin(user.HostId, rmiContext, user);
            S2CProxy.SystemChat(ServerLauncher.NetServer.GetClientHostIDs(), RmiContext.ReliableSend, message);
            
            Console.WriteLine(message);
            return true;
        }

        public void SystemChat(string str)
        {
            S2CProxy.SystemChat(ServerLauncher.NetServer.GetClientHostIDs(), RmiContext.ReliableSend, str);
        }
    }
}