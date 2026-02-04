using System;
/// <summary>
/// ローマ字の比較クラス
/// </summary>
public class RomanComparer
{
    private string lastInput;

    /// <summary>
    /// 比較対象と入力値とを比較する
    /// </summary>
    /// <param name="target">比較対象</param>
    /// <param name="input">入力値</param>
    /// <returns>合致した文字数 : int<br></br>合致したか : bool</returns>
    public bool Compare(string target, string input)
    {
        // TODO:判定ロジックを作る。。。
        /*
         * 同一視されるべきローマ字↓
         * si = shi,
         * ti = chi,
         * tya = cha,
         * tyu = chu,
         * tye = che,
         * tyo = cho,
         * fu = hu,
         * zi = ji,
         * zya = ja,
         * zyu = ju,
         * zye = je,
         * zyo = jo,
         * 『改定ローマ字のつづり方』を参照
         */

        return input.Equals(target);
    }
}