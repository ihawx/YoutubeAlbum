using System;
using System.Windows;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;


namespace YoutubeAlbum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = folderDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    label2.Content = folderDialog.SelectedPath;
                    textBlock.Text += "\nDownload path set to " + folderDialog.SelectedPath;
                    button1.Visibility = Visibility.Visible;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    label2.Content = "Wrong path";
                    break;
                default:
                    label2.Content = "Wrong path";
                    break;
            }
        }


        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            var ytbAPI = new YoutubeAPI(textBox.Text, textBlock);
            if (ytbAPI.InitializeSearch())
            {
                var trckList = new TrackList(ytbAPI.GetVideoDescription());
                textBlock.Text += "\n TRACK LIST: \n";
                foreach (var track in trckList.GetTrackList())
                {
                    textBlock.Text += "\nName: " + track.Key + " Time: " + track.Value;
                }
            }
        }
    }
}
