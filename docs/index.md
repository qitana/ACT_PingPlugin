# ACT_PingPlugin

## Ping Overlays

### 標準レイアウト (Normal Layout)

- [ping.html](ping_oneline.html)

### 1行レイアウト (One-Line Layout)

- [ping_oneline.html](ping_oneline.html)

1行レイアウトの場合は、下記のavg変数を使用して、表示される情報を1つだけにすることをおすすめします。  
If useing One-Line Layout, recommend to use `avg` variable.  
例: `https://qitana.github.io/ACT_PingPlugin/ping_oneline.html?avg=30`

## avg変数について (avg variable)



- URLの後ろに `?avg=30` と付けると、30アイテムの平均時間が表示されるようになります。  
`?avg=30` means show 30 items average only.
- URLの後ろに `?avg=5,10,30` と付けると、5アイテム、10アイテム、30アイテムの平均時間が表示されるようになります。  
`?avg=5,10,30` means show 5, 10 and 30 items average.
- 何も付けなかった場合は `?avg=5,30,60` が指定されたと見なして表示されます。  
If `avg` is not set, same as `?avg=5,30,60`.