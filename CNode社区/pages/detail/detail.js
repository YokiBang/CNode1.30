
// posts.js
var Api = require('../../utils/api.js');
var util = require('../../utils/util.js');


Page({
  data: {
    title: '话题详情',
    collectText:"收藏",
    //detail: {},
    hidden: false,
    modalHidden: true,
    is_zan:true
  },


  onLoad: function (options) {
    this.fetchData(options.id);
  },


  // 获取数据
  fetchData: function (id) {
    var that = this;
    var ApiUrl = Api.topic +'/'+ id +'?mdrender=false';
    that.setData({
      hidden: false
    });
    Api.fetchGet(ApiUrl, (err, res) => {
      console.log(res);
      res.create_at = util.getDateDiff(new Date(res.create_at));
      res.replies = res.replies.map(function (item) {
        item.reply_at = util.getDateDiff(new Date(item.reply_at));
          //item.zanNum = item.ups.length;
          return item;
      })
      that.setData({ detail: res });
      setTimeout(function () {
        that.setData({ hidden: true });
      }, 300);
    })
  },


   //收藏文章
  collect: function(e) {
    var that = this;
    var accesstoken = wx.getStorageSync('CuserInfo');
    var id = e.currentTarget.id;
    if(!id) return;
    if(!accesstoken.accesstoken){
      that.setData({ modalHidden: false });
      return;
    }
    var ApiUrl = Api.collect + '?author_id=' + accesstoken.id + '&active_id=' + id;
    Api.fetchGet(ApiUrl, (err, res) => {
      if(res){
          var detail = that.data.detail;
          detail.is_collect = true;
          that.setData({
          collectText: "取消收藏",
            detail: detail
          });
      }
      else {
        var detail = that.data.detail;
        detail.is_collect = false;
        that.setData({
          collectText: "收藏",
          detail: detail
        });
      }
    })
  },
  
  // 点赞
  reply: function(e) {
    console.log(e);
    var that = this;
    var accesstoken = wx.getStorageSync('CuserInfo');
    var id = e.currentTarget.id;
    var index = e.currentTarget.dataset.index;
    var ApiUrl = Api.zan + '?authorid=' + accesstoken.id + '&commentid=' + id;
    if(!id) return;
    if(!accesstoken.accesstoken){
      that.setData({ modalHidden: false });
      return;
     }
    Api.fetchGet(ApiUrl, (err, res) => {
      //if(res){
      // var detail = that.data.detail;
       //var replies = detail.replies[index];
       // if(res.action === "up"){
       //   replies.zanNum = replies.zanNum + 1;
       //}else{
       //  replies.zanNum = replies.zanNum - 1;
       //}
      // that.setData({ detail: detail });
  // }
      if (res) {
        var detail = that.data.detail;
        var replies = detail.replies[index];
        detail.is_zan = true;
        replies.zanNum = replies.zanNum + 1;
      }
      else {
        var detail = that.data.detail;
        var replies = detail.replies[index];
        detail.is_zan = false;
        replies.zanNum = replies.zanNum - 1;
      }
   })
  },


  // 关闭--模态弹窗
  cancelChange: function() {
    this.setData({ modalHidden: true });
  },
  // 确认--模态弹窗
  confirmChange: function() {
    this.setData({ modalHidden: true });
    wx.navigateTo({
      url: '/pages/login/login'
    });
  }


})
