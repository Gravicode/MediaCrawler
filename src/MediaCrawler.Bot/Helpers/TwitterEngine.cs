using MediaCrawler.Models;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCrawler.Bot.Helpers
{
    public class TwitterEngine : ICrawlerBot, IDisposable
    {
        PyConverter converter;
        public TwitterEngine()
        {
            Runtime.PythonDLL = AppConstants.PYTHON_DLLPATH;

            PythonEngine.Initialize();
            converter = new PyConverter();

            //XIncref is needed, or keep the PyObject
            converter.Add(PythonEngine.Eval("int").Handle, (args) => { return args.As<int>(); });
            converter.Add(PythonEngine.Eval("str").Handle, (args) => { return args.As<string>(); });
            converter.Add(PythonEngine.Eval("float").Handle, (args) => { return args.As<double>(); });
            converter.Add(PythonEngine.Eval("bool").Handle, (args) => { return args.As<bool>(); });
        }

        public void Dispose()
        {
            PythonEngine.Shutdown();
        }

        public async Task<List<CrawledContent>> StartCrawl(BotParameter param, CancellationToken token)
        {
            try
            {
                var result = new List<CrawledContent>();

                using (Py.GIL())
                {

                    //Converter for list, omit here
                    /*
                    tweetsPerQry = 100
                    maxTweets = 1000000
                    hashtag = "#mencatatindonesia"

                    
                    
                    maxId = -1
                    tweetCount = 0
                    while tweetCount < maxTweets:
	                    if(maxId <= 0):
		                    newTweets = api.search(q=hashtag, count=tweetsPerQry, result_type="recent", tweet_mode="extended")
	                    else:
		                    newTweets = api.search(q=hashtag, count=tweetsPerQry, max_id=str(maxId - 1), result_type="recent", tweet_mode="extended")

	                    if not newTweets:
		                    print("Tweet Habis")
		                    break
	
	                    for tweet in newTweets:
		                    print(tweet.full_text.encode('utf-8'))
		
	                    tweetCount += len(newTweets)	
	                    maxId = newTweets[-1].id
                    */
                    dynamic tweepy = Py.Import("tweepy");
                    var tweetsPerQry = 100;
                    var maxTweets = 1000000;
                    var query = string.Empty;
                    if (!string.IsNullOrEmpty(param.Keyword))
                    {
                        query = param.Keyword;
                    }
                    else if (param.HashTag != null)
                        query = string.Join(",", param.HashTag);
                    
                    double maxId = -1;
                    var tweetCount = 0;
                    dynamic newTweets;
                    //var authentication = tweepy.OAuthHandler(AppConstants.twitter_consumer_key, AppConstants.twitter_consumer_secret);
                    //authentication.set_access_token(AppConstants.twitter_access_token, AppConstants.twitter_access_secret);
                    var auth = tweepy.OAuth2BearerHandler(AppConstants.twitter_access_token);
                    var api = tweepy.API(auth);
                    //var api = tweepy.API(authentication);//, true, true);

                    //while (tweetCount < maxTweets)
                    {
                        if (maxId <= 0)
                        {
                            newTweets = api.search_tweets(query);//, tweetsPerQry, "recent", "extended");
                        }
                        else
                        {
                            newTweets = api.search_tweets(query);//, tweetsPerQry, (maxId - 1).ToString(), "recent", "extended");
                        }

                        if (newTweets == null)
                        {
                            Console.WriteLine("Tweet Habis");

                            //break;
                        }
                        
                        var count = 0;
                        foreach (var tweet in newTweets)
                        {
                            var newItem = new CrawledContent();
                            newItem.Content = (string)converter.Convert( tweet.text);
                            newItem.Ids = (string)converter.Convert(tweet.id_str);
                            result.Add(newItem);
                            count++;
                        }
                        tweetCount += count;

                        //maxId = (double)converter.Convert(newTweets[-1].id);
                    }


                }
                
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return default;
            }
        }
    }
}
