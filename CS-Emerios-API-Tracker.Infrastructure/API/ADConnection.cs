using CS_Emerios_API_Tracker.Core.DTOs;
using CS_Emerios_API_Tracker.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static CS_Emerios_API_Tracker.Core.DTOs.UserActiveDirectoryDTO;

namespace CS_Emerios_API_Tracker.Infrastructure.API
{
    public class ADConnection : IADConnection
    {
        public string[] main_urls = { "https://csadaws-02-api.collectivesolution.net", "http://13.57.229.182" };
        public string api_encrypt_key = "CS3FMuyajbFeUX7WXvxWyN";
        public string api_endpoint = string.Empty;
        public string api_key = string.Empty;
        public string api_username = string.Empty;
        public string api_password = string.Empty;

        public RestClient client = new RestClient();

        public async Task<bool> CheckStatus()
        {
            try
            {
                foreach (var url in main_urls)
                {
                    this.api_endpoint = "/online_filecheck.txt";

                    //Instansiate API Method using RestSharp
                    client = new RestClient(url);
                    RestSharp.Method api_method = Method.Get;
                    var request = new RestRequest(this.api_endpoint, api_method);

                    //Adding Headers for Rest API
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Content-Encoding", "gzip");

                    RestResponse response = await client.ExecuteAsync(request);

                    if (response.Content.Equals("iEKVwYT7jxwzwgUpErrMoTygKqjvftFFspiNsmWLTF7EbYHNXd"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Error, ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> CheckUserActive(string username)
        {
            try
            {
                var isOnline = await CheckStatus();

                if (isOnline)
                {
                    this.api_endpoint = "/ua";
                    this.api_key = "dsgmd4p9qrNJsMXUEsCzEtMgcgn33jn497e3Mfq9HzAarqLmq9";
                    this.api_username = username;

                    //Instansiate API Method using RestSharp
                    RestSharp.Method api_method = Method.Post;
                    var request = new RestRequest(this.api_endpoint, api_method);

                    //Adding Headers for Rest API
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                    //Add or Modify the value of the Key
                    request.AddOrUpdateParameter("k", this.api_key);
                    request.AddOrUpdateParameter("u", this.api_username);

                    RestResponse response = await client.ExecuteAsync(request);

                    StringBuilder sb = new StringBuilder();
                    var jObject = JObject.Parse(response.Content);
                    dynamic x = JsonConvert.DeserializeObject<dynamic>(jObject.ToString());

                    var status = x.Status.ToString();

                    if (status.ToLower() == "success")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Error, ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> CheckUserCredential(string username, string password)
        {
            try
            {
                var isOnline = await CheckStatus();

                if (isOnline)
                {
                    this.api_endpoint = "/uc";
                    this.api_key = "qbyUFmpSV5KzW6Nv1K8GQ5XaWjh9VROvefR3D9pj7tWEa18VX5";
                    this.api_username = username;
                    this.api_password = password;
                    bool is_domain_admin;

                    var encryptPass = this.Encrypt(this.api_password, this.api_encrypt_key);

                    //Instansiate API Method using RestSharp
                    RestSharp.Method api_method = Method.Post;
                    var request = new RestRequest(this.api_endpoint, api_method);

                    //Adding Headers for Rest API
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                    //Add or Modify the value of the Key
                    request.AddOrUpdateParameter("k", this.api_key);
                    request.AddOrUpdateParameter("u", this.api_username);
                    request.AddOrUpdateParameter("p", encryptPass);

                    RestResponse response = await client.ExecuteAsync(request);

                    StringBuilder sb = new StringBuilder();
                    var jObject = JObject.Parse(response.Content);
                    dynamic x = JsonConvert.DeserializeObject<dynamic>(jObject.ToString());

                    var status = x.Status.ToString();

                    if (status.ToLower() == "success")
                    {
                        var user_ad_info = await this.GetUserInfo(username);

                        is_domain_admin = user_ad_info.MemberOf.Any(group => group.Contains("Domain Admins"));

                        if (is_domain_admin)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Error, ex, ex.Message);
                return false;
            }
        }

        public async Task<UserActiveDirectoryDTO> GetUserInfo(string username)
        {
            UserActiveDirectoryDTO user_ad_info = new();

            try
            {
                var isOnline = await CheckStatus();

                if (isOnline)
                {
                    this.api_endpoint = "/ui";
                    this.api_key = "KN7wc3FEzW3wUnAikvcYajWp9kbCx4mMgqNPxNcxdH7qJgkT9o";
                    this.api_username = username;

                    //For Local Debugging Purposes
                    //RestClient client = new RestClient("https://localhost:7095");

                    //Instansiate API Method using RestSharp
                    RestSharp.Method api_method = Method.Post;
                    var request = new RestRequest(this.api_endpoint, api_method);

                    //Adding Headers for Rest API
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                    //Add or Modify the value of the Key
                    request.AddOrUpdateParameter("k", this.api_key);
                    request.AddOrUpdateParameter("u", this.api_username);

                    RestResponse response = await client.ExecuteAsync(request);

                    StringBuilder sb = new StringBuilder();
                    var jObject = JObject.Parse(response.Content);
                    user_ad_info = JsonConvert.DeserializeObject<UserActiveDirectoryDTO>(jObject.ToString());

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    int flags = Convert.ToInt32(x.useraccountcontrol.ToString());
                    //    userInfo = new ITCentral_Employee_DTO
                    //    {
                    //        UserName = x.samaccountname.ToString(),
                    //        FirstName = x.givenname.ToString(),
                    //        LastName = x.sn.ToString(),
                    //        Email = x.mail.ToString(),
                    //        EID = x.description.ToString(),
                    //        Department = x.department.ToString(),
                    //        Position = x.title.ToString(),
                    //        Country = x.c.ToString(),
                    //        Date_Created = x.whencreated.ToString(),
                    //        ActiveDirectoryID = x.objectguid.ToString(),
                    //        IsActive = !Convert.ToBoolean(flags & 0x0002)

                    //    };
                    //}

                    return user_ad_info;
                }
                return user_ad_info;
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Error, ex, ex.Message);
                return user_ad_info;
            }
        }

        public async Task<bool> CheckUserExist(string username)
        {
            try
            {
                var isOnline = await CheckStatus();

                if (isOnline)
                {
                    this.api_endpoint = "/ue";
                    this.api_key = "wTk7r7W5ywuE4mdrbAdNKHPHE1oamKg9aSH3Dqf2cJRjvBPdN7";
                    this.api_username = username;

                    //Instansiate API Method using RestSharp
                    RestSharp.Method api_method = Method.Post;
                    var request = new RestRequest(this.api_endpoint, api_method);

                    //Adding Headers for Rest API
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                    //Add or Modify the value of the Key
                    request.AddOrUpdateParameter("k", this.api_key);
                    request.AddOrUpdateParameter("u", this.api_username);

                    RestResponse response = await client.ExecuteAsync(request);

                    StringBuilder sb = new StringBuilder();
                    var jObject = JObject.Parse(response.Content);
                    dynamic x = JsonConvert.DeserializeObject<dynamic>(jObject.ToString());

                    var status = x.Status.ToString();

                    if (status.ToLower() == "success")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Write(LogEventLevel.Error, ex, ex.Message);
                return false;
            }
        }

        public string Encrypt(string clearText, string EncryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}
