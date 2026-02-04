/// <summary>
/// リザルトの状態
/// </summary>
public enum ResultState
{
    /// <summary>
    /// 初期化済み
    /// </summary>
    Initialized = 0,
    /// <summary>
    /// 待機中
    /// </summary>
    Waiting = 1,
    /// <summary>
    /// 結果取得済み
    /// </summary>
    Resulted = 2,
}