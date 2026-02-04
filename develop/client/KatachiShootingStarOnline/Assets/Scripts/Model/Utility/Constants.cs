using UnityEngine;

public struct Constants
{
    #region シーン名
    public struct Scene
    {
        /// <summary>
        /// タイトルシーン名
        /// </summary>
        public const string TITLE = "Title";
        /// <summary>
        /// マッチングシーン名
        /// </summary>
        public const string MATCH = "Match";
        /// <summary>
        /// タイピングシーン名
        /// </summary>
        public const string TYPING = "Typing";
        /// <summary>
        /// リザルトシーン名
        /// </summary>
        public const string RESULT = "Result";
    }
    #endregion

    #region PlayerPrefsキー
    public struct PlayerPrefsKey
    {
        /// <summary>
        /// プレイヤーキー
        /// </summary>
        public const string PLAYER = "player";
        /// <summary>
        /// 最後に参加したマッチキー
        /// </summary>
        public const string LAST_MATCH = "last_match";
        /// <summary>
        /// サーバー設定キー
        /// </summary>
        public const string SERVER_SETTINGS = "serverSettings";
    }
    #endregion

    #region APIキー名
    public struct API
    {
        /// <summary>
        /// アクチュエータ機能
        /// </summary>
        public struct Actuator
        {
            public const string BASE_KEY = "actuator";
            public const string HEALTH_KEY = "health";
        }

        /// <summary>
        /// プレイヤー機能
        /// </summary>
        public struct Player
        {
            public const string BASE_KEY = "player";
            public const string CONNECT_KEY = "connect";
            public const string DISCONNECT_KEY = "disconnect";

            public struct Placeholder
            {
                public const string PLAYER_ID = "{playerId}";
                public const string PLAYER_NAME = "{playerName}";
            }
        }

        /// <summary>
        /// マッチ機能
        /// </summary>
        public struct Match
        {
            public const string BASE_KEY = "match";
            public const string GET_ALL_KEY = "getAll";
            public const string GET_KEY = "get";
            public const string ENTER_KEY = "enter";
            public const string EXIT_KEY = "exit";
            public const string START_KEY = "start";

            public struct Placeholder
            {
                public const string MATCH_ID = "{matchId}";
            }
        }

        /// <summary>
        /// リザルト機能
        /// </summary>
        public struct Result
        {
            public const string BASE_KEY = "result";
            public const string POST_KEY = "post";

            public struct Placeholder
            {
                public const string RANK = "{rank}";
                public const string ELAPSSED_MILLI_SECONDS = "{time}";
                public const string FAILURE_COUNT = "{count}";
            }
        }

        /// <summary>
        /// WebSocket通信用キー
        /// </summary>
        public struct WebSocket
        {
            public const string BASE_KEY = "websocket";
            public const string MATCH_MESSAGE = "matchMessage";

            public struct HeaderName
            {
                public const string MATCH_ID = "Match-Id";
            }
        }
    }
    #endregion

    #region ストリーミングアセット名
    public struct StreamingAssets
    {
        /// <summary>
        /// デフォルトワード
        /// </summary>
        public const string DEFAULT_WORDS = "DefaultWords.csv";
        /// <summary>
        /// サーバー設定ファイル
        /// </summary>
        public const string SERVER_SETTINGS = "serverSettings.json";
    }
    #endregion

    #region ディスプレイ用文字
    public struct Label
    {
        public const string GAME_START = "よーいどん！";
        public const string GAME_FINISHED = "そこまで！";
        public const string SERVER_UP = "良い";
        public const string SERVER_DOWN = "悪い";
    }
    #endregion

    #region 数値周り
    public struct Float
    {
        public const float RESULT_SCALE = 1.0F;
        public const float ONE_TO_THIRD_SCALE = 0.6F;
        public const float FOURTH_TO_TEN_SCALE = 0.3F;
    }
    #endregion
}