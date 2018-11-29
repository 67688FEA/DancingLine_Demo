using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleGizmo {

    static GUIStyle sceneNote;

    static ObstacleGizmo()
    {
        sceneNote = new GUIStyle("box");
        sceneNote.fontStyle = FontStyle.Bold;
        sceneNote.normal.textColor = Color.white;
        sceneNote.margin = sceneNote.overflow = sceneNote.padding = new RectOffset(3, 3, 3, 3);
        sceneNote.richText = true;
        sceneNote.alignment = TextAnchor.MiddleCenter;
    }

    static void DrawNote(Vector3 position, string text, string warning = "", float distance = 10)
    {
        if (!string.IsNullOrEmpty(warning))
        {
            text = text + "<color=red>" + warning + "</color>";
        }
        if ((Camera.current.transform.position - position).magnitude <= distance)
        {
            Handles.Label(position, text, sceneNote);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy, typeof(RisingController))]
    static void DrawTransferGizmos(RisingController obstacle, GizmoType gizmoType)
    {
        if (obstacle.destination)
        {
            DrawNote(obstacle.transform.position, "Obstacle Triggle");
            Handles.color = Color.green * 0.5f;
            Handles.DrawDottedLine(obstacle.transform.position, obstacle.destination.transform.position, 5);
            DrawNote(obstacle.destination.transform.position, "Obstacle Object");
        }
        else
        {
            DrawNote(obstacle.transform.position, "Obstacle Triggle", "(No Destination!)");
        }
    }

}
