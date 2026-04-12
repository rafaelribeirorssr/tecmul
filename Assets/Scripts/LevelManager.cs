using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Moldes (Prefabs)")]
    public GameObject roadPrefab;      
    public GameObject[] obstaclePrefabs; 
    public Transform playerTransform;  
    
    [Header("Configurações da Estrada")]
    public float tileLength = 70f;     
    public int tilesOnScreen = 5;      
    public float spawnZ = -40f; 

    [Header("Dificuldade (Obstáculos)")]
    [Range(0f, 1f)]
    public float obstacleChance = 0.7f; // 70% de hipótese de ter obstáculo a cada 12 metros
    public float distanceBetweenObstacles = 12f; // Espaçamento entre obstáculos
    
    private List<GameObject> activeTiles = new List<GameObject>();
    private List<GameObject> activeObstacles = new List<GameObject>();

    // Ajuste de altura para não ficarem debaixo do chão
    // Como a tua pista tem Scale Y = 1, o topo dela está em Y = 0.5
    private float floorHeight = 0.5f; 

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (playerTransform.position.z > (spawnZ - (tilesOnScreen * tileLength) + tileLength))
        {
            SpawnTile();
            DeleteTile();
        }

        // Limpeza automática para manter o jogo leve
        if (activeObstacles.Count > 0 && activeObstacles[0] != null)
        {
            if (activeObstacles[0].transform.position.z < playerTransform.position.z - 30f)
            {
                Destroy(activeObstacles[0]);
                activeObstacles.RemoveAt(0);
            }
        }
    }

    void SpawnTile()
    {
        // Gerar a estrada com a rotação correta (90 graus que tens no prefab)
        GameObject go = Instantiate(roadPrefab, new Vector3(0, 0, spawnZ), roadPrefab.transform.rotation);
        activeTiles.Add(go);

        if (spawnZ > 20) // Só gera obstáculos depois de o jogador começar a correr
        {
            // Em vez de um número fixo, vamos "percorrer" os 70 metros da pista
            // e decidir a cada 12 metros se pomos um obstáculo
            for (float zPos = 10f; zPos < tileLength; zPos += distanceBetweenObstacles)
            {
                if (Random.value < obstacleChance)
                {
                    SpawnSingleObstacle(spawnZ + zPos);
                }
            }
        }

        spawnZ += tileLength; 
    }

    void SpawnSingleObstacle(float zPos)
    {
        if (obstaclePrefabs.Length == 0) return;

        // Escolher a pista (-3, 0, ou 3)
        int lane = Random.Range(0, 3);
        float xPos = (lane - 1) * 3f; // Atalho matemático para dar -3, 0 ou 3

        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[randomIndex];

        // CÁLCULO DA ALTURA (Y):
        // Pegamos no Scale Y do obstáculo para ele assentar perfeito no chão
        float obstacleScaleY = prefab.transform.localScale.y;
        float finalY = floorHeight + (obstacleScaleY / 2f);

        Vector3 spawnPos = new Vector3(xPos, finalY, zPos);
        GameObject obs = Instantiate(prefab, spawnPos, Quaternion.identity);
        activeObstacles.Add(obs);
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}