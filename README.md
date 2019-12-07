# ACT_PingPlugin

現在接続中のゲームサーバーに対するpingステータスをオーバーレイ表示する、[ngld/OverlayPlugin](https://github.com/ngld/OverlayPlugin) のアドオンです。 

## 注意事項
**v2.x からは ngld/OverlayPlugin でないと動きません！hibiyasleep/OverlayPlugin では動きません！  
また、ngld/OverlayPlugin と hibiyasleep/OverlayPlugin は併用できません！**

## できること
-現在FFXIVクライアントが通信しているサーバーを特定し、そのサーバーにpingを実行し、遅延等の情報を測定します。


## インストール方法
1. [ngld/OverlayPlugin](https://github.com/ngld/OverlayPlugin) をACTのプラグインとして導入します。
2. Release から PingPlugin をダウンロードし、任意のフォルダに展開します。  
   他のプラグインと同じフォルダに入れないよう、別フォルダを作って配置することをおすすめします。
3. PingPlugin を ACTのプラグインとして導入します。
4. PingPlugin を OverlayPlugin に認識させるため、一度ACTを再起動します。
5. OverlayPlugin の設定画面に Ping EventSource が増えていることを確認します。
6. OverlayPlugin で オーバーレイを追加します。種類は `MiniParse` を選びます。
7. 設定画面でURLをとりあえず `https://qitana.github.io/ACT_PingPlugin/ping.html` を設定します。
8. ゲーム中に情報が表示されることを確認します。

## URLと設定について

https://qitana.github.io/ACT_PingPlugin/  
を参照して下さい。
