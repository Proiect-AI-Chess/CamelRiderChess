using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CamelRiderChess
{
    public partial class Menu : Form
    {
        private volatile bool workThread;
        private TcpClient client;
        private Thread t;
        private NetworkStream clientStream;

        public Menu()
        {
            InitializeComponent();
            
        }

        private void RobotButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            var gameForm = new Form1(true);  // constructor cu AI mode
            gameForm.ShowDialog();
            this.Close();
        }

        private void FriendButton_Click(object sender, EventArgs e)
        {
            if (FriendButton.Text == "Play with a friend")
            {
                try
                {
                    client = new TcpClient("127.0.0.1", 5041);
                    clientStream = client.GetStream();

                    workThread = true;
                    t = new Thread(Ascult_Client);
                    t.IsBackground = true;
                    t.Start();

                    FriendButton.Text = "Disconnect";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nu s-a putut conecta la server: " + ex.Message);
                }
            }
            else
            {
                Disconnect();
            }
        }

        private void Disconnect()
        {
            try
            {
                workThread = false;
                if (t != null && t.IsAlive)
                    t.Join(500); // Așteaptă thread-ul să termine

                if (clientStream != null)
                {
                    using (StreamWriter scriere = new StreamWriter(clientStream) { AutoFlush = true })
                    {
                        scriere.WriteLine("#Gata");
                    }
                    clientStream.Close();
                    clientStream = null;
                }
                if (client != null)
                {
                    client.Close();
                    client = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la deconectare: " + ex.Message);
            }
            finally
            {
                FriendButton.Text = "Play with a friend";
            }
        }

        public void Ascult_Client()
        {
            try
            {
                using (StreamReader citire = new StreamReader(clientStream))
                {
                    string dateClient;
                    while (workThread && (dateClient = citire.ReadLine()) != null)
                    {
                        MethodInvoker m = new MethodInvoker(() =>
                        {
                            if (dateClient == "StartGame")
                            {
                                StartGame();
                            }
                            else if (dateClient == "#Gata")
                            {
                                workThread = false;
                            }
                            else if (dateClient.StartsWith("MOVE"))
                            {
                                ReceiveMove(dateClient.Substring(4));
                            }
                        });
                        this.Invoke(m);
                    }
                }
            }
            catch (Exception ex)
            {
                if (workThread)
                    MessageBox.Show("Eroare la comunicarea cu serverul: " + ex.Message);
            }
            finally
            {
                Disconnect();
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

        public void SendMove(string move)
        {
            if (clientStream != null)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(clientStream, Encoding.UTF8, 1024, leaveOpen: true) { AutoFlush = true })

                    {
                        writer.WriteLine(move);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la trimiterea mutării: " + ex.Message);
                }
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
                MessageBox.Show("Eroare la procesarea mutării primite: " + ex.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            workThread = false;
            Disconnect();
            base.OnFormClosing(e);
        }
    }
}
