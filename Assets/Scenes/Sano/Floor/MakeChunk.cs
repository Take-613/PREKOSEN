using UnityEngine;
using System.Collections.Generic;

public class TilemapChunkSpawner : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    public Transform player;
    public Transform initialChunkNextPoint; // �ŏ��̃}�b�v�̎��̈ʒu�iScene��Őݒ�j
    public int preSpawnChunks = 5;

    private float nextSpawnX;
    private Queue<GameObject> spawnedChunks = new Queue<GameObject>();

    void Start()
    {
        // �ŏ��̃}�b�v�̏I�_�iNextSpawnPoint�j����J�n
        nextSpawnX = initialChunkNextPoint.position.x;

        // �����`�����N�̂��Ƃɂ������`�����N�𐶐�
        for (int i = 0; i < preSpawnChunks; i++)
        {
            SpawnChunk();
        }
    }

    void Update()
    {
        if (player.position.x + 20f > nextSpawnX) // 20f �� chunkWidth * 2
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

        // Chunk��NextSpawnPoint��T���i����Spawn�ʒu���X�V�j
        Transform nextPoint = newChunk.transform.Find("NextSpawnPoint");
        if (nextPoint != null)
        {
            nextSpawnX = nextPoint.position.x;
        }
        else
        {
            // Fallback: chunkWidth �ɂ��v�Z
            nextSpawnX += 10f; // chunkWidth
        }

        spawnedChunks.Enqueue(newChunk);
    }
}
