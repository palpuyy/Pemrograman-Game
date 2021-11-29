using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class control : MonoBehaviour
{
    Animator anim;
    bool isJump = true; //cek diatas platform
    bool isDead = false; //cek player jatuh atautidak ?
    int gerak = 0; //0 gerak, 1 kekanan 2 kekiri
    // Start is called before the first frame update
    public Text infoText;
    private int score = 0;
    public GameObject winPrefab, losePrefab;
    public int minPoint = 10;
    public bool isGameFinished = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        updateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameFinished) return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            idle();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            idle();
        }
        move();
        Dead();
    }

    //kondisi ketika menyentuh tanah
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isJump)
        {
            anim.ResetTrigger("jump");
            if (gerak == 0) anim.SetTrigger("idle");
            isJump = false;
        }
    }

    //kondisi ketika tidak menyentuh tanah
    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetTrigger("jump");
        anim.ResetTrigger("run");
        anim.ResetTrigger("idle");
        isJump = true;
    }
    public void moveRight()
    {
        gerak = 1;
    }
    public void moveLeft()
    {
        gerak = 2;
    }

    void move()
    {
        // gerak kekanan
        if(gerak == 1 && !isDead)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        // gerak kekiri
        if (gerak == 2 && !isDead)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(-1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void jump()
    {
        if (!isJump)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < 1)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 650f);
            }
        }
    }

    public void idle()
    {
        if (!isJump)
        {
                anim.ResetTrigger("jump");
                anim.ResetTrigger("run");
                anim.SetTrigger("idle");
        }
        gerak = 0;
    }

    public void Dead()
    {
        if (!isDead)
        {
            if(transform.position.y< -10f)
            {
                isDead = true;
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("test");
        if (collision.gameObject.CompareTag("coin"))
        {
            Destroy(collision.gameObject);
            score = score + 10;
            updateScore();
        } else if (collision.CompareTag("Falls"))
        {
            losePrefab.SetActive(true);
            isGameFinished = true;
        }
    }

    public void updateScore()
    {

        infoText.text = $"Score: {score}";
        if (score >= minPoint)
        {
            winPrefab.SetActive(true);
            isGameFinished = true;
        }

    }
}
