﻿using System.Runtime.InteropServices;

namespace MetinLitBot.Libs;

public class User32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
}