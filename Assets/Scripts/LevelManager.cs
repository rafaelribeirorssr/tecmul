using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Moldes (Prefabs)")]
    public GameObject roadPrefab;      
    public GameObject[] obstaclePrefabs; 
    public GameObject coinPrefab; // <-- NOVO: O buraco para o molde da tua moeda!
    public Transform playerTransform;  
    
    [Header("Configurações da Estrada")]
    public float tileLength = 70f;     
    public int tilesOnScreen = 5;      
    public float spawnZ = -40f; 
    public float laneDistance = 5f; 

    [Header("Geração de Itens")]
    [Range(0f, 1f)] public float obstacleChance = 1f; 
    [Range(0f, 1f)] public float coinChance = 0.5f; // <-- NOVO: % de nascer uma moeda (50%)
    public float distanceBetweenItems = 10f; // Espaço entre cada linha de itens
    
    private List<GameObject> activeTiles = new List<GameObject>();
    
    // Agora esta lista guarda os obstáculos e as moedas misturadas
    private List<GameObject> activeItems = new List<GameObject>(); 

    private float floorHeight = 0.5f; 
    private float alturaDaMoeda = 1.5f; // Para a moeda ficar a flutuar no ar

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        // 1. Gera a estrada
        if (playerTransform.position.z > (spawnZ - (tilesOnScreen * tileLength) + tileLength))
        {
            SpawnTile();
            DeleteTile();
        }

        // 2. Limpeza automática dos itens que ficaram para trás
        if (activeItems.Count > 0)
        {
            if (activeItems[0] == null)
            {
                // Se o item for nulo, significa que o Player apanhou a moeda e ela já foi destruída!
                // Só precisamos de a remover da nossa lista.
                activeItems.RemoveAt(0);
            }
            else if (activeItems[0].transform.position.z < playerTransform.position.z - 30f)
            {
                // Se o obstáculo ou moeda ficou muito para trás nas costas do player, apagamos.
                Destroy(activeItems[0]);
                activeItems.RemoveAt(0);
            }
        }
    }

    void SpawnTile()
    {
        GameObject go = Instantiate(roadPrefab, new Vector3(0, 0, spawnZ), roadPrefab.transform.rotation);
        activeTiles.Add(go);

        if (spawnZ > 20) 
        {
            // Vamos percorrer a pista toda e gerar uma linha de itens de X em X metros
            for (float zPos = 10f; zPos < tileLength; zPos += distanceBetweenItems)
            {
                GerarItensNaLinha(spawnZ + zPos);
            }
        }

        spawnZ += tileLength; 
    }

    void GerarItensNaLinha(float zPos)
    {
        int pistaDoObstaculo = -1; // -1 significa "ainda não há obstáculo"

        // 1. Tentar gerar um obstáculo primeiro
        if (Random.value < obstacleChance && obstaclePrefabs.Length > 0)
        {
            pistaDoObstaculo = Random.Range(0, 3); // Escolhe pista 0, 1 ou 2
            SpawnObstacle(pistaDoObstaculo, zPos);
        }

        // 2. Nas outras pistas onde NÃO há obstáculo, tentamos pôr moedas!
        if (coinPrefab != null)
        {
            for (int pista = 0; pista < 3; pista++)
            {
                // Se esta pista está livre de barreiras...
                if (pista != pistaDoObstaculo)
                {
                    // Lança o dado para ver se nasce uma moeda
                    if (Random.value < coinChance)
                    {
                        SpawnCoin(pista, zPos);
                    }
                }
            }
        }
    }

    void SpawnObstacle(int lane, float zPos)
    {
        float xPos = (lane - 1) * laneDistance; 
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[randomIndex];

        float obstacleScaleY = prefab.transform.localScale.y;
        float finalY = floorHeight + (obstacleScaleY / 2f);

        Vector3 spawnPos = new Vector3(xPos, finalY, zPos);
        GameObject obs = Instantiate(prefab, spawnPos, Quaternion.identity);
        activeItems.Add(obs); // Adiciona à lista de limpeza
    }

    void SpawnCoin(int lane, float zPos)
    {
        float xPos = (lane - 1) * laneDistance; 
        
        // Coloca a moeda com a altura de flutuação e na rotação do teu prefab
        Vector3 spawnPos = new Vector3(xPos, alturaDaMoeda, zPos);
        GameObject moeda = Instantiate(coinPrefab, spawnPos, coinPrefab.transform.rotation);
        activeItems.Add(moeda); // Adiciona à lista de limpeza
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}