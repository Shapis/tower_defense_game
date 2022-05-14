using System;
using UnityEngine;

public interface IDraggableEvents
{
    void OnDraggingBegins(object sender, InputHandler.MouseInfo mouseInfo); // Invoked from Draggable.cs
    void OnDraggingEnds(object sender, InputHandler.MouseInfo mouseInfo); // Invoked from Draggable.cs
}