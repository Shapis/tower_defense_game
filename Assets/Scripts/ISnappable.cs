using System;

public interface ISnappable
{
    void OnSnap(object sender, Node targetNode); // Invoked from SnapToGrid.cs
    void OnUnsnap(object sender, Node targetNode); // Invoked from SnapToGrid.cs
    void OnFailedToSnap(object sender, Node targetNode); // Invoked from SnapToGrid.cs
}