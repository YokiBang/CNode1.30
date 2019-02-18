'use strict';
// api 路径
var HOST = 'http://localhost:1026/api/CNode';
// get /topics 主题首页
var topics = HOST + '/topics';
//get /topic/:id 主题详情
var topic = HOST + '/topic';
//get /collectid 用户id收藏
var collectid = HOST + '/GetCollectAuthorid';
// get /accesstoken 验证 accessToken 的正确性
var accesstoken = HOST + '/accesstoken';
//消息提示
var readableList = HOST + '/GetReadableList';
//消息提示(条数)
var readablecount = HOST + '/GetReadableCount';
//消息详情
var readable = HOST + '/GetReadable';
// post /添加数据
var s_PostActive = HOST + '/PostActive';
// post /topic_collect/collect 收藏主题
var collect = HOST + '/topic_collect';
// post /topic_collect/de_collect 取消主题
var de_collect = HOST + '/topic_collect';
// post /reply/:reply_id/ups 为评论点赞
var zan = HOST + "/zan";
// 招聘详情
var job = HOST + '/job';
//历史
var history = HOST + "/GetHistoryList";

// get请求方法
function fetchGet(url, callback) {
  // return callback(null, top250)
  wx.request({
    url: url,
    header : { 'Content-Type': 'application/json' },
    success (res) {
      callback(null, res.data)
    },
    fail (e) {
      console.error(e)
      callback(e)
    }
  })
}

// post请求方法
function fetchPost(url, data, callback) {
  wx.request({
    method: 'POST',
    url: url,
    data: data,
    success (res) {
      callback(null, res.data)
    },
    fail (e) {
      console.error(e)
      callback(e)
    }
  })
}

module.exports = {
  topics: topics,
  topic: topic,
  accesstoken: accesstoken,
  collectid: collectid,
  s_PostActive: s_PostActive,
  collect: collect,
  readableList: readableList,
  readable: readable,
  readablecount: readablecount,
  de_collect: de_collect,
  zan: zan,
  history: history,	
  fetchGet: fetchGet,
  fetchPost: fetchPost,
  job : job
}