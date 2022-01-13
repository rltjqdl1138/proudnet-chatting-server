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
            C2SStub.EnterRoom = EnterRoom;
            C2SStub.LeaveRoom = LeaveRoom;

            ServerLauncher.NetServer.AttachProxy(S2CProxy);
            ServerLauncher.NetServer.AttachStub(C2SStub);
        }
        // Chat 함수 로직 작성
        static public bool Chat(HostID remote, RmiContext rmiContext, string str)
        {
            ServerLauncher.UserList.TryGetValue(remote, out User user);
            S2CProxy.NotifyChat(GetHostIDsInRoom(user.RoomNumber), rmiContext, user.UserName, str);
            Console.WriteLine("{0} : {1}", user.UserName, str);
            return true;
        }
        static public bool Login(HostID remote, RmiContext rmiContext, string UserName)
        {
            string message = string.Format("{0} entered.", UserName);
            User user = new User(UserName, remote);

            // 유저 등록
            ServerLauncher.UserList.TryAdd(remote, user);
            
            S2CProxy.ResponseLogin(user.HostId, rmiContext, user);
            
            Console.WriteLine(message);
            return true;
        }
        static public bool EnterRoom(HostID remote, RmiContext rmiContext, int RoomNumber)
        {
            ServerLauncher.UserList.TryGetValue(remote, out User user);
            user.RoomNumber = RoomNumber;
            ServerLauncher.UserList.TryAdd(remote, user);

            string message = string.Format("{0} entered to Room {1}", user.UserName, user.RoomNumber);

            S2CProxy.SystemChat(GetHostIDsInRoom(RoomNumber), RmiContext.ReliableSend, message);
            Console.WriteLine(message);
            return true;
        }
        static public bool LeaveRoom(HostID remote, RmiContext rmiContext)
        {
            ServerLauncher.UserList.TryGetValue(remote, out User user);
            user.RoomNumber = 0;
            ServerLauncher.UserList.TryAdd(remote, user);
            return true;
        }
        static public HostID[] GetHostIDsInRoom(int RoomNumber)
        {
            HostID[] users = ServerLauncher.UserList
                .Where(p => p.Value.RoomNumber == RoomNumber)
                .Select(p => p.Value.HostId)
                .ToArray();

            return users;
        }
        public void SystemChat(string str)
        {
            S2CProxy.SystemChat(ServerLauncher.NetServer.GetClientHostIDs(), RmiContext.ReliableSend, str);
        }
    }
}