using Neuronia.Core.Twitter;
using Neuronia.Hub.Common;
using Neuronia.Hub.Detail.Parameter;
using Neuronia.Hub.Timeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 設定フライアウトの項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=273769 を参照してください

namespace Neuronia.Flyout
{
    public sealed partial class SearchDetailFlyout : SettingsFlyout
    {
        public event Action<string,string,TwitterAccount> onAddTimeline;
        NeuroniaViewModel viewModel;
        public SearchDetailFlyout(NeuroniaViewModel viewModel)
        {
            this.InitializeComponent();
            this.viewModel = viewModel;
            this.comboBoxAccount.ItemsSource = viewModel.AccountList;
            onAddTimeline += (s,ss,e) => { };
            this.DataContext = viewModel;
        }
        public SearchDetailFlyout(string searchWord,NeuroniaViewModel viewModel)
        {
            this.InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            this.viewModel.SearchDetail.SearchWord = searchWord;
            this.SearchAsync();

        }

        public async Task SearchAsync()
        {
            if (!string.IsNullOrEmpty(searchBoxSearchWord.QueryText))
            {
                viewModel.SearchCommand.Execute(new SearchDetailParameter(viewModel.GetAccountFirst().ScreenName, viewModel.SearchDetail.SearchWord));
            }
        }

        
        private async void searchBoxSearchWord_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            await SearchAsync();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            onAddTimeline(textTimeLineName.Text, viewModel.SearchDetail.SearchWord, comboBoxAccount.SelectedItem as TwitterAccount);
            this.Hide();
        }

        
    }
}
