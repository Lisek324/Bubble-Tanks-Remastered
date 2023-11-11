using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerController : EntityClass
{
    public GameObject turret;

    public int currentHealth;
    public int maxHealth;

    public bool isBig = true;

    public static PlayerController playerController;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Collectible Variables")]
    public AudioSource collectSound;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    BubbleCollectForce bubbleCollect;

    [Header("Player Components")]
    public static GameObject player;
    [SerializeField] public Rigidbody2D rb;
    public Vector2 movementVector;
    [Header("Movement Variables")]
    [SerializeField] public float maxSpeed;
    [SerializeField] public float acceleration;
    [SerializeField] public float linearDrag;
    [SerializeField] public float rotationSpeed;
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
        player = gameObject;
        playerController = this;
        SetHealth();
        //TODO: make an list/array of weapons to be rotatable, and remove everything from this list while rebuilding and add new components/weapons
        //SIDENOTE: A "weapon" tag can be missleading...i should change it to 'rotatable';
        turret = GameObject.FindGameObjectWithTag("Weapon");
        
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
        //TODO:reset players rotation before changing to bigger tank
        //FIX:player will be positioned to default params after rebuilding in tank editor (probably...i need to implement alternative steering after getting higher tank class before i can delete this comment)
        if (isBig)
        {
            movementVector = GetInput();
            if (movementVector!=Vector2.zero)
            {
                float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
                var targetRotation = new Vector3(0, 0, angle);
                var lookTo = Quaternion.Euler(targetRotation);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookTo, rotationSpeed * Time.deltaTime);
            }
            rb.AddForce(new Vector2(horizontalDirection, verticalDirection) * acceleration);

        }
        else
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
    }
    private void ApplyLinearDrag()
    {
        if (Mathf.Abs(horizontalDirection) < 0.3f && Mathf.Abs(verticalDirection) < 0.3f || changingDirectionX || changingDirectionY)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0.7f;
        }
    }

    private void Rotation()
    {
        if (isBig)
        {
            /*Vector2 directoin = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(directoin.y, directoin.x) * Mathf.Rad2Deg),99);///5 is turret rotation speed*/
        }
        else
        {
            Vector2 directoin = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(directoin.y, directoin.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, maxSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CollectBubble"))
        {
            bubbleCollect = collision.GetComponent<BubbleCollectForce>();
            Destroy(collision.gameObject);
            if (maxHealth == currentHealth)
            {
                GameManager.gameManager.bubbles+= bubbleCollect.worth;
                bubbleCollect.worth = 0;
            }
            else
            {
                currentHealth+= bubbleCollect.worth;
                bubbleCollect.worth = 0;
            }
            collectSound.Play();
            scoreText.text = "Bubbles: " + GameManager.gameManager.bubbles;
            healthText.text = "Health: " + currentHealth;
        }
    }

    public void SetHealth()
    {
        maxHealth = GetHealth(transform);
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth;
    }

    protected override int GetHealth(Transform entity)
    {
        return base.GetHealth(entity);
    }

    public void TakeDamage(int damageAmmount)
    {
        currentHealth = currentHealth - damageAmmount;
        healthText.text = "Health: " + currentHealth;
    }
}
