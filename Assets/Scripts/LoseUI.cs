using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class LoseUI : MonoBehaviour
{
    //InputAction killTest;
    public GameObject Lose;
    public CharacterBehavior character;
    public static LoseUI instance = null;
    public float deathDelay = 1f;
    

    bool co = false;
    
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        //killTest = InputSystem.actions.FindAction("Kill");
    }

    // void Update()
    // {
    //     if (killTest.IsPressed())
    //     {
    //         character.TakeDamage(100);
    //     }
    // }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(deathDelay);
        Lose.SetActive(true);
        Time.timeScale = 0f;
        
    }


    public void LoseScreen()
    {
        if(!co)
        {
            StartCoroutine(Delay());
            co = true;
        }
       
    }

    public void restartButton()
    {
        Debug.Log("Restart button pressed");
        Time.timeScale = 1f;
        Lose.SetActive(false);
        
        //GetComponent<CharacterBehavior>().Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        co = false;
    }

    private void OnActiveSceneChanged(Scene current,Scene next)
    {
        if(next.name != "MainMenu")
            character = FindFirstObjectByType<PlayerInput>().GetComponent<CharacterBehavior>();
    }
}
