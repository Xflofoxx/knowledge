using UnityEngine;
using UnityEngine.SceneManagement;

namespace Knowledge.Game.PS2
{
    public static class PS2_SceneBridge
    {
        public static void LoadScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName)) return;
            SceneManager.LoadScene(sceneName);
        }

        public static string CurrentScene => SceneManager.GetActiveScene().name;
    }
}
