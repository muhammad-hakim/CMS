using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class AuthorizationLdap
    {
        private const string BaseUrl = "https://toga.fasah.sa/api/login";
        private readonly HttpClient _client;

        public AuthorizationLdap( )
        {
            _client = new HttpClient() ;
        }



       

        public async Task<string> getUserRoles(string userName , string pass)
        {
             

          
            
             var content = JsonConvert.SerializeObject(new {
                 username = userName,
                 password= pass
             });
             _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

           
            var httpResponse = await _client.PostAsync("http://dev-icp-proxy.etabadul.com/api/login", new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                return "" ;
              
            }

            var createdTask = JsonConvert.DeserializeObject<object>(await httpResponse.Content.ReadAsStringAsync());
            JObject jObject = JObject.Parse(createdTask.ToString());
            string tokenValue = (string)jObject.SelectToken("token");
            if (string.IsNullOrEmpty(tokenValue))
            {
                return "";
            }
            string role = await getUserRolesDetails(tokenValue, userName);
            //string role2 = await getUserRolesTest2(tokenValue, "cms_test_1");
            return role.ToString();
        }

        public async Task<string> getUserRolesDetails(string token,string userName)
        {




         
              _client.DefaultRequestHeaders.Add("Accept-Language", "ar");
            //string getTokenReady = token.Replace("Bearer", "").Trim();
            //  _client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Bearer", getTokenReady.Trim());
            ///    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
             _client.DefaultRequestHeaders.Add("token", token);
            var httpResponse = await _client.GetAsync("https://toga.fasah.sa/api/user/"+ userName + "");

            if (!httpResponse.IsSuccessStatusCode)
            {
                return "";

            }

            var createdTask = JsonConvert.DeserializeObject<object>(await httpResponse.Content.ReadAsStringAsync());
            JObject jObject = JObject.Parse(createdTask.ToString());
            CMS.User.email = (string)jObject["email"]; //Added by Kalam, for show in comments section
            object listOfUserRoles = jObject.SelectToken("roles") ;
            return listOfUserRoles.ToString();
        }


        

    }
}
