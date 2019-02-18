var Api = require('../../utils/api.js');
var util = require('../../utils/util.js');

Page({
  data: {
    postsList: [],
    page: 1,
    limit: 20,
    modalHidden: true,
    id: 0
  },
  onLoad: function () {
    var accesstoken = wx.getStorageSync('CuserInfo').id;
    if (!accesstoken) {
      this.setData({ modalHidden: false });
      return;
    }
    this.setData({ id: accesstoken });
    this.getData();
  },
  onPullDownRefresh: function () {
    this.getData();
    console.log('下拉刷新', new Date());
  },
  onReachBottom: function () {
    this.lower();
    console.log('上拉刷新', new Date());
  },
  //获取文章列表数据
  getData: function () {
    var that = this;
    var id = that.data.id;
    var page = that.data.page;
    var limit = that.data.limit;
    var ApiUrl = Api.readableList + '?AuthorId=' + id;
    that.setData({ hidden: false });
    if (page == 1) {
      that.setData({ postsList: [] });
    }
    Api.fetchGet(ApiUrl, (err, res) => {
      console.log(res);
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

  // 滑动底部加载
  lower: function () {
    console.log('滑动底部加载', new Date());
    var that = this;
    that.setData({
      page: that.data.page + 1
    });
    this.getData({ page: that.data.page });
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