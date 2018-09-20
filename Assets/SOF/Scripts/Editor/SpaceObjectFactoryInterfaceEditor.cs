using UnityEngine;
using UnityEditor;

namespace EVE.SOF
{
    [CustomEditor(typeof(SOFInterface))]
    public class SpaceObjectFactoryInterfaceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SOFInterface sofIface = (SOFInterface)target;
            if (GUILayout.Button("Spawn Ship"))
                sofIface.SpawnShip();
            if (GUILayout.Button("Rebuild Cache"))
            {
                if (EditorUtility.DisplayDialog("Rebuild SOF Cache", "Are you sure you want to rebuild the sof cache?\nThis operation takes a long time.", "OK", "Cancel"))
                {
                    sofIface.RebuildCache();
                }
            }
        }
    }
}
