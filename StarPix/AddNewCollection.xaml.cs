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
    /// Interaction logic for AddNewCollection.xaml
    /// </summary>
    public partial class AddNewCollection : Window
    {
        CollectionManagement collectionManagement;
        SelectPhotos selectPhotos; 

        public AddNewCollection(CollectionManagement cm)
        {
            collectionManagement = cm;
            InitializeComponent();
            collectionName.Focus();
            collectionName.SelectionStart = collectionName.Text.Length;
        }

        public AddNewCollection(SelectPhotos sf)
        {
            selectPhotos = sf;
            InitializeComponent();
            collectionName.Focus();
            collectionName.SelectionStart = collectionName.Text.Length;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Collection_Name from Collections", connection);
            da.Fill(dt);
            try
            {
                if (collectionName.Text.ToLower() == "all photos")
                {

                    throw new Exception("Collection Exists");

                }
                foreach (DataRow myRow in dt.Rows)
                {
                    if (myRow[0].ToString().ToLower() == collectionName.Text.ToLower())
                    {
                        throw new Exception("Collection Exists");

                    }

                }
                if (collectionManagement != null)
                {
                    if (string.IsNullOrWhiteSpace(collectionName.Text))
                    {
                        errorLabel.Content = "Please enter a valid name.";
                        errorLabel.Visibility = Visibility.Visible;
                        DispatcherTimer t = new DispatcherTimer();
                        //Set the timer interval to the length of the animation.
                        t.Interval = new TimeSpan(0, 0, 5);
                        t.Tick += (EventHandler)delegate (object snd, EventArgs ea)
                        {
                            // The animation will be over now, collapse the label.
                            errorLabel.Visibility = Visibility.Collapsed;
                            // Get rid of the timer.
                            ((DispatcherTimer)snd).Stop();
                        };
                        t.Start();

                    }
                    else {
                        connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                        String sql = " INSERT INTO Collections (Collection_Name) VALUES ('" + collectionName.Text + "')";
                        OleDbCommand command = new OleDbCommand(sql, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        ListViewItem item = new ListViewItem();
                        item.Content = collectionName.Text;
                        collectionManagement.collectionList.Items.Add(item);
                        collectionManagement.collectionList.Items.RemoveAt(0);
                        collectionManagement.collectionList.Items.SortDescriptions.Add(
                        new System.ComponentModel.SortDescription("Content",
                        System.ComponentModel.ListSortDirection.Ascending));
                        ListViewItem allPhotos = new ListViewItem();
                        allPhotos.Content = "All Photos";
                        allPhotos.IsEnabled = false;
                        collectionManagement.collectionList.Items.Insert(0, allPhotos);
                        this.Close();
                    }

                }

            
                else if (selectPhotos != null)
                {
                    connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                    String sql = " INSERT INTO Collections (Collection_Name) VALUES ('" + collectionName.Text + "')";
                    OleDbCommand command = new OleDbCommand(sql, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    RadioButton collection = new RadioButton();
                    collection.Content = collectionName.Text;
                    collection.Checked += new RoutedEventHandler(selectPhotos.setCurrentCollection);
                    selectPhotos.collectionList.Items.Add(collection);

                    selectPhotos.collectionList.Items.RemoveAt(2);
                    selectPhotos.collectionList.Items.SortDescriptions.Add(
                        new System.ComponentModel.SortDescription("Content",
                        System.ComponentModel.ListSortDirection.Ascending));
                    RadioButton allPhotos = new RadioButton();
                    allPhotos.Content = "All Photos";
                    collection.Checked += new RoutedEventHandler(selectPhotos.setCurrentCollection);
                    allPhotos.IsChecked = true; 
                    selectPhotos.collectionList.Items.Insert(2, allPhotos);
                    this.Close();

                }
            }
            catch (Exception)
            {
                errorLabel.Content = "A collection with that name already exists.";
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
        
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void collectionName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                okButton_Click(sender, e);
            }
        }
    }
}
