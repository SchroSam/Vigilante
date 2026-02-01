using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class LoseUI : MonoBehaviour
{
    InputAction killTest;
    public GameObject Lose;
    public CharacterBehavior character;
    
    public void Start()
    {
        DontDestroyOnLoad(this);
        killTest = InputSystem.actions.FindAction("Kill");
    }

    void Update()
    {
        if (killTest.IsPressed())
        {
            character.TakeDamage(100);
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        Lose.SetActive(true);
        Time.timeScale = 0f;
    }


    public void LoseScreen()
    {
        StartCoroutine(Delay());
    }


    
}


