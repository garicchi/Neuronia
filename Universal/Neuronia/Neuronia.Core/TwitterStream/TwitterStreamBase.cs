using System.Diagnostics;
using Neuronia.Core.Common;
using Neuronia.Core.Tweets;
using Neuronia.Core.Tweets.Stream;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Neuronia.Core.Extentions;
using Neuronia.Core.Twitter;
using Neuronia.Core.Data;
using AsyncOAuth;

namespace Neuronia.Core.TwitterStream
{
    public class TwitterStreamBase:TwitterClientBase
    {
        protected int ConnectStreamCount { get; set; }

        private bool ConnectEndFlag { get; set; }

        private StreamState StreamState { get; set; }

        string streamingUrl;

        public string StreamingUrl
        {
            get { return streamingUrl; }
            set { streamingUrl = value; }
        }

        protected User UserInformation;

        public event Action<StreamState> ChangeStreamEvent;

        public event Action<HttpRequestException> OnStreamError;
        
        public TwitterStreamBase(ConsumerData consumerData,AccessTokenData token,User user,string streamingUrl)
            :base(consumerData,token)
        {
           
            this.UserInformation = user;
            this.streamingUrl = streamingUrl;
            ConnectEndFlag = false;
            ChangeStreamEvent += (state) => { };
            OnStreamError += e => { };
            StreamState = StreamState.DisConnect;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected virtual Task StreamProcess(string s)
        {
            return Task.Run(() =>
            {
            });
        }

        public async void ConnectStreamAsync()
        {

            await Task.Run(async () =>
            {
            
            try { 
                    if (ConnectStreamCount != 0)
                    {
                        int delay = 0;
                        if (ConnectStreamCount > 5)
                        {
                            delay = 4;
                        }
                        await Task.Delay(TimeSpan.FromSeconds(30) + TimeSpan.FromMinutes(delay));
                    }
                    var stream = await HttpClient.GetStreamAsync(streamingUrl);
                    var sr = new StreamReader(stream);


                    ChangeStreamState(StreamState.Connect);
                    while (!sr.EndOfStream)
                    {

                        if (ConnectEndFlag == true)
                        {
                            ConnectEndFlag = false;
                            break;
                        }
                        var s = await sr.ReadLineAsync();
                        ConnectStreamCount = 0;

                        if (s != "")
                        {
                            s = s.ReplaceSpecialCharactor();
                            await StreamProcess(s);
                        }
                    }

               
                       }
                catch (Exception e)
                {
                   
                    ConnectStreamCount++;
                    ChangeStreamState(StreamState.TryConnect);
                    if (e is HttpRequestException)
                    {
                        OnStreamError(e as HttpRequestException);
                    }
                    ConnectStreamAsync();

                }

            });
               
            
        }

        public void DisConnect()
        {
            ConnectEndFlag = true;
            ChangeStreamState(StreamState.DisConnect);
        }

        private void ChangeStreamState(StreamState state)
        {
             ChangeStreamEvent(StreamState);
        }
        

        
    }
}
