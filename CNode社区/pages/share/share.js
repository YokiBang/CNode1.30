var Api = require('../../utils/api.js');
var util = require('../../utils/util.js');

Page({
  data: {
     title:"",
     content:"",
     type:0,
     success:false,
     error: ""
  },
  //获取文本框的值
  bindKeyInput1: function (e) {
    this.setData({
      title: e.detail.value
    })
  },
  //获取文本框的值
  bindKeyInput2: function (e) {
    this.setData({
      content: e.detail.value
    })
  },
  radioChange: function (e) {
    this.setData({
      type: e.detail.value
    })
  },
  Addshare:function(){
    var that = this;
    var title=that.data.title;
    var content = that.data.content;
    var type = that.data.type;
    var accesstoken = wx.getStorageSync('CuserInfo').id;
    if (accesstoken === "") return;
    if (title === "") return;
    if (content === "") return;
    if (type === "") return;
    var ApiUrl = Api.s_PostActive + "?title=" + title + "&content=" + content + "&type=" + type + "&PublisheriD=" + accesstoken;
    Api.fetchPost(ApiUrl, { title: title, content: content, type: type, accesstoken: accesstoken }, (err, res) => {
      setTimeout(function () {
        that.setData({ loading: false });
        wx.navigateTo({
          url: '/pages/index/index'
        })
        wx.navigateBack();
      }, 3000);
    })
  }
})