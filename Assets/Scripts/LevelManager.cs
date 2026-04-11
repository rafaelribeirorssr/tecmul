using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // --- Moldes ---
    public GameObject roadPrefab;      
    public GameObject[] obstaclePrefabs; // A nossa lista de obstáculos
    public Transform playerTransform;  
    
    // --- Configurações da Estrada ---
    public float tileLength = 70f;     
    public int tilesOnScreen = 5;      
    public float spawnZ = -40f; 

    // --- Configurações dos Obstáculos ---
    public float laneDistance = 3f; 
    
    // --- Listas de Memória ---
    private List<GameObject> activeTiles = new List<GameObject>();
    private List<GameObject> activeObstacles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        // 1. Gera estradas e apaga estradas antigas
        if (playerTransform.position.z > (spawnZ - (tilesOnScreen * tileLength) + tileLength))
        {
            SpawnTile();
            DeleteTile();
        }

        // 2. Apaga obstáculos velhos
        if (activeObstacles.Count > 0 && activeObstacles[0].transform.position.z < playerTransform.position.z - 20f)
        {
            Destroy(activeObstacles[0]);
            activeObstacles.RemoveAt(0);
        }
    }

    void SpawnTile()
    {
        GameObject go = Instantiate(roadPrefab, new Vector3(0, 0, spawnZ), roadPrefab.transform.rotation);
        activeTiles.Add(go);

        // --- LÓGICA DE GERAR OBSTÁCULOS ALEATÓRIOS ---
        if (spawnZ > 0) 
        {
            // Sorteia a pista (0=Esq, 1=Centro, 2=Dir)
            int randomLane = Random.Range(0, 3);
            float spawnX = 0f;

            if (randomLane == 0) spawnX = -laneDistance;
            else if (randomLane == 1) spawnX = 0f;
            else if (randomLane == 2) spawnX = laneDistance;

            Vector3 obstaclePosition = new Vector3(spawnX, 0f, spawnZ + (tileLength / 2));

            // Sorteia QUAL obstáculo gerar da nossa lista
            int randomObstacleType = Random.Range(0, obstaclePrefabs.Length);

            // Gera o obstáculo escolhido!
            GameObject obs = Instantiate(obstaclePrefabs[randomObstacleType], obstaclePosition, Quaternion.identity);
            activeObstacles.Add(obs);
        }

        spawnZ += tileLength; 
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}