using TMPro;
using UnityEngine;

/// <summary>
/// タイピング画面のUI表示クラス
/// </summary>
public class TypingPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _roman;
    [SerializeField] private TextColor _correctColor; // 正解のテキスト色
    [SerializeField] private TextColor _incorrectColor; // 不正解のテキスト色

    private void Awake() => InitUI();
    /// <summary>
    /// タイピングUIの初期化
    /// </summary>
    public void InitUI()
    {
        if (_label) _label.text = string.Empty;
        if (_roman) _roman.text = string.Empty;
    }

    /// <summary>
    /// タイピングワードを画面に表示
    /// </summary>
    /// <param name="typingWord">表示するタイピングワード</param>
    public void ShowTypingWord(TypingWord typingWord)
    {
        if (_label) _label.text = typingWord.Label;
        if (_roman) _roman.text = typingWord.Roman;
    }

    private void SetVertexColors(TMP_CharacterInfo info, Color32 newColor)
    {
        var meshIndex = info.materialReferenceIndex;
        var vertIndex = info.vertexIndex;
        var vertColors = _roman.textInfo.meshInfo[meshIndex].colors32;
        vertColors[vertIndex + 0] = newColor;
        vertColors[vertIndex + 1] = newColor;
        vertColors[vertIndex + 2] = newColor;
        vertColors[vertIndex + 3] = newColor;
        vertColors[vertIndex + 3] = newColor;
    }

    /// <summary>
    /// 新しい文字色をセット
    /// </summary>
    /// <param name="characterIndex">文字インデックス</param>
    /// <param name="newColor">新しい文字色</param>
    private void SetNewColor(int characterIndex, Color32 newColor)
    {
        SetVertexColors(_roman.textInfo.characterInfo[characterIndex], newColor);

        _roman.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    /// <summary>
    /// 全部に新しい文字色をセット
    /// </summary>
    /// <param name="newColor">新しい文字色</param>
    private void SetNewColorToAll(Color32 newColor)
    {
        foreach (var info in _roman.textInfo.characterInfo)
        {
            SetVertexColors(info, newColor);
        }

        _roman.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    /// <summary>
    /// 入力があったときの色の変更
    /// </summary>
    /// <param name="characterIndex">文字インデックス</param>
    public void ChangeColorOf(int characterIndex, bool isCorrect)
    {
        SetNewColor(characterIndex,
            isCorrect ? _correctColor.Color : _incorrectColor.Color);
    }

    /// <summary>
    /// ローマ字状態をリセット
    /// </summary>
    public void ResetRomanUI()
    {
        SetNewColorToAll(new Color32(0, 0, 0, 255));
    }

    public void ShowLabel() { if (_label) _label.enabled = true; }
    public void ShowRoman() { if (_roman) _roman.enabled = true; }
    public void HideLabel() { if (_label) _label.enabled = false; }
    public void HideRoman() { if (_roman) _roman.enabled = false; }
}