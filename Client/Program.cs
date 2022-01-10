using System;
using System.Threading;
using Nettention.Proud;
using Common;

namespace Client
{
    class Program
    {
        // RMI stub instance
        // For details, check client source code first.
        static C2S.Stub g_Stub = new C2S.Stub();
        static S2C.Proxy g_Proxy = new S2C.Proxy();
    }
}