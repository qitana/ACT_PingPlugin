

// 関数
var getParam = function (name, url) {
  if (!url) url = window.location.href;
  name = name.replace(/[\[\]]/g, "\\$&");
  var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
    results = regex.exec(url);
  if (!results) return null;
  if (!results[2]) return '';
  return decodeURIComponent(results[2].replace(/\+/g, " "));
}

var sum = function (arr, fn) {
  if (fn) {
    return sum(arr.map(fn));
  }
  else {
    return arr.reduce(function (prev, current, i, arr) {
      return prev + current;
    });
  }
};
var average = function (arr, fn) {
  return sum(arr, fn) / arr.length;
};


var avg = getParam("avg") ? getParam("avg").split(",").filter(x => isFinite(x)) : [5, 30, 60];
var maxData = Math.max.apply(null, avg);


// データ処理
var vue = new Vue({
  el: '#vue',
  data: {
    updated: false,
    locked: false,
    collapsed: false,
    pingData: [],
    stats: {},
    latest: {
      Timestamp: 0,
      Address: "",
      Status: "",
      RTT: 0,
      TTL: 0,
    },
    lastSucceeded: {
      Timestamp: 0,
      Address: "",
      Status: "",
      RTT: 0,
      TTL: 0,
    }
  },
  mounted: function () {
    this.$nextTick(function () {
      document.addEventListener('onOverlayStateUpdate', this.updateState);
      window.addOverlayListener('onPingStatusUpdateEvent', this.update);
      window.startOverlayEvents();
    });
  },
  destroyed: function () {
    this.$nextTick(function () {
      window.removeOverlayListener('onPingStatusUpdateEvent', this.update);
      document.removeEventListener('onOverlayStateUpdate', this.updateState);
    });
  },
  methods: {
    update: function (e) {
      if (e.type == "onPingStatusUpdateEvent") {
        let data = JSON.parse(e.detail.statusJson)

        // update if ping succeeded
        if (data.Status && data.Status == "Success") {
          this.lastSucceeded = data;
        } else {
          data.Address = this.lastSucceeded.Address;
        }

        // update latest
        this.latest = data;

        // push to pingData (histroy)
        this.pingData.push(data)
        if (this.pingData.length > maxData) {
          for (var i = 0; i < (this.pingData.length - maxData); i++) {
            this.pingData.shift()
          }
        }

        // calcurate averages
        avg.forEach((e, i, a) => {
          let items = this.pingData.slice(-1 * e)

          let successItems = items.filter(x => x.Status == "Success")

          // Loss
          let loss = (successItems.length > 0) ? (1 - (successItems.length / items.length)) * 100 : 0.0

          // rtt
          let rttItems = successItems.map(x => { return x.RTT });
          let rtt = average(rttItems)

          // jitter
          let jitterSum = 0.0
          for (let i = 0; i < rttItems.length - 1; i++) {
            jitterSum += Math.abs(rttItems[i + 1] - rttItems[i])
          }
          let jitter = (jitterSum > 0) ? jitterSum / (rttItems.length - 1) : 0.0

          // update stats
          this.stats[String(e)] = { key: e, loss: loss, rtt: rtt, jitter: jitter }

        })

        this.updated = true;
      }
    },
    updateState: function (e) {
      this.locked = e.detail.isLocked;
    },
    toggleCollapse: function () {
      this.collapsed = !this.collapsed;
    },
  },
  filters: {
    checkLoss: function (e) {
      if (!e) return ''
      if (e.key >= 50) {
        // 1 loss: <2.0%
        if (e.loss > 5) return 'warning2'
        if (e.loss > 1) return 'warning1'
      } else if (e.key >= 30) {
        // 1 loss: 2.0% - 3.3%
        if (e.loss > 6) return 'warning2'
        if (e.loss > 2) return 'warning1'
      } else if (e.key >= 20) {
        // 1 loss: 3.3% - 5.0%
        if (e.loss > 8) return 'warning2'
        if (e.loss > 4) return 'warning1'
      } else if (e.key >= 10) {
        // 1 loss: 5.0% - 10%
        if (e.loss > 10) return 'warning2'
        if (e.loss > 5) return 'warning1'
      } else if (e.key >= 5) {
        // 1 loss: 10% - 20%
        if (e.loss > 20) return 'warning2'
        if (e.loss > 10) return 'warning1'
      } else {
        // 1 loss: >20%
        if (e.loss > 50) return 'warning2'
        if (e.loss > 25) return 'warning1'
      }
      return ''
    },
    checkDelay: function (e) {
      if (!e) return ''
      if (e.rtt >= 100) return 'warning2'
      if (e.rtt >= 50) return 'warning1'
      return ''
    },
    checkJitter: function (e) {
      if (!e) return ''
      if (((e.jitter / e.rtt) * 100) > 25) return 'warning2'
      if (((e.jitter / e.rtt) * 100) > 12.5) return 'warning1'
      return ''
    },
  }
});


