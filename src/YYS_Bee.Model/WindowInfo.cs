using System;
using System.Drawing;

namespace YYS_Bee.Model
{
    //自定义一个类，用来保存句柄信息，在遍历的时候，随便也用空上句柄来获取些信息，呵呵 
    public struct WindowInfo
    {
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public WindowHandle hWnd;
        /// <summary>
        /// 位置 x
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// 位置Y
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }
    }
}
