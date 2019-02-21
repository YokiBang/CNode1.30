using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WX.CNode.Repository
{
    using WX.CNode.Model;
    using WX.CNode.IRepository;
    using System.Net.Http;
    using Newtonsoft.Json;
    using WX.CNode.Cache;

    public class AuthorRepository : IAuthorRepository
    {
        /// <summary>
        /// 获取微信会话密钥
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Author Logins(string code,string accesstoken)
        {
            try
            {
                Author clientinfo = new Author();
                HttpClient httpclient = new HttpClient();

                //登陆公众平台 开发->基本配置中的开发者ID(AppID)和 开发者密码(AppSecret)
                string appid = "wx12f770201e77142b";//开发者ID
                string secret = "7236c7537bc3fdf1a4ed6046c1a3701e";//开发者秘钥
                httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpclient.PostAsync("https://api.weixin.qq.com/sns/jscode2session?appid=" + appid + "&secret=" + secret + "&js_code=" + code.ToString() + "&grant_type=authorization_code", null).Result;
                var result = "";
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
                httpclient.Dispose();
                var client = GetAuthor(accesstoken);//判断是否为已注册用户
                if (client != null)
                {
                    clientinfo = client;
                    clientinfo.success = true;
                }
                
                return clientinfo;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 登录判断
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Author GetAuthor(string accesstoken)
        {
            string sql = "select author.*,dataresource.avatar_url from author join dataresource on author.DataID = dataresource.id where OpenId = '" + accesstoken + "'";
            Author author = MySqlDapper.Query<Author>(sql).FirstOrDefault();
            if (author != null)
            {
                author.success = true;
            }
            return author;
        }
    }
}