using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// タイピング用言葉クラス
/// </summary>
public class TypingWord : Word
{
    private string roman;
    public string Roman { get => roman; }
    public TypingWord(string label, string roman)
        : base(label)
    {
        this.roman = roman;
    }
}