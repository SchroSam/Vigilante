using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject pauseUI;
    InputAction pause;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pause = InputSystem.actions.FindAction("Pause");
    }

    // Update is called once per frame
    void Update()
    {
        if (pause.WasPressedThisFrame()) Pause();
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void UpdateTimer(float timer)
    {
        int sec = ((int)timer % 60);
        timerText.text = "Timer: " + ((int)timer / 60) + " : " + ((sec < 10) ? ("0" + sec) : sec.ToString());
    }
}
