using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyCatalog;

public class Spawner : MonoBehaviour, IGameHandlerEvents
{
    [SerializeField] private Vector2Int m_GridPosition = new Vector2Int(0, 0);
    private Node m_Node;
    private NavMeshGenerator m_NavMeshGenerator;
    private EnemyCatalog m_EnemyCatalog;


    private void Awake()
    {
        m_NavMeshGenerator = FindObjectOfType<NavMeshGenerator>();
        m_EnemyCatalog = FindObjectOfType<EnemyCatalog>();
        GameHandler.OnRoundBeginsEvent += OnRoundBegins;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Node = m_NavMeshGenerator.Grid[m_GridPosition.x, m_GridPosition.y];
        transform.position = m_Node.transform.position;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForEndOfFrame(); // This is needed so that the spawner can be placed before the enemies are spawned. Otherwise, the spawner wont find the end crystal node and there will be an object null exception.
        NodePathFinding pathFinding = new NodePathFinding();
        List<Node> myPath = pathFinding.FindPath(m_Node, FindObjectOfType<EndGameCrystal>().m_Node, m_NavMeshGenerator.GetNodesList());
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);
            GameObject unit = Instantiate(m_EnemyCatalog.GetEnemy(EnemyName.Enemy1), m_Node.transform.position, Quaternion.identity);

            unit.GetComponent<UnitNodeMovement>().FollowPath(myPath);
        }
    }

    public void OnGamePause(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnGameResume(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnRoundBegins(object sender, EventArgs e)
    {
        StartCoroutine(SpawnEnemies());
    }

    public void OnRoundEnds(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}