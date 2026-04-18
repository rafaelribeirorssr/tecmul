using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject deathScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ShowDeathScreen()
    {
        Debug.Log("ShowDeathScreen chamado! DeathScreen: " + deathScreen);
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("DeathScreen é null!");
        }
    }
}