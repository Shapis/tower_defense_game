using UnityEngine;

public abstract class BaseAmmo : BaseEntity
{
    [SerializeField] protected float m_Damage = 1f;
    public abstract void ShootAt(BaseEnemy enemy);
}