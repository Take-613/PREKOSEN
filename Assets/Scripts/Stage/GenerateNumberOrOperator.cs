using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GenerateNumberOrOperator : MonoBehaviour
{
    [Header("オブジェクト生成")]
    [SerializeField] private GameObject operatorPrefab;
    [SerializeField] private GameObject numberPrefab;
    [SerializeField] private Transform spawnArea;
    [SerializeField] private Vector3 spawnAreaSizeMin = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 spawnAreaSizeMax = new Vector3(10f, 0f, 10f);
    
    [SerializeField] private int generateNumberCount = 10;
    [SerializeField] private int generateOperateCount = 10;
    
    [SerializeField] private List<GameObject> spawnedObjects = new List<GameObject>();
    void Start()
    {
        SpawnRandomObjects();
    }
public void SpawnRandomObjects()
    {
        ClearSpawnedObjects();
        
        // 演算子を生成
        for (int i = 0; i < generateOperateCount; i++)
        {
            SpawnOperator((OperatorType)i);
        }
        
        // 数値オブジェクトを生成（1-20の範囲）
        for (int i = 0; i < generateNumberCount; i++)
        {
            float randomValue = Random.Range(1f, 101f);
            SpawnNumber(randomValue);
        }
    }
    
    // 演算子オブジェクトを生成
    void SpawnOperator(OperatorType operatorType)
    {
        if (operatorPrefab != null)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject obj = Instantiate(operatorPrefab, spawnPosition, Quaternion.identity);
            
            Oparator operatorComponent = obj.GetComponent<Oparator>();
            if (operatorComponent != null)
            {
                // リフレクションを使用して演算子タイプを設定
                var field = typeof(Oparator).GetField("operatorType", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (field != null)
                {
                    field.SetValue(operatorComponent, operatorType);
                }
            }
            
            spawnedObjects.Add(obj);
        }
    }
    
    // 数値オブジェクトを生成
    void SpawnNumber(float value)
    {
        if (numberPrefab != null)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject obj = Instantiate(numberPrefab, spawnPosition, Quaternion.identity);
            
            NumberObject numberComponent = obj.GetComponent<NumberObject>();
            if (numberComponent != null)
            {
                numberComponent.SetValue((int)value);
            }
            
            spawnedObjects.Add(obj);
        }
    }
    
    // ランダムな生成位置を取得
    Vector3 GetRandomSpawnPosition()
    {
        Vector3 basePosition = spawnArea != null ? spawnArea.position : Vector3.zero;
        
        float x = Random.Range(-spawnAreaSizeMin.x, spawnAreaSizeMax.x);
        float y = Random.Range(-spawnAreaSizeMin.y-1, spawnAreaSizeMax.y);
        float z = Random.Range(-spawnAreaSizeMin.z, spawnAreaSizeMax.z);
        
        return basePosition + new Vector3(x, (int)y, z);
        
        
    }
    
    void ClearSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                DestroyImmediate(obj);
            }
        }
        spawnedObjects.Clear();
    }
    
}
