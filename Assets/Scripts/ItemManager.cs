using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<Box> items = new List<Box>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddChildrenItems();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(items.Count);
        foreach (var itemRanck in items.ToList().Where(item => item.IsPickedUp))
        {
            Destroy(itemRanck.gameObject);
            items.Remove(itemRanck);
        }
    }

    private void AddChildrenItems()
    {
        items.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            Box item = child.GetComponent<Box>();
            if (item != null)
            {
                items.Add(item);   
            }
        }
    }
}