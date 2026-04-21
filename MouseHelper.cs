using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace WEBGPT
{
    public static class MouseHelper
    {
        // 模拟鼠标事件的 Win32 API
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private const int MOUSEEVENTF_MOVE = 0x0001;     // 鼠标移动
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002; // 鼠标左键按下
        private const int MOUSEEVENTF_LEFTUP = 0x0004;   // 鼠标左键释放
        private const int MOUSEEVENTF_WHEEL = 0x0800;    // 鼠标滚轮滚动

        /// <summary>
        /// 模拟鼠标移动到指定屏幕位置
        /// </summary>
        /// <param name="position">目标位置</param>
        public static void MoveMouse(Point position)
        {
            SetCursorPos(position.X, position.Y);
        }

        /// <summary>
        /// 模拟鼠标点击
        /// </summary>
        /// <param name="position">点击位置</param>
        public static void MouseClick(Point position)
        {
            SetCursorPos(position.X, position.Y);
            Thread.Sleep(50);

            // 模拟鼠标按下和释放
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(100); // 延时确保事件生效
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        /// <summary>
        /// 模拟鼠标滚轮滚动
        /// </summary>
        /// <param name="delta">滚动量，正数向上，负数向下</param>
        /// <param name="position">滚动目标位置</param>
        public static void ScrollWheel(int delta, Point position)
        {
            // 确保鼠标在滚动目标区域
            SetCursorPos(position.X, position.Y);
            Thread.Sleep(50);

            // 模拟滚轮滚动
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, delta, 0);
        }

        /// <summary>
        /// 模拟鼠标拖动操作
        /// </summary>
        /// <param name="startPoint">起始点</param>
        /// <param name="endPoint">结束点</param>
        public static void DragMouse(Point startPoint, Point endPoint)
        {
            // 移动到起始点
            SetCursorPos(startPoint.X, startPoint.Y);
            Thread.Sleep(100);

            // 模拟鼠标按下
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(50);

            // 分步缓慢拖动，减少选中干扰
            int stepCount = 10; // 分步数
            for (int i = 0; i <= stepCount; i++)
            {
                int x = startPoint.X + (endPoint.X - startPoint.X) * i / stepCount;
                int y = startPoint.Y + (endPoint.Y - startPoint.Y) * i / stepCount;
                SetCursorPos(x, y);
                Thread.Sleep(50); // 延时，确保拖动平滑
            }

            // 模拟鼠标释放
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(100);
        }
    }
}
