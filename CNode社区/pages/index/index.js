
//index.js
//获取应用实例
var app = getApp()
var Headerlist = [
  { id: "share", title: "赞过的" },
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
  },
  onTapTag: function (e) {
    var that = this;
    var tab = e.currentTarget.id;
    var index = e.currentTarget.dataset.index;
    that.setData({
      activeIndex: index,
      tab: tab,
      page: 1
    });
    if (tab === 'share') {
      wx.navigateTo({
        url: '/pages/share/share'
      })
    } else if (tab === 'collect') {
      wx.navigateTo({
        url: '/pages/collect/collect'
      })
    } else if (tab === 'history') { 
      wx.navigateTo({
        url: '/pages/history/history'
      })
    } else if (tab === 'news') { 
      wx.navigateTo({
        url: '/pages/news/news'
      })
    }
  }
})