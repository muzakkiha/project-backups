using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float moveSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public float climbForce;
    public float downClimbingSpeed;
    public float feetRadius;
    public LayerMask ground;

    [SerializeField]
    Transform feetPos;
    [SerializeField]
    Transform handPos;
    [SerializeField]
    Transform handBackPos;
    [SerializeField]
    GameObject playerSprite;
    public bool flipSprite;
    public bool isGrounded;
    public bool isClimbing;
    public bool isClimbingBack;
    float directionX;
    Vector3 refVelocity = Vector3.zero;

    public float jumpTime;
    bool isJumping;
    float jumpTimeCounter;


    private float timeBtwShots;
    public float startTimeBtwShots;
    private float timeBtwShots2;
    public float startTimeBtwShots2;
    private float timeBtwShots4;
    public float startTimeBtwShots4;
    public Transform shotPoint;
    public Transform shotPoint2;
    public Transform pointer;
    public Transform pointer2;
    public GameObject projectile;
    public GameObject projectile2;
    public GameObject projectile4;
    private bool projectileState;

    private float timeBtwShots3;
    public float startTimeBtwShots3;
    public GameObject ringOfSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        flipSprite = false;
        isGrounded = false;
        isJumping = true;
        directionX = Input.GetAxis("Horizontal");
        projectileState = true;
    }

    void FixedUpdate()
    {
        directionX = Input.GetAxis("Horizontal");

        // movement in ground
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton5)) && isGrounded && Mathf.Abs(rb.velocity.x) < sprintSpeed)
        {
            Vector3 targetVelocity = new Vector2(directionX * sprintSpeed, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, 0.2f);
            anim.speed = 1.5f;
        }
        else if ((!Input.GetKey(KeyCode.LeftShift) || !Input.GetKey(KeyCode.JoystickButton5)) && isGrounded && Mathf.Abs(rb.velocity.x) < sprintSpeed)
        {
            Vector3 targetVelocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, 0.1f);
            anim.speed = 1f;
        }
        // to make the character slide in ground when in high speed by increasing the smoothing
        else if (isGrounded && Mathf.Abs(rb.velocity.x) > moveSpeed)
        {
            Vector3 targetVelocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, 0.4f);
        }
        else if (!isGrounded)
        {
            // air movement, let the character travel in high speed by ignoring the input for the same direction
            if ((rb.velocity.x >= 0 && directionX < 0) || (rb.velocity.x <= 0 && directionX > 0))
            {
                Vector3 targetVelocity = new Vector2(directionX * sprintSpeed, rb.velocity.y);
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, 0.4f);
            }
            // but still, the character will be able to accelerate to the direction if the air speed is low
            if (Mathf.Abs(rb.velocity.x) < sprintSpeed)
            {
                Vector3 targetVelocity = new Vector2(directionX * sprintSpeed, rb.velocity.y);
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, 0.4f);
            }
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetRadius, ground);
        isClimbing = Physics2D.OverlapCircle(handPos.position, feetRadius, ground);
        isClimbingBack = Physics2D.OverlapCircle(handBackPos.position, feetRadius, ground);

        // init jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && isGrounded && (!isClimbing || !isClimbingBack))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.AddForce(Vector2.up * jumpForce * 8f);
            anim.SetTrigger("takeoff");
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            isJumping = false;
        }

        if (directionX > 0)
        {
            flipSprite = false;
        }
        else if (directionX < 0)
        {
            flipSprite = true;
        }

        if (!flipSprite)
        {
            playerSprite.transform.localScale = new Vector3(1f, playerSprite.transform.localScale.y, playerSprite.transform.localScale.z);
        }
        else
        {
            playerSprite.transform.localScale = new Vector3(-1f, playerSprite.transform.localScale.y, playerSprite.transform.localScale.z);
        }

        if (directionX == 0f)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(projectile, shotPoint.position, pointer.transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
            if (Input.GetKey(KeyCode.JoystickButton4))
            {
                Instantiate(projectile, shotPoint2.position, pointer2.transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (timeBtwShots2 <= 0)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                Instantiate(projectile2, shotPoint.position, pointer.transform.rotation);
                timeBtwShots2 = startTimeBtwShots2;
            }
            if (Input.GetAxis("SecondaryFire") > 0)
            {
                Instantiate(projectile2, shotPoint2.position, pointer2.transform.rotation);
                timeBtwShots2 = startTimeBtwShots2;
            }
        }
        else
        {
            timeBtwShots2 -= Time.deltaTime;
        }

        if (timeBtwShots3 <= 0)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Instantiate(ringOfSpeed, transform.position, Quaternion.identity);
                timeBtwShots3 = startTimeBtwShots3;
            }
            if (Input.GetKey(KeyCode.JoystickButton3))
            {
                Instantiate(ringOfSpeed, transform.position, Quaternion.identity);
                timeBtwShots3 = startTimeBtwShots3;
            }
        }
        else
        {
            timeBtwShots3 -= Time.deltaTime;
        }

        if (timeBtwShots4 <= 0)
        {
            if (Input.GetMouseButton(2))
            {
                GameObject projectilezz = Instantiate(projectile4, shotPoint.position, pointer.transform.rotation);
                projectilezz.GetComponent<teleBullet>().player = gameObject;
                timeBtwShots4 = startTimeBtwShots4;
            }
            if (Input.GetAxis("ThirdFire") > 0)
            {
                GameObject projectilezz = Instantiate(projectile4, shotPoint2.position, pointer2.transform.rotation);
                projectilezz.GetComponent<teleBullet>().player = gameObject;
                timeBtwShots4 = startTimeBtwShots4;
            }
        }
        else
        {
            timeBtwShots4 -= Time.deltaTime;
        }

    }

    void Update()
    {
        // after init jump, jump higher by pressing the button longer, and reset after releasing the button
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0)) && isJumping && (!isClimbing || !isClimbingBack))
        {
            if (jumpTimeCounter > 0)
            {
                rb.AddForce(Vector2.up * jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }


        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && (isClimbing || isClimbingBack))
        {
            isJumping = true;
            if (isClimbing)
            {
                rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector2.zero, ref refVelocity, 0.1f);
                if (flipSprite)
                {
                    jumpTimeCounter = jumpTime;
                    rb.AddForce(new Vector2(climbForce, jumpForce * 5f));
                }
                else
                {
                    jumpTimeCounter = jumpTime;
                    rb.AddForce(new Vector2(climbForce * -1f, jumpForce * 5f));
                }
            }
            if (isClimbingBack)
            {
                rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector2.zero, ref refVelocity, 0.05f);
                if (flipSprite)
                {
                    jumpTimeCounter = jumpTime;
                    rb.AddForce(new Vector2(climbForce * -1.8f, jumpForce * 7f));
                }
                else
                {
                    jumpTimeCounter = jumpTime;
                    rb.AddForce(new Vector2(climbForce * 1.8f, jumpForce * 7f));
                }
            }
            anim.SetTrigger("takeoff");
        }

        if (isClimbing)
        {
            if (flipSprite && Input.GetAxis("Horizontal") < 0)
            {
                rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector2(rb.velocity.x, downClimbingSpeed), ref refVelocity, 0.4f);
            }
            else if (!flipSprite && Input.GetAxis("Horizontal") > 0)
            {
                rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector2(rb.velocity.x, downClimbingSpeed), ref refVelocity, 0.4f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            anim.SetTrigger("jumping");
            anim.SetBool("isJumping", false);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            anim.SetTrigger("jumping");
            anim.SetBool("isJumping", true);
        }
    }
}
