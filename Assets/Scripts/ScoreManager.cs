using UnityEngine;
using TMPro; // Necessário para mexer no TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Truque para outros scripts o encontrarem fácil
    
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        // Cria uma ligação direta para que as moedas te encontrem
        instance = this;
    }

    public void AdicionarPonto(int quantidade)
    {
        score += quantidade;
        AtualizarTexto();
    }

    void AtualizarTexto()
    {
        scoreText.text = "DADOS: " + score;
    }
}