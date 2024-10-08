# ACT_PingPlugin
The add-on of [OverlayPlugin](https://github.com/OverlayPlugin/OverlayPlugin) which shows ping status for connected server.
現在接続中のゲームサーバーに対するpingステータスをオーバーレイ表示する、[OverlayPlugin](https://github.com/OverlayPlugin/OverlayPlugin) のアドオンです。 

[日本語の説明はこちら / README for ja-JP is here](#Japanese)

## Requirements
- [OverlayPlugin](https://github.com/OverlayPlugin/OverlayPlugin)

## Features
It identifies the server with which the FFXIV client is currently communicating, pings that server, and measures information such as delays.

## How to use
1. Install [OverlayPlugin](https://github.com/OverlayPlugin/OverlayPlugin).
2. Download DFAPlugin from release page and extract it to any folder.  
It is recommended that you create and place a separate folder with other plugins.
3. Install PingPlugin as an ACT plugin.
4. Restart ACT once.
5. Confirm that Ping EventSource is added to the OverlayPlugin setting screen.
6. Add an overlay with OverlayPlugin. Select "MiniParse" as the type.
7. On the setting screen, set the URL to `https://qitana.github.io/ACT_PingPlugin/ping.html`.
8. Verify that information is displayed during the game. 

## Other overlay type
See https://qitana.github.io/ACT_PingPlugin/

# Japanese

## できること
-現在FFXIVクライアントが通信しているサーバーを特定し、そのサーバーにpingを実行し、遅延等の情報を測定します。

## インストール方法
1. [OverlayPlugin](https://github.com/OverlayPlugin/OverlayPlugin) をACTのプラグインとして導入します。
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
