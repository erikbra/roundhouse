using System;
using NUnit.Framework;

namespace roundhouse.tests
{
    public static class TestEnvironment
    {
        internal static bool SupportsOleDb
        {
            get
            {
#if NET461
                return Environment.OSVersion.Platform == PlatformID.Win32Windows;
#endif
                return false;
            }
        }

        internal static void RequireOleDb()
        {
            if (!TestEnvironment.SupportsOleDb) {
                Assert.Inconclusive("OleDb is not supported on this platform - unable to test.");
            }
        }
        
    }
}