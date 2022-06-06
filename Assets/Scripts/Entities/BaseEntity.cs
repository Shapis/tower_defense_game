using System;
using System.Collections;
using UnityEngine;
using TMPro;

public abstract class BaseEntity : MonoBehaviour
{
    public Node m_Node { get; set; }
    [SerializeField]
    private float m_Health = 10f;
    private float _currentHealth;
    private GameObject _healthBar;


    private void Start()
    {
        _currentHealth = m_Health;
    }


    private bool _healthBarShowing = false;

    protected void TakeDamage(float damageTaken)
    {
        if (_currentHealth - damageTaken >= 0)
        {
            _currentHealth -= damageTaken;
        }
        else
        {
            _currentHealth = 0;
        }

        if (_healthBar == null)
        {
            GameObject _go = FindObjectOfType<AssortedCatalog>().GetAssortedPrefab(AssortedCatalog.AssortedPrefab.HealthBar);
            _healthBar = Instantiate(_go, transform);
            _healthBar.GetComponent<Healthbar>().UpdateHealth(1f - (m_Health - _currentHealth) / m_Health);
        }
        else
        {
            _healthBar.GetComponent<Healthbar>().UpdateHealth(1f - ((m_Health - _currentHealth) / m_Health));
        }

        StartCoroutine(DamagePopUp(damageTaken));

        if (_currentHealth <= 0)
        {
            Die();
        }
    }



    IEnumerator DamagePopUp(float damageTaken)
    {
        GameObject _go = FindObjectOfType<AssortedCatalog>().GetAssortedPrefab(AssortedCatalog.AssortedPrefab.DamagePopUp);
        GameObject _damagePopUp = Instantiate(_go, transform.position, Quaternion.identity);

        TextMeshPro _textMeshPro = _damagePopUp.GetComponent<TextMeshPro>();
        _textMeshPro.text = damageTaken.ToString();
        float _offsetY = 0f;
        float _offsetX = 0f;
        float _xOffsetFactor = UnityEngine.Random.Range(-1f, 1f);
        while (_damagePopUp.transform.position.y <= transform.position.y + 1f * transform.localScale.y)
        {
            _damagePopUp.transform.position = transform.position;
            _damagePopUp.transform.position = new Vector3(
                transform.position.x + _offsetX,
                transform.position.y + _offsetY,
                transform.position.z
                );
            _offsetX += _xOffsetFactor * Time.deltaTime;
            _offsetY += 1f * Time.deltaTime;
            yield return null;
        }

        Destroy(_damagePopUp);
    }

    public abstract void Die();
}
