using UnityEngine;

public class CamaraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    
    // Controla a suavidade apenas quando o jogador vai para a esquerda/direita
    public float lateralSmoothness = 5f; 

    void LateUpdate()
    {
        if (player == null) return; // Evita erros se o player morrer/desaparecer

        // 1. Calculamos a posição onde a câmara deve estar
        Vector3 targetPosition = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y, 
            player.position.z + offset.z
        );

        // 2. Colamos a câmara aos eixos Z (frente) e Y (altura), mas suavizamos o eixo X (lados)
        float smoothX = Mathf.Lerp(transform.position.x, targetPosition.x, lateralSmoothness * Time.deltaTime);

        // 3. Aplicamos a posição
        transform.position = new Vector3(smoothX, targetPosition.y, targetPosition.z);

        // Removemos o LookAt! A câmara agora vai olhar sempre em frente.
    }
}