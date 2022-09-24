
using System;
using System.Runtime.InteropServices;

namespace Tempus.WindowPlacement
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        #region Point Structre Members

        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public Rect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        #endregion Point Structre Members

    }
}

