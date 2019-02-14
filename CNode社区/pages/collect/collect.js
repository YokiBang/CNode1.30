// pages/history/history.js
var Api = require('../../utils/api.js');
var util = require('../../utils/util.js');
Page({
  data: {
    postsList: [],
    modalHidden: true,
    hidden: false,
    page: 1,
    id: 0
  },
  onLoad: function () {
    var that = this;
    var id = that.data.id;
    console.log(id);
    var page = that.data.page;
    var ApiUrl = Api.collectid + '?Authorid' + id
    that.setData({ hidden: false });
    if (page == 1) {
      that.setData({ postsList: [] });
    }
    Api.fetchGet(ApiUrl, (err, res) => {
      //更新数据
      that.setData({
        postsList: that.data.postsList.concat(res.map(function (item) {
          item.last_reply_at = util.getDateDiff(new Date(item.last_reply_at));
          return item;
        }))
      });
      setTimeout(function () {
        that.setData({ hidden: true });
      }, 300);
    })
  },
  onPullDownRefresh: function () {
    this.getData();
    console.log('下拉刷新', new Date());
  },
  onReachBottom: function () {
    this.lower();
    console.log('上拉刷新', new Date());
  },
  // 滑动底部加载
  lower: function () {
    console.log('滑动底部加载', new Date());
    var that = this;
    that.setData({
      page: that.data.page + 1
    });
    if (that.data.tab !== 'all') {
      this.getData({ tab: that.data.tab, page: that.data.page });
    } else {
      this.getData({ page: that.data.page });
    }
  },
  // 关闭--模态弹窗
  cancelChange: function () {
    this.setData({ modalHidden: true });
  },
  // 确认--模态弹窗
  confirmChange: function () {
    this.setData({ modalHidden: true });
    wx.navigateTo({
      url: '/pages/login/login'
    });
  }
})