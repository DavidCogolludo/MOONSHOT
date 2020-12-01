using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    bool isAttacking;
    bool isAttackingTransition;

    private bool isDead = false;

    Vector3 originalPos;
    Vector3 attackPos;

    Animator animator;

    private CircleCollider2D circleCollider2D;
    private GameObject trash;
    private Rigidbody2D rigidBody2D;

    public float transition;
    public float transitionFactor;

    public AudioClip sound;

    private AudioSource soundManager;

    public Quaternion initialRotation;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        trash = GameObject.FindGameObjectWithTag("Trash");
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        isAttackingTransition = false;

        initialRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Vector2 currentPosition = gameObject.transform.position;

            if (isAttackingTransition)
            {
                Vector2 newPosition = Vector2.Lerp(currentPosition, attackPos, transition);
                gameObject.transform.position = newPosition;
            }

            currentPosition = gameObject.transform.position;

            if (Vector2.Distance(currentPosition, attackPos) < transitionFactor)
            {
                isAttacking = false;
                attackPos = originalPos;

                transform.rotation = initialRotation;
            }

            if (!isAttacking && Vector2.Distance(currentPosition, originalPos) < transitionFactor)
            {
                isAttackingTransition = false;
                animator.SetBool("IsAttacking", isAttackingTransition);
            }
        }
    }

    public void Attack(Vector3 AttackPos, float angleRotation)
    {
        if (!isAttacking && !isAttackingTransition)
        {
            isAttacking = true;
            isAttackingTransition = true;

            soundManager.PlayOneShot(sound, 1.0f);

            Vector3 dir = AttackPos - gameObject.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleRotation;
            gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            originalPos = gameObject.transform.position;
            attackPos = AttackPos;

            animator.SetBool("IsAttacking", isAttacking);
        }
    }

    public void Dead()
    {
        isDead = true;
        transform.SetParent(trash.transform);
        rigidBody2D.constraints = RigidbodyConstraints2D.None;
        rigidBody2D.gravityScale = 0.05f;
    }

    private void OnTriggerEnter2D(Collider2D oCollider)
    {
        if (isAttacking && oCollider.gameObject.tag == "Enemy")
        {
            //EnemyController enemy = oCollider.gameObject.GetComponent<EnemyController>();
            //enemy.RemoveLife();
        }
    }
}
