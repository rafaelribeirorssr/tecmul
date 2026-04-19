using UnityEngine;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 
    
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        instance = this;
    }

    public void AdicionarPonto(int quantidade)
    {
        score += quantidade;
        AtualizarTexto();
    }

    void AtualizarTexto()
    {
        // O \n funciona como um "Enter" para o texto ficar nas linhas certas!
        scoreText.text = "DINHEIRO\nROUBADO\n: " + score + " $$";
    }
}