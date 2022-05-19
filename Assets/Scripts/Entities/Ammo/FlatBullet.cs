using System;
using System.Collections;
using UnityEngine;

public class FlatBullet : BaseAmmo
{
    public override void Die()
    {
        throw new NotImplementedException();
    }

    protected override IEnumerator CreateTrajetory()
    {
        Vector3 temp = Target.transform.position;
        while (transform.position != temp)
        {
            transform.position = Vector3.MoveTowards(transform.position, temp, m_Speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BaseEnemy>() != null)
        {
            Debug.Log(other.gameObject.name);
            Target.TakeDamage(m_Damage);
            Destroy(this.gameObject);
        }
    }
}
