using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Sun), true)]
public class SunEditor : Editor {
    private void OnSceneGUI() {
   /*     Sun sun = (Sun)target;
        Handles.color = new Color(0f, 90f, 0f, 0.05f);

        Handles.DrawSolidArc(sun.transform.position, sun.transform.forward, sun.transform.up, 360, sun.radius);

        Handles.DrawSolidArc(sun.transform.position, branch.transform.forward, Quaternion.Euler(0f, 0f, Tree.Instance.branchAngle) * branch.transform.up, Tree.Instance.branchAngle * -2, branch.radius); */
    }
}

