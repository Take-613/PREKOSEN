using UnityEngine;
using TMPro;

// 数値オブジェクト
public class NumberObject : MonoBehaviour
{
    [Header("数値設定")]
    [SerializeField] private int numberValue;
    
    [Header("視覚的表示")]
    [SerializeField] private TextMeshPro textDisplay;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        UpdateDisplay();
    }
    
    // 数値の表示を更新
    private void UpdateDisplay()
    {
        if (textDisplay != null)
        {
            textDisplay.text = numberValue.ToString();
        }
    }
    
    // プレイヤーとの接触時に呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        PlayerCalculator player = other.GetComponent<PlayerCalculator>();
        if (player != null)
        {
            player.ApplyCalculation(numberValue);
            Debug.Log($"プレイヤーが数値 {numberValue} を取得しました");
            
            // オブジェクトを削除または非表示にする
            gameObject.SetActive(false);
        }
    }
    
    // Inspector上で値が変更された時に呼ばれる
    private void OnValidate()
    {
        UpdateDisplay();
    }
    
    public float GetValue()
    {
        return numberValue;
    }
    
    public void SetValue(int value)
    {
        numberValue = value;
        UpdateDisplay();
    }
}
