﻿using System;
using System.Diagnostics;
using System.Linq;
using WhiteMagic.Modules;

namespace WhiteMagic
{
    public static class MagicExtensions
    {
        public static T Call<T>(this ProcessDebugger pd, ModulePointer offs, CallingConventionEx cv, params object[] args) where T : struct
        {
            return pd.Call<T>(pd.GetAddress(offs), cv, args);
        }

        public static T Read<T>(this ProcessDebugger pd, ModulePointer offs)
        {
            return pd.Read<T>(pd.GetAddress(offs));
        }

        public static IntPtr GetAddress(this ProcessDebugger pd, ModulePointer offs)
        {
            return pd.GetModuleAddress(offs.ModuleName) + offs.Offset;
        }

        /// <summary>
        /// Converts byte array into space-separated hex string
        /// </summary>
        /// <param name="array"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        public static string AsHexString(this byte[] array, bool reverse = false)
        {
            if (array.Length == 0)
                return string.Empty;

            return string.Concat((reverse ? array.Reverse() : array).Select(e => string.Format("{0:X2} ", e)));
        }

        public static bool IsValid(this IntPtr p)
        {
            return p != new IntPtr(int.MaxValue);
        }

        public static uint ToUInt32(this IntPtr p)
        {
            return (uint)p.ToInt32();
        }

        public static IntPtr Add(this IntPtr pointer, int offset)
        {
            return IntPtr.Add(pointer, offset);
        }

        public static IntPtr Add(this IntPtr pointer, uint offset)
        {
            return IntPtr.Add(pointer, (int)offset);
        }

        public static IntPtr Add(this IntPtr pointer, IntPtr pointer2)
        {
            return IntPtr.Add(pointer, pointer2.ToInt32());
        }

        public static IntPtr Subtract(this IntPtr pointer, int offset)
        {
            return IntPtr.Subtract(pointer, offset);
        }

        public static IntPtr Subtract(this IntPtr pointer, IntPtr pointer2)
        {
            return IntPtr.Subtract(pointer, pointer2.ToInt32());
        }

        public static string GetVersionInfo(this Process process)
        {
            return string.Format("{0} {1}.{2}.{3} {4}",
                    process.MainModule.FileVersionInfo.FileDescription,
                    process.MainModule.FileVersionInfo.FileMajorPart,
                    process.MainModule.FileVersionInfo.FileMinorPart,
                    process.MainModule.FileVersionInfo.FileBuildPart,
                    process.MainModule.FileVersionInfo.FilePrivatePart);
        }
    }
}
