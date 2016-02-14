using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace StarPix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int totalImageCounter;
        public string currentCollectionName;
        private int imageWidth;
        private int imageHeight;
        private bool checkSlider;
        private bool hasLoaded;
        //private bool isSearching;
        public string currentSort;
        public string currentSortType;
        public bool inSearchMode;

        public MainWindow()
        {
            //isSearching = false;
            imageWidth = 235;
            imageHeight = 126;
            checkSlider = false;
            InitializeComponent();
            loadCollectionMenu();
            RadioButton collection = (RadioButton) collectionsList.Items[0];
            currentCollectionName = collection.Content.ToString();
            currentSort = "InsertedTime";
            currentSortType = "ASC";
            loadImagesToMainScreen();
            hasLoaded = true; 
        }

       

        public void loadImagesToMainScreen()
        {

            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT ImagePath from Images ORDER BY InsertedTime ASC", connection);
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                string cwd = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string solutionPath = cwd.Replace("\\bin\\Debug\\StarPix.exe", "");
                string directory = solutionPath + "\\images\\existing_photos\\";
                string[] images = System.IO.Directory.GetFiles(directory);
                images = images.Where(F => ImageExtensions.Contains(System.IO.Path.GetExtension(F))).ToArray();

                foreach (string path in images)
                {
                    Uri relativeUri = new Uri(path);
                    String imageRelativePath = getFileName(relativeUri.AbsoluteUri);
                    String fileName = System.IO.Path.GetFileNameWithoutExtension(imageRelativePath);
                    String fileExtension = System.IO.Path.GetExtension(imageRelativePath);
                    DateTime insertedDate = DateTime.Now;
                    connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                    String sql = " INSERT INTO Images (ImagePath, ImageName, FileType, InsertedTime) VALUES ('" + imageRelativePath + "','" + fileName + "','" + fileExtension + "','" + insertedDate + "')";
                    OleDbCommand command = new OleDbCommand(sql, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            System.Threading.Thread.Sleep(1000);
            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            dt = new DataTable();
            ds = new DataSet();
            ds.Tables.Add(dt);
            da = new OleDbDataAdapter("SELECT ImagePath from Images ORDER BY InsertedTime ASC", connection);
            da.Fill(dt);

            foreach (DataRow myRow in dt.Rows)
            {
                string cwd = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string solutionPath = cwd.Replace("\\bin\\Debug\\StarPix.exe", "");
                string filePath = solutionPath + myRow[0].ToString();
                addPhoto(filePath);
            }
            

            photoStatus.Text = "Showing " + totalImageCounter + " photos in " + currentCollectionName;
        }

        public string getFileName(string imgSource)
        {
            return imgSource.Substring(imgSource.LastIndexOf("images") - 1);
        }


        public void loadCollectionMenu()
        {

            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Collection_Name from Collections Order by Collection_Name ASC", connection);
            da.Fill(dt);

            foreach (DataRow myRow in dt.Rows)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Content = myRow[0].ToString();
                radioButton.Checked += new RoutedEventHandler(RadioButton_Checked_1);
                collectionsList.Items.Add(radioButton);
                MenuItem menuItem = new MenuItem();
                menuItem.Header = myRow[0].ToString();
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

            try
            {
                StackPanel sp = new StackPanel();
                TextBlock label = new TextBlock();
                TextBlock label2 = new TextBlock();
                label.TextAlignment = TextAlignment.Center;
                label.FontStyle = FontStyles.Italic;
                label.Margin = new Thickness(5);
                label2.TextAlignment = TextAlignment.Center;
                label2.FontStyle = FontStyles.Italic;
                FontFamilyConverter ffc = new FontFamilyConverter();
                FontFamily f = ffc.ConvertFromString("Calibri") as FontFamily;

                Image img = new Image();
                sp.Children.Add(img);
                sp.Children.Add(label);
                sp.MouseDown += new MouseButtonEventHandler(image_click);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(path);
                bitmap.EndInit();
                img.Source = bitmap;
                if (currentSort == "InsertedTime")
                {
                    string fileName = System.IO.Path.GetFileName(img.Source.ToString());
                    label.Inlines.Add(fileName);
                    label2.Text = "2015";
                    label2.FontWeight = FontWeights.Bold;
                    sp.Children.Add(label2);



                }
                else if (currentSort == "ImageName")
                {
                    string fileName = System.IO.Path.GetFileName(img.Source.ToString());
                    label.Inlines.Add(new Run(fileName[0].ToString().ToUpper()) { FontWeight = FontWeights.Bold });
                    label.Inlines.Add(fileName.Substring(1));

                }

                else if (currentSort == "FileType")
                {
                    string fileName = System.IO.Path.GetFileName(img.Source.ToString());
                    label.Inlines.Add(fileName.Substring(0, fileName.IndexOf(".")));
                    label.Inlines.Add(new Run(fileName.Substring(fileName.LastIndexOf('.')).ToUpper()) { FontWeight = FontWeights.Bold });


                }
                img.Height = imageHeight;
                img.Width = imageWidth;
                img.ToolTip = "Press space to preview photo";

                _listbox.Items.Add(sp);
                totalImageCounter++;
            }
            catch(FileNotFoundException)
            { }
        }

        private void removePhotoContextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_listbox.SelectedItems.Count == 1)
            {
                String messageBoxText = "Are you sure you want to delete the selected photo? This action is permanent.";
                String caption = "Delete Photo";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        deletePhotos(_listbox);
                        break;
                }
            }

            else
            {
                String messageBoxText = "Are you sure you want to delete " + _listbox.SelectedItems.Count + " photos? This action is permanent.";
                String caption = "Delete Photos";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        deletePhotos(_listbox);
                        break;
                }
            }
        }

        private void deletePhotos(ListBox listbox)
        {
            using (new WaitCursor())
            {
                foreach (StackPanel sp in listbox.SelectedItems)
                {
                    Image image = (Image) sp.Children[0];
                    string imagePath = getFileName(image.Source.ToString());
                    OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                    String sql = " DELETE FROM Images WHERE ImagePath = ?";
                    OleDbCommand command = new OleDbCommand(sql, connection);
                    command.Parameters.AddWithValue("@p1", imagePath);
                    connection.Open();
                    int i = command.ExecuteNonQuery();
                    connection.Close();


                    sql = " DELETE FROM Tags WHERE ImageID = ?";
                    command = new OleDbCommand(sql, connection);
                    command.Parameters.AddWithValue("@p1", imagePath);
                    connection.Open();
                    i = command.ExecuteNonQuery();

                }
                var selected = listbox.SelectedItems.Cast<Object>().ToArray();
                foreach (var item in selected)
                    listbox.Items.Remove(item);
                totalImageCounter--;

                if (listbox.Items.Count == 0)
                {
                    noPhotosLabel.Visibility = Visibility.Visible;
                }
                photoStatus.Text = "Showing " + listbox.Items.Count + " photos in " + currentCollectionName;

            }

        }

        private void keyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)
            {
                StackPanel sp = (StackPanel)_listbox.SelectedItem;
                Image img = (Image)sp.Children[0];
                ImagePopUp imgPopUp = new ImagePopUp(_listbox);
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
                clonedImage.ToolTip = null;
                imgPopUp.photoGrid.Children.Add(clonedImage);
                imgPopUp.ShowDialog();
            }
            else if (e.Key == Key.Delete)
            {
                StackPanel sp = (StackPanel)_listbox.SelectedItem;
                Image img = (Image)sp.Children[0];
                if (_listbox.SelectedItems.Count == 1)
                {
                    String messageBoxText = "Are you sure you want to delete the selected photo? This action is permanent.";
                    String caption = "Delete Photo";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Question;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            deletePhotos(_listbox);
                            break;
                    }
                }

                else
                {
                    String messageBoxText = "Are you sure you want to delete " + _listbox.SelectedItems.Count + " photos? This action is permanent.";
                    String caption = "Delete Photos";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Question;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            deletePhotos(_listbox);
                            break;
                    }
                }

            }
            else if (e.Key == Key.T && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                if (_listbox.SelectedItems.Count > 1)
                {
                    bool tagsExist = false;
                    String imageUid;
                    OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                    connection.Open();
                    foreach (StackPanel sp in _listbox.SelectedItems)
                    {
                        Image image = sp.Children[0] as Image;
                        imageUid = getFileName(image.Source.ToString());
                        DataTable dt = new DataTable();
                        DataSet ds = new DataSet();
                        ds.Tables.Add(dt);
                        OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Tags WHERE ImageID=" + "'" + imageUid + "'", connection);
                        da.Fill(dt);
                        if (dt.Rows.Count == 1)
                        {

                            tagsExist = true;

                        }
                    }

                    if (tagsExist == true)
                    {
                        String messageBoxText = "At least one of the selected photos have existing tags. Do you wish to re-assign all the selected photos with new tags?";
                        String caption = "Photo(s) Already Have Tags";
                        MessageBoxButton button = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                EnterTags enterTags = new EnterTags(_listbox);
                                enterTags.Owner = this;
                                enterTags.ShowDialog();
                                break;
                        }
                    }
                    else
                    {
                        EnterTags enterTags = new EnterTags(_listbox);
                        enterTags.Owner = this;
                        enterTags.ShowDialog();
                    }
                }
                else
                {
                    EnterTags enterTags = new EnterTags(_listbox);
                    enterTags.Owner = this;
                    enterTags.ShowDialog();
                }


            }
        }

        private void image_click(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            Image img = (Image)sp.Children[0];
       
            //Console.WriteLine(img.Source.Width);
            //Console.WriteLine(img.Source.Height);
            //img.KeyDown += new KeyEventHandler(deleteKeyDown);

            if (e.ChangedButton == MouseButton.Right)
            {
                ContextMenu contextMenu = new ContextMenu();
                MenuItem addTags = new MenuItem();
                MenuItem removeTags = new MenuItem();
                addTags.Click += new RoutedEventHandler(TagContextButton_Click);
                removeTags.Click += new RoutedEventHandler(RemoveTagContextButton_Click);
                MenuItem collectionsContextMenu = new MenuItem();
                MenuItem deletePhoto = new MenuItem();
                deletePhoto.Click += new RoutedEventHandler(removePhotoContextButton_Click);
                if (_listbox.SelectedItems.Count > 1)
                {
                    addTags.Header = "Tag " + _listbox.SelectedItems.Count + " Photos";
                    removeTags.Header = "Remove Tags from Photos";
                    deletePhoto.Header = "Delete " + _listbox.SelectedItems.Count + " Photos";
                    collectionsContextMenu.Header = "Add " + _listbox.SelectedItems.Count + " Photos to Collection";


                }
                else
                {
                    addTags.Header = "Tag Photo";
                    removeTags.Header = "Remove Tags From Photo";
                    deletePhoto.Header = "Delete Photo";
                    collectionsContextMenu.Header = "Add Photo to Collection";

                }

                contextMenu.Items.Add(addTags);

                OleDbConnection connection_tag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                connection_tag.Open();
                DataTable dt_tag = new DataTable();
                DataSet ds_tag = new DataSet();
                ds_tag.Tables.Add(dt_tag);
                string individualImagePath_tag = getFileName(img.Source.ToString());

                OleDbDataAdapter da_tag = new OleDbDataAdapter("SELECT Tag from Tags Where ImageID = '" + individualImagePath_tag + "'", connection_tag);
                da_tag.Fill(dt_tag);

                if (dt_tag.Rows.Count != 0)
                {
                    contextMenu.Items.Add(removeTags);
                }


                    contextMenu.Items.Add(deletePhoto);
                _listbox.ContextMenu = contextMenu;

                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                connection.Open();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT Collection_Name from Collections ORDER BY Collection_Name ASC", connection);
                da.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    contextMenu.Items.Add(collectionsContextMenu);

                    string individualImagePath = getFileName(img.Source.ToString());
                    connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                    connection.Open();
                    DataTable dt2 = new DataTable();
                    DataSet ds2 = new DataSet();
                    ds2.Tables.Add(dt2);
                    OleDbDataAdapter da2 = new OleDbDataAdapter("SELECT Collection from Images Where ImagePath = '" + individualImagePath + "'", connection);
                    da2.Fill(dt2);

                    foreach (DataRow myRow in dt.Rows)
                    {
                        foreach (DataRow myRow2 in dt2.Rows)
                        {
                            if (!myRow[0].Equals(myRow2[0]))
                            {
                                RadioButton radioButton = new RadioButton();
                                radioButton.Content = myRow[0].ToString();
                                MenuItem menuItem = new MenuItem();
                                menuItem.Click += new RoutedEventHandler(CollectionContextButton_Click);
                                menuItem.Header = myRow[0].ToString();
                                collectionsContextMenu.Items.Add(menuItem);

                            }

                        }

                    }

                    foreach (DataRow myRow in dt2.Rows)
                    {
                        if (myRow[0].ToString() != "")
                        {
                            MenuItem removePhotoFromCollection = new MenuItem();
                            if (_listbox.SelectedItems.Count > 1)
                            {
                                removePhotoFromCollection.Header = "Remove " + _listbox.SelectedItems.Count + " Photos from Collection";
                                removePhotoFromCollection.Click += new RoutedEventHandler(removePhotoFromCollection_Click);

                            }
                            else
                            {
                                removePhotoFromCollection.Header = "Remove Photo from Collection";
                                removePhotoFromCollection.Click += new RoutedEventHandler(removePhotoFromCollection_Click);

                            }
                            contextMenu.Items.Add(removePhotoFromCollection);

                        }
                    }
                }
            }
        }

        private void RemoveTagContextButton_Click(object sender, RoutedEventArgs e)
        {
            String messageBoxText;
            String caption;
            if (_listbox.SelectedItems.Count > 1)
            {

                messageBoxText = "Are you sure you want to remove all tags from " + _listbox.SelectedItems.Count + " photos?";
                caption = "Remove Tags From Photos";

            }
            else
            {
                messageBoxText = "Are you sure you want to remove all tags from the selected photo?";
                caption = "Remove Tags From Photo";

            }

            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    removeTagsFromPhotos(_listbox);
                    break;

            }
        }

        private void removeTagsFromPhotos(ListBox listbox)
        {
            using (new WaitCursor())
            {
                foreach (StackPanel sp in listbox.SelectedItems)
                {
                    Image image = (Image)sp.Children[0];
                    string imagePath = getFileName(image.Source.ToString());
                    OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                    String sql = " DELETE FROM Tags WHERE ImageID = ?";
                    OleDbCommand command = new OleDbCommand(sql, connection);
                    command.Parameters.AddWithValue("@p1", imagePath);
                    connection.Open();
                    int i = command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        private void removePhotoFromCollection_Click(object sender, RoutedEventArgs e)
        {
            String messageBoxText;
            String caption;
            if (_listbox.SelectedItems.Count > 1)
            {

                messageBoxText = "Do you wish to remove the selected photos from their current collection(s)? The photos will remain under All Photos.";
                caption = "Remove Photos From Collection(s)";

            }
            else
            {
                messageBoxText = "Do you wish to remove the selected photo from its current collection? The photo will remain under All Photos.";
                caption = "Remove Photo From Collection";

            }

            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    removeCollectonFromImage(_listbox);
                    break;

            }
        }
        private void removeCollectonFromImage(ListBox listbox)
        {
            using (new WaitCursor())
            {
                foreach (StackPanel sp in listbox.SelectedItems)
                {
                    Image image = sp.Children[0] as Image;
                    string imagePath = getFileName(image.Source.ToString());
                    OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                    String sql = " UPDATE Images SET Collection = NULL WHERE ImagePath = ?";
                    OleDbCommand command = new OleDbCommand(sql, connection);
                    command.Parameters.AddWithValue("@p1", imagePath);
                    connection.Open();
                    int i = command.ExecuteNonQuery();
                    connection.Close();

                }
                if (currentCollectionName != "All Photos")
                {
                    var selected = listbox.SelectedItems.Cast<Object>().ToArray();
                    foreach (var item in selected)
                    {
                        _listbox.Items.Remove(item);
                        totalImageCounter--;
                        photoStatus.Text = "Showing " + totalImageCounter + " photos in " + currentCollectionName;

                    }

                }

                if (_listbox.Items.Count == 0)
                {

                    noPhotosLabel.Visibility = Visibility.Visible;

                }
            }
        }

        public void CollectionContextButton_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            string collectionName = menuItem.Header.ToString();
            StackPanel currentStackPanel = _listbox.SelectedItem as StackPanel;
            Image currentImageSelected = currentStackPanel.Children[0] as Image; 
            string imagePath = getFileName(currentImageSelected.Source.ToString());
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            bool collectionExists = false;

            if (_listbox.SelectedItems.Count > 1)
            {
                foreach (StackPanel sp in _listbox.SelectedItems)
                {
                    Image image = sp.Children[0] as Image;
                    string individualImagePath = getFileName(image.Source.ToString());
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Images WHERE ImagePath=" + "'" + imagePath + "'", connection);
                    da.Fill(dt);
                    foreach (DataRow myRow in dt.Rows)
                    {
                        string dbCollectionName = myRow[2].ToString();
                        if (dbCollectionName.Length != 0)
                        {
                            collectionExists = true; 

                        }

                    }       
                }
                if (collectionExists == true)
                {
                    String messageBoxText = "At least one of the selected photos belongs to an existing collection. Do you wish to re-assign all the selected photos to the collection " + collectionName + "?";
                    String caption = "Photo(s) Already Assigned to an Existing Collection";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            updateMultipleImagesWithCollection(collectionName, _listbox);
                            break;

                    }

                }
                else
                {

                    updateMultipleImagesWithCollection(collectionName, _listbox);
                }

            }
            else
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Images WHERE ImagePath=" + "'" + imagePath + "'", connection);
                da.Fill(dt);

                foreach (DataRow myRow in dt.Rows)
                {
                    string dbCollectionName = myRow[2].ToString();
                    if (dbCollectionName.Length == 0)
                    {
                        updateImageWithCollection(collectionName, imagePath, currentStackPanel);

                    }
                    else if (dbCollectionName == collectionName)
                    {

                        String messageBoxText = "This photo already belongs to this collection. Please select another collection if you wish to re-assign the photo.";
                        String caption = "Photo Already Belongs to This Collection";
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Error;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                    }
                    else
                    {
                        String messageBoxText = "This photo currently belongs to the collection " + dbCollectionName + ". Do you wish to re-assign it to the collection " + collectionName + "?";
                        String caption = "Photo Already Assigned to an Existing Collection";
                        MessageBoxButton button = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                updateImageWithCollection(collectionName, imagePath, currentStackPanel);
                                break;

                        }

                    }

                }
            }
        }
        private void updateImageWithCollection(string collectionName, string imagePath, StackPanel sp)
        {
            Image image = sp.Children[0] as Image; 
            using (new WaitCursor())
            {
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                String sql = " UPDATE Images SET Collection = ? WHERE ImagePath = ?";
                OleDbCommand command = new OleDbCommand(sql, connection);
                command.Parameters.AddWithValue("@p1", collectionName);
                command.Parameters.AddWithValue("@p2", imagePath);
                connection.Open();
                int i = command.ExecuteNonQuery();
                if (currentCollectionName != "All Photos")
                {

                    _listbox.Items.Remove(sp);
                    totalImageCounter--;
                    photoStatus.Text = "Showing " + totalImageCounter + " photos in " + currentCollectionName;

                }
                connection.Close();

                if (_listbox.Items.Count == 0)
                {

                    noPhotosLabel.Visibility = Visibility.Visible;

                }
            }
        }

        private void updateMultipleImagesWithCollection(string collectionName, ListBox imageList)
        {
            using (new WaitCursor())
            {
                foreach (StackPanel sp in imageList.SelectedItems)
                {
                    Image image = sp.Children[0] as Image;
                    string individualImagePath = getFileName(image.Source.ToString());
                    OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                    String sql = " UPDATE Images SET Collection = ? WHERE ImagePath = ?";
                    OleDbCommand command = new OleDbCommand(sql, connection);
                    command.Parameters.AddWithValue("@p1", collectionName);
                    command.Parameters.AddWithValue("@p2", individualImagePath);
                    connection.Open();
                    int i = command.ExecuteNonQuery();
                    connection.Close();

                }

                if (currentCollectionName != "All Photos")
                {
                    var selected = _listbox.SelectedItems.Cast<Object>().ToArray();
                    foreach (var item in selected)
                    {
                        _listbox.Items.Remove(item);
                        totalImageCounter--;
                    }

                }
                photoStatus.Text = "Showing " + totalImageCounter + " photos in " + currentCollectionName;

                if (_listbox.Items.Count == 0)
                {

                    noPhotosLabel.Visibility = Visibility.Visible;

                }
            }
        }

        private void searchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            if (searchBox.Text == "Search photos by tags")
            {
                searchBox.Text = "";
                searchBox.Foreground = Brushes.Black;
                searchBox.FontStyle = FontStyles.Normal;
            }
        }

        private void searchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            if (searchBox.Text == "")
            {
                searchBox.Text = "Search photos by tags";
                searchBox.Foreground = Brushes.Gray;
                searchBox.FontStyle = FontStyles.Italic;
            }
        }

        private void sortByDate_Click(object sender, RoutedEventArgs e)
        {
            _listbox.Items.Clear();
            totalImageCounter = 0;
            currentSort = "InsertedTime";
            if (inSearchMode)
            {
                searchQuery(currentSort, currentSortType);

            }
            else
            {
                refreshMainWindowImages(currentCollectionName, currentSort, currentSortType);
            }

        }

        private void sortByName_Click(object sender, RoutedEventArgs e)
        {
            _listbox.Items.Clear();
            totalImageCounter = 0;
            currentSort = "ImageName";
            if (inSearchMode)
            {
                searchQuery(currentSort, currentSortType);
            }
            else
            {

                refreshMainWindowImages(currentCollectionName, currentSort, currentSortType);
            }

        }

        private void sortByFileType_Click(object sender, RoutedEventArgs e)
        {
            _listbox.Items.Clear();
            totalImageCounter = 0;
            currentSort = "FileType";
            if (inSearchMode)
            {
                searchQuery(currentSort, currentSortType);

            }
            else
            {
                refreshMainWindowImages(currentCollectionName, currentSort, currentSortType);
            }

        }

        private void sortAscending_Click(object sender, RoutedEventArgs e)
        {
            _listbox.Items.Clear();
            totalImageCounter = 0;
            currentSortType = "ASC";
            if (inSearchMode)
            {
                searchQuery(currentSort, currentSortType);

            }
            else
            {
                refreshMainWindowImages(currentCollectionName, currentSort, currentSortType);
            }

        }

        private void sortDescending_Click(object sender, RoutedEventArgs e)
        {
            _listbox.Items.Clear();
            totalImageCounter = 0;
            currentSortType = "DESC";
            if (inSearchMode)
            {
                searchQuery(currentSort, currentSortType);

            }
            else
            {
                refreshMainWindowImages(currentCollectionName, currentSort, currentSortType);
            }

        }

        private void importPhotos_Click(object sender, RoutedEventArgs e)
        {
            SelectPhotos selectPhotosWindow = new SelectPhotos(this);
            selectPhotosWindow.Owner = this;
            selectPhotosWindow.ShowDialog();
        }

        private void facebookButton_Click(object sender, RoutedEventArgs e)
        {
            if (_listbox.SelectedItems.Count == 0)
            {
                string messgeBoxText = "No photos were selected for Facebook upload. Please select photos first.";
                string messageBoxCaption = "Upload Photos to Facebook";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;
                MessageBoxResult rsltMessageBox = MessageBox.Show(messgeBoxText, messageBoxCaption, btnMessageBox, icnMessageBox);


            }
            else{
                FacebookLogin fbLogin = new FacebookLogin(this);
                fbLogin.Owner = this;
                fbLogin.ShowDialog();
                _listbox.SelectedIndex = -1;

            }
        }

     
        private void TagContextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_listbox.SelectedItems.Count > 1)
            {
                bool tagsExist = false;
                String imageUid;
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                connection.Open();
                foreach (StackPanel sp in _listbox.SelectedItems)
                {
                    Image image = sp.Children[0] as Image;
                    imageUid = getFileName(image.Source.ToString());
                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Tags WHERE ImageID=" + "'" + imageUid + "'", connection);
                    da.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {

                        tagsExist = true;

                    }
                }

                if (tagsExist == true)
                {
                    String messageBoxText = "At least one of the selected photos have existing tags. Do you wish to re-assign all the selected photos with new tags?";
                    String caption = "Photo(s) Already Have Tags";
                    MessageBoxButton button = MessageBoxButton.YesNo;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            EnterTags enterTags = new EnterTags(_listbox);
                            enterTags.Owner = this;
                            enterTags.ShowDialog();
                            break;
                    }
                }
                else
                {
                    EnterTags enterTags = new EnterTags(_listbox);
                    enterTags.Owner = this;
                    enterTags.ShowDialog();
                }
            }
            else
            {
                EnterTags enterTags = new EnterTags(_listbox);
                enterTags.Owner = this;
                enterTags.ShowDialog();
            }
        }


        private void manageCollections_Click(object sender, RoutedEventArgs e)
        {
            CollectionManagement manageCollections = new CollectionManagement(this);
            manageCollections.Owner = this;
            manageCollections.ShowDialog();

        }

        public void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if (hasLoaded == true)
            {
                sort.IsEnabled = true;
                photoSizeSlider.IsEnabled = true;
                noEntryLabel.Visibility = Visibility.Collapsed;
                totalImageCounter = 0;
                searchBar.Text = "Search photos by tags";
                searchBar.Foreground = Brushes.Gray;
                searchBar.FontStyle = FontStyles.Italic;
                _listbox.Focus();
                RadioButton menuItem = sender as RadioButton;
                _listbox.Items.Clear();
                totalImageCounter = 0; 
                refreshMainWindowImages(menuItem.Content.ToString(), currentSort, currentSortType);
                currentCollectionName = (menuItem.Content.ToString());
                photoStatus.Text = "Showing " + totalImageCounter + " photos in " + menuItem.Content;
            }

        }


        public void refreshMainWindowImages(string collectionName, string sort, string currentSortType)
        {
            noPhotosLabel.Visibility = Visibility.Collapsed;

            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            if (collectionName == "All Photos")
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Images ORDER BY " + sort + " " + currentSortType, connection);
                da.Fill(dt);
            }
            else
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Images WHERE Collection = '" + collectionName + "' ORDER BY " + sort + " " + currentSortType, connection);
                da.Fill(dt);

            }
            if (dt.Rows.Count == 0)
            {
                noPhotosLabel.Visibility = Visibility.Visible;

            }

            else
            {

                foreach (DataRow myRow in dt.Rows)
                {
                    string imageUrl = myRow[1].ToString();
                    string cwd = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string solutionPath = cwd.Replace("\\bin\\Debug\\StarPix.exe", "");
                    string path = solutionPath + imageUrl;
                    addPhoto(path);

                }

            }
            photoStatus.Text = "Showing " + totalImageCounter + " photos in " + currentCollectionName;


        }

        private void searchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (searchBar.Text.Length != 0)
            {
                if (e.Key == Key.Enter)
                {
                    searchQuery(currentSort, currentSortType);
                }
            }
            else
            {
                if (e.Key == Key.Enter)
                {
                    inSearchMode = false;
                    _listbox.Items.Clear();
                    noPhotosLabel.Visibility = Visibility.Collapsed;
                    noEntryLabel.Visibility = Visibility.Collapsed;
                    totalImageCounter = 0;
                    refreshMainWindowImages(currentCollectionName, currentSort, currentSortType);

                }

            }
        }

        public void searchQuery(string sort, string currentSortType)
        {

            inSearchMode = true;
            noPhotosLabel.Visibility = Visibility.Collapsed;
            noEntryLabel.Visibility = Visibility.Collapsed;
            //isSearching = true;
            _listbox.Items.Clear();
            totalImageCounter = 0;
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Tags INNER JOIN Images ON Tags.ImageID = Images.ImagePath WHERE Tag Like '%" + searchBar.Text.ToString() + "%' ORDER BY " + currentSort + " " + currentSortType, connection);
            da.Fill(dt);
            foreach (DataRow myRow in dt.Rows)
            {
                String tags = myRow[1].ToString();
                String imageUrl = myRow[2].ToString();
                String cwd = System.Reflection.Assembly.GetExecutingAssembly().Location;
                String solutionPath = cwd.Replace("\\bin\\Debug\\StarPix.exe", "");
                String path = solutionPath + imageUrl;
                addPhoto(path);
            }
            photoStatus.Text = "Showing " + totalImageCounter + " photos for keyword: '" + searchBar.Text + "'";

            if (_listbox.Items.Count == 0)
            {
                noEntryLabel.Visibility = Visibility.Visible;
            }

        }
        

        private void photoSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (photoSizeSlider.Value == 10 && checkSlider)
            {
                imageWidth = 235;
                imageHeight = 126;
                _listbox.Items.Clear();
                totalImageCounter = 0;
                if (inSearchMode)
                {
                    searchQuery(currentSort, currentSortType);
                }
                else
                {
                    refreshMainWindowImages(currentCollectionName, currentSort, currentSortType);
                }
            }
            else if (photoSizeSlider.Value == 5)
            {
                checkSlider = true;
                imageWidth = 170;
                imageHeight = 100;
                _listbox.Items.Clear();
                totalImageCounter = 0;
                if (inSearchMode)
                {
                    searchQuery(currentSort, currentSortType);
                }
                else
                {
                    refreshMainWindowImages(currentCollectionName, currentSort, currentSortType);
                }
            }
            else if (photoSizeSlider.Value == 0)
            {
                checkSlider = true;
                imageWidth = 100;
                imageHeight = 50;
                _listbox.Items.Clear();
                totalImageCounter = 0;
                if (inSearchMode)
                {
                    searchQuery(currentSort, currentSortType);
                }
                else
                {
                    refreshMainWindowImages(currentCollectionName, currentSort, currentSortType);
                }
            }
        }

        ListBox dragSource = null;

        private void _listbox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));


            if (data != null && _listbox.SelectedItems.Count > 0)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
                
            }

        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);
            // These Effects values are set in the drop target's
            // DragOver event handler.

            if (e.Effects.HasFlag(DragDropEffects.Move))
            {
                Mouse.SetCursor(Cursors.Arrow);
                DropShadowEffect effect = new DropShadowEffect();
                effect.ShadowDepth = 0;
                effect.Color = Colors.Gold;
                effect.Opacity = 1;
                effect.BlurRadius = 10;
                trashCan.Effect = effect;
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
                trashCan.Effect = null;

            }
            e.Handled = true;
        }
        //http://www.c-sharpcorner.com/uploadfile/dpatra/drag-and-drop-item-in-listbox-in-wpf/
        #region GetDataFromListBox(ListBox,Point)
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

        #endregion

        private void trashCan_Drop(object sender, DragEventArgs e)
        {
            ListBox listbox = _listbox;

            if (_listbox.SelectedItems.Count == 1)
            {
                String messageBoxText = "Are you sure you want to delete this photo? This action is permanent.";
                String caption = "Delete Photo";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        deletePhotos(_listbox);
                        break;
                }
                trashCan.Effect = null;

            }

            else
            {
                String messageBoxText = "Are you sure you want to delete " + _listbox.SelectedItems.Count + " photos? This action is permanent.";
                String caption = "Delete Photo";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        deletePhotos(_listbox);
                        break;
                }
                trashCan.Effect = null;

            }
        }

        private void mainWindowGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _listbox.SelectedIndex = -1;
        }
    }
}

