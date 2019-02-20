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

    public class AuthorRepository : IAuthorRepository
    {
        /// <summary>
        /// 获取微信会话密钥
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Author Logins(string code)
        {
            //try
            //{
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
                var results = JsonConvert.DeserializeObject<Author>(result);
                clientinfo.OpenId = results.OpenId;//用户唯一标识
                clientinfo.session_key = results.session_key;//密钥
                var client = GetAuthor(code);//判断是否为已注册用户
                if (client == null)
                {
                    return null;
                }
                else
                {
                    clientinfo.id = client.id;
                    clientinfo.loginname = client.loginname;
                    clientinfo.avatar_url = client.avatar_url;
                    clientinfo.DataID = client.DataID;
                }
                // RedisHelper.Set<ClientInfo>(clientinfo.session_key, clientinfo, DateTime.Now.AddHours(10));
                return clientinfo;
            //}
            //catch
            //{
            //    return null;
            //}
        }
        /// <summary>
        /// 登录判断
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Author GetAuthor(string accesstoken)
        {
            string sql = "select author.*,dataresource.avatar_url from author join dataresource on author.DataID = dataresource.id where loginname = '" + accesstoken + "'";
            Author author = MySqlDapper.Query<Author>(sql).FirstOrDefault();
            if (author != null)
            {
                author.success = true;
            }
            return author;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public int PostAuthor(string accesstoken)
        {
            string sql = "insert into author values(id,'" + accesstoken + "','1')";
            int rows = MySqlDapper.Execute(sql);
            return rows;
        }
    }
}