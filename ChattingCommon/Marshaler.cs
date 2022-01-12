using Nettention.Proud;
namespace ChattingCommon
{
    public class Marshaler : Nettention.Proud.Marshaler
    {
        public static void Write(Message msg, User user)
        {
            msg.Write(user.HostId);
            msg.Write(user.UserName);
            msg.Write(user.UserID);
        }
        public static User Read(Message msg, out User user)
        {
            msg.Read(out HostID HostId);
            msg.Read(out string UserName);
            msg.Read(out int UserID);

            user = new User();
            user.HostId = HostId;
            user.UserName = UserName;
            user.UserID = UserID;

            return user;
        }
    }
}
