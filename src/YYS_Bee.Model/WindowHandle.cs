using System;

namespace YYS_Bee.Model
{
    //自定义一个类，用来保存句柄信息，在遍历的时候，随便也用空上句柄来获取些信息，呵呵 
    public struct WindowHandle
    {
        public IntPtr hWnd;
        public string szWindowName;
        public string szClassName;
    }
}
