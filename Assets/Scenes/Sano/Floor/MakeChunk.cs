using UnityEngine;
using System.Collections.Generic;

public class TilemapChunkSpawner : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    public Transform player;
    public Transform initialChunkNextPoint; // 最初のマップの次の位置（Scene上で設定）
    public int preSpawnChunks = 5;

    private float nextSpawnX;
    private Queue<GameObject> spawnedChunks = new Queue<GameObject>();

    void Start()
    {
        // 最初のマップの終点（NextSpawnPoint）から開始
        nextSpawnX = initialChunkNextPoint.position.x;

        // 初期チャンクのあとにいくつかチャンクを生成
        for (int i = 0; i < preSpawnChunks; i++)
        {
            SpawnChunk();
        }
    }

    void Update()
    {
        if (player.position.x + 20f > nextSpawnX) // 20f ≒ chunkWidth * 2
        {
            SpawnChunk();

            if (spawnedChunks.Count > preSpawnChunks + 2)
            {
                Destroy(spawnedChunks.Dequeue());
            }
        }
    }

    void SpawnChunk()
    {
        int index = Random.Range(0, chunkPrefabs.Length);
        GameObject newChunk = Instantiate(chunkPrefabs[index], new Vector3(nextSpawnX, 0, 0), Quaternion.identity);

        // ChunkのNextSpawnPointを探す（次のSpawn位置を更新）
        Transform nextPoint = newChunk.transform.Find("NextSpawnPoint");
        if (nextPoint != null)
        {
            nextSpawnX = nextPoint.position.x;
        }
        else
        {
            // Fallback: chunkWidth による計算
            nextSpawnX += 10f; // chunkWidth
        }

        spawnedChunks.Enqueue(newChunk);
    }
}
