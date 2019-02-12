
//index.js
//获取应用实例
var app = getApp()
var Headerlist = [
  { id: "share", title: "分享" },
  { id: "collect", title: "收藏" },
  { id: "history", title: "历史" },
  { id: "news", title: "消息" },
];
Page({
  data: {
    Headerlist: Headerlist,
    islogin: false,
    userInfo: {}
  },
  //事件处理函数
  bindViewTap: function() {
    wx.navigateTo({
      url: '../logs/logs'
    })
  },
  onLoad: function () {
    console.log('onLoad')
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
    app.getUserInfo(function(userInfo){
      //更新数据
      that.setData({
        userInfo:userInfo
      })
    })
  }
})

