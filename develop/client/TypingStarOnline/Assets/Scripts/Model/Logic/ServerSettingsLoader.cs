using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ServerSettingsLoader
{
        private static ServerSettings cached;
        public static async Task<ServerSettings> GetServerSettings(string path, bool clearFlag)
        {
                if (clearFlag)
                        cached = default;

                if (cached != null)
                        return cached;

                ServerSettings serverSettings = default;
#if UNITY_EDITOR
                using (var stream = new StreamReader(path))
                {
                        var json = await stream.ReadToEndAsync();

#elif UNITY_WEBGL
                var www = UnityWebRequest.Get(path);
                await www.SendWebRequest();
                var json = www.downloadHandler.text;
#endif
                        if (string.IsNullOrEmpty(json) == false)
                                serverSettings = JsonUtility.FromJson<ServerSettings>(json);
#if UNITY_EDITOR
                        Debug.Log(JsonUtility.ToJson(serverSettings, true));
                }
#endif
                cached = serverSettings;
                return serverSettings;
        }
}