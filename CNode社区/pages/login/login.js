var Api = require('../../utils/api.js');
var util = require('../../utils/util.js');

Page({
  data: {
    loading: false,
    // post a ccesstoken 验证 accessToken 的正确性
    accesstoken: "",
    error: ""
  },
  onLoad: function () {
  },
  //事件处理函数
  bindKeyInput: function (e) {
    this.setData({
      accesstoken: e.detail.value
    })
  },
  // 验证token(登录)
  isLogin: function () {
    var that = this;
    var accesstoken = that.data.accesstoken;
    var ApiUrl = Api.accesstoken +"?accesstoken="+accesstoken;
    if (accesstoken === "") return;
    that.setData({ loading: true });
    Api.fetchGet(ApiUrl, (err, res) => {
      console.log(res.success);
      if (res.success) {
        wx.setStorageSync('CuserInfo', res);
        setTimeout(function () {
          that.setData({ loading: false });    
          wx.navigateBack();
          
        }, 3000);
      } else {
        that.setData({ error: res.error_msg });
        that.setData({ loading: false });
        setTimeout(function () {
          that.setData({ error: "" });
        }, 2000);
      }
    })
  }
})