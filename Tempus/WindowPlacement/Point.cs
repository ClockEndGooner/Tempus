
using System;
using System.Runtime.InteropServices;

namespace Tempus.WindowPlacement
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        #region Point Structre Members

        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion Point Structre Members
    }
}
