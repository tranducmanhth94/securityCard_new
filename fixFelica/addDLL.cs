﻿/*
 felicalib - FeliCa access wrapper library

 Copyright (c) 2007, Takuya Murakami, All rights reserved.

 Redistribution and use in source and binary forms, with or without
 modification, are permitted provided that the following conditions are
 met:

 1. Redistributions of source code must retain the above copyright notice,
    this list of conditions and the following disclaimer. 

 2. Redistributions in binary form must reproduce the above copyright
    notice, this list of conditions and the following disclaimer in the
    documentation and/or other materials provided with the distribution. 

 3. Neither the name of the project nor the names of its contributors
    may be used to endorse or promote products derived from this software
    without specific prior written permission. 

 THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Timers;
using System.Threading;
using System.Windows.Forms;


namespace FelicaLib
{

    // システムコード
    enum SystemCode : int
    {
        Any = 0xffff,           // ANY
        Common = 0xfe00,        // 共通領域
        Cyberne = 0x0003,       // サイバネ領域

        Edy = 0xfe00,           // Edy (=共通領域)
        Suica = 0x0003,         // Suica (=サイバネ領域)
        Pasmo = 0x090f,         // サービス
    }

    public class Felica : IDisposable
    {
        [DllImport("felicalib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr pasori_open(String dummy);

        [DllImport("felicalib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static void pasori_close(IntPtr p);


        [DllImport("felicalib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static int pasori_init(IntPtr p);

        [DllImport("felicalib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr felica_polling(IntPtr p, ushort systemcode, byte rfu, byte time_slot);


        [DllImport("felicalib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static void felica_free(IntPtr f);


        [DllImport("felicalib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static void felica_getidm(IntPtr f, byte[] data);


        [DllImport("felicalib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static void felica_getpmm(IntPtr f, byte[] data);


        [DllImport("felicalib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static int felica_read_without_encryption02(IntPtr f, int servicecode, int mode, byte addr, byte[] data);


        [DllImport("felicalib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static int felica_write_without_encryption(IntPtr f, int servicecode, byte addr, byte data);

        public static IntPtr pasorip = IntPtr.Zero;
        public static IntPtr felicap = IntPtr.Zero;

/*        void testC()
        {
            public Felica()
            {
                pasorip = pasori_open(null);
                if (pasorip == IntPtr.Zero)
                {
                    throw new Exception("felicalib.dll を開けません");
                }
                if (pasori_init(pasorip) != 0)
                {
                    throw new Exception("PaSoRi に接続できません");
                }
            }
        }*/

        public Felica()
        {
            pasorip = pasori_open(null);
            if (pasorip == IntPtr.Zero)
            {
                throw new Exception("felicalib.dll を開けません");
            }
            else if (pasori_init(pasorip) != 0)
            {
                throw new Exception("PaSoRi に接続できません");
            }
        }


        public void Dispose()
        {
            if (pasorip != IntPtr.Zero)
            {
                pasori_close(pasorip);
                pasorip = IntPtr.Zero;
            }
            
        }

        ~Felica()
        {
            Dispose();
        }

        public bool valueCheck = false;

     /*   public static IntPtr testC()
        {
            pasorip = pasori_open(null);
            IntPtr felicap1 = IntPtr.Zero;

            Thread.Sleep(1000);
            felica_free(felicap1);

            felicap1 = felica_polling(pasorip, (ushort)65535, 0, 0);

            return felicap1;

        }*/

        public IntPtr Polling(int systemcode)
        {
            Thread.Sleep(500);
            felica_free(felicap);

            felicap = felica_polling(pasorip, (ushort)systemcode, 0, 0);

            return felicap;

            if (felicap != IntPtr.Zero)
            {
                //0x07e73048
                //throw new Exception("カード読み取り失敗");
                // 2 cach xu li, ra hoi lien tuc 

            }
        }
        
      /*  public void testConnect()
        {
            if(valueCheck == true)
            {
                
            }
        }*/


        public byte[] IDm()
        {
            if (felicap == IntPtr.Zero)
            {
                throw new Exception("no polling executed.");
            }
            string IDM = "";
            byte[] buf = new byte[8];
            felica_getidm(felicap, buf);

            IDM = buf.ToString();
            return buf;
            // 012E4CE36184383A id
            // 146762279713256  byte
        }

        public byte[] PMm()
        {
            if (felicap == IntPtr.Zero)
            {
                throw new Exception("no polling executed.");
            }

            byte[] buf = new byte[8];
            felica_getpmm(felicap, buf);
            return buf;
        }


        public byte[] ReadWithoutEncryption(int servicecode, int addr)
        {
            
            if (felicap == IntPtr.Zero)
            {
                //throw new Exception("no polling executed.");    
            }

             byte[] data = new byte[16];

            try
            {

                int ret = felica_read_without_encryption02(felicap, servicecode, 0, (byte)addr, data);


                if (ret != 0)
                {
                    return null;
                }
              
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
            return data;
        }

    }
}
     

