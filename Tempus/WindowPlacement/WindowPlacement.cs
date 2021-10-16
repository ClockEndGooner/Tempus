
using System;
using System.Runtime.InteropServices;

namespace Tempus.WindowPlacement
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowPlacement
    {
        #region WindowPlacement Structure Members

        public int length;
        public int flags;
        public int showCmd;
        public Point minPosition;
        public Point maxPosition;
        public Rect normalPosition;

        #endregion WindowPlacement Structure Members
    }
}
