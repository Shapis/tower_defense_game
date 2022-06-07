using UnityEngine;
using static TowerCatalog;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] public TowerName m_TowerName;
    [SerializeField] public Sprite m_Sprite;
    private TowerCatalog m_TowerCatalog;

    private void Awake()
    {
        m_TowerCatalog = FindObjectOfType<TowerCatalog>();
    }

    private void Start()
    {

        GetComponent<SpriteRenderer>().sprite =
            m_TowerCatalog.GetTower(m_TowerName).GetComponentInChildren<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().color =
        m_TowerCatalog.GetTower(m_TowerName).GetComponentInChildren<SpriteRenderer>().color;
    }
}
