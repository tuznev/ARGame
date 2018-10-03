using UnityEngine;
using System.Collections.Generic;

public static class GenericExtension {

	public static void ShuffleList<T> (this List<T> list, List<T> customList = null) {
		if (customList != null)
			list = customList;

		for (int i = 0; i < list.Count; i++) {
			T temp = list[i];
			int id = Random.Range (0, list.Count);
			list[i] = list[id];
			list[id] = temp;
		}


	}
}
