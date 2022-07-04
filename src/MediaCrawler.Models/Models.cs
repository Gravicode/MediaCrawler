namespace MediaCrawler.Models
{
#region bot
    public interface ICrawlerBot
    {
        public Task<List<CrawledContent>> StartCrawl(BotParameter param,CancellationToken token);

        
    }
    public class CrawledContent
    {
        public string Ids { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PostedDate { get; set; }
        public string Author { get; set; }
        public string[] Mentions { get; set; }
        public string[] Hashtags { get; set; }
        public string[] ImageUrls { get; set; }
        public string[] LinkUrls { get; set; }
        public string[] VideoUrls { get; set; }
        public string[] MediaUrls { get; set; }
        public MediaSources Source { get; set; }
    }

    public enum MediaSources { Twitter, FB, Web, Tiktok, Youtube, Other }
    public class BotParameter
    {
        public string Keyword { get; set; }
        public string[] HashTag { get; set; }
        public string[] Usernames { get; set; }
        
    }

    #endregion
}