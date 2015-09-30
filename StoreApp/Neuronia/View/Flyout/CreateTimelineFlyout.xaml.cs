using Neuronia.Common;
using Neuronia.Core.Tweets.List;
using Neuronia.Core.Twitter;
using Neuronia.Hub.Common;
using Neuronia.Hub.Timeline;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 設定フライアウトの項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=273769 を参照してください

namespace Neuronia.View
{
    public sealed partial class CreateTimelineFlyout : SettingsFlyout
    {
        NeuroniaViewModel viewModel;
        Action<TimelineBase> onCreateCallBack;
        public CreateTimelineFlyout(NeuroniaViewModel viewModel,Action<TimelineBase> onCreateCallBack)
        {
            this.viewModel = viewModel;
            this.onCreateCallBack = onCreateCallBack;
            this.InitializeComponent();

            this.comboBoxAccount.ItemsSource = viewModel.AccountList;
            this.comboBoxTimelineType.Items.Add(TimelineType.Home.ToString());
            this.comboBoxTimelineType.Items.Add(TimelineType.Mention.ToString());
            this.comboBoxTimelineType.Items.Add(TimelineType.Notification.ToString());
            this.comboBoxTimelineType.Items.Add(TimelineType.Search.ToString());
            this.comboBoxTimelineType.Items.Add(TimelineType.List.ToString());
            this.comboBoxTimelineType.Items.Add(TimelineType.DirectMessage.ToString());
            this.comboBoxTimelineType.Items.Add(TimelineType.User.ToString());
            this.comboBoxTimelineType.Items.Add(TimelineType.Image.ToString());
            this.comboBoxTimelineType.Items.Add(TimelineType.Link.ToString());
        }


