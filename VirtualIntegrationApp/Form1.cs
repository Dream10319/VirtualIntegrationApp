using System;
using System.Windows.Forms;
using VirtualIntegrationApp.Properties;
using WebSocketSharp;

namespace VirtualIntegrationApp
{
    public partial class Form1 : Form
    {
        WebSocket ws;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ws = new WebSocket("ws://3.34.251.227:3000");

            // Set up event handlers for the WebSocket
            ws.OnMessage += (s, eArgs) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        Console.WriteLine("Received from server: " + eArgs.Data);
                        // Example: You can update a TextBox or ListBox with the received data
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data type error!");
                    }

                });
            };

            ws.OnOpen += (s, eArgs) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Console.WriteLine("Connected to server");
                    ws.Send("Hello from C# client!");
                });
            };

            ws.OnClose += (s, eArgs) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Console.WriteLine("Disconnected from server");
                });
            };

            ws.OnError += (s, eArgs) =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Console.WriteLine("Error: " + eArgs.Message);
                });
            };

            // Connect to the WebSocket server
            ws.Connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ws.Send("{\r\n\"shop\" : \"Baemin\",\r\n\"api_type\": \"Login\",\r\n\"status\": false\r\n}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ws.Send("{\r\n\"shop\" : \"Baemin\",\r\n\"api_type\": \"Login\",\r\n\"status\": true\r\n}");
        }
    }
}
