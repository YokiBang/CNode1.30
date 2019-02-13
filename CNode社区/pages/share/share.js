//获取应用实例
const app = getApp()
//引入wxParse
var WxParse = require('../wxParse/wxParse.js');
Page({
  /**
   * 页面的初始数据
   */
  data: {
    nodes: [{
      name: 'div',
      attrs: {
        class: 'div_class',
        style: 'line-height: 60px; color: red;'
      },
      children: [{
        type: 'text',
        text: 'Hello&nbsp;World!'
      }]
    }]
  },
  tap() {
    console.log('tap')
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    wx.showLoading({
      title: '加载中',
      mask: true
    })
    var that = this;
    console.log(options)
    that.setData({
      activityId: options.activityId
    })

    wx.request({
      url: app.globalData.subDomain + '/GetActivityInfo',
      data: {
        token: app.globalData.token,
        activity: that.data.activityId
      },
      method: 'POST',
      success: function (res) {
        //获取html代码      
        that.setData({
          article: unescape(res.data.activity.aintroduct)
        })
        WxParse.wxParse('article', 'html', that.data.article, that, 5);
        wx.hideLoading();
      }
    })
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})