using UnityEngine;

public class DestroyWithPar : MonoBehaviour
{
    // OnTriggerEnter2D は、Is Trigger がオンの Collider2D と他の Collider2D が接触したときに呼び出される
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突した相手が「Player」タグを持つオブジェクトか確認
        // プレイヤーオブジェクトに「Player」タグが付いていることを確認してください
        if (other.CompareTag("Player"))
        {
            Debug.Log("Playerと衝突");

            // 親オブジェクト（3Dアイテム本体）ごと破壊する
            // transform.parent は親のTransformコンポーネントを返す
            // 親がルートオブジェクトの場合、transform.parent は null になるので注意
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject); // 親GameObjectを破壊
            }
            else
            {
                // 親がいない場合は、この2D衝突判定オブジェクト自身を破壊
                // （ただし、このケースでは通常親がいるはずなのでデバッグ用）
                Destroy(gameObject);
            }
        }
    }
}