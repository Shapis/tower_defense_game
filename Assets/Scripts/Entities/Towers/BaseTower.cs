using System;
using System.Collections;
using UnityEngine;

public abstract class BaseTower : BaseEntity
{
    [SerializeField] private TowerCatalog.TowerName m_TowerName;
    [SerializeField] private float m_Range = 1f;
    [SerializeField] private float m_DelayToStartShooting = 0.5f;
    [SerializeField] private float m_TurnSpeed = 1f;
    [SerializeField] private float m_DelayBetweenShots = 0.2f;

    private Coroutine _attackCoroutine;
    private bool _isAttacking = false;
    public event EventHandler<BaseEnemy> EnemyInRangeEvent;
    public TowerCatalog.TowerName TowerName { get => m_TowerName; private set => m_TowerName = value; }



    private void Update()
    {
        foreach (var item in FindObjectsOfType<BaseEnemy>())
        {
            if (Vector2.Distance(item.transform.position, transform.position) <= m_Range)
            {
                EnemyInRange(this, item);

            }
        }
    }

    private void EnemyInRange(object sender, BaseEnemy enemy)
    {
        if (!_isAttacking)
        {
            _attackCoroutine = StartCoroutine(AttackRoutine());
            EnemyInRangeEvent?.Invoke(this, enemy);
        }
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(1f);
        _isAttacking = false;
    }
}
