using System;
using UnityEngine;

public class Draggable : MonoBehaviour, IDraggableEvents
{

    [SerializeField] private bool m_DebugLogging = false;
    public event EventHandler<InputHandler.MouseInfo> OnDraggingBeginsEvent;
    public event EventHandler<InputHandler.MouseInfo> OnDraggingEndsEvent;
    private bool _isPickedUp = false;
    private void Start()
    {
        FindObjectOfType<InputHandler>().OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;
        FindObjectOfType<InputHandler>().OnMouseHoverEvent += OnMouseHover;
        FindObjectOfType<InputHandler>().OnMouseButtonLeftUnpressedEvent += OnMouseButtonLeftUnpressed;
    }

    private void OnMouseButtonLeftUnpressed(object sender, InputHandler.MouseInfo e)
    {
        if (_isPickedUp)
        {
            _isPickedUp = false;
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
        foreach (var item in e.GameObjectsHit)
        {
            if (item.gameObject == gameObject && !_isPickedUp)
            {
                _isPickedUp = true;
                OnDraggingBegins(this, e);
            }
        }
    }

    public void OnDraggingBegins(object sender, InputHandler.MouseInfo mouseInfo)
    {
        if (m_DebugLogging)
        {
            Debug.Log(this.name + " Dragging begins");
        }
        OnDraggingBeginsEvent?.Invoke(sender, mouseInfo);
    }

    public void OnDraggingEnds(object sender, InputHandler.MouseInfo mouseInfo)
    {
        if (m_DebugLogging)
        {
            Debug.Log(this.name + " Dragging ends");
        }
        OnDraggingEndsEvent?.Invoke(sender, mouseInfo);
    }
}