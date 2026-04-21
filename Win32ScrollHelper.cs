using System;
using System.Runtime.InteropServices;

public static class Win32ScrollHelper
{
    // 定义 Win32 常量
    private const int WM_VSCROLL = 0x0115; // 垂直滚动消息
    private const int SB_THUMBPOSITION = 4; // 滑块位置
    private const int SB_VERT = 1; // 垂直滚动条
    private const uint SIF_TRACKPOS = 0x10; // 获取跟踪的滚动位置
    private const uint SIF_ALL = 0x17; // 获取所有滚动信息

    // 定义 SCROLLINFO 结构
    [StructLayout(LayoutKind.Sequential)]
    public struct SCROLLINFO
    {
        public uint cbSize; // 结构体大小
        public uint fMask; // 滚动信息标志
        public int nMin; // 最小滚动位置
        public int nMax; // 最大滚动位置
        public uint nPage; // 滚动条页面大小
        public int nPos; // 当前滚动条位置
        public int nTrackPos; // 拖动滚动条时的实时位置
    }

    [DllImport("user32.dll")]
    private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// 获取滚动条的当前位置
    /// </summary>
    public static int GetScrollPosition(IntPtr hwnd)
    {
        SCROLLINFO scrollInfo = new SCROLLINFO
        {
            cbSize = (uint)Marshal.SizeOf(typeof(SCROLLINFO)),
            fMask = SIF_TRACKPOS | SIF_ALL
        };

        if (GetScrollInfo(hwnd, SB_VERT, ref scrollInfo))
        {
            return scrollInfo.nPos; // 返回当前滚动条的位置
        }

        return -1; // 如果获取失败返回 -1
    }

    /// <summary>
    /// 设置滚动条位置
    /// </summary>
    public static void SetScrollPosition(IntPtr hwnd, int position)
    {
        // 组合 wParam 的值 (SB_THUMBPOSITION 表示设置到指定位置)
        IntPtr wParam = new IntPtr(SB_THUMBPOSITION | (position << 16));
        SendMessage(hwnd, WM_VSCROLL, wParam, IntPtr.Zero);
    }
}

