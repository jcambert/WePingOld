using MicroS_Common;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WePing.Service.Spid.Services
{
    public sealed class SpidAuthOptions
    {
        public string Password { get; set; }
        public string AppId { get; set; }
        public string Serie { get; set; }
    }

    public sealed class SpidAuth
    {
        public string tm;
        public string tmc;
    }
    public sealed class SpidRequester
    {
        private readonly IConfiguration _config;
        private readonly string _key;
        private readonly SpidAuthOptions _options;
        public SpidRequester(IConfiguration configuration)
        {
            this._config = configuration;
            //_options = _config.GetOptions<SpidAuthOptions>("spidauth");
            _options = new SpidAuthOptions()
            {
                AppId = configuration["spidauth:appid"],
                Password = configuration["spidauth:password"],
                Serie = configuration["spidauth:serie"]
            };
            _key = CreateKey(_options.Password);
        }



        private string CreateKey(string cle)
        {
            var ccle = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(cle));
            return BitConverter.ToString(ccle).Replace("-", string.Empty).ToLower();
        }

        internal string GetParameters(IDictionary<string, string> opts = null)
        {
            var dico = new Dictionary<string, string>(opts);
            var auth = CreateAuth();
            dico["id"] = _options.AppId;
            dico["serie"] = _options.Serie;
            dico["tm"] = auth.tm;
            dico["tmc"] = auth.tmc;
            var result = QueryHelpers.AddQueryString("", dico);
            return result;
        }

        public SpidAuth CreateAuth()
        {
            var tm = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            return new SpidAuth() { tm = tm, tmc = GetHash(tm, _key) };
        }

        private static String GetHash(String text, String key)
        {
            UTF8Encoding encoding = new UTF8Encoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA1 hash = new HMACSHA1(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
