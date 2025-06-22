using UnityEngine;
using TMPro; // TextMeshPro用の名前空間を追加

// 演算子の種類を定義
public enum OperatorType
{
    Add,        // 加算 (+)
    Subtract,   // 減算 (-)
    Multiply,   // 乗算 (×)
    Divide      // 除算 (÷)
}

// 演算子オブジェクト
public class Oparator : MonoBehaviour
{
    [Header("演算子設定")]
    [SerializeField] private OperatorType operatorType;
    [SerializeField] private string operatorSymbol;
    
    [Header("視覚的表示")]
    [SerializeField] private TextMeshPro textDisplay;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        UpdateDisplay();
    }
    
    // 演算子の表示を更新
    private void UpdateDisplay()
    {
        switch (operatorType)
        {
            case OperatorType.Add:
                operatorSymbol = "+";
                break;
            case OperatorType.Subtract:
                operatorSymbol = "-";
                break;
            case OperatorType.Multiply:
                operatorSymbol = "×";
                break;
            case OperatorType.Divide:
                operatorSymbol = "÷";
                break;
        }
        
        if (textDisplay != null)
        {
            textDisplay.text = operatorSymbol;
        }
    }
    
    // プレイヤーとの接触時に呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        PlayerCalculator player = other.GetComponent<PlayerCalculator>();
        if (player != null)
        {
            player.SetOperator(operatorType);
            Debug.Log($"プレイヤーが演算子 {operatorSymbol} を取得しました");
            
            // オブジェクトを削除または非表示にする
            gameObject.SetActive(false);
        }
    }
    
    // Inspector上で値が変更された時に呼ばれる
    private void OnValidate()
    {
        UpdateDisplay();
    }
    
    public OperatorType GetOperatorType()
    {
        return operatorType;
    }
    
    public string GetOperatorSymbol()
    {
        return operatorSymbol;
    }
}
