var Api = require('../../utils/api.js');
var util = require('../../utils/util.js');
var aa = 0;
var navList = [
  { id: "share", title: "分享" },
  { id: "ask", title: "问答" },
  { id: "job", title: "招聘" }
]
Page({
  data: {
    tab: "share",
    activeIndex: 0,
    navList: navList,
    title: "",
    content: "",
    type: 0,
    success: false,
    error: "",
    jobtitle: "",
    names: "",
    mess: "",
    addresss: "",
    asks: "",
    emails: ""
  },
  onTapTag: function (e) {
    var that = this;
    console.log(e.currentTarget.id);
    that.setData({
      tab: e.currentTarget.id
    })
  },
  //获取文本框的值
  titles: function (e) {
    this.setData({
      jobtitle: e.detail.value
    })
  },
  //获取文本框的值
  name: function (e) {
    this.setData({
      names: e.detail.value
    })
  },
  //获取文本框的值
  mes: function (e) {
    this.setData({
      mess: e.detail.value
    })
  },
  //获取文本框的值
  address: function (e) {
    this.setData({
      addresss: e.detail.value
    })
  },
  //获取文本框的值
  ask: function (e) {
    this.setData({
      asks: e.detail.value
    })
  },
  //获取文本框的值
  email: function (e) {
    this.setData({
      emails: e.detail.value
    })
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
  Addjob: function () {
    var that = this;
    var jobtitle = that.data.jobtitle;
    var names = that.data.names;
    var mess = that.data.mess;
    var addresss = that.data.addresss;
    var asks = that.data.asks;
    var emails = that.data.emails;
    var accesstoken = wx.getStorageSync('CuserInfo').id;
    var job = {
      Jobtitle: jobtitle,
      Jobname: names,
      Jobaddress: addresss,
      JobMes: mess,
      Jobask: asks,
      Jobemail: emails,
      Authorid: accesstoken
    };
    console.log(job);
    if (accesstoken === "") return;
    var ApiUrl = Api.addjob + "?Jobtitle=" + jobtitle + "&Jobname=" + names + "&Jobaddress=" + addresss + "&JobMes=" + mess + "&Jobask=" + asks + "&Jobemail=" + emails + "&Authorid=" + accesstoken;
    //  var ApiUrl = Api.addjob;
    Api.fetchGet(ApiUrl, (err, res) => {
      console.log(res);
      if (res > 0) {
        wx.redirectTo({
          url: '../topics/topics',
        })
      }
      setTimeout(function () {
        that.setData({ loading: false });
        wx.navigateTo({
          url: '/pages/index/index'
        })
        wx.navigateBack();
      }, 3000);
    })
  },
  Addshare: function () {
    var that = this;
    var title = that.data.title;
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