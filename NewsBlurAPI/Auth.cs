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
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace MetroBlur.NewsBlurAPI
{
    /// <summary>
    /// Handles authentication against the NewsBlur API
    /// </summary>
    public class Auth
    {
        static string BaseURL = "http://newsblur.com/api/";

        /// <summary>
        /// Contains user credentials
        /// </summary>
        private class AuthInfo
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public static void Login(string usrname, string passwd)
        {
            /*
             * Check if username was provided,
             * if not return an error
             */
            if (usrname == String.Empty)
            {
                throw new InvalidOperationException("Username missing");
            }
            /*
             * log into NewsBlur
             */
            else
            {
                // Check for password, if not password provided, ommit it in the request
                JObject parameters;
                if (passwd == String.Empty)
                {
                    parameters = JObject.FromObject(
                        new AuthInfo
                        {
                            username = usrname
                        });
                }
                else
                {
                    parameters = JObject.FromObject(
                        new AuthInfo
                        {
                            username = usrname,
                            password = passwd
                        });
                }

                WebClient wc = new WebClient();
                wc.UploadStringAsync(new Uri((BaseURL + "login")), "POST", parameters.ToString());
                wc.UploadStringCompleted += new UploadStringCompletedEventHandler(wc_loginComplete);
            }
        }

        static void wc_loginComplete(object sender, UploadStringCompletedEventArgs e)
        {
            Debug.WriteLine("Web service says: " + e.Result);
        }
    }


}