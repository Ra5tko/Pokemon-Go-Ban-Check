using Google.Protobuf;
using POGOLib.Net;
using POGOLib.Net.Authentication;
using POGOLib.Pokemon.Data;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAnimatedGif;

namespace PokemonGOBanCheck
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PTCLogin_Click(object sender, RoutedEventArgs e)
        {
            SessionSettings settings = new SessionSettings();
            settings.AuthType = AuthType.Ptc;
            settings.PtcUsername = ((TextBox)((StackPanel)((Button)sender).Parent).FindName("PTCUsername")).Text;
            settings.PtcPassword = ((TextBox)((StackPanel)((Button)sender).Parent).FindName("PTCPassword")).Text;
            Init(sender, settings);

        }

        private void GoogleLogin_Click(object sender, RoutedEventArgs e)
        {
            SessionSettings settings = new SessionSettings();
            settings.AuthType = AuthType.Google;
            settings.GoogleUsername = ((TextBox)((StackPanel)((Button)sender).Parent).FindName("GoogleEmail")).Text;
            settings.GooglePassword = ((TextBox)((StackPanel)((Button)sender).Parent).FindName("GooglePassword")).Text;
            Init(sender, settings);
        }

        private void Init(object sender, SessionSettings settings)
        {
            string status = "";
            StartLoading();

            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += new DoWorkEventHandler(
            delegate (object o, DoWorkEventArgs e)
            {
                Session session = SignIn(sender, settings);
                if (session != null)
                {
                    status = CheckBan(session);
                }
            });

            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate (object o, RunWorkerCompletedEventArgs e)
            {
                StopLoading();
                if (status == "Banned")
                {
                    if (MessageBox.Show("It seems that you account has been banned. Would you be willing to fill out a form to help us determine ban criteria?", "Banned :(", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start("https://goo.gl/forms/tjilsa8OApcjuhj82");
                    }
                }
                else if (status == "Not Banned")
                {
                    MessageBox.Show("Congratulations, your account has not been banned!", "Congratulations :)", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else if (status == ""){}
                else
                {
                    MessageBox.Show(status, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            bw.RunWorkerAsync();
        }

        private void StartLoading()
        {
            Loading.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Hidden;
            PTCBtn.ContextMenu.Visibility = Visibility.Hidden;
            GoogleBtn.ContextMenu.Visibility = Visibility.Hidden;
        }

        private void StopLoading()
        {
            Loading.Visibility = Visibility.Hidden;
            Main.Visibility = Visibility.Visible;
            PTCBtn.ContextMenu.Visibility = Visibility.Visible;
            GoogleBtn.ContextMenu.Visibility = Visibility.Visible;
        }

        private Session SignIn(object sender, SessionSettings settings)
        {
            Session session = null;
            if (settings.AuthType == AuthType.Ptc)
            {
                try
                {
                    session = Login.GetSession(settings.PtcUsername, settings.PtcPassword, LoginProvider.PokemonTrainerClub, 40.7127837, -74.005941);
                    session.Startup();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                try
                {
                    session = Login.GetSession(settings.GoogleUsername, settings.GooglePassword, LoginProvider.GoogleAuth, 40.7127837, -74.005941);
                    session.Startup();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            return session;
        }

        private string CheckBan(Session session)
        {
            try
            {
                var responseBytes = session.RpcClient.SendRemoteProcedureCall(new POGOProtos.Networking.Requests.Request
                {
                    RequestType = POGOProtos.Networking.Requests.RequestType.MarkTutorialComplete,
                    RequestMessage = new POGOProtos.Networking.Requests.Messages.MarkTutorialCompleteMessage
                    {
                        SendMarketingEmails = false,
                        SendPushNotifications = false
                    }.ToByteString()
                });
                return "Not Banned";
            }
            catch (Exception ex)
            {
                if (ex.Message == "There were only 1 responses, we expected 5.")
                {
                    session.Dispose();
                    return "Banned";
                }
                else
                {
                    session.Dispose();
                    MessageBox.Show("An error occurred: " + ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
                    return "Error: " + ex.Message;
                }
            }
        }
    }
}
