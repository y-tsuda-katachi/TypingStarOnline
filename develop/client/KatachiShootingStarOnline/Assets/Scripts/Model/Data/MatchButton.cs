using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private TextMeshProUGUI _matchIdLabel;
    [SerializeField] private TextMeshProUGUI _matchPlayersLabel;
    public Match Match { get; private set; }
    
    public MatchButton Init(Match match)
    {
        Match = match;
        button = GetComponent<Button>();
        button.interactable = Match.state == MatchState.Waiting;
        _matchIdLabel.text = Match.matchId;
        _matchPlayersLabel.text = string.Join(',', Match.players);
        return this;
    }

    public MatchButton SetOnClickCB(Action<MatchButton> action)
    {
        button.onClick.AddListener(() => action(this));
        return this;
    }
}