using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float maxSpeed = 8f;
    
    [Header("地面判定")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayerMask = 1;
    
    [Header("物理設定")]
    [SerializeField] private float airControl = 0.3f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float coyoteTime = 0.2f;
    
    // コンポーネント参照
    private Rigidbody2D rb2D;
    private bool isGrounded;
    private bool wasGrounded;
    private float lastGroundedTime;
    private float jumpBufferTimer;
    private float horizontalInput;
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
        // 地面判定オブジェクトが設定されていない場合、自動作成
        if (groundCheck == null)
        {
            CreateGroundCheck();
        }
    }
    
    void Update()
    {
        // 入力の取得
        GetInput();
        
        // 地面判定
        CheckGrounded();
        
        // ジャンプバッファー処理
        HandleJumpBuffer();
        
        // デバッグ情報
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ShowDebugInfo();
        }
    }
    
    void FixedUpdate()
    {
        // 移動処理
        HandleMovement();
        
        // ジャンプ処理
        HandleJump();
    }
    
    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        // ジャンプバッファー
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }
    }
    
    void CheckGrounded()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);
        
        // コヨーテタイム処理
        if (isGrounded)
        {
            lastGroundedTime = coyoteTime;
        }
        else
        {
            lastGroundedTime -= Time.deltaTime;
        }
    }
    
    void HandleMovement()
    {
        // 現在の速度を取得
        Vector2 currentVelocity = rb2D.linearVelocity;
        
        // 移動速度の計算
        float targetSpeed = horizontalInput * moveSpeed;
        
        // 空中での制御力調整
        float accelerationRate;
        if (isGrounded)
        {
            accelerationRate = 1f;
        }
        else
        {
            accelerationRate = airControl;
        }
        
        // 速度制限
        if (Mathf.Abs(currentVelocity.x) > maxSpeed && Mathf.Sign(currentVelocity.x) == Mathf.Sign(targetSpeed))
        {
            targetSpeed = Mathf.Sign(targetSpeed) * maxSpeed;
        }
        
        // 滑らかな移動
        float speedDifference = targetSpeed - currentVelocity.x;
        float movement = speedDifference * accelerationRate;
        
        rb2D.AddForce(movement * Vector2.right, ForceMode2D.Force);
        
        // キャラクターの向き変更
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    void HandleJump()
    {
        // ジャンプ条件：ジャンプバッファー内 かつ （地面にいる または コヨーテタイム内）
        if (jumpBufferTimer > 0 && (isGrounded || lastGroundedTime > 0))
        {
            // ジャンプ実行
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
            
            // タイマーリセット
            jumpBufferTimer = 0;
            lastGroundedTime = 0;
            
            Debug.Log("ジャンプ！");
        }
    }
    
    void HandleJumpBuffer()
    {
        // ジャンプバッファータイマーの更新は既にGetInput()で処理済み
    }
    
    void CreateGroundCheck()
    {
        // 子オブジェクトとして地面判定オブジェクトを作成
        GameObject groundCheckObj = new GameObject("GroundCheck");
        groundCheckObj.transform.SetParent(transform);
        groundCheckObj.transform.localPosition = new Vector3(0, -0.5f, 0);
        groundCheck = groundCheckObj.transform;
        
        Debug.Log("地面判定オブジェクトを自動作成しました");
    }
    
    void ShowDebugInfo()
    {
        Debug.Log($"地面判定: {isGrounded}");
        Debug.Log($"速度: {rb2D.linearVelocity}");
        Debug.Log($"水平入力: {horizontalInput}");
        Debug.Log($"ジャンプバッファー: {jumpBufferTimer}");
        Debug.Log($"コヨーテタイム: {lastGroundedTime}");
    }
    
    // 外部からのジャンプ呼び出し用
    public void Jump()
    {
        if (isGrounded || lastGroundedTime > 0)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
            lastGroundedTime = 0;
        }
    }
    
    // 速度のリセット
    public void ResetVelocity()
    {
        rb2D.linearVelocity = Vector2.zero;
    }
    
    // 移動速度の設定
    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
    
    // デバッグ用：地面判定の可視化
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
