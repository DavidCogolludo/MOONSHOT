using UnityEngine;

public class AlertOnEnter : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D oCollider)
    {
        Debug.Log("Collided with enemy on Alert Dissapear");
        if (oCollider.gameObject.tag == "Enemy")
            Debug.Log("Collided with enemy on Alert Enter");
    }
}
