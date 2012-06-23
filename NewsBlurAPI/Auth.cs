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
using RestSharp;

namespace MetroBlur.NewsBlurAPI
{
    /// <summary>
    /// Handles authentication against the NewsBlur API
    /// </summary>
    public class Auth
    {
        static RestClient client = new RestClient("http://newsblur.com/api/");

        /// <summary>
        /// Contains user credentials
        /// </summary>
        public class AuthInfo
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
                AuthInfo info;
                if (passwd == String.Empty)
                {
                    info = new AuthInfo { username = usrname };
                }
                else
                {
                    info = new AuthInfo
                        {
                            username = usrname,
                            password = passwd
                        };
                }

                RestRequest request = new RestRequest("login", Method.POST);
                request.AddObject(info);
                client.ExecuteAsync(request, response =>
                {
                    Debug.WriteLine(response.Content);
                });
            }
        }
    }


}