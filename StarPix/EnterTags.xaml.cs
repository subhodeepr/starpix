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

namespace StarPix
{
    /// <summary>
    /// </summary>
    public partial class EnterTags : Window
    {
        bool tagsExist = false; 
        String imageUid;
        ListBox imageList; 
        public EnterTags(ListBox il)
        {
            imageList = il; 
            InitializeComponent();
            tagTextBox.Focus();
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
            connection.Open();
            if (imageList.SelectedItems.Count == 1)
            {
                StackPanel sp = (StackPanel)imageList.SelectedItem;
                Image singleImage = (Image)sp.Children[0];
                imageUid = getFileName(singleImage.Source.ToString());
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Tags WHERE ImageID=" + "'" + imageUid + "'", connection);
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    foreach (DataRow myRow in dt.Rows)
                    {
                        tagTextBox.Text = myRow[1].ToString();
                        tagsExist = true;

                    }

                }

            }
            if (imageList.SelectedItems.Count > 1)
            {
                foreach (StackPanel sp in imageList.SelectedItems)
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
                        foreach (DataRow myRow in dt.Rows)
                        {
                            tagsExist = true;

                        }

                    }

                }

            }
            tagTextBox.SelectionStart = tagTextBox.Text.Length;

        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            using (new WaitCursor())
            {
                if (tagsExist == false)
                {
                    foreach (StackPanel sp in imageList.SelectedItems)
                    {
                        Image image = (Image) sp.Children[0];
                        imageUid = getFileName(image.Source.ToString());
                        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                        String sql = " INSERT INTO Tags (Tag, ImageID) VALUES ('" + tagTextBox.Text + "', '" + imageUid + "')";
                        OleDbCommand command = new OleDbCommand(sql, connection);
                        connection.Open();
                        int i = command.ExecuteNonQuery();
                    }
                    this.Close();
                }
                else
                {

                    foreach (StackPanel sp in imageList.SelectedItems)
                    {
                        Image image = (Image)sp.Children[0];
                        OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StarPix.accdb");
                        imageUid = getFileName(image.Source.ToString());
                        DataTable dt = new DataTable();
                        DataSet ds = new DataSet();
                        ds.Tables.Add(dt);
                        OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Tags WHERE ImageID=" + "'" + imageUid + "'", connection);
                        da.Fill(dt);
                        if (dt.Rows.Count == 1)
                        {
                            String sql = " UPDATE Tags SET Tag = ? WHERE ImageID = ?";
                            OleDbCommand command = new OleDbCommand(sql, connection);
                            command.Parameters.AddWithValue("@p1", tagTextBox.Text);
                            command.Parameters.AddWithValue("@p3", imageUid);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            String sql = " INSERT INTO Tags (Tag, ImageID) VALUES ('" + tagTextBox.Text + "', '" + imageUid + "')";
                            OleDbCommand command = new OleDbCommand(sql, connection);
                            connection.Open();
                            int i = command.ExecuteNonQuery();

                        }
                    }

                }

                this.Close();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tagTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                okButton_Click(sender, e);
            }
        }

        private string getFileName(string imgSource)
        {
           return imgSource.Substring(imgSource.LastIndexOf("images")-1);
        }
    }
}
