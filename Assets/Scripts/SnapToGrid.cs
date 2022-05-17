using System;
using UnityEngine;

public class SnapToGrid : MonoBehaviour, IDraggableEvents, ISnappable
{
    [SerializeField] private string m_ContainerName = "Entities";
    [SerializeField] private bool m_AutoMove = false;
    [SerializeField] private bool m_SnappingActive = true;
    [SerializeField] private Vector3 m_GridSize = new Vector3(.5f, .5f, 1f);
    [SerializeField] private bool m_DebugLogging = false;

    public event EventHandler<Node> OnSnapEvent;
    public event EventHandler<Node> OnUnsnapEvent;
    public event EventHandler<Node> OnFailedToSnapEvent;
    private NavMeshGenerator m_NavMesh;
    private Node _initialDraggingNode;

    private InputHandler m_InputHandler;

    private void Awake()
    {
        m_NavMesh = FindObjectOfType<NavMeshGenerator>();
    }

    private void Start()
    {
        if (gameObject.GetComponent<Draggable>() != null)
        {
            gameObject.GetComponent<Draggable>().OnDraggingBeginsEvent += OnDraggingBegins;
            gameObject.GetComponent<Draggable>().OnDraggingEndsEvent += OnDraggingEnds;
        }
    }

    public void OnDraggingBegins(object sender, InputHandler.MouseInfo mouseInfo)
    {
        foreach (var i in mouseInfo.GameObjectsHit)
        {
            foreach (var j in m_NavMesh.GetNodesList())
            {
                if (i.GetComponent<Node>() == j)
                {
                    _initialDraggingNode = j;
                    OnUnsnap(this, i.GetComponent<Node>());
                    return;
                }
            }
        }
    }

    public void OnDraggingEnds(object sender, InputHandler.MouseInfo mouseInfo)
    {
        foreach (var i in mouseInfo.GameObjectsHit)
        {
            foreach (var j in m_NavMesh.GetNodesList())
            {
                if (i.GetComponent<Node>() == j)
                {
                    if (_initialDraggingNode == j || i.GetComponent<Node>().EndGameCrystal)
                    {
                        OnFailedToSnap(this, i.GetComponent<Node>());
                        return;
                    }
                    else if (i.GetComponent<Node>().IsAccessible && GetComponent<Wall>() != null)
                    {
                        OnSnap(this, i.GetComponent<Node>());
                        return;
                    }
                    else if (!i.GetComponent<Node>().IsAccessible && i.GetComponent<Node>().m_TowerName == null && GetComponent<BaseTower>() != null)
                    {
                        OnSnap(this, i.GetComponent<Node>());
                        return;
                    }
                    else
                    {
                        OnFailedToSnap(this, i.GetComponent<Node>());
                        return;
                    }
                }
            }
        }
        OnFailedToSnap(this, null);
    }

    public void OnSnap(object sender, Node targetNode)
    {
        if (GetComponent<Wall>() != null)
        {
            targetNode.IsAccessible = false;
        }
        else if (GetComponent<BaseTower>() != null)
        {
            targetNode.m_TowerName = GetComponent<BaseTower>().TowerName;
        }
        transform.position = targetNode.transform.position;
    }

    public void OnUnsnap(object sender, Node targetNode)
    {
        if (GetComponent<Wall>() != null)
        {
            targetNode.IsAccessible = true;
        }
        else if (GetComponent<BaseTower>() != null)
        {
            targetNode.m_TowerName = null;
        }

        OnUnsnapEvent?.Invoke(sender, targetNode);
    }

    public void OnFailedToSnap(object sender, Node targetNode)
    {
        this.gameObject.GetComponent<Draggable>().
        DestroyThis();
        OnFailedToSnapEvent?.Invoke(sender, targetNode);
    }


    private void MoveToDirectory()
    {
        this.transform.parent = GameObject.Find(m_ContainerName).transform;
        if (m_DebugLogging)
        {
            Debug.Log(this.gameObject + " moved to " + m_ContainerName);
        }
    }


    private void OnDrawGizmos()
    {
        if ((this.transform.parent != GameObject.Find(m_ContainerName).transform) && m_AutoMove)
        {
            MoveToDirectory();
        }
        if (!Application.isPlaying && transform.hasChanged && m_SnappingActive)
        {
            SnapSceneEditorOnly();
        }
    }

    private void SnapSceneEditorOnly()
    {
        if (m_GridSize.x == 0 || m_GridSize.y == 0 || m_GridSize.z == 0)
        {
            Debug.Log(this + " : m_GridSize cannot be 0");
            return;
        };
        ;
        Vector3 _position = new Vector3(
            (Mathf.Round(this.transform.position.x / m_GridSize.x) * m_GridSize.x),
            (Mathf.Round(this.transform.position.y / m_GridSize.y) * m_GridSize.y),
            (Mathf.Round(this.transform.position.z / m_GridSize.z) * m_GridSize.z)
        ); ;
        this.transform.position = _position;
    }


}