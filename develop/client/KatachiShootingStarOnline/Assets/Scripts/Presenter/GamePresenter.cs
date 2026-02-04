using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

/// <summary>
/// ゲーム画面のUI表示クラス
/// </summary>
public class GamePresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameLabel;
    [SerializeField] private TextMeshProUGUI _timeLabel;
    [SerializeField] private GameObject _playerIconPanel;
    [SerializeField] private GameObject _playerIconPrefab;
    private List<PlayerIcon> playerIcons;

    public void InitUI()
    {
        if (_gameLabel) _gameLabel.text = string.Empty;
        if (_timeLabel) _timeLabel.text = string.Empty;
    }

    /// <summary>
    /// 案内を表示させる
    /// </summary>
    /// <param name="text">お知らせテキスト</param>
    public async Task Annouce(string text)
    {
        await SetGameLabel(text);
    }

    /// <summary>
    /// 開始&終了アナウンスラベルの設定
    /// </summary>
    /// <param name="text">表示テキスト</param>
    /// <returns></returns>
    public async Task SetGameLabel(string text)
    {
        foreach (var c in text)
        {
            // 一文字ずつ表示させる
            _gameLabel.text += c;
            // 間隔を少し空ける
            await UniTask.Delay(100);
        }

        // 最後まで表示したら少し待つ
        await UniTask.Delay(1000);
    }

    /// <summary>
    /// 経過時間ラベルの設定
    /// </summary>
    /// <param name="elapsedMilliSeconds">経過時間</param>
    public void SetTimeLabel(float elapsedMilliSeconds)
    {
        if (_timeLabel)
        {
            var text = elapsedMilliSeconds / 10f;

            _timeLabel.text = text.ToString("00:00");
        }
    }

    public void SetPlayerIcons(List<Player> players)
    {
        var iconList = new List<PlayerIcon>();
        players.ForEach(player =>
        {
            iconList.Add(
                Instantiate(
                    _playerIconPrefab,
                    _playerIconPanel.transform,
                    false)
                .GetComponent<PlayerIcon>()
                .Init(player)
            );
        });
        playerIcons = iconList;
    }

    public void UpdatePlayerIcon(string playerId, float progress)
    {
        playerIcons
            .First(pi => pi
                .Player
                    .playerId
                    .Equals(playerId))
            .UpdateProgress(progress);
    }

    public void ShowTimeLabel() { if (_timeLabel) _timeLabel.enabled = true; }
    public void ShowGameLabel() { if (_gameLabel) _gameLabel.enabled = true; }
    public void ShowPlayerIconPanel() => _playerIconPanel?.SetActive(true);
    public void HideTimeLabel() { if (_timeLabel) _timeLabel.enabled = false; }
    public void HideGameLabel() { if (_gameLabel) _gameLabel.enabled = false; }
    public void HidePlayerIconPanel() => _playerIconPanel?.SetActive(false);
}