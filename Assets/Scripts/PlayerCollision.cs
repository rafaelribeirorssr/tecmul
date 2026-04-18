using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisão detetada com: " + collision.gameObject.tag);
        
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.ShowDeathScreen();
        }
    }
}