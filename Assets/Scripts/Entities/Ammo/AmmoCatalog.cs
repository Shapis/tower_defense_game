using UnityEngine;

public class AmmoCatalog : MonoBehaviour
{
    [SerializeField] private GameObject[] m_AmmoPrefabs;


    public enum AmmoName
    {
        FlatBullet,
    }

    public GameObject GetAmmo(AmmoName ammoName)
    {
        switch (ammoName)
        {
            case AmmoName.FlatBullet:
                return m_AmmoPrefabs[0];
            default:
                return null;
        }
    }
}