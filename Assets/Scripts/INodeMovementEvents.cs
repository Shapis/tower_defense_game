using System;

internal interface INodeMovementEvents
{
    void OnTravelNodeReached(object sender, Node node); // Invoked from UnitNodeMovement.cs
    void OnTravelNodeDeparted(object sender, Node node); // Invoked from UnitNodeMovement.cs
    void OnDestinationNodeReachedEvent(object sender, Node node); // Invoked from UnitNodeMovement.cs
    void OnDestinationNodeDepartedEvent(object sender, Node node); // Invoked from UnitNodeMovement.cs
    void OnDestinationNotAccessibleEvent(object sender, Node node); // Invoked from UnitNodeMovement.cs
    void OnNoDestinationFoundEvent(object sender, string destinationName); // Invoked from UnitNodeMovement.cs
    void OnFinalDestinationNodeReached(object sender, Node node); // Invoked from UnitNodeMovement.cs
}