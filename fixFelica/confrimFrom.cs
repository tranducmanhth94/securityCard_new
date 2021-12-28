using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FelicaLib;
    

namespace fixFelica
{
    public partial class confirmFrom : Form
    {
        //System.Windows.Forms.Form f = System.Windows.Forms.Application.OpenForms["startFrom"];
        public confirmFrom()
        {
            InitializeComponent();
            addData();
            timer1.Start();
        }
        public static IntPtr pasorip = IntPtr.Zero;
        public static int index = 0;
        string zero = "0";
        public struct Service_suica
        {
            public const int SERVICE_SUICA_INOUT = 0x108f;
            public const int SERVICE_SUICA_HISTORY = 0x090f;
            public const int SERVICE_readonly = 0x000B;
        }


         public void getData()
         {
            string resultID = "";
            string resultPM = "";
            index++;
            //string mozi = "";
            

            using (Felica f = new Felica())
            {
                //ポートの指定
                f.Polling(0xFFFF);


                //block00を読み込む
                
                byte[] Block00 = f.ReadWithoutEncryption(Service_suica.SERVICE_readonly, 0x83);
                //0x83


                f.IDm();


                if (Block00 == null)
                {
                    MessageBox.Show("カードのデータを読み失敗");
                }
                else
                {
                    byte a = 0;
                    byte b = 0;
                    string Hexadecimal = "";
                    string seperatorID = ",";
                    string seperatorPM = ",";

                    for (int i = 0; i <= 7; i++)
                    {

                        a = Block00[i];
                        Hexadecimal = Convert.ToString(a, 16);

                        resultID += string.Join(seperatorID, Hexadecimal);
                    }

                    for (int i = 7; i < Block00.Length; i++)
                    {
                        b = Block00[i];
                        Hexadecimal = Convert.ToString(b, 16);
                        resultPM += string.Join(seperatorPM, b);
                    }

                    ID_textBox.Text = zero + resultID;

                    Pm_textbox.Text = resultPM;

                }

            }

            table.Rows.Add(index, zero + resultID, resultPM);
            dataGridView1.DataSource = table;
            
        }

        public DataTable table;

        public void addData()
        {
            table = new DataTable(); // tạo mới 

            table.Columns.Add("順番", typeof(int));
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("PM", typeof(string));
            
        }

     /*   private void confirmFrom_Load(object sender, EventArgs e)
        {
            addData();
        }*/


        private void backBut_Click(object sender, EventArgs e)
        {
            
           
            startFrom confirmfrom = new startFrom();
            this.Hide();
            confirmfrom.ShowDialog();
            this.Show();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Felica f = new Felica())
            {
                if (f.Polling(0xFFFF) != IntPtr.Zero)
                {
                    
                    getData();
                }

            }  
        }

        /*       public static void Main()
               {
                   var mc = new Form1();
                 try
                   {
                       using (Felica f = new Felica())
                       {
                           mc.readSuica(f);
                       }
                   }
                   catch(Exception ex)
                   {
                       Console.WriteLine(ex.Message);
                   }
               }*/

        /* private int readSuica(Felica f)
         {
             f.Polling((int)SystemCode.Pasmo);
             for (int i = 0; ; i++)
             {
                 byte[] data;
                 data = f.ReadWithoutEncryption(Service_suica.SERVICE_SUICA_INOUT, i);
                 if (data == null)
                     break;
                 suica_dump_history(data);
             }
             f.Dispose();
             return 0;
         }
         public void suica_dump_history(byte[] data)
         {
             DataSet dataset = new DataSet();
             DataTable dt = new DataTable("dataGridView");
             int ctype, proc, date, time, balance, seq, region;
             int in_line, in_sta, out_line, out_sta;
             int yy, mm, dd;
             // add cloumn
             dt.Columns.Add("端末類");
             dt.Columns.Add("処理");
             dt.Columns.Add("日付");
             dt.Columns.Add("入");
             dt.Columns.Add("残高");
             dt.Columns.Add("連番");
             dataset.Tables.Add(dt);
             DataRow datarow = dataset.Tables["dataGridView"].NewRow();

             ctype = data[0];//端末類
             proc = data[1]; //処理
             date = (data[4] << 8) + data[5];// 日付
             balance = (data[10] << 8) + data[11];// 残高
             balance = ((balance) >> 8) & 0xff | ((balance) << 8) & 0xff00;
             seq = (data[12] << 24) + (data[13] << 16) + (data[14] << 8) + data[15];
             region = seq & 0xff;
             seq >>= 8;

             out_line = -1;
             out_sta = -1;
             time = -1;*/

        /*  switch (ctype)
          {
              case 0xC7:  // 物販
              case 0xC8:  // 自販機  
                  time = (data[6] << 8) + data[7];
                  in_line = data[8];
                  break;
              case 0x05:  // 車載機
                  in_line = (data[6] << 8) + data[7];
                  in_sta = (data[8] << 8) + data[9];
                  break;

              default:
                  in_line = data[6];
                  in_sta = data[7];
                  out_line = data[8];
                  out_sta = data[9];
                  break;
          }
          datarow["端末類"] = ctype;  // check 
          datarow["処理"] = proc; // check 

          // date
          yy = date >> 9;
          mm = (date >> 5) & 0xf;
          dd = date & 0x1f;
          datarow["Date"] = yy.ToString() + "/" + mm.ToString() + "/" + dd.ToString() + " ";

          // time
          if (time > 0)
          {
              int hh = time >> 11;
              int min = (time >> 5) & 0x3f;

              datarow["日付"] += hh.ToString() + ":" + min.ToString();
          }

          datarow["入"] = in_line.ToString("X") + "/" + in_sta.ToString("X");
          if (out_line != -1)
          {
              datarow["入"] = out_line.ToString("X") + "/" + out_sta.ToString("X");
          }
          datarow["残高"] = balance.ToString();
          datarow["連番"] = seq.ToString();

      }
      private string consoleType(int ctype)
      {
          switch (ctype)
          {
              case 0x03: return "清算機";
              case 0x05: return "車載端末";
              case 0x08: return "券売機";
              case 0x12: return "券売機";
              case 0x16: return "改札機";
              case 0x17: return "簡易改札機";
              case 0x18: return "窓口端末";
              case 0x1a: return "改札端末";
              case 0x1b: return "携帯電話";
              case 0x1c: return "乗継清算機";
              case 0x1d: return "連絡改札機";
              case 0xc7: return "物販";
              case 0xc8: return "自販機";
          }
          return "???";
      }

      private string procType(int proc)
      {
          switch (proc)
          {
              case 0x01: return "運賃支払";
              case 0x02: return "チャージ";
              case 0x03: return "券購";
              case 0x04: return "清算";
              case 0x07: return "新規";
              case 0x0d: return "バス";
              case 0x0f: return "バス";
              case 0x14: return "オートチャージ";
              case 0x46: return "物販";
              case 0x49: return "入金";
              case 0xc6: return "物販(現金併用)";
          }
          return "???";*/

    }
}
