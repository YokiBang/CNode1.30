// pages/Job/Job.js
var Api = require('../../utils/api.js');
var util = require('../../utils/util.js');
Page({

  /**
   * 页面的初始数据
   */
  data: {
     aa:"2015-06-02",
     bb:"八维招人",
     job:{},
    hidden: false,
    modalHidden: true,
    is_collect: false
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.fetchData(options.id);
  },

  fetchData: function (id) {
    id = 1;
    var that = this;
    var ApiUrl = Api.job + '/' + id ;
    Api.fetchGet(ApiUrl, (err, res) => {
      console.log(res);
      console.log(res.fabutime);
      res.fabutime = util.getDateDiff(new Date(res.fabutime));
      that.setData({ job: res });
      setTimeout(function () {
        that.setData({ hidden: true });
      }, 300);
    })
  },

  //收藏文章
  collect: function (e) {
    var that = this;
    var accesstoken = wx.getStorageSync('CuserInfo');
    var id = e.currentTarget.id;
    if (!id) return;
    if (!accesstoken.accesstoken) {
      that.setData({ modalHidden: false });
      return;
    }
    var ApiUrl = Api.collect + '?author_id=' + accesstoken.id + '&active_id=' + id;
    Api.fetchGet(ApiUrl, (err, res) => {
      if (res) {  
        is_collect = true;
        that.setData({
          collectText: "取消收藏"
        });
      }
      else {
        is_collect = false;
        that.setData({
          collectText: "收藏"
        });
      }
    })
  },
})
