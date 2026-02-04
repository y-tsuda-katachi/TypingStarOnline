using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    private ResultController resultController;
    private WebSocketController webSocketController;

    private void Awake()
    {
        resultController = FindAnyObjectByType<ResultController>();
        webSocketController = FindAnyObjectByType<WebSocketController>();

        webSocketController.OnGameResultMessage += OnGameResultMessage;
    }

    private async void Start()
    {
        await SendGameResult();
        //テストコード
        /*var player = new Player
        {
            playerId = "0001",
            playerName = "テスト1",
            gameResult = new GameResult
            {
                rank = 1,
                elapsedMilliSeconds = 10f,
                failureCount = 1,
            }
        };
        resultController.UpdateMatchResult(player, new Match
        {
            players = new System.Collections.Generic.List<Player>()
            {
                player,
                new Player
                {
                    playerId = "0002",
                    playerName = "テスト2",
                    gameResult = new GameResult
                    {
                        rank = 2,
                        elapsedMilliSeconds = 20f,
                        failureCount = 2,
                    },
                },
                new Player
                {
                    playerId = "0003",
                    playerName = "テスト3",
                    gameResult = new GameResult
                    {
                        rank = 4,
                        elapsedMilliSeconds = 30f,
                        failureCount = 3,
                    },
                },
                new Player
                {
                    playerId = "0004",
                    playerName = "テスト4",
                    gameResult = new GameResult
                    {
                        rank = 3,
                        elapsedMilliSeconds = 40f,
                        failureCount = 4,
                    },
                },
                new Player
                {
                    playerId = "0005",
                    playerName = "テスト5",
                    gameResult = new GameResult
                    {
                        rank = 5,
                        elapsedMilliSeconds = 50f,
                        failureCount = 5,
                    },
                },
                new Player
                {
                    playerId = "0006",
                    playerName = "テスト6",
                    gameResult = new GameResult
                    {
                        rank = 7,
                        elapsedMilliSeconds = 60f,
                        failureCount = 6,
                    },
                },
                new Player
                {
                    playerId = "0007",
                    playerName = "テスト7",
                    gameResult = new GameResult
                    {
                        rank = 6,
                        elapsedMilliSeconds = 70f,
                        failureCount = 7,
                    },
                },
                new Player
                {
                    playerId = "0008",
                    playerName = "テスト8",
                    gameResult = new GameResult
                    {
                        rank = 8,
                        elapsedMilliSeconds = 80f,
                        failureCount = 8,
                    },
                },
                new Player
                {
                    playerId = "0009",
                    playerName = "テスト9",
                    gameResult = new GameResult
                    {
                        rank = 9,
                        elapsedMilliSeconds = 90f,
                        failureCount = 9,
                    },
                },
                new Player
                {
                    playerId = "0010",
                    playerName = "テスト10",
                    gameResult = new GameResult
                    {
                        rank = 10,
                        elapsedMilliSeconds = 100f,
                        failureCount = 10,
                    },
                },
            }
        });
        resultController.GetResulted(); */
    }

    public async void GoBackMatch()
    {
        await SceneManager.LoadSceneAsync(Constants.Scene.MATCH);
    }
    private async Task UpdateLastMatch()
    {
        Prefs.LastMatch = await resultController.GetGameResult(Prefs.Player, Prefs.LastMatch);
        resultController.UpdateMatchResult(Prefs.Player, Prefs.LastMatch);
    }

    private async Task SendGameResult()
    {
        // ロード中
        resultController.GetWaiting();
        // 結果をサーバーへ送信
        await resultController.PostGameResult(Prefs.Player, Prefs.LastMatch);
        // WSメッセージの送信
        webSocketController.SendMatchMessage(new MatchMessage
        {
            type = MatchMessageType.GameResultMessage,
            playerId = Prefs.Player.playerId,
            message = JsonUtility.ToJson(new GameResultMessage())
        });
        // TODO: テキトーに待たせる
        await UniTask.WaitForSeconds(2);
        // マッチ結果を更新する
        await UpdateLastMatch();
        resultController.GetResulted();
    }
    private async void OnGameResultMessage(MatchMessage msg) => await UpdateLastMatch();
}
