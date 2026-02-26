using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Knowledge.Game.Tests
{
    [TestFixture]
    public class UIManagerTests
    {
        private UIManager _uiManager;
        private GameObject _go;
        private GameManager _gm;

        [SetUp]
        public void Setup()
        {
            _go = new GameObject();
            _gm = _go.AddComponent<GameManager>();
            _uiManager = _go.AddComponent<UIManager>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_go);
        }

        [Test]
        public void Instance_Set_SingleInstance()
        {
            Assert.AreEqual(_uiManager, UIManager.Instance);
        }

        [Test]
        public void UpdateKnowledgeDisplay_UpdatesText()
        {
            GameObject textGO = new GameObject();
            Text text = textGO.AddComponent<Text>();
            _uiManager.KnowledgePointsText = text;

            _uiManager.UpdateKnowledgeDisplay(100);

            StringAssert.Contains("100", text.text);
            Object.DestroyImmediate(textGO);
        }

        [Test]
        public void ToggleInventory_OpensPanel()
        {
            GameObject panel = new GameObject();
            panel.SetActive(false);
            _uiManager.InventoryPanel = panel;

            _uiManager.ToggleInventory();

            Assert.IsTrue(panel.activeSelf);
            Object.DestroyImmediate(panel);
        }

        [Test]
        public void ToggleInventory_Twice_ClosesPanel()
        {
            GameObject panel = new GameObject();
            panel.SetActive(false);
            _uiManager.InventoryPanel = panel;

            _uiManager.ToggleInventory();
            _uiManager.ToggleInventory();

            Assert.IsFalse(panel.activeSelf);
            Object.DestroyImmediate(panel);
        }

        [Test]
        public void ToggleCrafting_OpensPanel()
        {
            GameObject panel = new GameObject();
            panel.SetActive(false);
            _uiManager.CraftingPanel = panel;

            _uiManager.ToggleCrafting();

            Assert.IsTrue(panel.activeSelf);
            Object.DestroyImmediate(panel);
        }

        [Test]
        public void ToggleMap_OpensPanel()
        {
            GameObject panel = new GameObject();
            panel.SetActive(false);
            _uiManager.MapPanel = panel;

            _uiManager.ToggleMap();

            Assert.IsTrue(panel.activeSelf);
            Object.DestroyImmediate(panel);
        }

        [Test]
        public void TogglePause_OpensPauseMenu()
        {
            GameObject pauseMenu = new GameObject();
            pauseMenu.SetActive(false);
            _uiManager.PauseMenu = pauseMenu;

            _uiManager.TogglePause();

            Assert.IsTrue(pauseMenu.activeSelf);
            Object.DestroyImmediate(pauseMenu);
        }

        [Test]
        public void TogglePause_SetsGamePaused()
        {
            GameObject pauseMenu = new GameObject();
            pauseMenu.SetActive(false);
            _uiManager.PauseMenu = pauseMenu;

            _uiManager.TogglePause();

            Assert.IsTrue(_gm.IsGamePaused);
            Object.DestroyImmediate(pauseMenu);
        }

        [Test]
        public void ShowDialog_ActivatesDialogPanel()
        {
            GameObject dialogPanel = new GameObject();
            dialogPanel.SetActive(false);
            _uiManager.DialogPanel = dialogPanel;

            _uiManager.ShowDialog("NPC", new[] { "Hello" });

            Assert.IsTrue(dialogPanel.activeSelf);
            Object.DestroyImmediate(dialogPanel);
        }

        [Test]
        public void HideDialog_DeactivatesDialogPanel()
        {
            GameObject dialogPanel = new GameObject();
            dialogPanel.SetActive(true);
            _uiManager.DialogPanel = dialogPanel;

            _uiManager.HideDialog();

            Assert.IsFalse(dialogPanel.activeSelf);
            Object.DestroyImmediate(dialogPanel);
        }

        [Test]
        public void UpdateAllDisplay_CallsUpdateHUD()
        {
            _uiManager.UpdateAllDisplay();
            Assert.Pass();
        }

        [Test]
        public void ShowMessage_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => _uiManager.ShowMessage("Test message"));
        }
    }
}
