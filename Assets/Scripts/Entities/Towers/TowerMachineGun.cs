using System.Collections;
using UnityEngine;

public class TowerMachineGun : BaseTower
{

    private void Start()
    {
        if (m_AmmoSpawnPoint.Count == 0)
        {
            m_AmmoSpawnPoint.Add(gameObject.transform);
        }
    }
    public override void Shoot(BaseEnemy enemy)
    {
        GameObject _ammo = Instantiate(m_AmmoPrefab, m_AmmoSpawnPoint[0].position, Quaternion.identity);
        _ammo.GetComponent<BaseAmmo>().ShootAt(enemy);
    }
}