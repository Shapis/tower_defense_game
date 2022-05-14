using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCrystal : MonoBehaviour
{
    [SerializeField] private Vector2Int m_GridPosition = new Vector2Int(0, 0);
    private Node m_Node;
    private NavMeshGenerator m_NavMeshGenerator;

    public Node Node { get => m_Node; private set => m_Node = value; }

    private void Awake()
    {
        m_NavMeshGenerator = FindObjectOfType<NavMeshGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Node = m_NavMeshGenerator.Grid[m_GridPosition.x, m_GridPosition.y];
        transform.position = Node.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
