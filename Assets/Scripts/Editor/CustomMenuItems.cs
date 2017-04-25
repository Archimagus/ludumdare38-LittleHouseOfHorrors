using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomMenuItems
{
	[MenuItem("GameObject/RotateLeft %q")]
	static void RotateLeft()
	{
		Rotate(-90);
	}
	[MenuItem("GameObject/RotateLeft %q", true)]
	static bool ValidateRotateLeft()
	{
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}
	[MenuItem("GameObject/RotateRight %e")]
	static void RotateRight()
	{
		Rotate(90);
	}
	static void Rotate(float angle)
	{
		//Selection.activeTransform.Rotate(0, 90 - (Selection.activeTransform.eulerAngles.y % 90), 0);

		//var center = Vector3.zero;
		//foreach (var t in Selection.transforms)
		//{
		//	center += t.position;
		//}
		//center /= Selection.transforms.Length;

		var center = Selection.activeTransform.position;

		foreach (var t in Selection.transforms)
		{
			t.RotateAround(center, Vector3.up, angle);
		}
	}
	[MenuItem("GameObject/RotateRight %e", true)]
	static bool ValidateRotateRight()
	{
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}
	[MenuItem("GameObject/Zero Y %w")]
	static void ZeroY()
	{
		foreach (var t in Selection.transforms)
		{
			Vector3 pos = t.position;
			pos.y = 0;
			pos.x = (Mathf.Round(pos.x / 4)) * 4;
			pos.z = (Mathf.Round(pos.z / 4)) * 4;
			t.position = pos;

		}
	}
	[MenuItem("GameObject/Zero Y %w", true)]
	static bool ValidateZeroY()
	{
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}

	[MenuItem("GameObject/RevertToPrefab &r")]
	static void RevertToPrefab()
	{
		var selection = Selection.gameObjects;

		if (selection.Length > 0)
		{
			foreach (var s in selection)
			{
				Debug.Log("Reverting " + s.name + " to prefab.");
				if (!PrefabUtility.RevertPrefabInstance(s))
				{
					Debug.Log("Cannot revert to prefab something went wrong.");
				}
			}
		}
		else
		{
			Debug.Log("Cannot revert to prefab - nothing selected");
		}
	}


	[MenuItem("Window/Collapse Hierarch %&LEFT")]
	public static void CollapseHierarchy()
	{
		EditorApplication.ExecuteMenuItem("Window/Hierarchy");
		var hierarchyWindow = EditorWindow.focusedWindow;

		var expandMethodInfo = hierarchyWindow.GetType().GetMethod("SetExpandedRecursive");

		foreach (GameObject root in SceneRoots())
		{
			expandMethodInfo.Invoke(hierarchyWindow, new object[] { root.GetInstanceID(), false });
		}
	}

	public static IEnumerable<GameObject> SceneRoots()
	{
		var prop = new HierarchyProperty(HierarchyType.GameObjects);
		var expanded = new int[0];
		while (prop.Next(expanded))
		{
			yield return prop.pptrValue as GameObject;
		}
	}
}
