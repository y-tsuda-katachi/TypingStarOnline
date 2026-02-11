using System;

[Serializable]
public class MatchMessage
{
    public MatchMessageType type;
    public string playerId;
    public string message;
}