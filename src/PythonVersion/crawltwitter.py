import tweepy

consumer_key = 'change with your consumer key'
consumer_secret = 'change with your consumer secret'
access_token = 'change with your access token'
access_secret = 'change with your access secret'
tweetsPerQry = 100
maxTweets = 1000000
hashtag = "#mencatatindonesia"

authentication = tweepy.OAuthHandler(consumer_key, consumer_secret)
authentication.set_access_token(access_token, access_secret)
api = tweepy.API(authentication, wait_on_rate_limit=True, wait_on_rate_limit_notify=True)
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