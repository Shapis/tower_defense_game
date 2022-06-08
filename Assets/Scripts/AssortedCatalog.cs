using UnityEngine;

public class AssortedCatalog : MonoBehaviour
{
    [SerializeField] private GameObject[] m_AssortedPrefabs;
    [SerializeField] private Sprite[] m_TileSprites;

    public enum AssortedPrefab
    {
        DamagePopUp,
        HealthBar,

    }

    public enum AssortedTile
    {
        Green1,
        Green2,
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

    public Sprite GetAssortedTile(AssortedTile assortedTileName)
    {
        switch (assortedTileName)
        {
            default:
                return null;
            case AssortedTile.Green1:
                return m_TileSprites[0];
            case AssortedTile.Green2:
                return m_TileSprites[1];
        }
    }
}