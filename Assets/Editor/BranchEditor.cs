using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Branch), true)]
public class BranchEditor : Editor {
    private void OnSceneGUI() {
        Branch branch = (Branch)target;
        Handles.color = new Color(0f, 90f, 0f, 0.05f);

    //    Handles.DrawSolidArc(branch.transform.position, branch.transform.forward, Quaternion.Euler(0f, 0f, Tree.Instance.branchAngle) * branch.transform.up, Tree.Instance.branchAngle * -2, branch.radius);     
    }
}

