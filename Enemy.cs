using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("Collision")]
    [SerializeField] private float GroundCheckDistance;
    [SerializeField] private float WallCheckDistance;
    [SerializeField] private Transform CheckNoGroundTransform;
    [SerializeField] private LayerMask LayerGround;

    public bool IsGrounded;
    public bool CHeckNoGrounded;
    public bool isWall;

    public bool faceRight = false;
    public float faceDir = -1;

    private float xScale;

    // وضعیت قبلی برخورد با دیوار یا لبه
    private bool wasFlipCondition = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        xScale = transform.localScale.x;
    }


    void Update()
    {
        // اول وضعیت زمین، لبه و دیوار را بررسی کن
        HandelColision();

        if (IsGrounded)
        {
            // فقط وقتی روی زمین هست حرکت کن
            rb.linearVelocity = new Vector2(
                faceDir * speed,
                rb.linearVelocity.y
            );

            HandleFlip();
        }
        else
        {
            // وقتی در هواست، حرکت افقی نداشته باشد
            rb.linearVelocity = new Vector2(
                0f,
                rb.linearVelocity.y
            );
        }
    }


    private void HandelColision()
    {
        IsGrounded = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            GroundCheckDistance,
            LayerGround
        );

        CHeckNoGrounded = Physics2D.Raycast(
            CheckNoGroundTransform.position,
            Vector2.down,
            GroundCheckDistance,
            LayerGround
        );

        isWall = Physics2D.Raycast(
            transform.position,
            Vector2.right * faceDir,
            WallCheckDistance,
            LayerGround
        );
    }


    private void HandleFlip()
    {
        // در هوا Flip نکن
        if (!IsGrounded)
        {
            wasFlipCondition = false;
            return;
        }

        // دیوار یا تمام شدن زمین جلوی دشمن
        bool shouldFlip = isWall || !CHeckNoGrounded;

        // فقط یک بار هنگام رسیدن به دیوار/لبه Flip کن
        if (shouldFlip && !wasFlipCondition)
        {
            faceRight = !faceRight;

            faceDir = faceRight ? 1 : -1;

            transform.localScale = new Vector3(
                faceRight ? -xScale : xScale,
                transform.localScale.y,
                transform.localScale.z
            );
        }

        // وضعیت فعلی را برای فریم بعد ذخیره کن
        wasFlipCondition = shouldFlip;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(
            transform.position,
            new Vector2(
                transform.position.x,
                transform.position.y - GroundCheckDistance
            )
        );

        if (CheckNoGroundTransform != null)
        {
            Gizmos.DrawLine(
                CheckNoGroundTransform.position,
                new Vector2(
                    CheckNoGroundTransform.position.x,
                    CheckNoGroundTransform.position.y - GroundCheckDistance
                )
            );
        }

        Gizmos.DrawLine(
            transform.position,
            new Vector2(
                transform.position.x + (faceDir * WallCheckDistance),
                transform.position.y
            )
        );
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<player>() != null)
        {
            collision.GetComponent<player>().Hit();
        }
    }
}