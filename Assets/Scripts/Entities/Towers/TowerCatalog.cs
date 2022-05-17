using UnityEngine;

public class TowerCatalog : MonoBehaviour
{
    [SerializeField] private GameObject[] m_TowerPrefabs;

    public enum TowerName
    {
        Wall,
        Tower1,
    }

    public GameObject GetTower(TowerName towerName)
    {
        switch (towerName)
        {
            default:
                return null;
            case TowerName.Wall:
                return m_TowerPrefabs[0];
            case TowerName.Tower1:
                return m_TowerPrefabs[1];
        }
    }
}