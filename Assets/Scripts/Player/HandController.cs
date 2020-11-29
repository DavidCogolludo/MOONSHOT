using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    bool isAttacking;
    bool isAttackingTransition;

    Vector2 originalPos;
    Vector2 attackPos;

    Animator animator;

    private CircleCollider2D circleCollider2D;

    public float transition;
    public float transitionFactor;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        isAttackingTransition = false;
    }

    // Update is called once per frame
    void Update()
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
        }

        if (!isAttacking && Vector2.Distance(currentPosition, originalPos) < transitionFactor)
        {
            isAttackingTransition = false;
            animator.SetBool("IsAttacking", isAttackingTransition);
        }
    }

    public void Attack(Vector2 AttackPos)
    {
        if (!isAttacking && !isAttackingTransition)
        {
            isAttacking = true;
            isAttackingTransition = true;

            originalPos = gameObject.transform.position;
            attackPos = AttackPos;

            animator.SetBool("IsAttacking", isAttacking);
        }
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
