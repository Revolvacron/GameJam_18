using UnityEngine;

/// <summary>
/// Used to prevent unity switching to the game tab when the game is started.
/// </summary>
public class StopGameTabSwitching : MonoBehaviour
{
#if UNITY_EDITOR
    // Use this for initialization
    void Start ()
    {
        UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
    }
#endif
}
