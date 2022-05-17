using UnityEngine;

public class EnemyCatalog : MonoBehaviour
{
    [SerializeField] private GameObject[] m_EnemyPrefabs;


    public enum EnemyName
    {
        Enemy1,
    }

    public GameObject GetEnemy(EnemyName enemyName)
    {
        switch (enemyName)
        {
            case EnemyName.Enemy1:
                return m_EnemyPrefabs[0];
            default:
                return null;
        }
    }
}