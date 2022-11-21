using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    Rigidbody2D rb;

    private float health = 100;

    public bool turnedLeft = false;

    public Image healthFill;
    private float healthWidth;

    public Text MainText;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthWidth = healthFill.sprite.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);

        turnedLeft = false;

        if (horizontal > 0)
        {
            GetComponent<Animator>().Play("Right");
        } else if (horizontal < 0)
        {
            GetComponent<Animator>().Play("Left");
            turnedLeft = true;
        } else if (vertical > 0)
        {
            GetComponent<Animator>().Play("Up");
        } else if (vertical < 0)
        {
            GetComponent<Animator>().Play("Down");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= collision.gameObject.GetComponent<EnemyScript>().GetHitStrength();
            if (health < 1)
            {
                healthFill.enabled = false;
                MainText.gameObject.SetActive(true);
                MainText.text = "Game Over";
            }
            Vector2 temp = new Vector2(healthWidth * (health/100), healthFill.sprite.rect.height);
            healthFill.rectTransform.sizeDelta = temp;
        }
    }
}
