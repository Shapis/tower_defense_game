using UnityEngine;

public class AssortedCatalog : MonoBehaviour
{
    [SerializeField] private GameObject[] m_AssortedPrefabs;

    public enum AssortedPrefab
    {
        DamagePopUp,

    }

    public GameObject GetAssortedPrefab(AssortedPrefab assortedPrefabName)
    {
        switch (assortedPrefabName)
        {
            default:
                return null;
            case AssortedPrefab.DamagePopUp:
                return m_AssortedPrefabs[0];

        }
    }
}