using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    // Função para a Qualidade Gráfica
    public void DefinirQualidade(int indiceQualidade)
    {
        // Esta linha mágica muda as luzes, sombras e texturas do Unity de uma só vez!
        QualitySettings.SetQualityLevel(indiceQualidade);
        
        Debug.Log("Qualidade alterada para o nível: " + indiceQualidade);
    }

    // Aproveitamos e fazemos já a do Ecrã Inteiro (Fullscreen)
    public void DefinirEcraInteiro(bool estaLigado)
    {
        Screen.fullScreen = estaLigado;
    }
}