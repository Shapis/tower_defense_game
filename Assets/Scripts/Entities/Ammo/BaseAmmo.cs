using System;
using System.Collections;
using UnityEngine;

public abstract class BaseAmmo : BaseEntity
{
    private BaseEnemy _target;
    [SerializeField] protected float m_Damage = 1f;
    [SerializeField] public float m_Speed = 10f;
    protected abstract IEnumerator CreateTrajetory();

    public BaseEnemy Target { get => _target; set => _target = value; }


    private void Start()
    {
        ShootAt(CreateTrajetory());
    }

    // Describes the movement of the ammo towards the target.
    public void ShootAt(IEnumerator trajectory)
    {
        StartCoroutine(trajectory);
    }
}