using System;
using System.Collections.Generic;
using System.Linq;
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
        //public static WindowHandle GetWindowHandle(string windowName)
        //{
        //    var windowHanles = Win.GetAllDesktopWindows();

        //    foreach (var item in windowHanles)
        //    {
        //        if (item.szWindowName.IndexOf(windowName) > -1)
        //        {
        //            return item;
        //        }
        //    }
        //    throw new Exception("未找到 "+windowName+" 窗口,请打开并置顶！");
        //}
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <param name="windowName"></param>
        /// <returns></returns>
        public static List<WindowHandle> GetWindowHandles(string windowName)
        {
            var windowHanles = Win.GetAllDesktopWindows();
            if (windowHanles == null || windowHanles.Count() == 0)
            {
                throw new Exception("未找到 " + windowName + " 窗口,请打开并置顶！");
            }
            var result = windowHanles.Where(n => n.szWindowName.IndexOf(windowName) > -1).ToList();
            if (result==null || result.Count()==0)
            {
                throw new Exception("未找到 " + windowName + " 窗口,请打开并置顶！");
            }
            return result;
            //foreach (var item in windowHanles)
            //{
            //    if (item.szWindowName.IndexOf(windowName) > -1)
            //    {
            //        return item;
            //    }
            //}
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
            };

        }

        //public static WindowInfo GetWindowInfo(string windowName)
        //{
        //    return GetWindowInfo(GetWindowHandle(windowName));
        //}
        public static bool SetWindowSize(WindowInfo info, GameSetting setting)
        {
            if (info.Width != setting.WindowWidth || info.Height != setting.WindowHeight)
            {
                Win.MoveWindow(info.hWnd.hWnd, info.X, info.Y, setting.WindowWidth, setting.WindowHeight, true);
                return true;
            }
            return false;
        }
    }
}
