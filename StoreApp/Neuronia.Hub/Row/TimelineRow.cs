using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Neuronia.Core.Common;
using Neuronia.Core.Tweets;
using Neuronia.Hub.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Neuronia.Hub.Data;
using Neuronia.Hub.Detail.Parameter;
using Neuronia.Core.Tweets.Entities;


namespace Neuronia.Hub.Row
{
    [DataContract]
    public class TimelineRow:RowBase
    {
        
        
        

        public RelayCommand FavoriteCommand { get; set; }

        public RelayCommand RetweetCommand { get; set; }

        public RelayCommand QuoteCommand { get; set; }

        public RelayCommand TweetDetailCommand { get; set; }

        public RelayCommand ReplyCommand { get; set; }

        public RelayCommand<string> UserDetailCommand { get; set; }

        public RelayCommand<string> SearchCommand { get; set; }

        public RelayCommand<string> BrowseCommand { get; set; }

        public RelayCommand<TweetMedia> ImagePreviewCommand { get; set; }

        public RelayCommand ImageBrowseCommand { get; set; }

        public RelayCommand SavePreviewImageCommand { get; set; }

        public RelayCommand DescriptionCommand { get; set; }

        public RelayCommand ShareCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        private Uri previewImageUri;

        public Uri PreviewImageUri
        {
            get { return previewImageUri; }
            set { previewImageUri = value; ModelPropertyChanged("PreviewImageUri"); }
        }

        private Uri browseImageUri;

        public Uri BrowseImageUri
        {
            get { return browseImageUri; }
            set { browseImageUri = value; ModelPropertyChanged("BrowseImageUri"); }
        }
        
        
        
        
        public TimelineRow(Tweet tweet,string ownerScreenName,SettingData setting,Action<RowAction> actionCallback)
            :base(tweet,ownerScreenName,setting,actionCallback,RowType.Tweet)
        {

            if (tweet.retweeted_status == null)
            {
                this.RowType = RowType.Tweet;
                if (Tweet.text.Contains("@" + ownerScreenName))
                {
                    SharedDispatcher.RunAsync(() =>
                    {
                        BarColorBrush = (Application.Current.Resources["MentionForegroundBrush"] as SolidColorBrush).Color;
                    });

                }
            }
            else
            {
                this.RowType = RowType.Retweet;
                SharedDispatcher.RunAsync(() =>
                {
                    BarColorBrush = (Application.Current.Resources["RetweetForegroundBrush"] as SolidColorBrush).Color;
                });

            }
            Initialize(rowActionCallback);
            
        }

        public void Initialize(Action<RowAction> rowActionCallBack)
        {
            
            CommandInitialize();
        }

        private void CommandInitialize()
        {
            FavoriteCommand = new RelayCommand(() =>
            {
                this.rowActionCallback(new RowAction(RowActionType.Favorite,Tweet));
            });

            RetweetCommand = new RelayCommand(() =>
            {
                this.rowActionCallback(new RowAction(RowActionType.Retweet, Tweet));
            });

            QuoteCommand = new RelayCommand(() =>
            {
                this.rowActionCallback(new RowAction(RowActionType.Quote, Tweet));
            });

            TweetDetailCommand = new RelayCommand(() =>
            {
                this.rowActionCallback(new RowAction(RowActionType.TweetDetail, new TweetDetailParameter(this.OwnerScreenName,this.Tweet)));
            });

            ReplyCommand = new RelayCommand(() =>
            {
                this.rowActionCallback(new RowAction(RowActionType.Reply, Tweet));
            });

            UserDetailCommand = new RelayCommand<string>(screenName =>
            {
                this.rowActionCallback(new RowAction(RowActionType.UserDetail, new UserDetailParameter(this.OwnerScreenName,screenName)));
            });

            SearchCommand = new RelayCommand<string>(word =>
            {
                this.rowActionCallback(new RowAction(RowActionType.Search, new SearchDetailParameter(this.OwnerScreenName,word)));
            });

            BrowseCommand = new RelayCommand<string>(url =>
            {
                this.rowActionCallback(new RowAction(RowActionType.Browse,url));
            });

            DescriptionCommand = new RelayCommand(() =>
            {
                this.rowActionCallback(new RowAction(RowActionType.Description,Tweet));
            });

            ShareCommand=new RelayCommand(() =>
            {
                this.rowActionCallback(new RowAction(RowActionType.Share, Tweet));
                DataTransferManager.GetForCurrentView().DataRequested += (s, e) =>
                {
                    e.Request.Data.Properties.Title = "@"+Tweet.user.screen_name+"のツイート";
                    e.Request.Data.Properties.Description = "Shared Tweet from Neuronia";
                    e.Request.Data.SetText(Tweet.text+" from @"+Tweet.user.screen_name);
                    
                   
                };
                DataTransferManager.ShowShareUI();
            });

            DeleteCommand = new RelayCommand(() =>
            {
                this.rowActionCallback(new RowAction(RowActionType.Delete,Tweet));
            });

            ImagePreviewCommand=new RelayCommand<TweetMedia>((media)=>
            {
                PreviewImageUri = new Uri(media.media_url);
                BrowseImageUri = new Uri(media.url);
            });

            ImageBrowseCommand = new RelayCommand(() =>
            {
                this.rowActionCallback(new RowAction(RowActionType.Browse,BrowseImageUri));
            });

            SavePreviewImageCommand = new RelayCommand(() =>
            {
                
                this.rowActionCallback(new RowAction(RowActionType.SavePreviewImage,PreviewImageUri));
            });
            
        }
    }
}
