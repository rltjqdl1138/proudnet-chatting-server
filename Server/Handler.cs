using System.Diagnostics.CodeAnalysis;
using Nettention.Proud;
namespace Server
{
    internal class Handler
    {
        public bool ConnectionRequestHandler(AddrPort clientAddr, ByteArray userDataFromClient, [NotNull] ByteArray reply)
        {
            reply = new ByteArray();
            reply.Clear();

            return true;
        }

        public void ClientHackSuspectedHandler(HostID clientId, HackType hackType)
        {
        }

        public void ClientJoinHandler(NetClientInfo clientInfo)
        {
            //string message = string.Format("[System] Host{0} enter", clientInfo.hostID);
            //HostID[] others = Process.CommonProcess.GetHostIDs(clientInfo.hostID);
            //Process.CommonProcess.NotifyChat(others, RmiContext.ReliableSend, message);
        }

        public void ClientLeaveHandler(NetClientInfo clientInfo, ErrorInfo errorinfo, ByteArray comment)
        {
            //string message = string.Format("[System] Host{0} left", clientInfo.hostID);
            //HostID[] others = Process.CommonProcess.GetHostIDs(clientInfo.hostID);
            //Process.CommonProcess.NotifyChat(others, RmiContext.ReliableSend, message);
        }

        public void ErrorHandler(ErrorInfo errorInfo)
        {

        }

        public void WarningHandler(ErrorInfo errorInfo)
        {

        }

        public void ExceptionHandler(Exception e)
        {

        }

        public void InformationHandler(ErrorInfo errorInfo)
        {

        }

        public void NoRmiProcessedHandler(RmiID rmiId)
        {

        }

        public void P2PGroupJoinMemberAckCompleteHandler(HostID groupHostId, HostID memberHostId, ErrorType result)
        {

        }

        public void TickHandler(object contextBoundObject)
        {

        }

        public void UserWorkerThreadBeginHandler()
        {

        }

        public void UserWorkerThreadEndHandler()
        {

        }
    }
}
