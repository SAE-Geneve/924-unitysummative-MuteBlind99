using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Transform boxPosition;
    Box _box;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _box = GetComponent<Box>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _box.Position = boxPosition.position;
    }

    public Vector3 Position { get; set; }
}
