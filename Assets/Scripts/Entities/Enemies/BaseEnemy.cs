using System;
using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : BaseEntity, INodeMovementEvents
{
    private void Start()
    {
        SafeUpdateRotation();
        GetComponent<UnitNodeMovement>().OnDestinationNodeReachedEvent += OnDestinationNodeReachedEvent;
    }
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

    public void OnTravelNodeReached(object sender, Node node)
    {
        throw new NotImplementedException();
    }

    public void OnTravelNodeDeparted(object sender, Node node)
    {
        throw new NotImplementedException();
    }

    public void OnDestinationNodeReachedEvent(object sender, Node node)
    {
        m_Node = node;
        SafeUpdateRotation();
    }

    public void OnDestinationNodeDepartedEvent(object sender, Node node)
    {
        throw new NotImplementedException();
    }

    public void OnDestinationNotAccessibleEvent(object sender, Node node)
    {
        throw new NotImplementedException();
    }

    public void OnNoDestinationFoundEvent(object sender, string destinationName)
    {
        throw new NotImplementedException();
    }

    public void OnFinalDestinationNodeReached(object sender, Node node)
    {
        throw new NotImplementedException();
    }
}