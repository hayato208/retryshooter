# NovelRPG
ノベルRPG
Git運用は「GitHub Flow」
⇒安定バージョンはmasterブランチ。Unity上でmasterブランチから各用途に適したブランチを切る。切るブランチ名は「yyyymmdd_用途_内容」
ブランチ名用途
開発・機能追加：dev
修正：fix
環境構築：env
Unityを使用したGithubマージ方法
１）masterブランチからブランチを切って修正を行う
２）GitHub UnityのChangesタブでローカルリポジトリの修正を行う
３）GitHub UnityのPushタブでリモートリポジトリにプッシュする
４）GitHub上（https://github.com/hayato208/unity1week_24th）でプルリクエストを行う（Contribute→Open pull requestの順にクリック）
５）マージ処理を行う（Merge pull request→confirm mergeの順にクリック）
＊４）５）の参考https://qiita.com/samurai_runner/items/7442521bce2d6ac9330b

GitHub for UnityのSign inが外れたら
１）ブラウザであらかじめGitHubにサインインする
２）GitHub for Unityの右上Sign inからGitHubにサインインする
３）GitHubタブのSign in with your browserをクリックする
４）Webブラウザが起動し（画面真っ白）10秒程度待機後Unityを起動するとSign inがユーザーアカウント名に変更している

Consoleのデバッグで『NullReferenceException: Object reference not set to an instance of an object
GitHub.Unity.Window.Update () (at C:/projects/unity/src/UnityExtension/Assets/Editor/GitHub.Unity/UI/Window.cs:539)』が表示されたら
１）GitHub for UnityのタブをChangesかHistory表示後『再生』クリック。