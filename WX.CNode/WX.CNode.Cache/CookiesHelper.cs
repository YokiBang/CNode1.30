using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WX.CNode.Cache
{
    /// <summary>
    /// Description: cookie操作类
    /// </summary>
    public static class CookiesHelper
    {
        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="cookiename">cookie名称</param>
        /// <param name="key">键名称</param>
        /// <returns>返回键值</returns>
        public static string GetCookie(string cookiename, string key)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookiename];
            return cookie == null ? null : cookie[key];
        }

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="cookiename">cookie名称</param>
        /// <returns>返回键值</returns>
        public static string GetCookie(string cookiename)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookiename];
            return cookie == null ? null : cookie.Value;
        }

        /// <summary>
        /// 设置cookie值
        /// </summary>
        /// <param name="cookiename">cookie名称</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="expires">失效期</param>
        public static void SetCookie(string cookiename, string key, string val, DateTime expires)
        {
            var isSetExpires = expires.Date != DateTime.Today;
            SetCookie(cookiename, key, val, expires, isSetExpires);
        }

        /// <summary>
        /// 设置cookie值
        /// </summary>
        /// <param name="cookiename">cookie名称</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="expires">失效期</param>
        /// <param name="isSetExpires">是否记录有效期</param>
        public static void SetCookie(string cookiename, string key, string val, DateTime expires, bool isSetExpires)
        {
            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;
            var cookie = request.Cookies[cookiename] ?? new HttpCookie(cookiename);
            cookie.Domain = FormsAuthentication.CookieDomain;
            if (val == null)
            {
                cookie.Values.Remove(key);
            }
            else
            {
                cookie.Values.Remove(key);
                cookie.Values.Add(key, val);
                cookie.HttpOnly = true;//true代表客户端只能读，不能写。只有服务端可写，防止被篡改
                if (isSetExpires)
                {
                    cookie.Expires = expires;
                }
            }
            response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 设置cookie值
        /// </summary>
        /// <param name="cookiename">cookie名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">失效期</param>
        public static void SetCookie(string cookiename, string value, DateTime expires)
        {
            SetCookie(cookiename, value, expires, true);
        }

        /// <summary>
        /// 设置cookie值
        /// </summary>
        /// <param name="cookiename">cookie名称</param>
        /// <param name="value">值</param>
        /// <param name="expires">失效期</param>
        /// <param name="isSetExpires">是否记录有效期</param>
        private static void SetCookie(string cookiename, string value, DateTime expires, bool isSetExpires)
        {
            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;
            var cookie = request.Cookies[cookiename] ?? new HttpCookie(cookiename);
            cookie.Domain = FormsAuthentication.CookieDomain;
            if (value == null)
            {
                RemoveCookie(cookiename);
            }
            else
            {
                cookie.Value = value;

                //true代表客户端只能读，不能写。只有服务端可写，防止被篡改
                cookie.HttpOnly = true;

                if (isSetExpires)
                {
                    cookie.Expires = expires;
                }
            }
            response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 移除指定名称的cookie对象中的集合对
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        public static void RemoveCookie(string cookieName)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            var response = HttpContext.Current.Response;
            if (cookie == null) return;
            cookie.Values.Clear();
            cookie.Domain = FormsAuthentication.CookieDomain;
            cookie.Expires = DateTime.Now.AddDays(-10000d);
            response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 移除指定名称的cookie对象中的集合中的键值
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param>
        public static void RemoveCookieKey(string cookieName, string key)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            var response = HttpContext.Current.Response;
            if (cookie == null) return;
            cookie.Values.Remove(key);
            response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 移除指定名称的cookie对象中的集合中的键值
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="index">键值名</param>
        public static void RemoveCookieKey(string cookieName, int index)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            var response = HttpContext.Current.Response;
            if (cookie == null) return;
            cookie.Values.Remove(cookie.Values.GetKey(index));
            response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 获取cookie对象的集合数量
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        public static int GetCookieItemsCount(string cookieName)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            return cookie == null ? 0 : cookie.Values.Cast<string>().Count(value => value != null);
        }
    }
}