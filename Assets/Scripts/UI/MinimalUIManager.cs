using System;
using UnityEngine;
using UnityEngine.UI;

namespace Knowledge.Game
{
    // Lightweight, runtime UI to bootstrap the UX for the first release
    public class MinimalUIManager : MonoBehaviour
    {
        private void Start()
        {
            EnsureUI();
        }

        private void EnsureUI()
        {
            // If a UI Canvas already exists, do nothing
            if (FindObjectOfType<Canvas>() != null) return;

            var canvasObj = new GameObject("UI_Canvas");
            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();

            // Simple panel
            var panel = new GameObject("Panel");
            panel.transform.SetParent(canvasObj.transform, false);
            var panelImage = panel.AddComponent<Image>();
            panelImage.color = new Color(0f, 0f, 0f, 0.25f);
            var panelRect = panel.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0, 0);
            panelRect.anchorMax = new Vector2(0, 0);
            panelRect.pivot = new Vector2(0, 0);
            panelRect.anchoredPosition = new Vector2(16, 16);
            panelRect.sizeDelta = new Vector2(260, 120);

            // World Map Button
            var buttonObj = new GameObject("WorldMapButton");
            buttonObj.transform.SetParent(panel.transform, false);
            var btn = buttonObj.AddComponent<Button>();
            var btnImg = buttonObj.AddComponent<Image>();
            btnImg.color = new Color(1f, 1f, 1f, 0.9f);
            var btnRect = buttonObj.GetComponent<RectTransform>();
            btnRect.sizeDelta = new Vector2(240, 40);
            btnRect.anchoredPosition = new Vector2(40, -20);

            var txtObj = new GameObject("Text");
            txtObj.transform.SetParent(buttonObj.transform, false);
            var txt = txtObj.AddComponent<Text>();
            txt.text = "Apri Mappa Mondiale";
            txt.alignment = TextAnchor.MiddleCenter;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.color = Color.black;
            var txtRect = txtObj.GetComponent<RectTransform>();
            txtRect.sizeDelta = new Vector2(240, 40);
            txtRect.anchoredPosition = Vector2.zero;

            btn.onClick.AddListener(() => {
                var loader = SceneLoader.Instance;
                if (loader != null) loader.LoadWorldMap();
                else Debug.LogWarning("SceneLoader not found");
            });
        }
    }
}
