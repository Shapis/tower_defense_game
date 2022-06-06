using UnityEngine;

public abstract class BaseEnemy : BaseEntity
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BaseAmmo>() is not null)
        {
            TakeDamage(other.gameObject.GetComponent<BaseAmmo>().Damage);
        }
    }
}