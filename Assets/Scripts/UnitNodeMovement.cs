using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNodeMovement : MonoBehaviour
{
    private Coroutine _followPathCoroutine;
    private Node m_CurrentNode;
    [SerializeField] private bool m_DebugLoggingEnabled = false;
    [SerializeField] private float m_Speed = 3f;
    [SerializeField] private float m_TurnSpeed = 10f;

    public event EventHandler<Node> OnInitialTravelNodeLoadedEvent;
    public event EventHandler<Node> OnTravelNodeReachedEvent;
    public event EventHandler<Node> OnTravelNodeDepartedEvent;
    public event EventHandler<Node> OnDestinationNodeReachedEvent;
    public event EventHandler<Node> OnDestinationNodeDepartedEvent;
    public event EventHandler<Node> OnDestinationNotAccessibleEvent;
    public event EventHandler<string> OnNoDestinationFoundEvent;

    void Start()
    {

    }

    private void MoveUp(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_UpDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_UpDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted(this, m_CurrentNode);
                StartCoroutine(DoUp());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(this, m_CurrentNode.m_UpDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound(this, "up");
        }
    }

    private void MoveDown(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_DownDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_DownDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted(this, m_CurrentNode);
                StartCoroutine(DoDown());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(this, m_CurrentNode.m_DownDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound(this, "down");
        }
    }

    private void MoveLeft(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_LeftDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_LeftDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted(this, m_CurrentNode);
                StartCoroutine(DoLeft());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(this, m_CurrentNode.m_LeftDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound(this, "left");
        }
    }

    private void MoveRight(object sender, EventArgs e)
    {
        if (m_CurrentNode.m_RightDestination != null)
        {
            if (gameObject.transform.position == m_CurrentNode.transform.position && m_CurrentNode.m_RightDestination.GetComponent<Node>().IsAccessible)
            {
                OnDestinationNodeDeparted(this, m_CurrentNode);
                StartCoroutine(DoRight());
            }
            else if (gameObject.transform.position == m_CurrentNode.transform.position)
            {
                OnDestinationNotAccessible(this, m_CurrentNode.m_RightDestination);
            }
        }
        else if (gameObject.transform.position == m_CurrentNode.transform.position)
        {
            OnNoDestinationFound(this, "right");
        }
    }

    private IEnumerator DoUp()
    {
        while (gameObject.transform.position != m_CurrentNode.m_UpDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_UpDestination.transform.position, m_Speed * Time.deltaTime);


            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_UpDestination);
    }
    private IEnumerator DoDown()
    {
        while (gameObject.transform.position != m_CurrentNode.m_DownDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_DownDestination.transform.position, m_Speed * Time.deltaTime);

            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_DownDestination);
    }
    private IEnumerator DoLeft()
    {
        while (gameObject.transform.position != m_CurrentNode.m_LeftDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_LeftDestination.transform.position, m_Speed * Time.deltaTime);
            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_LeftDestination);
    }
    private IEnumerator DoRight()
    {
        while (gameObject.transform.position != m_CurrentNode.m_RightDestination.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_CurrentNode.m_RightDestination.transform.position, m_Speed * Time.deltaTime);
            yield return null;
        }

        NodeReachedDecider(m_CurrentNode.m_RightDestination);

    }


    private void DoFollow(Node previousNode)
    {
        string myDirection = "";


        if (m_CurrentNode.m_DownDestination != null)
        {
            if (m_CurrentNode.m_DownDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "down";
            }
        }

        if (m_CurrentNode.m_UpDestination != null)
        {
            if (m_CurrentNode.m_UpDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "up";
            }
        }

        if (m_CurrentNode.m_LeftDestination != null)
        {
            if (m_CurrentNode.m_LeftDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "left";
            }
        }

        if (m_CurrentNode.m_RightDestination != null)
        {
            if (m_CurrentNode.m_RightDestination.GetComponent<Node>() != previousNode)
            {
                myDirection = "right";
            }
        }

        switch (myDirection)
        {
            case "up": OnTravelNodeDeparted(this, m_CurrentNode); StartCoroutine(DoUp()); break;
            case "down": OnTravelNodeDeparted(this, m_CurrentNode); StartCoroutine(DoDown()); break;
            case "left": OnTravelNodeDeparted(this, m_CurrentNode); StartCoroutine(DoLeft()); break;
            case "right": OnTravelNodeDeparted(this, m_CurrentNode); StartCoroutine(DoRight()); break;
            default: Debug.Log("Couldnt figure out which direction to go from the travel node! ps, if this happened, assign destinations at the node: " + m_CurrentNode); break;
        }
    }

    // Decides what to do once a node is reached.
    private void NodeReachedDecider(Node destination)
    {
        if ((gameObject.transform.position == destination.transform.position) && !destination.GetComponent<Node>().IsTravelNode)
        {
            m_CurrentNode = destination.GetComponent<Node>();
            OnDestinationNodeReached(this, m_CurrentNode);
        }
        else if ((gameObject.transform.position == destination.transform.position) && destination.GetComponent<Node>().IsTravelNode)
        {
            Node previousNode = m_CurrentNode;
            m_CurrentNode = destination.GetComponent<Node>();
            OnTravelNodeReached(this, m_CurrentNode);
            DoFollow(previousNode);
        }
    }

    public void OnDestinationNodeReached(object sender, Node _currentNode)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node reached: " + _currentNode.GetComponent<Node>());
        }
        OnDestinationNodeReachedEvent?.Invoke(this, _currentNode);
    }

    public void OnDestinationNodeDeparted(object sender, Node _currentNode)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node departed: " + _currentNode.GetComponent<Node>());
        }
        OnDestinationNodeDepartedEvent?.Invoke(this, _currentNode);
    }

    public void OnTravelNodeReached(object sender, Node _currentNode)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Travel node reached: " + _currentNode.GetComponent<Node>());
        }
        OnTravelNodeReachedEvent?.Invoke(this, _currentNode);
    }

    public void OnTravelNodeDeparted(object sender, Node _currentNode)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination node departed: " + _currentNode.GetComponent<Node>());
        }
        OnTravelNodeDepartedEvent?.Invoke(this, _currentNode);
    }

    public void OnNoDestinationFound(object sender, string targetDestination)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("No destination found! Target destination: " + targetDestination);
        }
        OnNoDestinationFoundEvent?.Invoke(this, targetDestination);
    }

    public void OnDestinationNotAccessible(object sender, Node targetDestination)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Destination not accessible! Target destination: " + targetDestination.GetComponent<Node>());
        }
        OnDestinationNotAccessibleEvent?.Invoke(this, targetDestination);
    }

    public void OnInitialDestinationNodeLoaded(object sender, Node _currentNode)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Initial destination node loaded: " + _currentNode.GetComponent<Node>());
        }
        OnInitialTravelNodeLoadedEvent?.Invoke(this, _currentNode);
    }

    // This doesn't work well with travel nodes. Since if you are on a travel node, the nodePath you will receive will have your current travel node as the first element in the list. Meaning that the coroutine will start from the current travel node, and not the initial node that the player will end up on once the previous traveling concludes.
    public void OnNodeTouched(object sender, List<Node> nodePath)
    {
        if (!m_CurrentNode.IsTravelNode && nodePath != null)
        {
            if (_followPathCoroutine != null)
            {
                StopCoroutine(_followPathCoroutine);
            }
            _followPathCoroutine = StartCoroutine("DoFollowPath", nodePath);
        }
    }

    public void FollowPath(List<Node> nodePath)
    {
        List<Node> nodePathCopy = new List<Node>(nodePath); // Copying so that the original list is not modified.
        m_CurrentNode = nodePathCopy[0];
        if (!m_CurrentNode.IsTravelNode && nodePathCopy != null)
        {
            if (_followPathCoroutine != null)
            {
                StopCoroutine(_followPathCoroutine);
            }
            _followPathCoroutine = StartCoroutine("DoFollowPath", nodePathCopy);
        }
    }


    // TODO: Make it so it doesn't call MoveUp/Down/Left/Right when travel nodes are reached. It's unnecessary.
    IEnumerator DoFollowPath(List<Node> nodePathCopy)
    {

        if (m_DebugLoggingEnabled)
        {
            Debug.Log("Following path...");
        }

        bool inTransit = false;
        while (nodePathCopy.Count > 1)
        {
            if (!inTransit)
            {
                if (nodePathCopy[1] == nodePathCopy[0].m_UpDestination)
                {
                    inTransit = true;
                    MoveUp(this, EventArgs.Empty);
                }
                else if (nodePathCopy[1] == nodePathCopy[0].m_DownDestination)
                {
                    inTransit = true;
                    MoveDown(this, EventArgs.Empty);
                }
                else if (nodePathCopy[1] == nodePathCopy[0].m_LeftDestination)
                {
                    inTransit = true;
                    MoveLeft(this, EventArgs.Empty);
                }
                else if (nodePathCopy[1] == nodePathCopy[0].m_RightDestination)
                {
                    inTransit = true;
                    MoveRight(this, EventArgs.Empty);
                }
            }

            if (m_CurrentNode == nodePathCopy[1])
            {
                inTransit = false;
                nodePathCopy.RemoveAt(0);
            }
            yield return null;
        }
    }
}