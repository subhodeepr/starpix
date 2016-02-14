using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace StarPix
{
    /// <summary>
    /// Interaction logic for ImagePopUp.xaml
    /// </summary>
    public partial class ImagePopUp : Window
    {
        ListBox listBox;
        int counter;
        int counter2;
        public ImagePopUp(ListBox lb)
        {
            InitializeComponent();
            listBox = lb;
            counter = listBox.SelectedIndex;
            counter2 = 0;
        }

        private void photoWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                try
                {
                    //StackPanel sp = (StackPanel)listBox.SelectedItems[counter];
                    //Image firstImage = sp.Children[0] as Image;
                    StackPanel sp2;
                    if (listBox.SelectedItems.Count > 1)
                    {
                        sp2 = (StackPanel)listBox.SelectedItems[counter2 + 1];
                        counter2++;
                    }
                    else
                    {
                        sp2 = (StackPanel)listBox.Items[counter + 1];
                        counter++;
                    }
                    Image img = sp2.Children[0] as Image;
                    photoGrid.Children.RemoveAt(0);
                    ////Load it into a new object:
                    string childXaml = XamlWriter.Save(img);
                    StringReader stringReader = new StringReader(childXaml);
                    XmlReader xmlReader = XmlReader.Create(stringReader);
                    Image clonedImage = (Image)XamlReader.Load(xmlReader);
                    clonedImage.Stretch = Stretch.None;
                    clonedImage.Height = img.Source.Height;
                    clonedImage.Width = img.Source.Width;
                    photoWindow.Title = "Preview of " + System.IO.Path.GetFileName(img.Source.ToString());
                    this.Height = clonedImage.Height * 1.2;
                    this.Width = clonedImage.Width * 1.2;
                    clonedImage.ToolTip = null;
                    photoGrid.Children.Add(clonedImage);
                }
                catch (Exception)
                { }


            }
            else if (e.Key == Key.Left)
            {
                try
                {
                    //StackPanel sp = (StackPanel)listBox.SelectedItems[counter];
                    //Image firstImage = sp.Children[0] as Image;
                    StackPanel sp2;
                    if (listBox.SelectedItems.Count > 1)
                    {
                        sp2 = (StackPanel)listBox.SelectedItems[counter2 - 1];
                        counter2--;
                    }
                    else
                    {
                        sp2 = (StackPanel)listBox.Items[counter - 1];
                        counter--;
                    }
                    Image img = sp2.Children[0] as Image;
                    photoGrid.Children.RemoveAt(0);
                    ////Load it into a new object:
                    string childXaml = XamlWriter.Save(img);
                    StringReader stringReader = new StringReader(childXaml);
                    XmlReader xmlReader = XmlReader.Create(stringReader);
                    Image clonedImage = (Image)XamlReader.Load(xmlReader);
                    clonedImage.Stretch = Stretch.None;
                    clonedImage.Height = img.Source.Height;
                    clonedImage.Width = img.Source.Width;
                    photoWindow.Title = "Preview of " + System.IO.Path.GetFileName(img.Source.ToString());
                    this.Height = clonedImage.Height * 1.2;
                    this.Width = clonedImage.Width * 1.2;
                    clonedImage.ToolTip = null;
                    photoGrid.Children.Add(clonedImage);
                }
                catch (Exception)
                { 
                    
                }

            }
            if (e.Key == Key.Space)
            {
                this.Close(); 
            }
        }
    }
}
