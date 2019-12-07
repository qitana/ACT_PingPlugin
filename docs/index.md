# ACT_PingPlugin

## Ping Overlays

### 標準レイアウト

- [ping.html](ping_oneline.html)

### 1行レイアウト

- [ping_oneline.html](ping_oneline.html)

1行レイアウトの場合は、下記のavg変数を使用して、表示される情報を1つだけにすることをおすすめします。  
例: `https://qitana.github.io/ACT_PingPlugin/ping_oneline.html?avg=30`

## avg変数について

- URLの後ろに `?avg=30` と付けると、30アイテムの平均時間が表示されるようになります。
- URLの後ろに `?avg=5,10,30` と付けると、5アイテム、10アイテム、30アイテムの平均時間が表示されるようになります。
- 何も付けなかった場合は `?avg=5,30,60` が指定されたと見なして表示されます。

