using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClickAutorization : MonoBehaviour
{
	#region list normal

	public static List<int> list = new List<int>();

	public static int CurrentHigherLayer;

	public static void Add(GameObject obj)
	{
		if(V.IsInMain && V.script_Scene_Main_Administrator.ShowAutorization)
			print("add " + obj.gameObject.name);
		Add(obj.layer);
	}

	public static void Add(int layerInt)
	{
		if (V.IsInMain && V.script_Scene_Main_Administrator.ShowAutorization)
			print("add " +layerInt);

		int toAdd = layerInt;

		list.Add(toAdd);

		if (toAdd > CurrentHigherLayer) CurrentHigherLayer = toAdd;
	}

	public static void Remove(GameObject obj)
	{
		Remove(obj.layer);
	}

	public static void Remove(int layerInt)
	{
		if (V.IsInMain && V.script_Scene_Main_Administrator.ShowAutorization)
			print("remove " + layerInt);

		int toRemove = layerInt;

		if (list.Contains(toRemove)) list.Remove(toRemove);

		if (list.Count == 0)
			CurrentHigherLayer = -1;
		else
			CurrentHigherLayer = list.Max();
	}

	public static void Clear()
	{
		CurrentHigherLayer = -1;
		list.Clear();
	}

	public static void Modify(GameObject obj, bool add)
	{
		if (add)
			Add(obj);
		else
			Remove(obj);
	}

	public static bool Autorized(GameObject obj)
	{
		if (list_forbidden.Contains(obj)) return false;

		if (list_exception.Contains(obj)) return true;

		return Autorized(obj.layer);
	}

	public static bool Autorized(int layerInt)
	{
		return layerInt >= CurrentHigherLayer;
	}

	#endregion

	#region exception list

	public static List<GameObject> list_exception = new List<GameObject>();

	public static void Exception_Add(GameObject obj)
	{
		list_exception.Add(obj);
	}

	public static void Exception_Remove(GameObject obj)
	{
		list_exception.Remove(obj);
	}

	public static void Exception_Clear()
	{
		list_exception.Clear();
	}

	#endregion

	#region forbiden list

	/// <summary>
    /// Object that we cannot click on
    /// </summary>
	static List<GameObject> list_forbidden = new List<GameObject>();

	public static void forbidden_Add(GameObject obj)
	{
		list_forbidden.Add(obj);
	}

	public static void forbidden_Remove(GameObject obj)
	{
		list_forbidden.Remove(obj);
	}

	public static void forbidden_Clear()
	{
		list_forbidden.Clear();
	}

	#endregion
}
