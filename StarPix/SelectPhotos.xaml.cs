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
using System.Windows.Media.Effects;
using System.Windows.Markup;
using System.IO;
using System.Xml;

namespace StarPix
{
    /// <summary>
    /// Interaction logic for SelectPhotos.xaml
    /// </summary>
    public partial class SelectPhotos : Window
    {

        MainWindow mainWindow;
        private String currentCollection;
        public SelectPhotos(MainWindow mw)
        {
            mainWindow = mw;
            InitializeComponent();
            loadCollectionMenu();
            loadImagesToSelectPhotos();
        }

        private void loadImagesToSelectPhotos()
        {

            noPhotosLabel.Visibility = Visibility.Hidden;
            string cwd = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string solutionPath = cwd.Replace("\\bin\\Debug\\StarPix.exe", "");
            string directory = solutionPath + "\\images\\new_photos\\";
            string[] images = System.IO.Directory.GetFiles(directory);
            images = images.Where(F => ImageExtensions.Contains(System.IO.Path.GetExtension(F))).ToArray();
            foreach (string path in images)
            {
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                connection.Open();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                string fileName = getFileName(path);
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT ImagePath from Images WHERE ImagePath LIKE '%" + fileName + "%' ORDER BY ImagePath ASC", connection);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    addPhoto(path);
                }
            }

            if (selectListBox.Items.Count == 0)
            {
                noPhotosLabel.Visibility = Visibility.Visible;
            }


        }

        public string getFileName(string imgSource)
        {
            return imgSource.Substring(imgSource.LastIndexOf("\\") + 1);
        }

