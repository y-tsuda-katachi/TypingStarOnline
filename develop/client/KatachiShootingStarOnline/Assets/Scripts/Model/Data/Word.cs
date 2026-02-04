/// <summary>
/// 表示用言葉クラス
/// </summary>
public class Word
{
    protected string label;
    public string Label { get => label; }
    public Word(string label)
    {
        this.label = label;
    }
}