using System.Diagnostics;

public class GameInspector
{
    private Stopwatch stopwatch;
    private int failureCount;

    public GameInspector()
    {
        stopwatch = new Stopwatch();
        failureCount = 0;
    }

    public float GetElapssedMilliSeconds() => stopwatch.ElapsedMilliseconds;
    public void StartWatching() => stopwatch.Start();
    public void StopWatching() => stopwatch.Stop();
    public int GetFailureCount() => failureCount;
    public void IncrementFailureCount() => failureCount++;

    /// <summary>
    /// 結果をまとめて取得する
    /// </summary>
    public GameResult GetGameResult()
    {
        return new GameResult
        {
            elapsedMilliSeconds = GetElapssedMilliSeconds(),
            failureCount = GetFailureCount()
        };
    }
}