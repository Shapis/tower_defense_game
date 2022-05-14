using System;
using UnityEngine;

public class BeginButton : MonoBehaviour, IGameHandlerEvents
{

    private void Start()
    {
        FindObjectOfType<InputHandler>().OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;
    }

    private void OnMouseButtonLeftPressed(object sender, InputHandler.MouseInfo e)
    {
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
        throw new NotImplementedException();
    }

    public void OnRoundEnds(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}