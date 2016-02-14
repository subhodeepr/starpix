using System;
using System.Collections.Generic;
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
using System.Data.OleDb;
using System.Data;
namespace StarPix
{
    /// <summary>
    /// Interaction logic for CollectionManagement.xaml
    /// </summary>
    public partial class CollectionManagement : Window
    {
        MainWindow mainWindow;
        public CollectionManagement(MainWindow mw)
        {
            mainWindow = mw; 
            InitializeComponent();
            loadCollectionList();

        }

        private void loadCollectionList()
        {

            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Collection_Name from Collections ORDER BY Collection_Name ASC", connection);
            da.Fill(dt);

            foreach(DataRow myRow in dt.Rows)
            {
                ListViewItem listItem = new ListViewItem();
                listItem.Content = myRow[0].ToString();
                collectionList.Items.Add(listItem);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddNewCollection addCollection = new AddNewCollection(this);
            addCollection.Owner = this;
            addCollection.ShowDialog();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            String messageBoxText;
            String caption; 
            if (collectionList.SelectedItems.Count == 1)
            {
                ListViewItem item = collectionList.SelectedItem as ListViewItem;
                messageBoxText = "Are you sure you want to remove the collection " + item.Content + "? Your existing photos will remain under All Photos.";
                caption = "Remove Collections";
            }
            else{
                messageBoxText = "Are you sure you want to remove the selected collections? Your existing photos will remain under All Photos.";
                caption = "Remove Collections";
            }
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    using (new WaitCursor())
                    {
                        foreach (ListViewItem item in collectionList.SelectedItems)
                        {
                            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                            String sql = " DELETE FROM Collections WHERE Collection_Name =?";
                            OleDbCommand command = new OleDbCommand(sql, connection);
                            connection.Open();
                            command.Parameters.AddWithValue("@p1", item.Content);
                            command.ExecuteNonQuery();
                            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                            sql = " UPDATE Images SET Collection = ? WHERE Collection = ?";
                            command = new OleDbCommand(sql, connection);
                            command.Parameters.AddWithValue("@p1", "");
                            command.Parameters.AddWithValue("@p2", item.Content);
                            connection.Open();
                            int i = command.ExecuteNonQuery();

                            foreach (RadioButton rb in mainWindow.collectionsList.Items)
                            {
                                if (rb.Content.ToString() == item.Content.ToString())
                                {
                                    if (rb.IsChecked == true)
                                    {
                                        foreach (RadioButton rb2 in mainWindow.collectionsList.Items)
                                        {
                                            if (rb2.Content.ToString() == "All Photos")
                                            {
                                                rb2.IsChecked = true;
                                                mainWindow._listbox.Items.Clear();
                                                mainWindow.totalImageCounter = 0;
                                                mainWindow.refreshMainWindowImages("All Photos", mainWindow.currentSort, mainWindow.currentSortType);
                                            }
                                        }

                                    }

                                }

                            }

                        }
                        var selected = collectionList.SelectedItems.Cast<Object>().ToArray();
                        foreach (var item in selected)
                            collectionList.Items.Remove(item);
                        break;
                    }

            }
            }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (collectionList.SelectedItem != null)
            {
                RenameCollection renameCollection = new RenameCollection(this);
                renameCollection.Owner = this;
                renameCollection.ShowDialog();
            }
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.collectionsList.Items.Clear();
            RadioButton allPhotos = new RadioButton();
            allPhotos.Content = "All Photos";
            allPhotos.Checked += new RoutedEventHandler(mainWindow.RadioButton_Checked_1);
            mainWindow.collectionsList.Items.Add(allPhotos);
            mainWindow.loadCollectionMenu();
            foreach (RadioButton rb in mainWindow.collectionsList.Items)
            {
                if (rb.Content.ToString() == mainWindow.currentCollectionName)
                {
                    rb.IsChecked = true; 

                }
                

            }
            mainWindow.photoStatus.Text = "Showing " + mainWindow.totalImageCounter + " photos in " + mainWindow.currentCollectionName;

        }

        private void Window_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            collectionList.SelectedIndex = -1; 
        }

    }
}
