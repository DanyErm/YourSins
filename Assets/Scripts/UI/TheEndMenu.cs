using UnityEngine;

public class TheEndMenu : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private GameObject theEndPanel;


    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Time.timeScale = 0f;
            theEndPanel.SetActive(true);
        }
    }
}