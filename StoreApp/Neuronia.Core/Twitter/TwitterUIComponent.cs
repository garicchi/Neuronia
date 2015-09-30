using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Neuronia.Core.Common;
using Neuronia.Core.Post;
using Neuronia.Core.Tweets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronia.Core.Twitter
{
    public class TwitterUIComponent:ModelBase
    {
        public TwitterUIComponent()
        {
            suggestList = new ObservableCollection<string>();
            mentionSuggestSourceList = new ObservableCollection<string>();
            hashSuggestSourceList = new ObservableCollection<string>();
            
            postText = "";
            isHashTagLock = false;
            isPostWithMedia = false;
            PostMedia=new PostMedia();
            InReplyToStatus = Tweet.ZeroTweet;
        }
        string postText;

        public string PostText
        {
            get { return postText; }
            set 
            {
                postText = value;
                
                ChangeSuggestList();
                ModelPropertyChanged("PostText"); 
            }
        }


        bool isHashTagLock;

        public bool IsHashTagLock
        {
            get { return isHashTagLock; }
            set { isHashTagLock = value; ModelPropertyChanged("IsHashTagLock"); }
        }

        

        bool isPostWithMedia;

        public bool IsPostWidhMedia
        {
            get { return isPostWithMedia; }
            set { isPostWithMedia = value; ModelPropertyChanged("IsPostWidhMedia"); }
        }

        Tweet inReplyToStatus;

        public Tweet InReplyToStatus
        {
            get { return inReplyToStatus; }
            set { inReplyToStatus = value; ModelPropertyChanged("InReplyToStatus"); }
        }

        PostMedia postMedia;

        public PostMedia PostMedia
        {
            get { return postMedia; }
            set { postMedia = value; ModelPropertyChanged("PostMedia"); }
        }

        bool isAcceptMedia;

        public bool IsAcceptMedia
        {
            get { return isAcceptMedia; }
            set { isAcceptMedia = value; }
        }

        bool isAcceptReply;

        public bool IsAcceptReply
        {
            get { return isAcceptReply; }
            set { isAcceptReply = value; }
        }

        ObservableCollection<string> mentionSuggestSourceList;

        public ObservableCollection<string> MentionSuggestSourceList
        {
            get { return mentionSuggestSourceList; }
            set { mentionSuggestSourceList = value; }
        }

        ObservableCollection<string> hashSuggestSourceList;

        public ObservableCollection<string> HashSuggestSourceList
        {
            get { return hashSuggestSourceList; }
            set { hashSuggestSourceList = value; }
        }

        ObservableCollection<string> suggestList;

        public ObservableCollection<string> SuggestList
        {
            get { return suggestList; }
            set { suggestList = value; }
        }

        

        private async void ChangeSuggestList()
        {
            if (!(PostText.Contains("@") || PostText.Contains("#")))
            {
                return;
            }
            ObservableCollection<string> resultSuggest = new ObservableCollection<string>();
            await Task.Run(() =>
            {
            
            int mIndex = PostText.LastIndexOf('@');
            int hIndex = PostText.LastIndexOf('#');

            if (mIndex > hIndex)
            {
                var s = PostText.Split('@');
                string name = s[s.Count() - 1];

                int i = 0;
                foreach (var n in MentionSuggestSourceList.Where(q => q.StartsWith(name)).Select(q => q))
                {
                    resultSuggest.Add("@" + n);
                    if (i > 10)
                    {
                        break;
                    }
                    i++;
                }
            }
            else if (mIndex < hIndex)
            {
                var s = PostText.Split('#');
                string name = s[s.Count() - 1];
                int i = 0;
                foreach (var n in HashSuggestSourceList.Where(q => q.StartsWith(name)).Select(q => q))
                {
                    resultSuggest.Add("#" + n);
                    if (i > 10)
                    {
                        break;
                    }
                    i++;
                }
            }
            });
            
            SuggestList.Clear();
            foreach (var r in resultSuggest)
            {
                SuggestList.Add(r);
            }
        }

        public void ResetPostText()
        {
            if (IsAcceptReply)
            {
                IsAcceptReply = false;
                InReplyToStatus = Tweet.ZeroTweet;
            }

            if (IsAcceptMedia)
            {
                ResetPostMedia();
            }
            SetPostText(string.Empty);
        }
        public void SetPostText(string text)
        {
            if (!IsHashTagLock)
            {
                PostText = text;
            }
            else
            {
                PostText = HashtagLockText(PostText, text);

            }
            
        }

        public void SetPostText(string text, Tweet in_reply_to_status)
        {
            this.InReplyToStatus = in_reply_to_status;
            IsAcceptReply = true;
            if (!IsHashTagLock)
            {
                PostText = text;
            }
            else
            {

                PostText = HashtagLockText(PostText, text);

            }
            
        }

        public void AddPostText(string text)
        {
            if (!IsHashTagLock)
            {
                PostText += text;
            }
            else
            {

                PostText += HashtagLockText(PostText, text);

            }
            
        }

        public void AddPostText(string text, Tweet in_reply_to_status)
        {
            this.InReplyToStatus = in_reply_to_status;
            IsAcceptReply = true;
            if (!IsHashTagLock)
            {
                PostText += text;
            }
            else
            {
                
                PostText += HashtagLockText(PostText,text);

            }
            
        }

        public void SetPostMedia(PostMedia media)
        {
            this.PostMedia = media;
            IsAcceptMedia = true;
        }

        public void ResetPostMedia()
        {
            this.PostMedia.Reset();
            IsAcceptMedia = false;
        }

        public PostStatusBase GetPostStatus()
        {
            PostStatusBase postStatus=null;
            if (IsAcceptMedia == false && IsAcceptReply == false)
            {
                postStatus = new PostStatus(PostText);
            }
            else if (IsAcceptMedia == false && IsAcceptReply == true)
            {
                if (PostText.Contains("@"))
                {
                    postStatus = new PostStatusWithReply(PostText,InReplyToStatus.id_str);
                }
                else
                {
                    postStatus = new PostStatus(PostText);
                }
            }
            else if (IsAcceptMedia == true && IsAcceptReply == false)
            {
                postStatus = new PostStatusMedia(PostText,PostMedia);
                
            }
            else if (IsAcceptMedia == true && IsAcceptReply == true)
            {
                if (PostText.Contains("@"))
                {
                    postStatus = new PostStatusMediaWithReply(PostText, PostMedia, InReplyToStatus.id_str);
                }
                else
                {
                    postStatus = new PostStatusMedia(PostText, PostMedia);
                    

                }
            }
            return postStatus;
        }

        private string HashtagLockText(string oldText,string addText)
        {
            string[] strs = oldText.Split(' ');
            var tags = strs.Where(q => q.StartsWith("#") == true).Select(q => q).ToList();
            string newText = addText;
            foreach (var tag in tags)
            {
                newText += " " + tag;
            }
            return newText;
        } 

    }
}
