/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using UnityEditor;
using UnityEngine;
using UnrealFPS.AI;

namespace UnrealFPS.Editor
{
	[CustomEditor(typeof(AISpawnManager))]
	[CanEditMultipleObjects]
	public class AISpawnManagerEditor : UEditor
	{
		/// <summary>
		/// Callback on scene GUI
		/// </summary>
		protected virtual void OnSceneGUI()
		{
			AISpawnManager spawnManager = (AISpawnManager) target;

			Vector3 pos = spawnManager.transform.position;
            switch (spawnManager.Shape)
            {
                case SpawnShape.Rectangle:

                    Vector3[] verts = new Vector3[]
                    {
                        new Vector3(pos.x + spawnManager.Weight / 2, pos.y, pos.z - spawnManager.Lenght / 2),
                        new Vector3(pos.x + spawnManager.Weight / 2, pos.y, pos.z + spawnManager.Lenght / 2),
                        new Vector3(pos.x - spawnManager.Weight / 2, pos.y, pos.z + spawnManager.Lenght / 2),
                        new Vector3(pos.x - spawnManager.Weight / 2, pos.y, pos.z - spawnManager.Lenght / 2)
                    };

                    Handles.DrawSolidRectangleWithOutline(verts, new Color(0.25f, 0.25f, 0.25f, 0.1f), new Color(0, 0, 0, 1));

                    foreach (Vector3 posCube in verts)
                    {
                        Handles.color = new Color32(167, 1, 42, 255);
                        Handles.CubeHandleCap(0, posCube, Quaternion.identity, 0.25f, EventType.Repaint);
                    }
                    Handles.color = Color.white;
                    spawnManager.Lenght = Handles.ScaleSlider(spawnManager.Lenght, new Vector3(pos.x, pos.y, pos.z - spawnManager.Lenght / 2), -spawnManager.transform.forward, spawnManager.transform.rotation, 2.0f, 0.5f);
                    spawnManager.Weight = Handles.ScaleSlider(spawnManager.Weight, new Vector3(pos.x + spawnManager.Weight / 2, pos.y, pos.z), spawnManager.transform.right, spawnManager.transform.rotation, 2.0f, 0.5f);
                    spawnManager.Lenght = Handles.ScaleSlider(spawnManager.Lenght, new Vector3(pos.x, pos.y, pos.z + spawnManager.Lenght / 2), spawnManager.transform.forward, spawnManager.transform.rotation, 2.0f, 0.5f);
                    spawnManager.Weight = Handles.ScaleSlider(spawnManager.Weight, new Vector3(pos.x - spawnManager.Weight / 2, pos.y, pos.z), -spawnManager.transform.right, spawnManager.transform.rotation, 2.0f, 0.5f);
                    break;

                case SpawnShape.Circle:

                    Vector3[] cverts = new Vector3[]
                    {
                        new Vector3(pos.x - spawnManager.Radius, pos.y, pos.z),
                        new Vector3(pos.x, pos.y, pos.z + spawnManager.Radius),
                        new Vector3(pos.x + spawnManager.Radius, pos.y, pos.z),
                        new Vector3(pos.x, pos.y, pos.z - spawnManager.Radius)
                    };

                    Handles.color = Color.black;
                    Handles.DrawWireArc(spawnManager.transform.position, Vector3.up, Vector3.forward, 360, spawnManager.Radius);
                    Handles.color = new Color(0.25f, 0.25f, 0.25f, 0.1f);
                    Handles.DrawSolidArc(spawnManager.transform.position, Vector3.up, Vector3.forward, 360, spawnManager.Radius);
                    foreach (Vector3 posCube in cverts)
                    {
                        Handles.color = new Color32(167, 1, 42, 255);
                        Handles.CubeHandleCap(0, posCube, Quaternion.identity, 0.25f, EventType.Repaint);
                    }

                    Handles.color = Color.white;
                    spawnManager.Radius = Handles.ScaleSlider(spawnManager.Radius, new Vector3(pos.x, pos.y, pos.z - spawnManager.Radius), -spawnManager.transform.forward, spawnManager.transform.rotation, 2.0f, 0.5f);
                    spawnManager.Radius = Handles.ScaleSlider(spawnManager.Radius, new Vector3(pos.x + spawnManager.Radius, pos.y, pos.z), spawnManager.transform.right, spawnManager.transform.rotation, 2.0f, 0.5f);
                    spawnManager.Radius = Handles.ScaleSlider(spawnManager.Radius, new Vector3(pos.x, pos.y, pos.z + spawnManager.Radius), spawnManager.transform.forward, spawnManager.transform.rotation, 2.0f, 0.5f);
                    spawnManager.Radius = Handles.ScaleSlider(spawnManager.Radius, new Vector3(pos.x - spawnManager.Radius, pos.y, pos.z), -spawnManager.transform.right, spawnManager.transform.rotation, 2.0f, 0.5f);
                    break;
            }

		}

		/// <summary>
		/// Custom Inspector GUI
		/// </summary>
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			BeginBackground();
			Title("AI Spawn Manager");
			BeginBox();
			base.OnInspectorGUI();
			EndBox();
			EndBackground();
			serializedObject.ApplyModifiedProperties();
		}
	}
}