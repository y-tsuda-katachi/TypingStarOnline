using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Match : IReplaceable
{
    public string matchId;
    public MatchState state;
    public List<Player> players;
    public string[] strValues =
    {
        Constants.API.Match.Placeholder.MATCH_ID
    };

    public string GetStrValue(string placeholder)
    {
        switch (placeholder)
        {
            case Constants.API.Match.Placeholder.MATCH_ID:
                return matchId;
        }
        return null;
    }

    public bool HasStrValue(string placeholder)
    {
        return strValues.Contains(placeholder);
    }
}