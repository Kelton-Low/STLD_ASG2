using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }
    public void Quit()
    {
        // If running in the Unity Editor, stop the Play Mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running as a standalone built game, close the application
        Application.Quit();
        #endif
    }
}
