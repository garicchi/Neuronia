using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Neuronia.Core.Post;
using Neuronia.Core.Tweets;
using Neuronia.Core.Tweets.DirectMessage;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Detail.Parameter;
using Neuronia.Hub.Tab;
using Neuronia.Hub.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Hub.Common
{
    public class NeuroniaViewModel:BindableBase
    {
        public NeuroniaViewModel()
        {
            this.CommandInitialize();
        }

        public DelegateCommand<TwitterAccount> AddAccountCommand { get; set; }

        public DelegateCommand<TwitterAccount> DeleteAccountCommand { get; set; }

        public DelegateCommand PostStatusCommand { get; set; }

        public DelegateCommand<PostMedia> SetPostImageCommand { get; set; }

        public DelegateCommand DeletePostImageCommand { get; set; }

        public DelegateCommand<TimelineTab> AddTimelineTabCommand { get; set; }

        public DelegateCommand<TimelineTab> DeleteTimelineTabCommand { get; set; }

        public DelegateCommand<TimelineTab> ChangeTabCommand { get; set; }

        public DelegateCommand<TimelineBase> AddTimelineCommand { get; set; }

        public DelegateCommand<TimelineBase> DeleteTimelineCommand { get; set; }

        public DelegateCommand<TimelineTab> EditTimelineTabCommand { get; set; }

        public DelegateCommand<TimelineBase> EditTimelineCommand { get; set; }

        public DelegateCommand<Tweet> FavoriteCommand { get; set; }

        public DelegateCommand<Tweet> RetweetCommand { get; set; }

        public DelegateCommand<string> PinAuthCommand { get; set; }

        public DelegateCommand BeginAuthCommand { get; set; }

        public DelegateCommand<Tweet> QuoteCommand { get; set; }

        public DelegateCommand<TweetDetailParameter> TweetDetailCommand { get; set; }

        public DelegateCommand<Tweet> ReplyCommand { get; set; }

        public DelegateCommand<Tweet> DescriptionDommand { get; set; }

        public DelegateCommand<UserDetailParameter> UserDetailCommand { get; set; }

        public DelegateCommand<DirectMessageDetailParameter> DirectMessageDetailCommand { get; set; }

        public DelegateCommand<SearchDetailParameter> SearchCommand { get; set; }

        public DelegateCommand<string> BrowseCommand { get; set; }

        public DelegateCommand NextTabCommand { get; set; }

        public DelegateCommand PrevTabCommand { get; set; }

        public DelegateCommand<string> SelectSuggestCommand { get; set; }

        public DelegateCommand<TwitterAccount> ToggleAccountActivityCommand { get; set; }

        public DelegateCommand<SendDirectMessage> SendDirectMessageCommand { get; set; }

        public DelegateCommand<double> ChangeTimelineWidthCommand { get; set; }

        public DelegateCommand ExitCommand { get; set; }

        public DelegateCommand<string> AddSuggestPostText { get; set; }

        public DelegateCommand<string> ChangeUIBrushImageCommand { get; set; }

        public DelegateCommand ResetThemeSettingCommand { get; set; }

        public DelegateCommand PurchaseApplicationThemeCommand { get; set; }

        public DelegateCommand<string> AddMuteAccountCommand { get; set; }

        public DelegateCommand<string> CopyClipBoardCommand { get; set; }

        private void CommandInitialize()
        {
            AddAccountCommand = new DelegateCommand<TwitterAccount>(account =>
            {



            });

            DeleteAccountCommand = new DelegateCommand<TwitterAccount>(account =>
            {

            });

            PostStatusCommand = new DelegateCommand(async () =>
            {





            });


            AddTimelineTabCommand = new DelegateCommand<TimelineTab>(tab =>
            {

            });

            DeleteTimelineTabCommand = new DelegateCommand<TimelineTab>(tab =>
            {


            });

            AddTimelineCommand = new DelegateCommand<TimelineBase>(timeline =>
            {

            });

            DeleteTimelineCommand = new DelegateCommand<TimelineBase>(timeline =>
            {

            });

            ChangeTabCommand = new DelegateCommand<TimelineTab>(tab =>
            {


            });

            EditTimelineTabCommand = new DelegateCommand<TimelineTab>(tab =>
            {

            });

            EditTimelineCommand = new DelegateCommand<TimelineBase>(timeline =>
            {

            });

            FavoriteCommand = new DelegateCommand<Tweet>(async tweet =>
            {


            });

            RetweetCommand = new DelegateCommand<Tweet>(async tweet =>
            {

            });


            QuoteCommand = new DelegateCommand<Tweet>(tweet => );

            TweetDetailCommand = new DelegateCommand<TweetDetailParameter>(async tweet =>
            {


            });

            ReplyCommand = new DelegateCommand<Tweet>(tweet =>
            {

            });

            DescriptionDommand = new DelegateCommand<Tweet>(tweet =>
            {


            });

            UserDetailCommand = new DelegateCommand<UserDetailParameter>(async screen_name =>
            {

            });

            SearchCommand = new DelegateCommand<SearchDetailParameter>(async searchWord =>
            {

            });

            BrowseCommand = new DelegateCommand<string>(url => );

            BeginAuthCommand = new DelegateCommand(async () =>
            {

            });
            PinAuthCommand = new DelegateCommand<string>(async pin =>
            {

            });

            SetPostImageCommand = new DelegateCommand<PostMedia>(media =>
            {

            });

            DeletePostImageCommand = new DelegateCommand(() =>
            {

            });

            SelectSuggestCommand = new DelegateCommand<string>(item =>
            {

            });

            ToggleAccountActivityCommand = new DelegateCommand<TwitterAccount>(account => );

            DirectMessageDetailCommand = new DelegateCommand<DirectMessageDetailParameter>(async dm =>
            {


            });

            SendDirectMessageCommand = new DelegateCommand<SendDirectMessage>(async message =>
            {

            });

            ChangeTimelineWidthCommand = new DelegateCommand<double>(size =>
            {

            });

            ExitCommand = new DelegateCommand(async () =>
            {

            });

            AddSuggestPostText = new DelegateCommand<string>(str =>
            {

            });

            ChangeUIBrushImageCommand = new DelegateCommand<string>(str =>
            {


            });

            ResetThemeSettingCommand = new DelegateCommand(() =>
            {

            });

            PurchaseApplicationThemeCommand = new DelegateCommand(async () =>
            {


            });

            AddMuteAccountCommand = new DelegateCommand<string>(async (screenName) =>
            {

            });

            CopyClipBoardCommand = new DelegateCommand<string>(async str =>
            {

            });

            NextTabCommand = new DelegateCommand(() =>
            {

            });

            PrevTabCommand = new DelegateCommand(() =>
            {


            });

        }
    }
}
