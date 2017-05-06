using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeAlbum
{
    class YoutubeAPI
    {
        private string videoID { get; set; }
        private System.Windows.Controls.TextBlock textBlock { get; set; }
        public IList<Google.Apis.YouTube.v3.Data.Video> responseItems { get; set; }

        public YoutubeAPI(string videoID, System.Windows.Controls.TextBlock textBlock)
        {
            this.videoID = videoID;
            this.textBlock = textBlock;
        }

        public bool InitializeSearch()
        {
            textBlock.Text += "\nSearching for video " + videoID;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "REPLACE_ME",
                ApplicationName = this.GetType().ToString()
            });
            var videoRequest = youtubeService.Videos.List("snippet");
            videoRequest.Id = ExtractVideoId(videoID);
            var response = videoRequest.Execute();
            if (response.Items.Count == 0)
            {
                textBlock.Text += "\nSearch failed";
                return false;
            }
            else {
                textBlock.Text += "\nVideo found: " + response.Items[0].Snippet.Title;
                responseItems = response.Items;
                return true;
            }
        }

        public string GetVideoDescription()
        {
            if (responseItems == null)
            {
                return null;
            }
            else
            {
                return responseItems[0].Snippet.Description;
            }
        }

        private string ExtractVideoId(string text)
        {
            if (text.Contains("youtube.com/watch"))
            {
                return text.Substring(text.IndexOf("=") + 1, text.IndexOf("&") - (text.IndexOf("=") + 1));
            }
            else if (text.Contains("youtu.be"))
            {
                return text.Substring(text.IndexOf(".be/") + 4, text.IndexOf("?") - (text.IndexOf(".be/") + 4));
            }
            else
            {
                return text;
            }
        }
    }
}
