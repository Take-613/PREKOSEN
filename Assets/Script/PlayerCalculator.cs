using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを追加

// プレイヤーの計算機能を管理するクラス
public class PlayerCalculator : MonoBehaviour
{
    [Header("計算設定")]
    [SerializeField] private float currentValue = 2;
    [SerializeField] private OperatorType? currentOperator = null;
    
    [Header("UI表示")]
    [SerializeField] private TextMeshProUGUI valueDisplayText;
    [SerializeField] private TextMeshProUGUI operatorDisplayText;
    [SerializeField] private Canvas uiCanvas;
    
    [Header("プレイヤー見た目")]
    [SerializeField] private TextMeshPro playerValueDisplay; // プレイヤーの数値表示（メイン）
    [SerializeField] private MeshRenderer playerMeshRenderer; // プレイヤーのメッシュレンダラー
    [SerializeField] private Transform playerTransform; // プレイヤーのTransform（サイズ変更用）
    [SerializeField] private bool hideOriginalMesh = true; // 元のメッシュを隠すか
    [SerializeField] private Color positiveColor = Color.green; // プラス値の色
    [SerializeField] private Color negativeColor = Color.red; // マイナス値の色
    [SerializeField] private Color zeroColor = Color.white; // ゼロの色
    [SerializeField] private float minScale = 0.5f; // 最小スケール
    [SerializeField] private float maxScale = 3.0f; // 最大スケール
    [SerializeField] private float scaleMultiplier = 0.1f; // スケール倍率
    
    [Header("デバッグ")]
    [SerializeField] private bool showDebugInfo = true;
    
    void Start()
    {
        InitializePlayerAppearance();
        UpdateUI();
        UpdatePlayerAppearance();
    }
    
    // プレイヤーの見た目を初期化
    void InitializePlayerAppearance()
    {
        // プレイヤーのTransformが設定されていない場合、自分自身を使用
        if (playerTransform == null)
        {
            playerTransform = transform;
        }
        
        // MeshRendererが設定されていない場合、自分自身から取得
        if (playerMeshRenderer == null)
        {
            playerMeshRenderer = GetComponent<MeshRenderer>();
        }
        
        // 数値表示用TextMeshProを作成または設定
        if (playerValueDisplay == null)
        {
            CreatePlayerNumberDisplay();
        }
        
        // 元のメッシュを隠す（数値のみを表示）
        if (hideOriginalMesh && playerMeshRenderer != null)
        {
            playerMeshRenderer.enabled = false;
        }
    }
    
    // プレイヤーの数値表示を作成
    void CreatePlayerNumberDisplay()
    {
        // 子オブジェクトとしてTextMeshProを作成
        GameObject numberDisplayObj = new GameObject("PlayerNumberDisplay");
        numberDisplayObj.transform.SetParent(transform);
        numberDisplayObj.transform.localPosition = Vector3.zero;
        numberDisplayObj.transform.localRotation = Quaternion.identity;
        numberDisplayObj.transform.localScale = Vector3.one;
        
        // TextMeshProコンポーネントを追加
        playerValueDisplay = numberDisplayObj.AddComponent<TextMeshPro>();
        
        // TextMeshProの設定
        playerValueDisplay.text = currentValue.ToString("F1");
        playerValueDisplay.fontSize = 10;
        playerValueDisplay.color = Color.white;
        playerValueDisplay.alignment = TextAlignmentOptions.Center;
        
        // 常にカメラの方を向くようにする
        numberDisplayObj.AddComponent<LookAtCamera>();
        
        Debug.Log("プレイヤー数値表示を作成しました");
    }
    
    // 演算子を設定
    public void SetOperator(OperatorType operatorType)
    {
        currentOperator = operatorType;
        UpdateUI();
        
        if (showDebugInfo)
        {
            Debug.Log($"演算子が設定されました: {GetOperatorSymbol(operatorType)}");
        }
    }
    
    // 数値を使って計算を実行
    public void ApplyCalculation(float value)
    {
        if (currentOperator.HasValue)
        {
            float oldValue = currentValue;
            
            switch (currentOperator.Value)
            {
                case OperatorType.Add:
                    currentValue += value;
                    break;
                case OperatorType.Subtract:
                    currentValue -= value;
                    break;
                case OperatorType.Multiply:
                    currentValue *= value;
                    break;
                case OperatorType.Divide:
                    if (value != 0)
                    {
                        currentValue /= value;
                    }
                    else
                    {
                        Debug.LogWarning("0で割ろうとしました！");
                        return;
                    }
                    break;
            }
            
            if (showDebugInfo)
            {
                Debug.Log($"計算実行: {oldValue} {GetOperatorSymbol(currentOperator.Value)} {value} = {currentValue}");
            }
            
            // 演算子をリセット
            currentOperator = null;
        }
        else
        {
            // 演算子が設定されていない場合は、値を直接設定
            currentValue = value;
            
            if (showDebugInfo)
            {
                Debug.Log($"値を設定: {currentValue}");
            }
        }
        
        UpdateUI();
        UpdatePlayerAppearance(); // 見た目も更新
    }
    
