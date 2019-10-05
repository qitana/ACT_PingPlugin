// データ処理
var pingStatus = new Vue({
  el: '#pingStatus',
  data: {
    updated: false,
    locked: false,
    collapsed: false,
    pingData: {},
    stats: {
      Address: '',
      LossRate: 0,
      RTT: 0,
      Jitter: 0,
      TTL: 0,
      Style: {
        LossRate: '',
        RTT: '',
        Jitter: '',
      }
    },
  },
  attached: function () {
    document.addEventListener('onOverlayDataUpdate', this.update);
    document.addEventListener('onOverlayStateUpdate', this.updateState);
  },
  detached: function () {
    document.removeEventListener('onOverlayStateUpdate', this.updateState);
    document.removeEventListener('onOverlayDataUpdate', this.update);
  },
  methods: {
    update: function (e) {
      this.updated = true;

      this.pingData = e.detail.PingData;

      this.stats.Address = e.detail.PingData.CurrentAddress;
      this.stats.LossRate = parseFloat(e.detail.PingData.Stats30.LossRate).toFixed(1);
      this.stats.RTT = parseFloat(e.detail.PingData.Stats30.RTT).toFixed(1);
      this.stats.Jitter = parseFloat(e.detail.PingData.Stats30.Jitter).toFixed(1);
      this.stats.TTL = e.detail.PingData.CurrentTTL;

      if (this.stats.LossRate > 5.0) {
        this.stats.Style.LossRate = 'Critical';
      } else if (this.stats.LossRate > 0.0) {
        this.stats.Style.LossRate = 'Warning';
      } else {
        this.stats.Style.LossRate = '';
      }

      if (this.stats.RTT > 100.0) {
        this.stats.Style.RTT = 'Critical';
      } else if (this.stats.RTT > 50.0) {
        this.stats.Style.RTT = 'Warning';
      } else {
        this.stats.Style.RTT = '';
      }

      if (this.stats.Jitter > 30.0) {
        this.stats.Style.Jitter = 'Critical';
      } else if (this.stats.Jitter > 15.0) {
        this.stats.Style.Jitter = 'Warning';
      } else {
        this.stats.Style.Jitter = '';
      }

    },
    updateState: function (e) {
      this.locked = e.detail.isLocked;
    },
    toggleCollapse: function () {
      this.collapsed = !this.collapsed;
    }
  }
});

