using System;
using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : BaseEntity
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BaseAmmo>() is not null)
        {
            TakeDamage(other.gameObject.GetComponent<BaseAmmo>().Damage);
        }
        else if (other.gameObject.GetComponent<EndGameCrystal>() is not null)
        {
            Die();
        }
    }

    public override void Die()
    {
        GetComponent<UnitNodeMovement>().StopAllCoroutines();
        StartCoroutine(DestroyAfterTime(1f));
    }

    private IEnumerator DestroyAfterTime(float v)
    {
        yield return new WaitForSeconds(v);
        Destroy(gameObject);
    }
}