using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// ゲーム全体の計算システムを管理
public class CalculationGameManager : MonoBehaviour
{
    [Header("ゲーム設定")]
    [SerializeField] private PlayerCalculator player;
    [SerializeField] private float targetValue = 100f;
    [SerializeField] private float tolerance = 0.1f;
    
    [Header("オブジェクト生成")]
    [SerializeField] private GameObject operatorPrefab;
    [SerializeField] private GameObject numberPrefab;
    [SerializeField] private Transform spawnArea;
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);
    
    [Header("UI")]
    [SerializeField] private Text targetValueText;
    [SerializeField] private Text gameStatusText;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button spawnObjectsButton;
    
    [Header("オブジェクト管理")]
    [SerializeField] private List<GameObject> spawnedObjects = new List<GameObject>();
    
    private bool gameWon = false;
    
    void Start()
    {
        InitializeGame();
        SetupUI();
    }
    
    void InitializeGame()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerCalculator>();
        }
        
        UpdateTargetUI();
        SpawnInitialObjects();
    }
    
    void SetupUI()
    {
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetGame);
        }
        
        if (spawnObjectsButton != null)
        {
            spawnObjectsButton.onClick.AddListener(SpawnRandomObjects);
        }
    }
    
    void Update()
    {
        CheckWinCondition();
    }
    
    // 勝利条件をチェック
    void CheckWinCondition()
    {
        if (player != null && !gameWon)
        {
            float currentValue = player.GetCurrentValue();
            
            if (Mathf.Abs(currentValue - targetValue) <= tolerance)
            {
                gameWon = true;
                if (gameStatusText != null)
                {
                    gameStatusText.text = "おめでとう！目標値に到達しました！";
                    gameStatusText.color = Color.green;
                }
                Debug.Log($"ゲームクリア！ 目標値: {targetValue}, 現在値: {currentValue}");
            }
            else
            {
                if (gameStatusText != null)
                {
                    gameStatusText.text = $"目標値: {targetValue:F1} まで あと {Mathf.Abs(currentValue - targetValue):F1}";
                    gameStatusText.color = Color.white;
                }
            }
        }
    }
    
    // 初期オブジェクトを生成
    void SpawnInitialObjects()
    {
        SpawnRandomObjects();
    }
    
    // ランダムなオブジェクトを生成
    public void SpawnRandomObjects()
    {
        ClearSpawnedObjects();
        
        // 演算子を生成
        for (int i = 0; i < 4; i++)
        {
            SpawnOperator((OperatorType)i);
        }
        
        // 数値オブジェクトを生成（1-20の範囲）
        for (int i = 0; i < 6; i++)
        {
            float randomValue = Random.Range(1f, 21f);
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
                numberComponent.SetValue(value);
            }
            
            spawnedObjects.Add(obj);
        }
    }
    
    // ランダムな生成位置を取得
    Vector3 GetRandomSpawnPosition()
    {
        Vector3 basePosition = spawnArea != null ? spawnArea.position : Vector3.zero;
        
        float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float z = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);
        
        return basePosition + new Vector3(x, 0f, z);
    }
    
    // 生成されたオブジェクトをクリア
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
    
    // ゲームをリセット
    public void ResetGame()
    {
        if (player != null)
        {
            player.ResetValue();
        }
        
        gameWon = false;
        targetValue = Random.Range(50f, 200f);
        UpdateTargetUI();
        SpawnRandomObjects();
        
        Debug.Log($"ゲームリセット - 新しい目標値: {targetValue}");
    }
    
    // 目標値UIを更新
    void UpdateTargetUI()
    {
        if (targetValueText != null)
        {
            targetValueText.text = $"目標値: {targetValue:F1}";
        }
    }
    
    // デバッグ用の描画
    void OnDrawGizmosSelected()
    {
        if (spawnArea != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(spawnArea.position, spawnAreaSize);
        }
    }
    
    // 目標値を設定
    public void SetTargetValue(float value)
    {
        targetValue = value;
        UpdateTargetUI();
    }
    
    // 許容誤差を設定
    public void SetTolerance(float value)
    {
        tolerance = value;
    }
}
