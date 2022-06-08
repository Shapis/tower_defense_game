using System;
using System.Collections.Generic;
using UnityEngine;
using static AssortedCatalog;

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
    [SerializeField] public TowerCatalog.TowerName? m_TowerName = null;

    private void Start()
    {
        PickSprite();
    }

    private void PickSprite()
    {
        if (UnityEngine.Random.Range(0, 1) < 1)
        {
            GetComponent<SpriteRenderer>().sprite =
        FindObjectOfType<AssortedCatalog>().GetAssortedTile(AssortedTile.Green1);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite =
                    FindObjectOfType<AssortedCatalog>().GetAssortedTile(AssortedTile.Green2);
        }
    }
}
