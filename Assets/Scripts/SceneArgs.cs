using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneArgs : object {

	public static List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>();

	public static void addArg(string key, string value)
	{
		args.Add (new KeyValuePair<string, string>(key, value));
	}

	public static string getArg(string key)
	{
		foreach (var kv in args) {
			if (kv.Key == key) {
				return kv.Value;
			}
		}
		return null;
	}

	public static void clear()
	{
		args.Clear ();
	}
}
