using UnityEngine;

public class Moeda : MonoBehaviour
{
    public float velocidadeGiro = 150f;
    public int valorDaMoeda = 10;

    void Update()
    {
        transform.Rotate(0, 0, velocidadeGiro * Time.deltaTime);
    }

    void OnTriggerEnter(Collider outro)
    {
        if (outro.CompareTag("Player"))
        {
            // Avisa o ScoreManager para subir os pontos!
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AdicionarPonto(valorDaMoeda);
            }

            // O efeito de desaparecer:
            // 1. Podes spawnar um efeito de partículas aqui depois
            // 2. Destrói a moeda
            Destroy(gameObject);
        }
    }
}