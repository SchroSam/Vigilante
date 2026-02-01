using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public CharacterBehavior behavior;
    public Mask mask;
    float size;

    private void Start()
    {
        behavior.damageTaken.AddListener(UpdateHealthBar);
        size = mask.rectTransform.rect.width;
    }

    public void UpdateHealthBar()
    {
        if (behavior.health > 0) mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size * ((float)behavior.health / (float)behavior.maxHealth));
        else mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
    }
}
