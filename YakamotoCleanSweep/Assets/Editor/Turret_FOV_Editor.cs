using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FOV))]
public class Turret_FOV_Editor : Editor
{
    private void OnSceneGUI()
    {
        FOV fov = (FOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

        Vector3 viewAngle1 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.fov / 2);
        Vector3 viewAngle2 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.fov / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle1 * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.viewRadius);
    }


    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
