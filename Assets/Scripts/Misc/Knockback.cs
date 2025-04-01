using System;
using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool GettingKnockedBack{get; private set;}
    [SerializeField] private float knockBackTime = .2f;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetKnockedback(Transform damageSourcem, float knockBackThrush){
        GettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSourcem.position).normalized*knockBackThrush*rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.linearVelocity = Vector2.zero;
        GettingKnockedBack = false;
    }
}
