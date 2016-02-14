using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ImportPhotos.xaml
    /// </summary>
    public partial class ImportPhotos : Window
    {
        SelectPhotos selectPhotos;
        MainWindow mainWindow;
        BackgroundWorker worker;
        private bool isCancelled;

        public ImportPhotos(SelectPhotos sp, MainWindow mw)
        {
            InitializeComponent();
            selectPhotos = sp;
            mainWindow = mw;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(25);
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            importStatus.Value = e.ProgressPercentage;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!isCancelled)
            {
                using (new WaitCursor())
                {
                    selectPhotos.getSelectedPhotos();
                }
                Thread.Sleep(1000);
                this.Close();
                mainWindow.successLabel.Content = "Your photos have been successfully imported.";
                mainWindow.successLabel.Visibility = Visibility.Visible;
                DispatcherTimer t = new DispatcherTimer();
                //Set the timer interval to the length of the animation.
                t.Interval = new TimeSpan(0, 0, 6);
                t.Tick += (EventHandler)delegate (object snd, EventArgs ea)
                {
                    // The animation will be over now, collapse the label.
                    mainWindow.successLabel.Visibility = Visibility.Collapsed;
                    // Get rid of the timer.
                    ((DispatcherTimer)snd).Stop();
                };
                t.Start();

            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string messgeBoxText = "Are you sure you want to cancel the import?";
            string messageBoxCaption = "Cancel Import";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(messgeBoxText, messageBoxCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    worker.CancelAsync();
                    isCancelled = true; 
                    this.Close();
                    break;

            }
        }
    }
}
