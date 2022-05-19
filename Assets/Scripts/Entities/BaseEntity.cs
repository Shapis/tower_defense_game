using System;
using System.Collections;
using UnityEngine;
using TMPro;

public abstract class BaseEntity : MonoBehaviour
{
    public Node m_Node { get; set; }
    [SerializeField]
    private float m_Health = 10f;

    public void TakeDamage(float damageTaken)
    {
        m_Health -= damageTaken;
        StartCoroutine(DamagePopUp());
        if (m_Health <= 0)
        {
            Die();
        }
    }

    IEnumerator DamagePopUp()
    {
        GameObject _go = FindObjectOfType<AssortedCatalog>().GetAssortedPrefab(AssortedCatalog.AssortedPrefab.DamagePopUp);
        GameObject _damagePopUp = Instantiate(_go, transform.position, Quaternion.identity);

        float _offsetY = 0f;
        while (_damagePopUp.transform.position.y <= transform.position.y + 1f * transform.localScale.y)
        {
            _damagePopUp.transform.position = transform.position;
            _damagePopUp.transform.position = new Vector3(
                transform.position.x,
                transform.position.y + _offsetY,
                transform.position.z
                );
            _offsetY += 0.3f * Time.deltaTime;
            yield return null;
        }

        Destroy(_damagePopUp);
    }

    public abstract void Die();
}
