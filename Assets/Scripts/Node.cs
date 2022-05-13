using System;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Stuff for NodePathFinding.cs
    public float f;
    public float g;
    public float h;
    public Node previousPathNode;
    //

    [Header("Node Destinations")]

    [SerializeField] public Node m_UpDestination;
    [SerializeField] public Node m_DownDestination;
    [SerializeField] public Node m_LeftDestination;
    [SerializeField] public Node m_RightDestination;
    [SerializeField] public bool IsTravelNode;
    [SerializeField] public bool IsAccessible = true;
    private InputHandler m_InputHandler;
    private NavMeshGenerator m_NavMeshGenerator;

    private void Awake()
    {
        m_InputHandler = FindObjectOfType<InputHandler>();
        m_NavMeshGenerator = FindObjectOfType<NavMeshGenerator>();
    }

    private void Start()
    {
        m_InputHandler.OnMouseButtonRightPressedEvent += OnMouseButtonRightPressed;
        m_InputHandler.OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;
    }

    private void OnMouseButtonLeftPressed(object sender, InputHandler.MouseInfo e)
    {
        foreach (var item in e.GameObjectsHit)
        {
            if (item.gameObject == gameObject)
            {
                NodePathFinding pathFinding = new NodePathFinding();
                List<Node> myPath = pathFinding.FindPath(m_NavMeshGenerator.Grid[0, 0], GetComponent<Node>(), m_NavMeshGenerator.GetNodesList());

                foreach (var item2 in myPath)
                {
                    item2.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }


    }

    private void OnMouseButtonRightPressed(object sender, InputHandler.MouseInfo e)
    {
        foreach (var item in e.GameObjectsHit)
        {
            if (item.gameObject == gameObject)
            {
                IsAccessible = !IsAccessible;
                gameObject.GetComponent<SpriteRenderer>().color = IsAccessible ? Color.white : Color.red;
            }
        }
    }
}
