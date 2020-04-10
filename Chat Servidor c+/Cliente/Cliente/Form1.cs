using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace Cliente
{    

    public partial class Form1 : Form
    {

        static private NetworkStream stream;
        static private StreamWriter streamw;
        static private StreamReader streamr;
        static private TcpClient client = new TcpClient();
        static private string nick = "unknown";

        private delegate void DAddItem(String s); 
        
   
              
        
        private void AddItem(String s)
        {
            listBox1.Items.Add(s);
        }



        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            streamw.WriteLine(textBox1.Text);
            streamw.Flush();
            textBox1.Clear();


        }


         void Listen()
        {
            while (client.Connected)
            {
                try
                {
                    this.Invoke(new DAddItem(AddItem), streamr.ReadLine());
                   
                }
                catch
                {
                    MessageBox.Show("No se ha podido conectar al servidor");
                    Application.Exit();
                }
            }
        }

         void Conectar()
        {
            try
            {
                client.Connect("127.0.0.1", 8000);
                if (client.Connected)
                {
                    Thread t = new Thread(Listen);

                    stream = client.GetStream();
                    streamw = new StreamWriter(stream);
                    streamr = new StreamReader(stream);

                    streamw.WriteLine(nick);
                    streamw.Flush();

                    t.Start();
                }
                else
                {
                    MessageBox.Show("Servidor no Disponible");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Servidor no Disponible");
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox2.Visible = false;
            button2.Visible = false;
            listBox1.Visible = true;
            textBox1.Visible = true;
            Enviar.Visible = true;

            nick = textBox2.Text;

            Conectar();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }





    }
}
