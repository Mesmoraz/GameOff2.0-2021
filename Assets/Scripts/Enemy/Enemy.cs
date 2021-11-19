using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float enemySpeed;
    public int maxHealth;
    int currentHealth;

    public float speed;
    public bool eggHeld;
    private Vector2 currentState;
    public Rigidbody2D enemyRigidBody2D;
    private float direction;

    private GameObject closestExit;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        eggHeld = false;
        currentState = transform.localScale;
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Move
        if (!eggHeld)
            ChaseEgg();
        else if (eggHeld)
            RunAway(closestExit);
        // Put AI in here
    }       
    private void PickUpEgg(GameObject obj)
    {
        switch(obj.tag)
        {
            case "Egg":
                Destroy(obj);
                eggHeld = true;
                closestExit = GameObject.Find("Exit_R");
                float distanceRight = Vector2.Distance(gameObject.transform.position, closestExit.transform.position);
                float distanceLeft = Vector2.Distance(gameObject.transform.position, GameObject.Find("Exit_L").transform.position);
                if (distanceRight > distanceLeft)
                {
                    closestExit = GameObject.Find("Exit_L");
                }
                    break;
            default:
                Debug.Log($"Can't pick this up: {obj.tag}.");
                break;
        }
            
    }
    private void ChaseEgg()
    {

        if (!eggHeld && GameObject.Find("Egg"))
        {
            
            Vector3 enemyDirectionLocal = GameObject.Find("Egg").transform.InverseTransformPoint(transform.position);

            if (enemyDirectionLocal.x > 0)
            {
                //Debug.Log("LEFT");
                transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);


            }
            else if (enemyDirectionLocal.x < 0)
            {
                // Debug.Log("RIGHT");
                transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
            }
        }

        // Try to move towards it at a constant rate.
        // check if the egg is in front of this sprite yet 
        // Grab the egg and make it inopperable to grab for other enemies. 
        // Tell other enemies to stop chasing the egg and to chase player instead

        
    }

    private void RunAway(GameObject closestExit)
    {
        transform.position = Vector2.MoveTowards(transform.position, closestExit.transform.position, speed * Time.deltaTime);  
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Egg")
        {
            PickUpEgg(collision.gameObject);
            
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }
        Debug.Log("damage Taken!");
    }
    
    public void Die()
    {
        Debug.Log("enemy died");
    }    
}