        private void toggleTimelineFiltering_Toggled(object sender, RoutedEventArgs e)
        {
            if (toggleTimelineFiltering.IsOn)
            {
                stackPanelFiltering.Visibility = Visibility.Visible;
            }
            else
            {
                stackPanelFiltering.Visibility = Visibility.Collapsed;
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool isInputOK = true;
            if (textTimelineName.Text == string.Empty)
            {
                MessageDialog dialog = new MessageDialog("タイムライン名が入力されていません","入力エラー");
                await dialog.ShowAsync();
                isInputOK = false;
            }
            foreach (var t in viewModel.NowTimelineList)
            {
                if (t.ListTitle == textTimelineName.Text)
                {
                    MessageDialog dialog = new MessageDialog("タイムライン名が競合しています。固有のタイムライン名を入力してください", "入力エラー");
                    await dialog.ShowAsync();
                    isInputOK = false;
                }
            }
            if (this.comboBoxAccount.SelectedIndex == -1)
            {
                MessageDialog dialog = new MessageDialog("アカウント名が選択されていません", "入力エラー");
                await dialog.ShowAsync();
                isInputOK = false;
            }
            if (this.comboBoxTimelineType.SelectedIndex == -1)
            {
                MessageDialog dialog = new MessageDialog("タイムラインタイプが選択されていません", "入力エラー");
                await dialog.ShowAsync();
                isInputOK = false;
            }

            if (isInputOK == true)
            {
                
                
                TimelineBase resultModel=null;
                TwitterAccount account=(TwitterAccount)comboBoxAccount.SelectedItem;
                switch(comboBoxTimelineType.SelectedIndex)
                {
                    case 0:
                        resultModel = new HomeTimeline(account, textTimelineName.Text,viewModel.GetNowTab().TabTitle,viewModel.Setting,viewModel.CallTimelineAction,viewModel.CallRowAction);
                        break;
                    case 1:
                        resultModel = new MentionTimeline(account, textTimelineName.Text, viewModel.GetNowTab().TabTitle, viewModel.Setting, viewModel.CallTimelineAction, viewModel.CallRowAction);
                        
                        break;
                    case 2:
                        resultModel = new NotificationTimeline(account, textTimelineName.Text, viewModel.GetNowTab().TabTitle, viewModel.Setting, viewModel.CallTimelineAction, viewModel.CallRowAction);
                        
                        break;
                    case 3:
                        if (textSearchWord.Text ==string.Empty)
                        {
                            MessageDialog dialog = new MessageDialog("検索ワードが入力されていません","入力エラー");
                            await dialog.ShowAsync();
                            return;
                        }
                        resultModel = new SearchTimeline(account, textTimelineName.Text, viewModel.GetNowTab().TabTitle, textSearchWord.Text, viewModel.Setting, viewModel.CallTimelineAction, viewModel.CallRowAction);
                        
                        break;
                    case 4:
                        if (comboList.SelectedIndex==-1)
                        {
                            MessageDialog dialog = new MessageDialog("リストが選択されていません", "入力エラー");
                            await dialog.ShowAsync();
                            return;
                        }
                        resultModel = new ListTimeline(account, textTimelineName.Text, viewModel.GetNowTab().TabTitle, (TwitterList)comboList.SelectedItem, viewModel.Setting, viewModel.CallTimelineAction, viewModel.CallRowAction);
                        
                        break;
                    case 5:
                        resultModel = new DirectMessageTimeline(account, textTimelineName.Text, viewModel.GetNowTab().TabTitle, viewModel.Setting, viewModel.CallTimelineAction, viewModel.CallRowAction);
                        break;
                    case 6:
                        if (textUser.Text==string.Empty)
                        {
                            MessageDialog dialog = new MessageDialog("ユーザー名が入力されていません", "入力エラー");
                            await dialog.ShowAsync();
                            return;
                        }
                        resultModel = new UserTimeline(account, textTimelineName.Text, viewModel.GetNowTab().TabTitle, textUser.Text, viewModel.Setting, viewModel.CallTimelineAction, viewModel.CallRowAction);
                        
                        break;
                    case 7:
                        resultModel = new ImageTimeline(account, textTimelineName.Text,viewModel.GetNowTab().TabTitle,viewModel.Setting,viewModel.CallTimelineAction,viewModel.CallRowAction);
                        break;
                    case 8:
                        resultModel = new LinkTimeline(account, textTimelineName.Text, viewModel.GetNowTab().TabTitle, viewModel.Setting, viewModel.CallTimelineAction, viewModel.CallRowAction);
                        
                        break;

                }
                if (toggleTimelineFiltering.IsOn)
                {
                    resultModel.ExtractionAccountScreenNameStr = textExtractionAccount.Text;
                    resultModel.ExcludeAccountScreenNameStr = textExcludeAccount.Text;
                    resultModel.ExtractionWordStr = textExtractionWord.Text;
                    resultModel.ExcludeWordStr = textExcludeWord.Text;
                    resultModel.IsTimelineFiltering = toggleTimelineFiltering.IsOn;
                    resultModel.IsNewNotification = toggleNotification.IsOn;
                }
                
                viewModel.AddTimelineCommand.Execute(resultModel);
                onCreateCallBack(resultModel);
                this.Hide();
            }
           
        }

        private async void comboBoxTimelineType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stackList.Visibility = Visibility.Collapsed;
            stackSearch.Visibility = Visibility.Collapsed;
            stackUser.Visibility = Visibility.Collapsed;
            
            switch (comboBoxTimelineType.SelectedIndex)
            {
                case 0:
                    
                    break;
                case 1:
                    

                    break;
                case 2:
                    break;
                case 3:
                    stackSearch.Visibility = Visibility.Visible;
                    break;
                case 4:
                    stackList.Visibility = Visibility.Visible;
                    if (comboBoxAccount.SelectedIndex != -1)
                    {
                        TwitterAccount ac=(comboBoxAccount.SelectedItem as TwitterAccount);
                        comboList.ItemsSource = await ac.TwitterClient.GetListListAsync(ac.UserInfomation);
                    }
                    break;
                case 5:
                    break;
                case 6:
                    stackUser.Visibility = Visibility.Visible;
                    break;

            }
        }

        private void toglleNotification_Toggled(object sender, RoutedEventArgs e)
        {

        }
    }
}
