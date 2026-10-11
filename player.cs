using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [SerializeField] private Transform mtmeterwall;
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Health")]
    public int Health = 3;

    [Header("Shield")]
    public bool HasShield = false;

    [Header("movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jump;

    [Header("colision")]
    [SerializeField] private float GroundCheckDistance;
    [SerializeField] private float WallCheckDistance;
    [SerializeField] private LayerMask LayerGround;

    [Header("Prefab")]
    [SerializeField] private GameObject DieEffect;

    [Header("Audio")]
    public AudioSource EFX;
    public AudioClip JumpSound;
    public AudioClip HitSound;

    private bool isGrounded;
    private bool isWall;
    private bool isWallJumping;
    private bool isHit;
    private bool DontHit;

    private float xInput;
    private bool faceRight = true;
    private float faceDir = 1;
    private float xScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        xScale = transform.localScale.x;
    }


    void Update()
    {
        if (Health <= 0)
        {
            Instantiate(DieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (isWall)
        {
            rb.linearDamping = 8;
        }
        else
        {
            rb.linearDamping = 0;
        }
        xInput = Input.GetAxisRaw("Horizontal");
        if (isHit)
            return;
        HandelJump();
        move();
        Handelflip();
        Handelanim();
        HandelColosion();
    }

    private void HandelColosion()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, LayerGround);
        isWall = Physics2D.Raycast(transform.position, Vector2.right * faceDir, WallCheckDistance, LayerGround);
    }

    private void HandelJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && isWall)
        {
            if (faceRight)
            {
                faceRight = false;
                faceDir = -1;
                transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
                rb.linearVelocity = new Vector2(-6, jump);
                AudioSource.PlayClipAtPoint(JumpSound, transform.position);
            }
            else if (!faceRight)
            {
                faceRight = true;
                faceDir = 1;
                transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
                rb.linearVelocity = new Vector2(6, jump);
                AudioSource.PlayClipAtPoint(JumpSound, transform.position);
            }
            StopAllCoroutines();
            GetComponent<SpriteRenderer>().enabled = true;
            DontHit = false;
            StartCoroutine(WallJumping());
        }
    }

    private IEnumerator WallJumping()
    {
        isWallJumping = true;
        yield return new WaitForSeconds(0.7f);
        isWallJumping = false;
    }

    private void Handelanim()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWall", isWall);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void Handelflip()
    {
        if (isWallJumping)
            return;
        if (xInput == -1)
        {
            faceRight = false;
            faceDir = -1;
            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
        }
        else if (xInput == 1)
        {
            faceRight = true;
            faceDir = 1;
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - GroundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (WallCheckDistance * faceDir), transform.position.y));
    }
    public void Hit()
    {
        if (isHit)
            return;
        if (DontHit)
            return;
        if (HasShield)
            return;
        Health -= 1;
        anim.SetTrigger("isHit");
        isWallJumping = false;
        if (faceRight)
        {
            rb.linearVelocity = new Vector2(-4, rb.linearVelocity.y);
            AudioSource.PlayClipAtPoint(HitSound, transform.position);
        }
        if (!faceRight)
        {
            rb.linearVelocity = new Vector2(4, rb.linearVelocity.y);
            AudioSource.PlayClipAtPoint(HitSound, transform.position);
        }
        StopAllCoroutines();
        StartCoroutine(Hiting());
    }
    private IEnumerator Hiting()
    {
        isHit = true;
        yield return new WaitForSeconds(0.3f);
        isHit = false;
        StartCoroutine(HitingAnimation());
    }
    private IEnumerator HitingAnimation()
    {
        DontHit = true;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        DontHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Hit();
        if (collision.CompareTag("End"))
        {
            GameManger gameManager = FindFirstObjectByType<GameManger>();

            if (gameManager != null)
            {
                gameManager.LoadNextSceneAfterDelay();
            }
        }
    }

    private void move()
    {
        if (isWall)
            return;
        if (isWallJumping)
            return;
        if (isHit)
            return;
        rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocity.y);
    }

    /*private void move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }
    }*/
    public void ClickLeftDown()
    {
        xInput = -1;
    }
    public void ClickLeftUp()
    {
        xInput = 0;
    }
    public void ClickRightDown()
    {
        xInput = 1;
    }
    public void ClickRightUp()
    {
        xInput = 0;
    }
    public void ClickJump()
    {
        if(isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
        }
        if(!isGrounded && isWall)
        {
            if (faceRight)
            {
                faceRight = false;
                faceDir = -1;
                transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
                rb.linearVelocity = new Vector2(-6, jump);
            }
            else if (!faceRight)
            {
                faceRight = true;
                faceDir = 1;
                transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
                rb.linearVelocity = new Vector2(6, jump);
            }
            StopAllCoroutines();
            GetComponent<SpriteRenderer>().enabled = true;
            DontHit = false;
            StartCoroutine(WallJumping());


        }
    }
}
