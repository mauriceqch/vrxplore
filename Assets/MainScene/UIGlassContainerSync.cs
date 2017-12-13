using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
class UIGlassContainerSync
{
    static UIGlassContainerSync()
    {
        EditorApplication.update += Update;
    }

    static void Update()
    {
        Debug.Log("Updating");
        GameObject[] objects = GameObject.FindGameObjectsWithTag("UIGlassContainer");
        foreach (GameObject obj in objects)
        {
            UIGlassContainerReferences refs = obj.GetComponent<UIGlassContainerReferences>();
            GameObject UIGlass = refs.UIGlass;
            GameObject Canvas = refs.Canvas;

            Vector3 UIGlassScale = UIGlass.transform.localScale;
            Vector3 CanvasPosition = Canvas.transform.localPosition;
            Canvas.transform.localPosition = new Vector3(
                CanvasPosition.x,
                Round(- (1 - UIGlassScale.y) / 2.0f),
                Round((1 - UIGlassScale.z) / 2.0f)
            );
        }
    }

    private static float Round(float f)
    {
        float result = Mathf.Round(f * 10.0f) / 10.0f;
        Debug.Log(result.ToString("F10"));
        return result;
    }
}