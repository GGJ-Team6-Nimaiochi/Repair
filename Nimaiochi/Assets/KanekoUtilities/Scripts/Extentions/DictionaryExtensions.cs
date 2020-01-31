using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public static class DictionaryExtensions
{
    public static V SafeGetValue<K, V>(this Dictionary<K, V> dic, K key)
	{
		V value;
		dic.TryGetValue(key, out value);
		return value;
	}
}
