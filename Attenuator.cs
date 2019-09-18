using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AllionTR398Tool
{
    class Attenuator
    {

        [DllImport("VNX_atten.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void fnLDA_SetTestMode(bool testmode);

        [DllImport("VNX_atten.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int fnLDA_GetNumDevices();

        [DllImport("VNX_atten.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int fnLDA_InitDevice(int deviceID);

        [DllImport("VNX_atten.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int fnLDA_CloseDevice(int deviceID);

        [DllImport("VNX_atten.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int fnLDA_SetAttenuation(int deviceID, int attenuation);

        [DllImport("VNX_atten.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int fnLDA_GetSerialNumber(int deviceID);

        [DllImport("VNX_atten.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int fnLDA_GetAttenuation(int deviceID);
    }
}
