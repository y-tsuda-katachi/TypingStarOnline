using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// タイピング入力制御クラス
/// </summary>
public class TypingController : MonoBehaviour
{
    public enum State
    {
        /// <summary>
        /// 初期化済み
        /// </summary>
        Initialized = 0,
        /// <summary>
        /// タイピングワード待ち
        /// </summary>
        Waiting = 1,
        /// <summary>
        /// 入力待ち
        /// </summary>
        Listening = 2,
        /// <summary>
        /// 終了中
        /// </summary>
        Finishing = 3,
    }

    private TypingPresenter typingPresenter;
    private RomanComparer romanComparer;
    private TypingWord currentWord;
    private int currentIndex = 0; // 現在のインデックス
    private int maxIndex = 0; // 最大インデックス

    public InputAction InputAction { private get; set; }
    public State CurrentState { get; private set; } = State.Initialized;
    public Action OnFailureInput { get; set; }

    private void Awake()
    {
        typingPresenter = FindAnyObjectByType<TypingPresenter>();
        romanComparer = new RomanComparer();
    }

    /// <summary>
    /// 入力の確認
    /// </summary>
    /// <returns>全部の文字が終わったかどうか</returns>
    public bool TryListen()
    {
        var isFinished = false;
        if (InputAction.IsPressed())
        {
            isFinished = Judge();
            // 入力状態をリセット
            InputAction.Reset();
        }
        return isFinished;
    }

    /// <summary>
    /// 判定処理
    /// </summary>
    /// <returns>ワードが終了したかどうか</returns>
    private bool Judge()
    {
        var input = InputAction.activeControl.name; // 入力文字の取得
        var target = currentWord.Roman[currentIndex].ToString(); // 入力対象の取得
        var isCorrect = romanComparer.Compare(target, input); // 一致しているかどうか調べる

        typingPresenter.ChangeColorOf(currentIndex, isCorrect);

        if (isCorrect)
        {
            // 次の文字へ
            if (++currentIndex > maxIndex)
            {
                currentIndex = 0; // 判定文字インデックスのリセット
                typingPresenter.ResetRomanUI();
                return true;
            }
        }
        else
        {
            OnFailureInput();
        }

        return false;
    }

    /// <summary>
    /// タイピングワードを設定する
    /// </summary>
    public void SetTypingWord(TypingWord typingWord)
    {
        if (typingWord is not null)
        {
            currentWord = typingWord;
            maxIndex = typingWord.Roman.Length - 1;
        }
        // ラベルとローマ字を表示
        typingPresenter.ShowTypingWord(currentWord);
    }

    public void GetWaiting()
    {
        typingPresenter.HideLabel();
        typingPresenter.HideRoman();
        CurrentState = State.Waiting;
    }
    public void GetListening()
    {
        typingPresenter.ShowLabel();
        typingPresenter.ShowRoman();
        CurrentState = State.Listening;
    }

    public void GetFinishing()
    {
        typingPresenter.HideLabel();
        typingPresenter.HideRoman();
        CurrentState = State.Finishing;
    }
}
