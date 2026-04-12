using UnityEngine;
using UnityEngine.SceneManagement; // A biblioteca que nos permite mudar de cenas

public class PlayerCollision : MonoBehaviour
{
    // Esta função dispara automaticamente no momento exato em que o boneco bate em algo
    void OnCollisionEnter(Collision collision)
    {
        // Verifica se aquilo em que batemos tem a etiqueta (Tag) "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Mensagem para ti (programador) veres na consola
            Debug.Log("GAME OVER! Bateu num obstáculo. A carregar o Menu Principal...");
            
            // Manda o jogador de volta para a cena do Menu
            SceneManager.LoadScene("MainMenu");
        }
    }
}