using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GCCollectX
{
    /// <summary>
    /// Utilities to free and compact memory
    /// </summary>
    public static class GCX
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(
            IntPtr process,
            UIntPtr minimumWorkingSetSize,
            UIntPtr maximumWorkingSetSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcessHeap();

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool HeapLock(IntPtr heap);

        [DllImport("kernel32.dll")]
        private static extern uint HeapCompact(IntPtr heap, uint flags);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool HeapUnlock(IntPtr heap);

        private static void SetProcessWorkingSetSizeToMin()
        {
            SetProcessWorkingSetSize(
                Process.GetCurrentProcess().Handle,
                (UIntPtr)0xFFFFFFFF,
                (UIntPtr)0xFFFFFFFF);
        }

        /// <summary>
        /// Call Garbage collector, wait for it and Compact heap memory
        /// </summary>
        public static void CollectX()
        {
            CollectX(true, true);
        }
        /// <summary>
        /// Compact heap memory
        /// </summary>
        /// <param name="gcCollect">If true call GC.Collect</param>
        /// <param name="waitCollect">If true wait for GC.Collect</param>
        public static void CollectX(bool gcCollect, bool waitCollect)
        {
            if (gcCollect && waitCollect)
            {
                System.GC.Collect(System.GC.MaxGeneration);
                System.GC.WaitForPendingFinalizers();
            }
            else if (gcCollect && !waitCollect)
            {
                System.GC.Collect(System.GC.MaxGeneration);
            }

            SetProcessWorkingSetSizeToMin();
            HeapCompact();
        }

        /// <summary>
        /// Compact heap memory
        /// </summary>
        private static void HeapCompact()
        {
            IntPtr heap = GetProcessHeap();

            if (HeapLock(heap))
            {
                try
                {
                    if (HeapCompact(heap, 0) == 0)
                    {
                        //error ignored
                    }
                }
                finally
                {
                    HeapUnlock(heap);
                }
            }
        }

    }
}
