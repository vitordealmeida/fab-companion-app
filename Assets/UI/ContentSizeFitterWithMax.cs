using System;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
#endif

namespace UnityEngine.UI
{
    [AddComponentMenu("Layout/Content Size Fitter With Max", 141)]
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class ContentSizeFitterWithMax : ContentSizeFitter
    {
        [NonSerialized] private RectTransform m_Rect;

        private RectTransform rectTransform
        {
            get
            {
                if (m_Rect == null)
                {
                    m_Rect = GetComponent<RectTransform>();
                }

                return m_Rect;
            }
        }

        [SerializeField] private float m_MaxWidth = -1;

        public float maxWidth
        {
            get => m_MaxWidth;
            set => m_MaxWidth = value;
        }

        [SerializeField] private float m_MaxHeight = -1;

        public float maxHeight
        {
            get => m_MaxHeight;
            set => m_MaxHeight = value;
        }
        
        [SerializeField] private bool m_CanOverflowBounds;

        public bool canOverflowBounds
        {
            get => m_CanOverflowBounds;
            set => m_CanOverflowBounds = value;
        }

        public override void SetLayoutHorizontal()
        {
            base.SetLayoutHorizontal();

            if (maxWidth > 0)
            {
                var available = transform.parent.GetComponent<RectTransform>().rect.width; 
                var limit = canOverflowBounds ? maxWidth : Mathf.Min(maxWidth, available);
                if (horizontalFit == FitMode.MinSize)
                {
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                        Mathf.Min(LayoutUtility.GetMinSize(m_Rect, 0), limit));
                }
                else if (horizontalFit == FitMode.PreferredSize)
                {
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                        Mathf.Min(LayoutUtility.GetPreferredSize(m_Rect, 0), limit));
                }
            }
        }

        public override void SetLayoutVertical()
        {
            base.SetLayoutVertical();

            if (maxHeight > 0)
            {
                var available = transform.parent.GetComponent<RectTransform>().rect.height; 
                var limit = canOverflowBounds ? maxHeight : Mathf.Min(maxHeight, available);
                if (verticalFit == FitMode.MinSize)
                {
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                        Mathf.Min(LayoutUtility.GetMinSize(m_Rect, 1), limit));
                }
                else if (verticalFit == FitMode.PreferredSize)
                {
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                        Mathf.Min(LayoutUtility.GetPreferredSize(m_Rect, 1), limit));
                }
            }
        }
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(ContentSizeFitterWithMax), true)]
    [CanEditMultipleObjects]
    public class ContentSizeFitterWithMaxEditor : ContentSizeFitterEditor
    {
        SerializedProperty m_MaxWidth;
        SerializedProperty m_MaxHeight;
        SerializedProperty m_CanOverflowBounds;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_MaxWidth = serializedObject.FindProperty("m_MaxWidth");
            m_MaxHeight = serializedObject.FindProperty("m_MaxHeight");
            m_CanOverflowBounds = serializedObject.FindProperty("m_CanOverflowBounds");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_MaxWidth, true);
            EditorGUILayout.PropertyField(m_MaxHeight, true);
            EditorGUILayout.PropertyField(m_CanOverflowBounds, true);
            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
#endif
}