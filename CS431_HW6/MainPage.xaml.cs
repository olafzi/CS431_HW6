using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CS431_HW6
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CheckConnection();
        }

        public bool CheckConnection()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // Connection to internet is available
                return true;
            }


            DisplayAlert("Lonely local sheep", "If you have no internet, how will you see results, hm?", "OK");
            return false;
        }

        private void Handle_QueryWord(object sender, EventArgs e)
        {
            string WordQuery = WordBoxInput.Text;
            if (string.IsNullOrWhiteSpace(WordQuery))
            { 
                DisplayAlert("404: Word not found", "A dictionary might have an empty page, but fill this search box, please", "OK");
            }
            else

            if (CheckConnection())
            {
                WordQuery = WordQuery.ToLower();
                GetWordInfo(WordQuery);
            }
        }

        async void GetWordInfo(string WordQuery)
        {
            var client = new HttpClient();
            var OwlBotAddress = "https://owlbot.info/api/v2/dictionary/" + WordQuery;
            var uri = new Uri(OwlBotAddress);

            List<Dictionary> wordData = new List<Dictionary>();
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                
                var jsonContent = await response.Content.ReadAsStringAsync();
                wordData = JsonConvert.DeserializeObject<List<Dictionary>>(jsonContent);
            }

            WordDataView.ItemsSource = wordData;
        }
    }
}
