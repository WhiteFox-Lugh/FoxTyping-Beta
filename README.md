# CheetahTyping

## 概要

初心者からタイパーまで遊べるタイピングゲームを作る

## 機能とか

### 入力方法

ローマ字、英文の2種類を用意するつもり（余裕があればかな入力も）。

ローマ字のレギュレーション: ひらがな、数字、伸ばし棒、句読点、読点（1キーで1文字）
英文のレギュレーション: 英文に出てきそうなもの

とりあえずローマ字を先に実装する

英文は例文が欲しい

### 文章

完全な文を考えて逐一登録するのは大変なので、短いワードをくっつけて文章を生成する（前半の語群からランダム + 後半の語群からランダム みたいにする）。

構成パターン:

- 1: 「なんちゃら」な「名詞」、「なんちゃら」は「名詞」
- 2: 「名詞」が「どうした」

### タイピングワード投稿機能

語をユーザに投稿してもらうと手間が省けそう。

あまり望ましくないワード（公序良俗に反する、宗教的な観点からNGなワード、単純に打ちにくすぎるワード、滅多に出てこない「きぇ」とか「ゑ」みたいなのを含む）は弾かないといけないので実装するかどうか悩む。

### ミスタイプ

ミスタイプの多いキーを表示したい。

余裕があれば入力の速い文字、遅い文字を表示みたいな機能も。

### Practice モード

初めての人向け。ひらがな1文字単位で打つとか。

### Easy, Normal, Lunatic モード

違いは以下の通り

- 文章の長さ
  - Normal / Lunatic: 前半の語群 + 後半の語群
  - Easy: 後半の語群のみ
- ローマ字表示
  - Lunatic: なし（すでに打ち終わった文字を表示、ミスタイプを表示）
  - Easy / Normal: まだ打っていない文字を全て表示(e-typing 風)
- スコア
  - Normal: 次の「スコア表示」で示す1プレイスコアの値をそのまま用いる
  - Easy: 1プレイスコアの値の 80 %

### Lunatic モード

指定速度以上かつノーミスで打ち続けるライフ制モード。

### スコア表示

1プレイスコア = kpm x (1.05 + MAX(-1.00, k * log(Accuracy)))

k = 1 + 2284815.0 / 2406151.0

Accuracy 1.00 なら kpm x 1.05、Accuracy 0.95 だと kpm と一致。

Accuracy 0.95 で 1kpm あげると 1 ポイント上がるような計算式でそれなりに意味を持たせている

95% 以上の正確さなら毎パソの「特別点」的な感じでボーナス点をあげたい

Easy モードはもうちょっと割り引きたい（双曲線関数のオーダーになりました）

### レーティング機能

スコアの Best 10 の合計値でレートをつけることにする（音ゲーっぽい）。

レートカラーの予定（かっこ内はスピードと正確さの目安）

- 灰色:
- 黒色: 100 x 10 = 1,000 pts (100 kpm / 95%)
- 茶色: 150 x 10 = 1,500 pts (150 kpm / 95%) : 大学生平均程度
- 青色: 200 x 10 = 2,000 pts (200 kpm / 95%)
- 水色: 250 x 10 = 2,500 pts (250 kpm / 95%) : e-typing 平均程度
- 緑色: 300 x 10 = 3,000 pts (300 kpm / 95%)
- 黄色: 350 x 10 = 3,500 pts (350 kpm / 95%) : 情報系はこれくらいありそう
- 橙色: 400 x 10 = 4,000 pts (400 kpm / 95%)
- 赤色: 450 x 10 = 4,500 pts (450 kpm / 95%)
- 紫色: 500 x 10 = 5,000 pts (500 kpm / 95%)
- 銅　: 550 x 10 = 5,500 pts (550 kpm / 95%) : ようこそタイパーの世界へ
- 銀　: 650 x 10 = 6,500 pts (650 kpm / 95%)
- 金　: 750 x 10 = 7,500 pts (750 kpm / 95%)
- 虹　: 850 x 10 = 8,500 pts (850 kpm / 95%) : 殿堂入り
- 以降 500 pts.ごとに星マークとかつけて転生みたいな感じにする

次のランクまでを表示するだけだといまいちわからないので、ベスト1の記録を10回出した時の
予測レート値を Possible みたいに表示しておきたい

Best 10 が埋まっていなければ Predict みたいにしておく

適当なデータ形式で保存する必要がある

### ランキング機能

レーティング機能つけるならレートによってランキングつけると良さそう

これはユーザとデータを紐づける仕組みが必要

### レートグラフ化

レートの上昇をグラフ化できたら面白そう

### SNS 連携

したいよね

## デザイン, インタフェース関連

### 操作

キーボードだけで操作が完結するが、一応マウスでもOKみたいなスタンス（本当はタイピングゲームなのでキーボードで全部済ませて欲しい！という気持ちがある）

### モチーフ

今のところはチーター（ロゴとか背景とか）

### お借りした素材

- フォント
- 木漏れ日ゴシック : http://modi.jpn.org/font_komorebi-gothic.php
- https://befonts.com/brela-typeface.html
- SE, BGM : まだ借りてない
