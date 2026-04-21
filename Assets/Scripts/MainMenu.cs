using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Painéis")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject configuracoesPanel;
    [SerializeField] private GameObject comoJogarPanel;
    [SerializeField] private GameObject creditosPanel;

    [Header("Configurações")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle fullscreenToggle;

    private void Start()
    {
        MostrarApenasPanel(mainMenuPanel);

        // Carregar preferências guardadas
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
            volumeSlider.onValueChanged.AddListener(AlterarVolume);
        }

        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(AlterarFullscreen);
        }
    }

    // ========== NAVEGAÇÃO ==========

    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // Garante que a cena se chama "Game"
    }
    
    public void AbrirComoJogar()
    {
        MostrarApenasPanel(comoJogarPanel);
    }

    public void AbrirConfiguracoes()
    {
        MostrarApenasPanel(configuracoesPanel);
    }

    public void AbrirCreditos()
    {
        MostrarApenasPanel(creditosPanel);
    }

    public void Voltar()
    {
        MostrarApenasPanel(mainMenuPanel);
    }

    public void QuitGame()
    {
        Debug.Log("O jogador saiu do jogo!");
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // ========== CONFIGURAÇÕES ==========

    // MUDOU AQUI: Agora é public em vez de private!
    public void AlterarVolume(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("Volume", valor);
        PlayerPrefs.Save();
    }

    public void AlterarFullscreen(bool ativo)
    {
        Screen.fullScreen = ativo;
        Debug.Log("Ecrã Inteiro: " + ativo);
    }

    public void DefinirQualidade(int indiceQualidade)
    {
        QualitySettings.SetQualityLevel(indiceQualidade);
        Debug.Log("Qualidade alterada para o nível: " + indiceQualidade);
    }

    // ========== UTILIDADE ==========

    private void MostrarApenasPanel(GameObject panelAtivo)
    {
        mainMenuPanel?.SetActive(panelAtivo == mainMenuPanel);
        configuracoesPanel?.SetActive(panelAtivo == configuracoesPanel);
        comoJogarPanel?.SetActive(panelAtivo == comoJogarPanel);
        creditosPanel?.SetActive(panelAtivo == creditosPanel);
    }
}