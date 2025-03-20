using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyAi>())
        {
            Debug.Log("hello");
        }
    }
}
