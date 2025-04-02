using UnityEngine;

public class Destructble : MonoBehaviour
{
    [SerializeField] private GameObject desstroyVFX;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<DamageSource>())
        {
            Instantiate(desstroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
