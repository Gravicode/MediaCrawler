using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCrawler.Bot.Helpers
{
    public class AppConstants
    {
        public static string GrpcUrl = "";
        //key-pair
        public const string Authentication = "auth";
        public static string twitter_consumer_key;
        public static string twitter_consumer_secret;
        public static string twitter_access_key;
        public static string twitter_access_secret;
        public static string twitter_access_token;

        public const int FACE_WIDTH = 180;
        public const int FACE_HEIGHT = 135;
        public const string FACE_SUBSCRIPTION_KEY = "a068e60df8254cc5a187e3e8c644f316";
        public const string FACE_ENDPOINT = "https://southeastasia.api.cognitive.microsoft.com/";
        public static string SQLConn = "";
        public static string BlobConn { get; set; }
        public const string GemLic = "EDWG-SKFA-D7J1-LDQ5";

        public static string RedisCon { set; get; }
        public static string PYTHON_DLLPATH { set; get; } = "C:\\Users\\mifma\\AppData\\Local\\Programs\\Python\\Python38\\python38.dll";
    }
}
