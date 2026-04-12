using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para mudar de cena

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Carrega a cena que chamámos de "Game"
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("O jogador saiu do jogo!");
        Application.Quit(); // Só funciona depois de exportares o jogo (.exe)
    }
}