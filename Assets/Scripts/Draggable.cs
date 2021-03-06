using System;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour, IDraggableEvents, IGameHandlerEvents
{

    [SerializeField] private bool m_DebugLogging = false;
    public event EventHandler<InputHandler.MouseInfo> OnDraggingBeginsEvent;
    public event EventHandler<InputHandler.MouseInfo> OnDraggingEndsEvent;
    private bool _isPickedUp = false;
    private bool _isRoundInProgress = false;

    private string _originalSortingLayer;
    private void Start()
    {
        FindObjectOfType<InputHandler>().OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;
        FindObjectOfType<InputHandler>().OnMouseHoverEvent += OnMouseHover;
        FindObjectOfType<InputHandler>().OnMouseButtonLeftUnpressedEvent += OnMouseButtonLeftUnpressed;
        GameHandler.OnRoundBeginsEvent += OnRoundBegins;
        GameHandler.OnRoundEndsEvent += OnRoundEnds;
    }

    public void DestroyThis()
    {
        FindObjectOfType<InputHandler>().OnMouseButtonLeftPressedEvent -= OnMouseButtonLeftPressed;
        FindObjectOfType<InputHandler>().OnMouseHoverEvent -= OnMouseHover;
        FindObjectOfType<InputHandler>().OnMouseButtonLeftUnpressedEvent -= OnMouseButtonLeftUnpressed;
        Destroy(gameObject);
    }

    private void OnMouseButtonLeftUnpressed(object sender, InputHandler.MouseInfo e)
    {
        if (_isPickedUp)
        {
            OnDraggingEnds(this, e);
        }

    }

    private void OnMouseHover(object sender, InputHandler.MouseInfo e)
    {
        if (_isPickedUp)
        {
            transform.position = e.WorldPosition;
        }
    }

    private void OnMouseButtonLeftPressed(object sender, InputHandler.MouseInfo e)
    {
        if (_isRoundInProgress)
        {
            return;
        }
        List<GameObject> towerHits = new List<GameObject>();
        foreach (var item in e.GameObjectsHit)
        {
            if (item.GetComponent<BaseTower>() != null)
            {
                towerHits.Add(item);
            }
        }

        if (towerHits.Count > 0 && this.gameObject.GetComponent<BaseTower>() == null)
        {
            return;
        }

        foreach (var item in e.GameObjectsHit)
        {
            if (item.gameObject == gameObject && !_isPickedUp)
            {
                OnDraggingBegins(this, e);
            }
        }


    }

    public void OnDraggingBegins(object sender, InputHandler.MouseInfo mouseInfo)
    {
        foreach (var item in GetComponentsInChildren<SpriteRenderer>())
        {
            _originalSortingLayer = item.sortingLayerName;
        }
        foreach (var item in GetComponentsInChildren<SpriteRenderer>())
        {
            item.sortingLayerName = "Draggables";
        }

        _isPickedUp = true;
        if (m_DebugLogging)
        {
            Debug.Log(this.name + " Dragging begins");
        }
        OnDraggingBeginsEvent?.Invoke(sender, mouseInfo);
    }

    public void OnDraggingEnds(object sender, InputHandler.MouseInfo mouseInfo)
    {
        foreach (var item in GetComponentsInChildren<SpriteRenderer>())
        {
            item.sortingLayerName = _originalSortingLayer;
        }
        _isPickedUp = false;
        if (m_DebugLogging)
        {
            Debug.Log(this.name + " Dragging ends");
        }
        OnDraggingEndsEvent?.Invoke(sender, mouseInfo);
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