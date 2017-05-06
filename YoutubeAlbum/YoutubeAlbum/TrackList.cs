using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeAlbum
{
    class TrackList
    {
        private string videoDescription { get; set; }

        private Dictionary<string, string> trackList { get; set; }


        public TrackList(string videoDescription)
        {
            this.videoDescription = videoDescription;
            trackList = new Dictionary<string, string>();
        }

        public Dictionary<string, string> GetTrackList()
        {
            var newVideoDescription = videoDescription.Split('\n').Where(desc => desc.Contains(":") && !desc.Contains("http"));
            foreach (var desc in newVideoDescription)
            {
                int throwaway;
                if (int.TryParse(desc.Substring(desc.IndexOf(":") + 1, 2), out throwaway))
                {
                    var trackName = desc.Substring(desc.LastIndexOf(":") + 3);
                    string trackTime = "";
                    if (desc.Count(c => c == ':') == 1)
                    {
                        trackTime = desc.Substring(desc.IndexOf(":") - 2, desc.LastIndexOf(":") + 3 - (desc.IndexOf(":") - 2));
                    }
                    else
                    {
                        trackTime = desc.Substring(desc.IndexOf(":") - 1, desc.LastIndexOf(":") + 3 - (desc.IndexOf(":") - 1));
                    }
                    trackList.Add(trackName, trackTime);
                }
            }
            return trackList;
        }
    }
}
