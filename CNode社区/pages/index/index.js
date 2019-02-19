var Api = require('../../utils/api.js');
var util = require('../../utils/util.js');
//index.js
//获取应用实例
var app = getApp()
Page({
  data: {
    // tab切换
    currentTab: 0,
  },
  swichNav: function (e) {
    console.log(e);
    var that = this;
    if (this.data.currentTab === e.target.dataset.current) {
      return false;
    }
    else {
      that.setData({
        currentTab: e.target.dataset.current,
      })
    }
  },
  swiperChange: function (e) {
    console.log(e);
    this.setData({
      currentTab: e.detail.current,
    })
  },
  
  onLoad: function () {
    var that = this;
    var CuserInfo = wx.getStorageSync('CuserInfo');
    if (CuserInfo.accesstoken){
      that.setData({ islogin:true });
    }
    console.log(CuserInfo);
    that.setData({
      userInfo: CuserInfo
    })
    //调用应用实例的方法获取全局数据
    app.getUserInfo(function(userInfo) {
      //更新数据
      that.setData({
        userInfo:userInfo
      })
    })
    var authorid = wx.getStorageSync('CuserInfo').id;
    var ApiUrl = Api.readablecount + '?AuthorId=' + authorid;
    Api.fetchGet(ApiUrl, (err, res) => {
      console.log(res);
      that.setData({
        Headerlist: [
          { id: "share", title: "赞过的" },
          { id: "collect", title: "收藏" },
          { id: "history", title: "历史" },
          { id: "news", title: "消息(" + res+")" },
        ]
      })
    })
  },
   
  
})