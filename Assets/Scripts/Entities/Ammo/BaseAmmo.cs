using System;
using System.Collections;
using UnityEngine;

public abstract class BaseAmmo : BaseEntity
{
    private BaseEnemy _target;
    [SerializeField] private float m_Damage = 1f;
    [SerializeField] public float m_Speed = 10f;
    protected abstract IEnumerator CreateTrajetory();

    public BaseEnemy Target { get => _target; set => _target = value; }
    public float Damage { get => m_Damage; private set => m_Damage = value; }

    private void Start()
    {
        ShootAt(CreateTrajetory());
    }

    // Describes the movement of the ammo towards the target.
    private void ShootAt(IEnumerator trajectory)
    {
        StartCoroutine(trajectory);
    }
}