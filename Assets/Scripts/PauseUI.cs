using UnityEngine;
using UnityEngine.InputSystem;

public class PauseUI : MonoBehaviour
{
    public GameObject game;
    InputAction pause;

    private void Start()
    {
        pause = InputSystem.actions.FindAction("Pause");
    }

    // Update is called once per frame
    void Update()
    {
        if (pause.WasPressedThisFrame()) Unpause();
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        game.SetActive(true);
        gameObject.SetActive(false);
    }
}
