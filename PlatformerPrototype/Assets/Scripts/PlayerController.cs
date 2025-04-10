using UnityEngine; // Unity 엔진 기능을 사용하기 위해 필요

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;    // 플레이어의 좌우 이동 속도
    public float jumpForce = 10f;   // 점프할 때의 힘 (위쪽으로 가는 속도)

    private Rigidbody2D rb;         // Rigidbody2D 컴포넌트를 저장할 변수
    private bool isGrounded;        // 플레이어가 땅에 닿아 있는지 여부를 저장

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 게임 시작 시 Rigidbody2D 컴포넌트를 가져옴
    }

    void Update()
    {
        // 좌우 방향 입력 받기 (-1: 왼쪽, 0: 없음, 1: 오른쪽)
        float moveInput = Input.GetAxisRaw("Horizontal");

        // 현재 y 속도는 유지한 채, x 방향 속도만 moveInput에 따라 설정
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 스페이스바를 누르고 있고, 플레이어가 땅 위에 있을 때 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // x 속도는 그대로 유지하고, y 방향으로 점프력만큼 속도 적용
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // 무언가와 충돌했을 때 호출됨 (2D 충돌)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 면의 법선(normal)이 위쪽(0,1) 방향에 가까울 때 땅으로 판단
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true; // 땅에 닿았다고 표시
        }
    }

    // 충돌이 끝났을 때 호출됨
    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false; // 더 이상 땅에 닿아있지 않음
    }
}
