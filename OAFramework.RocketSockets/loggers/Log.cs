using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OAFramework.RocketSockets.loggers
{
    class Log
    {
        public static Boolean Enabled = true;
        public static void d(String message) => Debug.WriteLineIf(Enabled, message);
    }
}
