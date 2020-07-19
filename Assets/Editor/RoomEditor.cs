using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class RoomParameters
{
    public Transform floorPrefab;
    public Transform wallPrefab;
    public Transform cornerPrefab;
    public int width = 4;
    public int height = 4;
    public float spacing = 1.0f;

    public bool IsValid()
    {
        return width >= 1 && height >= 1 && spacing > 0.0f && floorPrefab != null && wallPrefab != null && cornerPrefab != null;
    }
}

public class RoomEditor : EditorWindow
{
    public RoomParameters parameters = new RoomParameters();

    [MenuItem("Example/Room")]
    static void Init()
    {
        RoomEditor window = (RoomEditor)EditorWindow.GetWindow(typeof(RoomEditor));
    }

    void OnGUI()
    {
        parameters.width = EditorGUILayout.IntField("width:", parameters.width);
        parameters.height = EditorGUILayout.IntField("height:", parameters.height);
        parameters.spacing = EditorGUILayout.FloatField("spacing:", parameters.spacing);
        parameters.floorPrefab = EditorGUILayout.ObjectField("floor prefab:", parameters.floorPrefab, typeof(Transform), true) as Transform;
        parameters.wallPrefab = EditorGUILayout.ObjectField("wall prefab:", parameters.wallPrefab, typeof(Transform), true) as Transform;
        parameters.cornerPrefab = EditorGUILayout.ObjectField("wall corner:", parameters.cornerPrefab, typeof(Transform), true) as Transform;

        if (GUILayout.Button("create"))
        {
            Create();
        }
    }

    //void DestroyRoom()
    //{
    //    for (int i = this.transform.childCount; i > 0; --i)
    //        DestroyImmediate(this.transform.GetChild(0).gameObject);
    //}

    void Create()
    {
        RoomParameters p = parameters;
        Transform node = new GameObject("room").transform;

        Transform floor = new GameObject("floor").transform;
        floor.SetParent(node);
        for (int i = 0; i < p.width; i++)
        {
            for (int j = 0; j < p.height; j++)
            {
                Transform t = Instantiate(p.floorPrefab);
                t.position = new Vector3(i * p.spacing, 0, j * p.spacing);
                t.SetParent(floor);
            }
        }

        Transform walls = new GameObject("walls").transform;
        walls.SetParent(node);
        List<Transform> wallSections = new List<Transform>();

        float halfWidth = p.spacing / 2.0f;
        wallSections.AddRange(Duplicate(p.wallPrefab, p.width, new Vector3(0, 0, -halfWidth), new Vector3(p.spacing, 0, 0), Quaternion.Euler(0.0f, 0.0f, 0.0f), walls));
        wallSections.AddRange(Duplicate(p.wallPrefab, p.width, new Vector3(0, 0, p.height * p.spacing - halfWidth), new Vector3(p.spacing, 0, 0), Quaternion.Euler(0.0f, 180.0f, 0.0f), walls));
        wallSections.AddRange(Duplicate(p.wallPrefab, p.height, new Vector3(-halfWidth, 0, 0), new Vector3(0, 0, p.spacing), Quaternion.Euler(0.0f, 90.0f, 0.0f), walls));
        wallSections.AddRange(Duplicate(p.wallPrefab, p.height, new Vector3(p.width * p.spacing - halfWidth, 0), new Vector3(0, 0, p.spacing), Quaternion.Euler(0.0f, 270.0f, 0.0f), walls));

        Transform corners = new GameObject("corners").transform;
        corners.SetParent(node);
        Transform corner;
        corner = CopyTo(p.cornerPrefab, new Vector3(-halfWidth, 0, -halfWidth), corners);
        //corner.gameObject.AddComponent<SeeThrough>();

        corner = CopyTo(p.cornerPrefab, new Vector3(-halfWidth, 0, p.height * p.spacing - halfWidth), corners);
        //corner.gameObject.AddComponent<SeeThrough>();

        corner = CopyTo(p.cornerPrefab, new Vector3(p.width * p.spacing - halfWidth, 0, -halfWidth), corners);
        //corner.gameObject.AddComponent<SeeThrough>();

        corner = CopyTo(p.cornerPrefab, new Vector3(p.width * p.spacing - halfWidth, 0, p.height * p.spacing - halfWidth), corners);
        //corner.gameObject.AddComponent<SeeThrough>();
    }

    List<Transform> Duplicate(Transform obj, int n, Vector3 startPos, Vector3 delta, Quaternion rotation, Transform parent)
    {
        List<Transform> copies = new List<Transform>();
        for (int i = 0; i < n; i++)
        {
            Transform t = Instantiate(obj);
            t.position = startPos + i * delta;
            t.localRotation = rotation;
            t.SetParent(parent.transform);
            copies.Add(t);
        }
        return copies;
    }

    Transform CopyTo(Transform obj, Vector3 position, Transform parent)
    {
        Transform t = Instantiate(obj);
        t.position = position;
        t.SetParent(parent);
        return t;
    }
}
