using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerCatalog;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] public TowerCatalog.TowerName m_TowerName;
    [SerializeField] public Sprite m_Sprite;
    private TowerCatalog m_TowerCatalog;

    private void Awake()
    {
        m_TowerCatalog = FindObjectOfType<TowerCatalog>();
    }

    private void Start()
    {
        Debug.Log(m_TowerCatalog.GetTower(m_TowerName).GetComponent<SpriteRenderer>().sprite);
        GetComponent<SpriteRenderer>().sprite =
            m_TowerCatalog.GetTower(m_TowerName).GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().color =
        m_TowerCatalog.GetTower(m_TowerName).GetComponent<SpriteRenderer>().color;
    }
}
