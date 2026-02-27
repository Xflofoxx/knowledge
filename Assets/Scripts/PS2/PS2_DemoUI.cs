using UnityEngine;
using UnityEngine.UI;

namespace Knowledge.Game.PS2
{
    // Minimal UI bootstrap for PS2 demo
    public class PS2_DemoUI : MonoBehaviour
    {
        void Awake()
        {
            EnsureUI();
        }

        private void EnsureUI()
        {
            if (FindObjectOfType<Canvas>() != null) return;

            var go = new GameObject("PS2_Demo_Canvas");
            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            go.AddComponent<UnityEngine.UI.CanvasScaler>();
            go.AddComponent<UnityEngine.UI.GraphicRaycaster>();

            // Panel
            var panel = new GameObject("Panel");
            panel.transform.SetParent(go.transform, false);
            var panelImg = panel.AddComponent<Image>();
            panelImg.color = new Color(0f, 0f, 0f, 0.25f);
            var rect = panel.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(10, 10); rect.offsetMax = new Vector2(-10, -10);

            // Button
            var btnObj = new GameObject("OpenWorldMapBtn");
            btnObj.transform.SetParent(panel.transform, false);
            var btn = btnObj.AddComponent<Button>();
            var btnImg = btnObj.AddComponent<Image>();
            btnImg.color = new Color(1f, 1f, 1f, 0.9f);
            var btnRect = btnObj.GetComponent<RectTransform>();
            btnRect.sizeDelta = new Vector2(200, 40);
            btnRect.anchoredPosition = new Vector2(0, -20);

            var textObj = new GameObject("Text");
            textObj.transform.SetParent(btnObj.transform, false);
            var text = textObj.AddComponent<Text>();
            text.text = "Open World Map";
            text.alignment = TextAnchor.MiddleCenter;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.color = Color.black;
            var textRect = textObj.GetComponent<RectTransform>();
            textRect.sizeDelta = new Vector2(200, 40);
            textRect.anchoredPosition = Vector2.zero;

            btn.onClick.AddListener(() => {
                Knowledge.Game.PS2.PS2_SceneBridge.LoadScene("WorldMap");
            });
        }
    }
}
