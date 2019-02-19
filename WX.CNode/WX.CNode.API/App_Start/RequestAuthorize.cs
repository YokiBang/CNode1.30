using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WX.CNode.API.App_Start
{
    public class RequestAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证token值是否有效
            var authorization = actionContext.Request.Headers.Authorization;

            if ((authorization != null) && (authorization.Parameter != null))
            {
                //验证token值是否有效
                var encryptTicket = authorization.Parameter;
                base.IsAuthorized(actionContext);
                
                //var clientinfo = RedisHelper.Get<ClientInfo>(encryptTicket);
                //if (clientinfo != null)
                //{
                //    base.IsAuthorized(actionContext);
                //}
                //else
                //{
                //    base.HandleUnauthorizedRequest(actionContext);
                //}
            }
            else//如果取不到token值，并且不允许匿名访问，则返回未验证401
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);//验证当前动作是否允许匿名访问
                if (isAnonymous)
                {
                    base.OnAuthorization(actionContext);
                }
                else
                {
                    base.HandleUnauthorizedRequest(actionContext);
                }
            }
        }
    }
}
