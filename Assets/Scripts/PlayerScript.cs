using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed = 10f;

    public Text score;

    public Text win;

    public Text madeBy;

    public Text lives;

    private int scoreValue = 0;

    private int numLives = 3;

    public AudioSource musicSource;
    public AudioClip music;
    public AudioClip winSound;

    Animator anim;
    private bool facingRight = true;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    void Start()
    {
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();

        anim = GetComponent<Animator>();

        rb2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = "Lives: " + numLives.ToString();
        win.text = "";
        madeBy.text = "";
    }

    void FixedUpdate ()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

         isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        rb2d.AddForce(new Vector2(hozMovement * speed * Time.deltaTime, vertMovement * speed * Time.deltaTime));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (isOnGround == false)
        {
            anim.SetInteger("State", 2);
        } else if (hozMovement != 0)
        {
            anim.SetInteger("State", 1);
        } else if (hozMovement == 0)
        {
            anim.SetInteger("State", 0);
        }

            if (facingRight == false && hozMovement > 0)
        {
            Flip();
        } else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue++;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                this.transform.position = new Vector3 (50, 0, 0);

                numLives = 3;
                lives.text = "Lives: " + numLives.ToString();
            }


            if (scoreValue == 8)
            {
                win.text = "You Win!!!";
                madeBy.text = "Made by Marcus Napoloni";

                musicSource.clip = winSound;
                musicSource.loop = false;
                musicSource.Play();
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            numLives--;
            lives.text = "Lives: " + numLives.ToString();
            Destroy(collision.collider.gameObject);

            if (numLives <= 0)
            {
                win.text = "You Lose!!!";
                madeBy.text = "Made by Marcus Napoloni";

                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
