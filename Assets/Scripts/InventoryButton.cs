using UnityEngine;
using static TowerCatalog;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] public TowerName m_TowerName;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    private TowerCatalog m_TowerCatalog;

    private void Awake()
    {
        m_TowerCatalog = FindObjectOfType<TowerCatalog>();
    }

    private void Start()
    {
        m_SpriteRenderer.sprite = m_TowerCatalog.GetTower(m_TowerName).GetComponentInChildren<SpriteRenderer>().sprite;

    }
}
