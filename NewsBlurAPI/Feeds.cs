using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace MetroBlur.NewsBlurAPI
{
    public class Feeds
    {
        static string baseURL = "http://newsblur.com/";

        /// <summary>
        /// Retrieve a list of feeds to which a user is actively subscribed.
        /// Includes the 3 unread counts (positive, neutral, negative), as well as optional favicons.
        /// </summary>
        /// <param name="includeFavicons">Include favicons inline. Since they can be time consuming to 
        /// download, you can optionally turn them off. Use /api/v1/feeds/favicons/ to retrieve the 
        /// favicons in a separate request. </param>
        /// <param name="flat">Returns a flat folder structure instead of nested folders. 
        /// Useful when displaying all folders in a single depth without recursive descent. </param>
        /// <param name="updateCounts">Forces recalculation of unread counts on all feeds.
        /// The preferred method is to call this endpoint without updated counts, then call 
        /// refresh_feeds to get updated counts. That way you can quickly show the user's 
        /// feeds, then update the counts. Turning this option on will lead to a slower load-time. </param>
        public static void GetFeeds(bool includeFavicons, bool flatP, bool updateCounts)
        {
            _GetFeeds(includeFavicons, flatP, updateCounts);
        }

        public static void GetFeeds()
        {
            _GetFeeds(false, false, false);
        }

        private static void _GetFeeds(bool includeFavicons, bool flatP, bool updateCounts)
        {
            JObject parameters = JObject.FromObject(
                new
                {
                    include_favicons = includeFavicons,
                    flat = flatP,
                    update_counts = updateCounts
                });

            var wc = new WebClient();
            wc.DownloadStringAsync(new Uri(baseURL + "reader/feeds"));
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);

        }

        static void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Debug.WriteLine(e.Result);
        }
    }
}
