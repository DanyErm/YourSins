using UnityEngine;
using Zenject;

public class Damage : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [Inject] private Character _char;


    [SerializeField] private GameObject hpIndicator;
    [SerializeField] private Sprite hp3;
    [SerializeField] private Sprite hp2;
    [SerializeField] private Sprite hp1;
    [SerializeField] private Sprite hp0;

    private SpriteRenderer hpIndicatorSprite;


    private void Start()
    {
        hpIndicatorSprite = hpIndicator.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            _char.HP--;
            HpSpriteChanger(_char.HP);
            Destroy(collision.gameObject);
        }

        if(_char.HP <= 0)
        {
            Time.timeScale = 0f;
            deathPanel.SetActive(true);
        }
    }

    private void HpSpriteChanger(int hp)
    {
        hpIndicatorSprite.sprite = hp switch
        {
            3 => hp3,
            2 => hp2,
            1 => hp1,
            0 => hp0,
            _ => hp0
        };
    }
}