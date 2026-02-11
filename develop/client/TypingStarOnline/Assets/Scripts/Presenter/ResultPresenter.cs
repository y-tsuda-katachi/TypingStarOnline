using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// リザルト画面のプレゼンター
/// </summary>
public class ResultPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _waitingPanel;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private Transform _content;
    [SerializeField] private Transform _oneToThird;
    [SerializeField] private Transform _fourthToTen;
    [SerializeField] private GameObject _resultPrefab;
    private List<Result> playerResults;

    public void ShowMatchResult(string playerId, Match match)
    {
        var temp = new List<Result>();
        match
            .players
                .OrderBy(p => p.gameResult.rank)
                .ToList()
                .ForEach(p =>
                {
                    if (p.playerId.Equals(playerId))
                    {
                        var result = Instantiate(_resultPrefab, _content, false);
                        temp.Add(result.GetComponent<Result>().Init(p));
                    }
                    else
                    {
                        Result result = default;
                        Transform parent = default;
                        var scale = 0f;
                        var rank = p.gameResult.rank;
                        if (1 <= rank && rank <= 3)
                        {
                            parent = _oneToThird;
                            scale = Constants.Float.ONE_TO_THIRD_SCALE;
                        }
                        else if (4 <= rank && rank <= 10)
                        {
                            parent = _fourthToTen;
                            scale = Constants.Float.FOURTH_TO_TEN_SCALE;
                        }
                        result = Instantiate(_resultPrefab, parent, false).GetComponent<Result>();
                        result.transform.localScale *= scale;
                        temp.Add(result.Init(p));
                    }
                });
        playerResults = temp;
    }

    public void ShowResultPanel() => _resultPanel?.SetActive(true);
    public void ShowWaitingPanel() => _waitingPanel?.SetActive(true);
    public void HideResultPanel() => _resultPanel?.SetActive(false);
    public void HideWaitingPanel() => _waitingPanel?.SetActive(false);
}