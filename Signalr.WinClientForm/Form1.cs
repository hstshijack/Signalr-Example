using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace Signalr.WinClientForm
{
    public partial class Form1 : Form
    {
        HubConnection hubConnection = new HubConnectionBuilder().WithAutomaticReconnect().WithUrl("http://localhost:50696/ChatHub").Build();
        
        public Form1()
        {
            InitializeComponent();
            hubConnection.StartAsync();

            hubConnection.On<int>("HasNewConnection", (connectionCount) =>
            {
                label1.Text = $"Toplam Bağlantı Sayısı : {connectionCount}";
            });
            hubConnection.On<string, string>("HasNewMessage", (connectionId, message) =>
            {
                textBox1.AppendText($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}{connectionId} tarafından yazıldı :{message}");
            });
            hubConnection.StartAsync();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hubConnection.SendAsync("PublicMessage", textBox2.Text);
            textBox2.Text = string.Empty;
        }
    }
}
