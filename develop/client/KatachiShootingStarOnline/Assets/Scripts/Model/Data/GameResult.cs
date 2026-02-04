using System;
using System.Linq;

[Serializable]
public class GameResult : IReplaceable
{
    public int rank;
    public float elapsedMilliSeconds;
    public int failureCount;

    private string[] strValues =
    {
        Constants.API.Result.Placeholder.RANK,
        Constants.API.Result.Placeholder.ELAPSSED_MILLI_SECONDS,
        Constants.API.Result.Placeholder.FAILURE_COUNT,
    };

    public string GetStrValue(string placeholder)
    {
        switch (placeholder)
        {
            case Constants.API.Result.Placeholder.RANK:
                return rank.ToString();
            case Constants.API.Result.Placeholder.ELAPSSED_MILLI_SECONDS:
                return (elapsedMilliSeconds / 10F).ToString("00:00");
            case Constants.API.Result.Placeholder.FAILURE_COUNT:
                return failureCount.ToString();
        }
        return null;
    }

    public bool HasStrValue(string placeholder)
    {
        return strValues.Contains(placeholder);
    }
}