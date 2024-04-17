using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace AbstractHumanoidToy.Editor
{
    [CustomEditor(typeof(SpriteAnimation))]
    public class SpriteAnimationEditor : UnityEditor.Editor
    {
        private SerializedProperty _frames;
        private Object _spritesheet;

        private void OnEnable()
        {
            _frames = serializedObject.FindProperty(SpriteAnimation.FramesRelativePath);
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            _spritesheet = EditorGUILayout.ObjectField("Spritesheet to Import", _spritesheet, typeof(Texture2D), false);
            if (GUILayout.Button("Import"))
            {
                ImportSpritesheet();
                serializedObject.ApplyModifiedProperties();
            }
        }
        private void ImportSpritesheet()
        {
            string spritesheetPath = AssetDatabase.GetAssetPath(_spritesheet);
            Object[] subspriteAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(spritesheetPath);
            Sprite[] subsprites = subspriteAssets.Select(ObjectAsSprite).Where(SpriteIsNotNull).OrderBy(SpriteName).ToArray();
            for (int i = 0; i < subsprites.Length; i++)
            {
                SetSprite(subsprites[i], i);
            }
        }
        private void SetSprite(Sprite sprite, int index)
        {
            _frames.arraySize = Mathf.Max(_frames.arraySize, index + 1);
            SerializedProperty property = GetSpriteProperty(index);
            property.objectReferenceValue = sprite;
        }
        private SerializedProperty GetSpriteProperty(int index)
        {
            return _frames.GetArrayElementAtIndex(index).FindPropertyRelative(SpriteAnimationFrame.SpriteRelativePath);
        }

        private Sprite ObjectAsSprite(Object obj)
        {
            return obj as Sprite;
        }
        private bool SpriteIsNotNull(Sprite sprite)
        {
            return sprite != null;
        }
        private string SpriteName(Sprite sprite)
        {
            return sprite.name;
        }
    }
}