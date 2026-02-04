/// <summary>
/// ゲームの状態
/// </summary>
public enum GameState
{
    /// <summary>
    /// 初期化中
    /// </summary>
    Initializing = 0,
    /// <summary>
    /// プレイ中
    /// </summary>
    Playing = 1,
    /// <summary>
    /// ゲームクリア
    /// </summary>
    GameClear = 2,
    /// <summary>
    /// 終了中
    /// </summary>
    Finalizing = 3,
}