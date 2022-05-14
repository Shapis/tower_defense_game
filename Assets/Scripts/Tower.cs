using System;
using System.Collections;
using UnityEngine;


public class Tower : BaseEntity
{
    [SerializeField] private float m_Range = 1f;
    public TowerCatalog.TowerName m_TowerName;

    private bool _isAttacking = false;

    public event EventHandler<Enemy> EnemyInRangeEvent;


    private void Update()
    {
        foreach (var item in FindObjectsOfType<Enemy>())
        {
            if (Vector2.Distance(item.transform.position, transform.position) <= m_Range)
            {
                EnemyInRange(this, item);

            }
        }
    }

    private void EnemyInRange(object sender, Enemy e)
    {
        if (!_isAttacking)
        {
            StartCoroutine(Attack());
        }
        EnemyInRangeEvent?.Invoke(this, e);
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        throw new NotImplementedException();
    }
}