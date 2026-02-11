using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNameLabel;
    private Player player;
    private List<ResultLabel> resultLabels;
    public Player Player { get => player; private set => player = value; }

    public Result Init(Player player)
    {
        Player = player;
        resultLabels = GetComponentsInChildren<ResultLabel>()?.ToList();
        resultLabels.ForEach(label =>
            label
                .InitValueLabel(
                    Player.gameResult.GetStrValue(label.Value))
            );
        _playerNameLabel.text = Player.GetStrValue(_playerNameLabel.text);
        return this;
    }
}