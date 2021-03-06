using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCrystal : BaseEntity
{
    [SerializeField] private Vector2Int m_GridPosition = new Vector2Int(0, 0);
    private NavMeshGenerator m_NavMeshGenerator;

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        m_NavMeshGenerator = FindObjectOfType<NavMeshGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Node = m_NavMeshGenerator.Grid[m_GridPosition.x, m_GridPosition.y];
        transform.position = m_Node.transform.position;
    }
}