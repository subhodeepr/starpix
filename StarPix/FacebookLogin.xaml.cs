using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StarPix
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class FacebookLogin : Window
    {
        MainWindow mainWindow; 
        //private String email;
        //private String password;

        public FacebookLogin(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw; 
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Credentials", connection);
            da.Fill(dt);
            foreach (DataRow myRow in dt.Rows)
            {
                if ((bool)myRow[3] == true)
                {
                    EmailTextbox.Text = myRow[1].ToString();
                    PasswordTextbox.Password = myRow[2].ToString();
                    saveCredentials.IsChecked = true; 

                }

            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            if (EmailTextbox.Text.Length == 0 || PasswordTextbox.Password.Length == 0)
            {
                errorLabel.Visibility = Visibility.Visible;
                DispatcherTimer t = new DispatcherTimer();
                //Set the timer interval to the length of the animation.
                t.Interval = new TimeSpan(0, 0, 5);
                t.Tick += (EventHandler)delegate(object snd, EventArgs ea)
                {
                    // The animation will be over now, collapse the label.
                    errorLabel.Visibility = Visibility.Collapsed;
                    // Get rid of the timer.
                    ((DispatcherTimer)snd).Stop();
                };
                t.Start();
            }

            else
            {
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                String sql = "UPDATE Credentials SET Username = " + "'" + EmailTextbox.Text + "'" + ", [Password] =" + "'" + PasswordTextbox.Password + "'" + ", isSaved=" + saveCredentials.IsChecked + " WHERE ID=1";
                OleDbCommand command = new OleDbCommand(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();

                this.Close();
                string messgeBoxText;
                if (mainWindow._listbox.SelectedItems.Count == 1)
                {
                    messgeBoxText = "Are you sure you want to upload 1 photo to Facebook?";
                }
                else
                {
                    messgeBoxText = "Are you sure you want to upload " + mainWindow._listbox.SelectedItems.Count + " photo(s) to Facebook?";

                }
                string messageBoxCaption = "Upload Photo(s) to Facebook";


                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Question;

                MessageBoxResult rsltMessageBox = MessageBox.Show(messgeBoxText, messageBoxCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        FacebookUploadProgress facebookUploadProgress = new FacebookUploadProgress(mainWindow);
                        facebookUploadProgress.ShowDialog();

                        break;

                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PasswordTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LoginButton_Click(sender, e);
            }
        }

        private void EmailTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LoginButton_Click(sender, e);
            }
        }

    }
}
