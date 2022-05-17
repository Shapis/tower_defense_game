using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTower : BaseEntity
{
    [SerializeField] private TowerCatalog.TowerName m_TowerName;
    [SerializeField] protected GameObject m_AmmoPrefab;
    [SerializeField] protected List<Transform> m_AmmoSpawnPoint = new List<Transform>();
    [SerializeField] private float m_Range = 1f;
    [SerializeField] private float m_DelayToStartShooting = 0f;
    [SerializeField] private float m_TurnSpeed = 1f;
    [SerializeField] private float m_DelayBetweenShots = 0.2f;
    [SerializeField] private float m_NumberOfShots = 1f;

    protected AmmoCatalog m_AmmoCatalog;

    private Coroutine _attackCoroutine;
    private bool _isAttacking = false;
    public event EventHandler<BaseEnemy> EnemyInRangeEvent;
    public TowerCatalog.TowerName TowerName { get => m_TowerName; private set => m_TowerName = value; }

    private void Awake()
    {
        m_AmmoCatalog = FindObjectOfType<AmmoCatalog>();
    }



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
            _isAttacking = true;
            _attackCoroutine = StartCoroutine(AttackRoutine(enemy));
            EnemyInRangeEvent?.Invoke(this, enemy);
        }
    }

    private IEnumerator AttackRoutine(BaseEnemy e)
    {
        yield return new WaitForSeconds(m_DelayToStartShooting);

        for (int i = 0; i < m_NumberOfShots; i++)
        {
            Shoot(e);
            yield return new WaitForSeconds(m_DelayBetweenShots);
        }
        _isAttacking = false;
    }

    public abstract void Shoot(BaseEnemy enemy);
}
