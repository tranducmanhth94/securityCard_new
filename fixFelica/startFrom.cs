using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FelicaLib;


namespace fixFelica
{
    public partial class startFrom : Form
    {
        
        public startFrom()
        {
            InitializeComponent();
            

        }

        public static bool NotEndLoop = false;

        private void button1_Click(object sender, EventArgs e)
        {
            
            NotEndLoop = true;

           // tip check form
            var confirmfrom = (confirmFrom)Application.OpenForms["confirmFrom"];
            confirmfrom?.Focus();
            if (confirmfrom != null)
            {
                confirmfrom.Show();
                confirmfrom.getData();
               // timer1.Start();
                /* NotEndLoop = true;
                 while (NotEndLoop == true)
                 {
                     confirmfrom.getData();
                 }*/
                
            }
            else
            {
                confirmfrom = new confirmFrom();
                confirmfrom.Show();
                /*confirmfrom.getData();*/
                //timer1.Start();
            }
            
        }


    /*    private void loop()
        {
            if (NotEndLoop == true)
            {
                void OnShown(object sender ,EventArgs e)
                {
                    //base.OnShown(e);
                    startBut.PerformClick();
                }
                    
            }
        }*/
         
        private void timer1_Tick(object sender, EventArgs e)
        {
            var confirmfrom = (confirmFrom)Application.OpenForms["confirmFrom"];
            confirmfrom?.Focus();
            if (confirmfrom != null)
            {
                confirmfrom.addData();
                confirmfrom.getData();
            }
                
            
        }
    }
}
