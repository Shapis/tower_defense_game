using UnityEngine;

public class AssortedCatalog : MonoBehaviour
{
    [SerializeField] private GameObject[] m_AssortedPrefabs;

    public enum AssortedPrefab
    {
        DamagePopUp,
        HealthBar,

    }

    public GameObject GetAssortedPrefab(AssortedPrefab assortedPrefabName)
    {
        switch (assortedPrefabName)
        {
            default:
                return null;
            case AssortedPrefab.DamagePopUp:
                return m_AssortedPrefabs[0];
            case AssortedPrefab.HealthBar:
                return m_AssortedPrefabs[1];
        }
    }
}