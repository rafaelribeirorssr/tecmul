using UnityEngine;

public class Moeda : MonoBehaviour
{
    public float velocidadeGiro = 150f;
    public int valorDaMoeda = 10;
    public AudioClip somMoeda;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(0, 0, velocidadeGiro * Time.deltaTime);
    }

    void OnTriggerEnter(Collider outro)
    {
        if (outro.CompareTag("Player"))
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AdicionarPonto(valorDaMoeda);
            }

            if (somMoeda != null)
            {
                AudioSource.PlayClipAtPoint(somMoeda, transform.position);
            }

            Destroy(gameObject);
        }
    }
}