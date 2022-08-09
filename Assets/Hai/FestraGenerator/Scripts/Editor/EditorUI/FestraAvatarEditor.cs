using Hai.FestraGenerator.Scripts.Components;
using UnityEditor;

namespace Hai.FestraGenerator.Scripts.Editor.EditorUI
{
    [CustomEditor(typeof(FestraAvatar))]
    public class FestraAvatarEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}