using System;
using System.Threading;
using YYS_Bee.Model;
using YYS_Bee.WinApi;

namespace YYS_Bee.Imp
{
    public class WindowTools
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowName"></param>
        /// <returns></returns>
        public static WindowHandle GetWindowHandle(string windowName)
        {
            var windowHanles = Win.GetAllDesktopWindows();

            foreach (var item in windowHanles)
            {
                if (item.szWindowName.IndexOf(windowName) > -1)
                {
                    return item;
                }
            }
            throw new Exception("未找到 "+windowName+" 窗口,请打开并置顶！");
        }

        public static WindowInfo GetWindowInfo(WindowHandle windowHanle)
        {
            RECT rc = new RECT();
            Win.GetWindowRect(windowHanle.hWnd, ref rc);
            int width = rc.Right - rc.Left; //窗口的宽度
            int height = rc.Bottom - rc.Top; //窗口的高度
            int x = rc.Left;
            int y = rc.Top;
           


            return new WindowInfo() {
                hWnd = windowHanle,
                Height = height,
                Width = width,
                X = x,
                Y = y,
               // WinImage = image
            };

        }
    }
}
