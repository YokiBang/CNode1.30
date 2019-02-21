var Api = require('../../utils/api.js');
var util = require('../../utils/util.js');
//index.js
//获取应用实例
var app = getApp()
Page({
  data: {
    // tab切换
    currentTab: 0,
    postsList: [],
    hidden: false,
    page: 1,
    limit: 20,
    tab: 'all',
    modalHidden: true,
    id: 0,
    detail: {},
  },
  // 获取点赞数据
  fetchData: function (id) {
    var that = this;
    var ApiUrl = Api.praise +'?roleid='+1;
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

  onPullDownRefresh: function () {
    this.getData();
    console.log('下拉刷新', new Date());
  },
  onReachBottom: function () {
    this.lower();
    console.log('上拉刷新', new Date());
  },
  // 点击获取对应分类的数据
  onTapTag: function (e) {

    var that = this;
    var tab = e.currentTarget.id;
    var index = e.currentTarget.dataset.index;
    that.setData({
      activeIndex: index,
      tab: tab,
      page: 1
    });
    if (tab !== 'all') {
      that.getData({ tab: tab });
    } else {
      that.getData();
    }
  },
  //获取文章列表数据
  getData: function () {
    var that = this;
    var id = that.data.id;
    console.log(id);
    var tab = that.data.tab;
    var page = that.data.page;
    var limit = that.data.limit;
    console.log("swiperChange");
    var currentTab = this.data.currentTab;
    that.setData({ hidden: false });
    if (page == 1) {
      that.setData({ postsList: [] });
    }
    var apiurl="";
    if (currentTab == 2) {
       apiurl = Api.history + '?roleid=' + 1;
    }
    else if(currentTab==1){
       apiurl=Api.collectid + '?Authorid=' + 1;
    }
    else if (currentTab == 0) {
      apiurl = Api.praise + '?roleid=' + 1;
    }
    Api.fetchGet(apiurl, (err, res) => {
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
  },

  swichNav: function (e) {
    console.log("swichnav");
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
    
    this.setData({
      currentTab: e.detail.current,
    })
    var rows=1;
  },
  onLoad: function () {
    var accesstoken = wx.getStorageSync('CuserInfo').id;
    if (!accesstoken) {
      this.setData({ modalHidden: false });
      return;
    }
    this.setData({ id: accesstoken });
    this.getData();
    var that = this;
    var CuserInfo = wx.getStorageSync('CuserInfo');
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