
// posts.js
var Api = require('../../utils/api.js');
var util = require('../../utils/util.js');


Page({
  data: {
    title: '话题详情',
    collectText:"收藏",
    //detail: {},
    hidden: false,
    modalHidden: true
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
    var ApiUrl = Api.collect;
    console.log(ApiUrl);
    var accesstoken = wx.getStorageSync('CuserInfo');
    var id = e.currentTarget.id;
    if(!id) return;
    if(!accesstoken.accesstoken){
     that.setData({ modalHidden: false });
      return;
    }
    console.log('ok');
    Api.fetchPost(ApiUrl, { author_id: accesstoken.id, active_id: id }, (err, res) => {
      if(res){
          var detail = that.data.detail;
          detail.is_collect = true;
          that.setData({
          collectText: "取消收藏",
            detail: detail
          });
      }
    })
  },
  


  // 点赞
  //reply: function(e) {
  //  console.log(e);
  //  var that = this;
  //  var accesstoken = wx.getStorageSync('CuserInfo').accesstoken;
  //  var id = e.currentTarget.id;
  //  var index = e.currentTarget.dataset.index;
  //  var ApiUrl = Api.reply(id);
  //  if(!id) return;
  //  if(!accesstoken){
  //    that.setData({ modalHidden: false });
  //    return;
  //  }


  //  Api.fetchPost(ApiUrl, { accesstoken:accesstoken }, (err, res) => {
  //    if(res.success){
  //      var detail = that.data.detail;
  //      var replies = detail.replies[index];


  //      if(res.action === "up"){
  //        replies.zanNum = replies.zanNum + 1;
  //      }else{
  //        replies.zanNum = replies.zanNum - 1;
  //      }


  //      that.setData({ detail: detail });


  //    }
  //  })


  //},


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
