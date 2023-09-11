using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MOBILE_WEBAPITEST
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public class testGETmodel
        {
            public string[] all { get; set; }
            public string[] corp { get; set; }
        }
        public class testPOSTmodel
        {
            public DateTime 예약일자 { get; set; }
            public string 관리번호 { get; set; }
        }

        private async void GETrequest(object sender, EventArgs e)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sendar, cert, chain, sslPolicyErrors) => { return true; };


                var client = new HttpClient(clientHandler);
                client.BaseAddress = new Uri("https://coever.co.kr/PMS/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("cache-control", "no-cache");

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "CarReservation_S2");

                HttpResponseMessage response = await client.SendAsync(requestMessage).ConfigureAwait(false);
                var message = response.Content.ReadAsStringAsync().Result;
                await DisplayAlert("Success",message.Substring(0,20),"확인");
            }
            catch (Exception ex)
            {
                await DisplayAlert("False", ex.Message, "확인");
            }
          
        }

        private async void POSTrequest(object sender, EventArgs e)
        {
            var json = new testPOSTmodel()
            {
                예약일자 = Convert.ToDateTime("2023-09-15"),
                관리번호 = "005"
            };


            try
            {
             

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sendar, cert, chain, sslPolicyErrors) => { return true; };


                var httpClient = new HttpClient(clientHandler);
                var jsonData = JsonConvert.SerializeObject(json);

                Uri uri = new Uri("https://coever.co.kr/PMS/CarReservation_S3");
                StringContent content = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uri, content);
                httpResponseMessage.EnsureSuccessStatusCode();
                var httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                await DisplayAlert("Success", httpResponseBody, "확인");
            }
            catch (Exception ex)
            {
                await DisplayAlert("False", ex.Message, "확인");
            }
 
        }
    }
}
