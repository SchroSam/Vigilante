using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public CharacterBehavior behavior;
    public Mask mask;
    float size;

    private void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        behavior.damageTaken.AddListener(UpdateHealthBar);
        size = mask.rectTransform.rect.width;
    }

    public void UpdateHealthBar()
    {
        if (behavior.health > 0) mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size * ((float)behavior.health / (float)behavior.maxHealth));
        else mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
    }

    private void OnActiveSceneChanged(Scene current, Scene next)
    {
        behavior = FindFirstObjectByType<Player>().GetComponent<CharacterBehavior>();
        behavior.Reset();
        UpdateHealthBar();
    }
}