        private void loadCollectionMenu()
        {

            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Collection_Name from Collections ORDER BY Collection_Name ASC", connection);
            da.Fill(dt);
            foreach (DataRow myRow in dt.Rows)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Checked += new RoutedEventHandler(setCurrentCollection);
                radioButton.Content = myRow[0].ToString();
                collectionList.Items.Add(radioButton);
            }

        }

        public void setCurrentCollection(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.Content.ToString() == "All Photos")
            {

                currentCollection = null;
            }
            else
            {
                currentCollection = radioButton.Content.ToString();

            }

        }

        public static readonly List<string> ImageExtensions = new List<string> {
           ".jpg",
           ".JPG",
           ".jpe",
           ".JPE",
           ".bmp",
           ".BMP",
           ".gif",
           ".GIF",
           ".png",
           ".PNG",
           ".tif",
           ".TIF"
        };

        public void addPhoto(string path)
        {
            StackPanel sp = new StackPanel();
            Label label = new Label();
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.FontStyle = FontStyles.Italic;
            FontFamilyConverter ffc = new FontFamilyConverter();
            FontFamily f = ffc.ConvertFromString("Calibri") as FontFamily;
            label.FontFamily = f;
            Image img = new Image();
            sp.Children.Add(img);
            sp.Children.Add(label);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            img.ToolTip = "Press space to preview photo";
            img.Source = bitmap;
            label.Content = System.IO.Path.GetFileName(img.Source.ToString());
            img.Height = 128;
            img.Width = 135;
            selectListBox.Items.Add(sp);
        }

        private void spaceKeyDown(object sender, KeyEventArgs e)
        {
            StackPanel sp = (StackPanel)selectListBox.SelectedItem;
            Image img = sp.Children[0] as Image;
            if (e.Key == Key.Space)
            {
                ImagePopUp imgPopUp = new ImagePopUp(selectListBox);
                imgPopUp.Owner = this;
                //StackPanel parent = (StackPanel) img.Parent;
                //int imgIndex = parent.Children.IndexOf(img);
                string childXaml = XamlWriter.Save(img);

                //Load it into a new object:
                StringReader stringReader = new StringReader(childXaml);
                XmlReader xmlReader = XmlReader.Create(stringReader);
                Image clonedImage = (Image)XamlReader.Load(xmlReader);
                //clonedImage.Stretch = Stretch.None;
                clonedImage.Height = img.Source.Height;
                clonedImage.Width = img.Source.Width;
                imgPopUp.photoWindow.Title = "Preview of " + System.IO.Path.GetFileName(img.Source.ToString());
                //if (clonedImage.Height < imgPopUp.Height)
                //{
                //    imgPopUp.photoScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                //}
                //if (clonedImage.Width < imgPopUp.Width)
                //{
                //    imgPopUp.photoScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                //}
                imgPopUp.Height = clonedImage.Height * 1.2;
                imgPopUp.Width = clonedImage.Width * 1.2;
                imgPopUp.photoGrid.Children.Add(clonedImage);
                imgPopUp.ShowDialog();
            }
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (selectListBox.SelectedItems.Count == 0)
            {
                string messgeBoxText = "Unable to import photos. No photos were selected";
                string messageBoxCaption = "No Photos Selected.";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                MessageBoxResult rsltMessageBox = MessageBox.Show(messgeBoxText, messageBoxCaption, btnMessageBox, icnMessageBox);


            }
            else
            {
                string messgeBoxText;
                if (selectListBox.SelectedItems.Count == 1)
                {
                    messgeBoxText = "Are you sure you want to import 1 photo?";
                }
                else
                {
                    messgeBoxText = "Are you sure you want to import " + selectListBox.SelectedItems.Count + " photos?";

                }
                string messageBoxCaption = "Import Photos";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Question;

                MessageBoxResult rsltMessageBox = MessageBox.Show(messgeBoxText, messageBoxCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        this.Close();
                        ImportPhotos importPhotosWindow = new ImportPhotos(this, mainWindow);
                        importPhotosWindow.Show();
                        break;
                }
            }
        }

        public void getSelectedPhotos()
        {
            mainWindow._listbox.Items.Clear();
            foreach (StackPanel sp in selectListBox.SelectedItems)
            {
                Image i = sp.Children[0] as Image;
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                connection.Open();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                string relativeImagePath = mainWindow.getFileName(i.Source.ToString());
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT ImagePath from Images WHERE ImagePath = '" + relativeImagePath + "' ORDER BY ImagePath ASC", connection);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                    if (currentCollection == null)
                    {
                        String fileName = System.IO.Path.GetFileNameWithoutExtension(relativeImagePath);
                        String fileExtension = System.IO.Path.GetExtension(relativeImagePath);
                        DateTime insertedDate = DateTime.Now;
                        connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                        String sql = " INSERT INTO Images (ImagePath, ImageName, FileType, InsertedTime) VALUES ('" + relativeImagePath + "','" + fileName + "','" + fileExtension + "','" + insertedDate + "')";

                        OleDbCommand command = new OleDbCommand(sql, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        String fileName = System.IO.Path.GetFileNameWithoutExtension(relativeImagePath);
                        String fileExtension = System.IO.Path.GetExtension(relativeImagePath);
                        DateTime insertedDate = DateTime.Now;
                        connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                        String sql = " INSERT INTO Images (ImagePath, Collection, ImageName, FileType, InsertedTime) VALUES ('" + relativeImagePath + "','" + currentCollection+ "','" + fileName + "','" + fileExtension + "','" + insertedDate + "')";
                        OleDbCommand command = new OleDbCommand(sql, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    
                }
            }
            System.Threading.Thread.Sleep(1000);
            mainWindow.totalImageCounter = 0;
            if (currentCollection == null)
            {
                currentCollection = "All Photos";
            }
            mainWindow.currentCollectionName = currentCollection;
            mainWindow.refreshMainWindowImages(currentCollection, mainWindow.currentSort, mainWindow.currentSortType);
            foreach (RadioButton mwRb in mainWindow.collectionsList.Items)
            {
                if (mwRb.Content.ToString() == mainWindow.currentCollectionName)
                {
                    mwRb.IsChecked = true;
                }
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddNewCollection addNewCollection = new AddNewCollection(this);
            addNewCollection.Owner = this; 
            addNewCollection.ShowDialog();
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


                mainWindow.photoStatus.Text = "Showing " + mainWindow.totalImageCounter + " photos in " + mainWindow.currentCollectionName;
            }
        }

        private void selectAllButton_Click(object sender, RoutedEventArgs e)
        {

            selectListBox.SelectAll();
            selectListBox.Focus();
        }

        private void selectNoneButton_Click(object sender, RoutedEventArgs e)
        {
            selectListBox.SelectedIndex = -1; 
           
        }

    }
}
