using System;
using System.Collections.Generic;

/// <summary>
/// JsonUtilityではルート配列をデシリアライズできないため
/// 簡易的にWrapperオブジェクトを生成して
/// そこから配列オブジェクトを取得する
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class JsonHelper<T>
{
    public List<T> root;
}