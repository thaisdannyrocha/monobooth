using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace PhotoBooth
{
    class UrlShortner
    {
        /// <summary>
        /// Encode a Web Uri in tinyURL.
        /// This will take any web address and submit it to the TinyURL service
        /// and return the newly created TinyURL
        /// </summary>
        /// <param name="UrlToEncode">The Url of the site to encode to a tinyurl</param>
        /// <returns></returns>
        public string encodeTinyUrl(Uri UrlToEncode)
        {
            System.Net.WebRequest tinyURLRequest = System.Net.WebRequest.Create(string.Format("http://tinyurl.com/api-create.php?url={0}", UrlToEncode.ToString()));
            WebResponse tinyUrlResponse = tinyURLRequest.GetResponse();
            System.IO.StreamReader responseReader = new System.IO.StreamReader(tinyUrlResponse.GetResponseStream());
            string tinyUrl = responseReader.ReadLine();
            tinyUrlResponse.Close();
            Console.WriteLine(tinyUrl);
            return tinyUrl;
        }

        /// <summary>
        /// Encode a Web Uri in bit.ly
        /// This will take any web address and submit it to the bit.ly service
        /// and return the newly created bit.ly 
        /// </summary>
        /// <param name="UrlToEncode">The Url of the site to encode to a bit.ly</param>
        /// <param name="apiKey">Your bit.ly API key</param>
        /// <param name="login">Your bit.ly login name</param>
        /// <returns></returns>
        public string encodeBitly(Uri UrlToEncode,string login, string apiKey)
        {
            System.Net.WebRequest BitlyRequest = System.Net.WebRequest.Create(string.Format("http://api.bitly.com/v3/shorten?login={0}&apiKey={1}&longUrl={2}&format=txt", login, apiKey, System.Web.HttpUtility.UrlPathEncode(UrlToEncode.ToString())));
            WebResponse BitlyResponse = BitlyRequest.GetResponse();
            System.IO.StreamReader responseReader = new System.IO.StreamReader(BitlyResponse.GetResponseStream());
            string Bitly = responseReader.ReadLine();
            BitlyResponse.Close();
            Console.WriteLine(Bitly);
            return Bitly;
        }



    }
}
