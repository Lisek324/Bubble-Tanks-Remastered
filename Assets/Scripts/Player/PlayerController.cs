using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : EntityClass
{
    //current health
    private int health;
    private int maxHealth;

    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Collectible Variables")]
    public AudioSource collectSound;
    public static int bubbles = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    

    [Header("Player Components")]
    public GameObject player;
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement Variables")]
    [SerializeField] public float maxSpeed;
    [SerializeField] public float acceleration;
    [SerializeField] public float linearDrag;
    private float horizontalDirection;
    private float verticalDirection;
    private bool changingDirectionX => (rb.velocity.x > 0f && horizontalDirection < 0f || (rb.velocity.x < 0f && horizontalDirection > 0));
    private bool changingDirectionY => (rb.velocity.y > 0f && verticalDirection < 0f || (rb.velocity.y < 0f && verticalDirection > 0));

    void Update()
    {
        horizontalDirection = GetInput().x;
        verticalDirection = GetInput().y;
    }

    private void Start()
    {
        health = GetHealth(transform);
        maxHealth = health;
        healthText.text = "Health: " + health;
    }
    private void FixedUpdate()
    {
        MoveCharacter();
        ApplyLinearDrag();
        Rotation();
        
    }
    private static Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void MoveCharacter()
    {
        rb.AddForce(new Vector2(horizontalDirection, verticalDirection) * acceleration);
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        if (Mathf.Abs(rb.velocity.y) > maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxSpeed);
        }
    }
    private void ApplyLinearDrag()
    {
        if (Mathf.Abs(horizontalDirection) < 0.4f && Mathf.Abs(verticalDirection) < 0.4f || changingDirectionX || changingDirectionY)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void Rotation()
    {
        Vector2 directoin = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(directoin.y, directoin.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, maxSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CollectBubble"))
        {
            Destroy(collision.gameObject);
            if (maxHealth == health)
            {
                bubbles++;
            }
            else
            {
                health++;
            }
            collectSound.Play();
            scoreText.text = "Bubbles: " + bubbles;
            healthText.text = "Health: " + health;
        }
    }

    protected override int GetHealth(Transform entity)
    {
        return base.GetHealth(entity);
    }

    public void TakeDamage(int damageAmmount)
    {
        health = health - damageAmmount;
        healthText.text = "Health: " + health;
    }
}
