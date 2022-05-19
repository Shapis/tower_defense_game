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
        GameObject _ammoPrefab = Instantiate(m_AmmoPrefab, m_AmmoSpawnPoint[0].position, Quaternion.identity);
        _ammoPrefab.GetComponent<BaseAmmo>().Target = enemy;
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}