using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using MediaCrawler.Bot.Helpers;
using MediaCrawler.Models;
using MediaCrawler.Tools;
using Microsoft.Extensions.Configuration;

namespace MediaCrawler.Bot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            ReadConfig();
            Setup();
            await TestTwitterAsync();
            Console.WriteLine("Hello, World!");
        }

        static async Task TestTwitterAsync()
        {
            var param = new BotParameter() { Keyword = "#jokowi" };
            var bot = new TwitterEngine();
            CancellationTokenSource source = new CancellationTokenSource();
            var hasil = await bot.StartCrawl(param, source.Token);
            Console.WriteLine("total :" + hasil.Count);
        }

        static void Setup()
        {
            //var channel = GrpcChannel.ForAddress(
            //  AppConstants.GrpcUrl, new GrpcChannelOptions
            //  {
            //      MaxReceiveMessageSize = 8 * 1024 * 1024, // 5 MB
            //      MaxSendMessageSize = 8 * 1024 * 1024, // 2 MB                
            //      HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            //  });
            //ObjectContainer.Register<GrpcChannel>(channel);
            //ObjectContainer.Register<DataCounterService>(new DataCounterService(channel));
            //ObjectContainer.Register<CCTVService>(new CCTVService(channel));
            //ObjectContainer.Register<GatewayService>(new GatewayService(channel));
        }

        static void ReadConfig()
        {
            try
            {
                var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: false);

                IConfiguration Configuration = builder.Build();

                MailService.MailUser = Configuration["MailSettings:MailUser"];
                MailService.MailPassword = Configuration["MailSettings:MailPassword"];
                MailService.MailServer = Configuration["MailSettings:MailServer"];
                MailService.MailPort = int.Parse(Configuration["MailSettings:MailPort"]);
                MailService.SetTemplate(Configuration["MailSettings:TemplatePath"]);
                MailService.SendGridKey = Configuration["MailSettings:SendGridKey"];
                MailService.UseSendGrid = true;


                SmsService.UserKey = Configuration["SmsSettings:ZenzivaUserKey"];
                SmsService.PassKey = Configuration["SmsSettings:ZenzivaPassKey"];
                SmsService.TokenKey = Configuration["SmsSettings:TokenKey"];

                AppConstants.GrpcUrl = Configuration["App:GrpcUrl"];
                AppConstants.twitter_consumer_key = Configuration["App:twitter_consumer_key"];
                AppConstants.twitter_consumer_secret = Configuration["App:twitter_consumer_secret"];
                AppConstants.twitter_access_key = Configuration["App:twitter_access_key"];
                AppConstants.twitter_access_secret = Configuration["App:twitter_access_secret"];
                AppConstants.twitter_access_token = Configuration["App:twitter_access_token"];



            }
            catch (Exception ex)
            {
                Console.WriteLine("read config failed:" + ex);
            }
            Console.WriteLine("Read config successfully.");
        }
    }
}