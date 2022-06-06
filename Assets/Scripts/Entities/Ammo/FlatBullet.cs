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
        Vector3 direction = Target.transform.position - transform.position;
        Vector3 temp = transform.position
        + (direction * 5f);
        while (transform.position != temp)
        {
            transform.position = Vector3.MoveTowards(transform.position, temp, m_Speed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BaseEnemy>() != null && other.gameObject.GetComponent<BaseEnemy>().IsAlive)
        {
            //  Debug.Log(other.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
