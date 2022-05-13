using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshGenerator : MonoBehaviour
{
    [SerializeField] private GameObject NodePrefab;
    private Node[,] m_Grid = new Node[14, 9];
    public Node[,] Grid { get => m_Grid; private set => m_Grid = value; }

    public List<Node> GetNodesList()
    {
        List<Node> nodes = new List<Node>();
        foreach (var item in Grid)
        {
            nodes.Add(item);
        }
        return nodes;
    }

    // Start is called before the first frame update
    void Awake()
    {

        // Generate main grid
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Grid[i, j] = Instantiate(NodePrefab, new Vector3(i + 0.5f - 8f, (8 - j) + 0.5f - 4.5f, 0), Quaternion.identity).gameObject.GetComponent<Node>();
                Grid[i, j].name = "Node " + i + " " + j;
            }
        }

        // Connect main grid
        for (int i = 0; i < 14; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (j > 0)
                {
                    Grid[i, j].m_UpDestination = Grid[i, j - 1];
                }
                if (j < 8)
                {
                    Grid[i, j].m_DownDestination = Grid[i, j + 1];
                }
                if (i > 0)
                {
                    Grid[i, j].m_LeftDestination = Grid[i - 1, j];
                }
                if (i < 13)
                {
                    Grid[i, j].m_RightDestination = Grid[i + 1, j];
                }
            }
        }

        // Generate spawn points

    }
}
