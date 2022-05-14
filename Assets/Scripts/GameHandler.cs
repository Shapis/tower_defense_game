using UnityEngine;
using System;

public static class GameHandler
{

    public static event EventHandler OnGamePauseEvent;
    public static event EventHandler OnGameResumeEvent;
    public static event EventHandler OnRoundBeginsEvent;
    public static event EventHandler OnRoundEndsEvent;
    public static bool isPaused = false;

    public static void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        OnGamePause();
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        OnGameResume();
    }
    public static void BeginRound()
    {
        OnRoundBegins();
    }

    public static void RoundEnds()
    {
        OnRoundEnds();
    }

    private static void OnGamePause()
    {
        OnGamePauseEvent?.Invoke(null, EventArgs.Empty);
    }

    private static void OnGameResume()
    {
        OnGameResumeEvent?.Invoke(null, EventArgs.Empty);
    }

    private static void OnRoundBegins()
    {
        OnRoundBeginsEvent?.Invoke(null, EventArgs.Empty);
    }

    private static void OnRoundEnds()
    {
        OnRoundEndsEvent?.Invoke(null, EventArgs.Empty);
    }
}