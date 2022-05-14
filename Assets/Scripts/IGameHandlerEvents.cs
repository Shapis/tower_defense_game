using System;

public interface IGameHandlerEvents
{
    void OnGamePause(object sender, EventArgs e); // Invoked from GameHandler.cs
    void OnGameResume(object sender, EventArgs e); // Invoked from GameHandler.cs
    void OnRoundBegins(object sender, EventArgs e); // Invoked from GameHandler.cs
    void OnRoundEnds(object sender, EventArgs e); // Invoked from GameHandler.cs

}