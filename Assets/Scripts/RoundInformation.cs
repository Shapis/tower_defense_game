using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class RoundInformation : MonoBehaviour, IGameHandlerEvents
{
    private int _roundNumber = 0;
    private bool _isRoundInProgress = false;
    private bool _roundIsInProgress = false;

    private void Start()
    {
        GameHandler.OnRoundBeginsEvent += OnRoundBegins;
    }
    private void Update()
    {
        GetComponent<TextMeshPro>().text = "Round: " + _roundNumber + " / " + "Enemies: " + FindObjectsOfType<BaseEnemy>().Length;

        if (_isRoundInProgress && FindObjectsOfType<BaseEnemy>().Length > 0)
        {
            _roundIsInProgress = true;
        }

        if (_roundIsInProgress && FindObjectsOfType<BaseEnemy>().Length == 0)
        {
            _isRoundInProgress = false;
            _roundIsInProgress = false;
            OnRoundEnds(this, EventArgs.Empty);
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
        Debug.Log("here");
        GameHandler.RoundEnds();
    }
}