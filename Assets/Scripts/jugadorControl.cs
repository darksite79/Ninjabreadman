using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugadorControl : MonoBehaviour
{
    public float speed;
    public bool grounded;
    public float jumpPower = 6.5f;
    public float maxSpeed = 5f;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;

    Vector3 inicialPosition;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        inicialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded == true)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        rb2d.AddForce(Vector2.right * speed * h);

        //Regular la velocidad del personaje

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.x);
        }

        if (jump == true)
        {
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }


        if (h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 0, 0);
        }

        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Grounded")
        {
            grounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Grounded")
        {
            grounded = false;
        }
    }

    public void resetPosition()
    {
        transform.position = inicialPosition;
    }
}
