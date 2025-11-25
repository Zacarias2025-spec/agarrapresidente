using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float laneDistance = 2.0f; // distância entre pistas
    public int currentLane = 1; // 0 = esquerda, 1 = centro, 2 = direita
    public float forwardSpeed = 6f;
    public float laneChangeSpeed = 10f;
    public float jumpForce = 7f;
    public Sprite[] presidentSkins; // arrasta sprites no inspector

    private Vector3 targetPosition;
    private Rigidbody rb;
    private bool isGrounded = true;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
        if (presidentSkins != null && presidentSkins.Length > 0) sr.sprite = presidentSkins[0];
    }

    void Update()
    {
        // movimento automático para frente (em eixo z)
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // movimento lateral suave
        Vector3 desiredPos = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * laneChangeSpeed);

        // saltar (teclado para teste)
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveRight();
    }

    public void MoveLeft()
    {
        if (currentLane > 0) currentLane--;
    }

    public void MoveRight()
    {
        if (currentLane < 2) currentLane++;
    }

    public void Jump()
    {
        if (!isGrounded) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.PlayerHitObstacle(collision.gameObject);
        }
    }

    // trocar skin por índice
    public void SetSkin(int index)
    {
        if (presidentSkins != null && index >= 0 && index < presidentSkins.Length)
            sr.sprite = presidentSkins[index];
    }
}
