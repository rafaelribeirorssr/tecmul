using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // --- Movimento para a Frente ---
    public float playerSpeed = 2f;
    public float acceleration = 0.1f;
    public float maxSpeed = 20f;

    // --- Sistema de Pistas (Lanes) ---
    private int desiredLane = 1; // 0 = Esquerda, 1 = Centro, 2 = Direita
    public float laneDistance = 3f; // Distância em metros entre cada pista (ajusta no Inspector)
    public float sideSpeed = 10f; // Quão rápido ele muda de faixa

    void Update()
    {
        // 1. Aceleração e movimento constante para a frente
        if (playerSpeed < maxSpeed)
        {
            playerSpeed += acceleration * Time.deltaTime;
        }
        // Move sempre para a frente em Z
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);

        // 2. Input para mudar de pista (Usamos GetKeyDown para reagir apenas ao "clique")
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3) desiredLane = 2; // Bate na "parede" da direita e não passa
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1) desiredLane = 0; // Bate na "parede" da esquerda e não passa
        }

        // 3. Calcular a posição X (lateral) onde o boneco devia estar
        Vector3 targetPosition = transform.position;
        
        if (desiredLane == 0)
            targetPosition.x = -laneDistance; // Vai para a esquerda
        else if (desiredLane == 1)
            targetPosition.x = 0;             // Fica no centro
        else if (desiredLane == 2)
            targetPosition.x = laneDistance;  // Vai para a direita

        // 4. Mover o boneco suavemente para essa posição (no eixo X)
        // Mantemos o Y e o Z onde estão, só alteramos a posição lateral
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, targetPosition.x, sideSpeed * Time.deltaTime),
            transform.position.y,
            transform.position.z
        );
    }
}