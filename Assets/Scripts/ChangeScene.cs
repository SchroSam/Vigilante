using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    public void Change()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
