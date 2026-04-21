using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public AudioClip somColisao;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = somColisao;
        audioSource.ignoreListenerPause = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (somColisao != null)
            {
                audioSource.Play();
            }

            // Para o movimento imediatamente
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            GetComponent<PlayerMovement>().enabled = false;

            Invoke("MostrarTela", 0.2f);
        }
    }

    void MostrarTela()
    {
        GameManager.Instance.ShowDeathScreen();
    }
}