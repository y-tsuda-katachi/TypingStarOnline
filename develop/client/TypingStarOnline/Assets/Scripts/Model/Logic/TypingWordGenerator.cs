using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TypingWordGenerator
{
    /// <summary>
    /// ストリーミングアセットの基底URI
    /// </summary>
    private static readonly string baseUri =
        Application.streamingAssetsPath;
    public static async Task<TypingWord[]> GenerateFromCSV(string matchId)
    {
        var typingWords = new List<TypingWord>();
        var path = Path.Combine(baseUri, $"{matchId}.csv");
#if UNITY_EDITOR
        var streamReader = new StreamReader(path);
        var text = await streamReader.ReadToEndAsync();
#elif UNITY_WEBGL
        var www = UnityWebRequest.Get(path);
        await www.SendWebRequest();
        var text = www.downloadHandler.text;
#endif
        using (var reader = new StringReader(text))
        {
            while (true)
            {
                var line = reader.ReadLine();
                if (line is null)
                    break;
                var words = line.Trim().Split(',');
                typingWords.Add(new TypingWord(
                    words[0], words[1]));
            }
        }
        return typingWords.ToArray();
    }
}