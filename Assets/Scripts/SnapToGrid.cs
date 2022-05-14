using System;
using UnityEngine;

public class SnapToGrid : MonoBehaviour, IDraggableEvents
{
    [SerializeField] private string m_ContainerName = "Entities";
    [SerializeField] private bool m_AutoMove = true;
    [SerializeField] private bool m_SnappingActive = true;
    [SerializeField] private Vector3 m_GridSize = new Vector3(.5f, .5f, 1f);
    [SerializeField] private bool m_DebugLogging = false;
    private NavMeshGenerator m_NavMesh;

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


    public void OnDraggingEnds(object sender, InputHandler.MouseInfo mouseInfo)
    {
        foreach (var i in mouseInfo.GameObjectsHit)
        {
            foreach (var j in m_NavMesh.GetNodesList())
            {
                if (i.GetComponent<Node>() == j)
                {
                    i.GetComponent<Node>().IsAccessible = false;
                    return;
                }
            }
        }
    }

    private void OnMouseButtonLeftUnpressed(object sender, InputHandler.MouseInfo e)
    {

    }

    private void OnDrawGizmos()
    {
        if ((this.transform.parent != GameObject.Find(m_ContainerName).transform) && m_AutoMove)
        {
            MoveToDirectory();
        }
        if (!Application.isPlaying && transform.hasChanged && m_SnappingActive)
        {
            Snap();
        }
    }

    private void MoveToDirectory()
    {
        this.transform.parent = GameObject.Find(m_ContainerName).transform;
        if (m_DebugLogging)
        {
            Debug.Log(this.gameObject + " moved to " + m_ContainerName);
        }
    }



    private void Snap()
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

    public void OnDraggingBegins(object sender, InputHandler.MouseInfo mouseInfo)
    {
        //throw new NotImplementedException();
    }

}