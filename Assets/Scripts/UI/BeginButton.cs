using System;
using UnityEngine;

public class BeginButton : MonoBehaviour, IGameHandlerEvents
{
    private bool _isRoundInProgress;

    private void Start()
    {
        FindObjectOfType<InputHandler>().OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;
        GameHandler.OnRoundBeginsEvent += OnRoundBegins;
        GameHandler.OnRoundEndsEvent += OnRoundEnds;
    }

    private void OnMouseButtonLeftPressed(object sender, InputHandler.MouseInfo e)
    {
        if (_isRoundInProgress)
        {
            return;
        }

        foreach (var i in e.GameObjectsHit)
        {
            if (i == gameObject)
            {
                GameHandler.BeginRound();
            }
        }
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