    // UIを更新
    private void UpdateUI()
    {
        if (valueDisplayText != null)
        {
            valueDisplayText.text = $"値: {currentValue:F2}";
        }
        
        if (operatorDisplayText != null)
        {
            if (currentOperator.HasValue)
            {
                operatorDisplayText.text = $"演算子: {GetOperatorSymbol(currentOperator.Value)}";
            }
            else
            {
                operatorDisplayText.text = "演算子: なし";
            }
        }
        
        // プレイヤーの見た目を更新
        UpdatePlayerAppearance();
    }
    
    // プレイヤーの見た目を更新
    private void UpdatePlayerAppearance()
    {
        UpdatePlayerValueDisplay();
        UpdatePlayerScale();
        
        if (showDebugInfo)
        {
            Debug.Log($"プレイヤー見た目更新: 値={currentValue:F1}");
        }
    }
    
    // プレイヤーの数値表示を更新（メインの見た目）
    private void UpdatePlayerValueDisplay()
    {
        if (playerValueDisplay != null)
        {
            // 数値をメインの見た目として表示
            playerValueDisplay.text = currentValue.ToString("F1");
            
            // 値に応じて色を変更
            Color targetColor;
            if (currentValue > 0.01f)
            {
                targetColor = positiveColor;
            }
            else if (currentValue < -0.01f)
            {
                targetColor = negativeColor;
            }
            else
            {
                targetColor = zeroColor;
            }
            
            playerValueDisplay.color = targetColor;
            
            // 値に応じてフォントサイズも調整
            float fontSize = Mathf.Clamp(8 + Mathf.Abs(currentValue) * 0.5f, 6, 20);
            playerValueDisplay.fontSize = fontSize;
        }
    }
    
    // プレイヤーのサイズを値に応じて変更（数値テキストのスケール）
    private void UpdatePlayerScale()
    {
        if (playerValueDisplay != null)
        {
            // 値の絶対値に基づいてスケールを計算
            float absValue = Mathf.Abs(currentValue);
            float targetScale = Mathf.Clamp(1f + (absValue * scaleMultiplier), minScale, maxScale);
            
            // TextMeshProのスケールを変更
            playerValueDisplay.transform.localScale = Vector3.one * targetScale;
            
            if (showDebugInfo)
            {
                Debug.Log($"数値表示スケール更新: {targetScale:F2} (値: {currentValue:F1})");
            }
        }
    }
    
    // 演算子のシンボルを取得
    private string GetOperatorSymbol(OperatorType operatorType)
    {
        switch (operatorType)
        {
            case OperatorType.Add: return "+";
            case OperatorType.Subtract: return "-";
            case OperatorType.Multiply: return "×";
            case OperatorType.Divide: return "÷";
            default: return "?";
        }
    }
    
    // 値をリセット
    public void ResetValue()
    {
        currentValue = 0f;
        currentOperator = null;
        UpdateUI();
        UpdatePlayerAppearance(); // 見た目も更新
        
        if (showDebugInfo)
        {
            Debug.Log("値がリセットされました");
        }
    }
    
    // 現在の値を取得
    public float GetCurrentValue()
    {
        return currentValue;
    }
    
    // 現在の演算子を取得
    public OperatorType? GetCurrentOperator()
    {
        return currentOperator;
    }
    
    // 値を直接設定（デバッグ用）
    public void SetValue(float value)
    {
        currentValue = value;
        UpdateUI();
        UpdatePlayerAppearance(); // 見た目も更新
    }
    
    // Update method for debugging
    void Update()
    {
        // デバッグ用のキー入力
        if (showDebugInfo)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetValue();
            }
        }
    }
    
    // エディターでプレイヤー上にTextMeshProを自動作成（デバッグ用）
    [ContextMenu("プレイヤー数値表示を作成")]
    private void CreatePlayerValueDisplay()
    {
        if (playerValueDisplay == null)
        {
            // 子オブジェクトとしてTextMeshProを作成
            GameObject textObject = new GameObject("PlayerValueDisplay");
            textObject.transform.SetParent(transform);
            textObject.transform.localPosition = new Vector3(0, 2f, 0); // プレイヤーの上に配置
            textObject.transform.localRotation = Quaternion.identity;
            textObject.transform.localScale = Vector3.one;
            
            // TextMeshProコンポーネントを追加
            playerValueDisplay = textObject.AddComponent<TextMeshPro>();
            playerValueDisplay.text = currentValue.ToString("F1");
            playerValueDisplay.fontSize = 4;
            playerValueDisplay.color = Color.white;
            playerValueDisplay.alignment = TextAlignmentOptions.Center;
            
            Debug.Log("プレイヤー数値表示を作成しました");
        }
        else
        {
            Debug.Log("プレイヤー数値表示は既に存在します");
        }
    }
}
