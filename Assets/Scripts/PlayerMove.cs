using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    public GameObject basicPlayerBullet;
    float speed = 8;
    public float bulletSpeed;
    public float power;
    public float shootDelay;
    float curDelay;
    bool TouchTop = false;
    bool TouchBottom = false;
    bool TouchRight = false;
    bool TouchLeft = false;
    void Awake()
    {
        animator = GetComponent<Animator>();    
    }
    void Update()
    {
        curDelay += Time.deltaTime;
        if (Input.GetKey(KeyCode.Z))
        {
            Shoot();
        }
        Move();   
    }
    void Shoot()
    {
        if (curDelay < shootDelay)
            return;
        GameObject basicBulletR = Instantiate(basicPlayerBullet, transform.position + (Vector3.right * 0.25f) + (Vector3.up * 0.3f), basicPlayerBullet.transform.rotation);
        GameObject basicBulletL = Instantiate(basicPlayerBullet, transform.position + (Vector3.left * 0.25f) + (Vector3.up * 0.3f), basicPlayerBullet.transform.rotation);
        Rigidbody2D rigidR = basicBulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = basicBulletL.GetComponent<Rigidbody2D>();
        rigidR.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
        curDelay = 0;
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 4;
        float h = Input.GetAxisRaw("Horizontal");
        if ((h == 1 && TouchRight) || (h == -1 && TouchLeft))
            h = 0;
        float v = Input.GetAxisRaw("Vertical");
        if ((v == 1 && TouchTop) || (v == -1 && TouchBottom))
            v = 0;
        Vector3 vec = new Vector3(h, v, 0);
        transform.position += vec * speed * Time.deltaTime;
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            animator.SetInteger("Move", (int)h);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    TouchTop = true;
                    break;
                case "Bottom":
                    TouchBottom = true;
                    break;
                case "Right":
                    TouchRight = true;
                    break;
                case "Left":
                    TouchLeft = true;
                    break;
            }
        }    
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    TouchTop = false;
                    break;
                case "Bottom":
                    TouchBottom = false;
                    break;
                case "Right":
                    TouchRight = false;
                    break;
                case "Left":
                    TouchLeft = false;
                    break;
            }
        }
    }
}
