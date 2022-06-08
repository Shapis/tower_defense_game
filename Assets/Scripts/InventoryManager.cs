using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDraggableEvents, IGameHandlerEvents
{
    [SerializeField] private GameObject[] m_WallsAndTurrets;

    private InputHandler m_InputHandler;
    private bool _isRoundInProgress;

    private void Awake()
    {
        m_InputHandler = FindObjectOfType<InputHandler>();

    }

    private void Start()
    {
        m_InputHandler.OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;
        GameHandler.OnRoundBeginsEvent += OnRoundBegins;
        GameHandler.OnRoundEndsEvent += OnRoundEnds;
    }

    private void OnMouseButtonLeftPressed(object sender, InputHandler.MouseInfo e)
    {
        if (_isRoundInProgress || GameHandler.isPaused)
        {
            return;
        }

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

    public void OnGamePause(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnGameResume(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnRoundBegins(object sender, EventArgs e)
    {
        _isRoundInProgress = true;
    }

    public void OnRoundEnds(object sender, EventArgs e)
    {
        _isRoundInProgress = false;
    }
}