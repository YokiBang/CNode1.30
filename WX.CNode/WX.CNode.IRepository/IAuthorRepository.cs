﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WX.CNode.Model;

namespace WX.CNode.IRepository
{
    public interface IAuthorRepository
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        Author GetAuthor(string accesstoken);
    }
}
