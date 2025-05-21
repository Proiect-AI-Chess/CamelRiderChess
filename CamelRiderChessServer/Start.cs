using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamelRiderChessServer
{
    public partial class Start : Form
    {
        public TcpListener server;
        public NetworkStream streamServer;
        public Socket socketServer;
        public bool workThread;

        public Start()
        {
            InitializeComponent();
            server = new TcpListener(System.Net.IPAddress.Any, 5041);
            server.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            server.Start();
            workThread = true;
            _ = Asculta_ServerAsync(); // pornim metoda asincronă, fără blocarea UI-ului
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            workThread = false;
            server.Stop();
            // nu mai avem thread de sincronizat, dar putem lăsa o mică pauză
        }

       public async Task Asculta_ServerAsync()
{
    while (workThread)
    {
        try
        {
            socketServer = await server.AcceptSocketAsync();
            streamServer = new NetworkStream(socketServer);
            using (StreamReader citireServer = new StreamReader(streamServer))
            {
                while (workThread)
                {
                    string dateServer = await citireServer.ReadLineAsync();
                    if (dateServer == null) break;

                    if (dateServer == "#Gata")
                    {
                        workThread = false;
                    }

                    if (dateServer == "StartGame")
                    {
                        Invoke((MethodInvoker)(() => StartGame()));
                    }

                    if (dateServer.StartsWith("MOVE"))
                    {
                        string move = dateServer.Substring(4);
                        Invoke((MethodInvoker)(() => ReceiveMove(move)));
                    }
                }
            }
            streamServer.Close();
        }
        catch (Exception e)
        {
#if LOG
            Console.WriteLine(e.Message);
#endif
        }
        finally
        {
            socketServer?.Close();
        }
    }
}

        private void StartGame()
        {
            Form1 newGame = new Form1();
            Visible = false;
            if (!newGame.IsDisposed)
                newGame.ShowDialog();
            Visible = true;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (socketServer != null && socketServer.Connected)
            {
                StreamWriter scriereServer = new StreamWriter(streamServer) { AutoFlush = true };
                scriereServer.WriteLine("StartGame");
                StartGame();
            }
            else
            {
                MessageBox.Show("No client connected.");
            }
        }

        public void SendMove(string move)
        {
            if (streamServer != null)
            {
                StreamWriter writer = new StreamWriter(streamServer) { AutoFlush = true };
                writer.WriteLine("MOVE " + move);
            }
        }

        public void ReceiveMove(string move)
        {
            try
            {
                Form1 form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
                if (form1 != null)
                {
                    form1.UpdateChessBoard(move);
                }
                else
                {
                    MessageBox.Show("Eroare: Form1 nu este deschis.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la procesarea mutării primite de la server: " + ex.Message);
            }
        }
    }
}
