using System.Threading.Tasks;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    private ResultPresenter resultPresenter;
    public ResultState CurrentState { get; private set; } = ResultState.Initialized;

    private void Awake()
    {
        resultPresenter = FindAnyObjectByType<ResultPresenter>();
    }

    public async Task<Match> GetGameResult(Player player, Match match)
    {
        return await REST.GetMatch(player, match);
    }

    public async Task<bool> PostGameResult(Player player, Match match)
    {
        return await REST.PostResult(player, match);
    }

    public void UpdateMatchResult(Player player, Match match)
    {
        resultPresenter.ShowMatchResult(player.playerId, match);
    }

    public void GetWaiting()
    {
        resultPresenter.HideResultPanel();
        resultPresenter.ShowWaitingPanel();
        CurrentState = ResultState.Waiting;
    }

    public void GetResulted()
    {
        resultPresenter.HideWaitingPanel();
        resultPresenter.ShowResultPanel();
        CurrentState = ResultState.Resulted;
    }

}