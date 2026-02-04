using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Player : IReplaceable
{
    public string playerId;
    public string playerName;
    public GameResult gameResult;
    public PlayerState state;
    private string[] strValues =
    {
        Constants.API.Player.Placeholder.PLAYER_ID,
        Constants.API.Player.Placeholder.PLAYER_NAME
    };

    public bool HasStrValue(string placeholder) => strValues.Contains(placeholder);
    public string GetStrValue(string placeholder)
    {
        switch (placeholder)
        {
            case Constants.API.Player.Placeholder.PLAYER_ID:
                return playerId;
            case Constants.API.Player.Placeholder.PLAYER_NAME:
                return playerName;
        }
        return null;
    }
    public override string ToString() => playerName;
}
