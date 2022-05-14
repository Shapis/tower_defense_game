using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerCatalog;
public class InventoryManager : MonoBehaviour, IDraggableEvents
{
    [SerializeField] private GameObject[] m_WallsAndTurrets;

    private TowerCatalog m_TowerCatalog;

    private InputHandler m_InputHandler;
    private void Awake()
    {
        m_InputHandler = FindObjectOfType<InputHandler>();
        m_TowerCatalog = FindObjectOfType<TowerCatalog>();
    }

    private void Start()
    {
        m_InputHandler.OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;

        foreach (var item in m_WallsAndTurrets)
        {
            item.GetComponent<SpriteRenderer>().sprite = m_TowerCatalog.GetTower(item.GetComponent<InventoryButton>().m_TowerName).GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void OnMouseButtonLeftPressed(object sender, InputHandler.MouseInfo e)
    {
        foreach (var i in e.GameObjectsHit)
        {
            for (int j = 0; j < m_WallsAndTurrets.Length; j++)
            {
                if (i == m_WallsAndTurrets[j])
                {
                    GenerateEntity(m_WallsAndTurrets[j], e);
                    return;
                }
            }
        }
    }

    private void GenerateEntity(GameObject inventorySlot, InputHandler.MouseInfo e)
    {
        GameObject _go;
        _go = Instantiate(FindObjectOfType<TowerCatalog>().GetTower(inventorySlot.GetComponent<InventoryButton>().m_TowerName), new Vector3(e.WorldPosition.x, e.WorldPosition.y, 0), Quaternion.identity);
        _go.GetComponent<Draggable>().OnDraggingBegins(this, e);
    }

    public void OnDraggingBegins(object sender, InputHandler.MouseInfo mouseInfo)
    {
        throw new NotImplementedException();
    }

    public void OnDraggingEnds(object sender, InputHandler.MouseInfo mouseInfo)
    {
        throw new NotImplementedException();
    }
}
