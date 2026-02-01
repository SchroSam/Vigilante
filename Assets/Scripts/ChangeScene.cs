using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    public void Change()
    {
        Time.timeScale = 1f;
        PauseUI pause = FindFirstObjectByType<PauseUI>();
        if (pause != null) pause.Unpause();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(sceneName);
    }
}
