using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSerializer : GameObjectSerializer
{
    private static readonly string POINT_PREFAB_PATH = "Prefabs/Point";

    public override void CreateObject(Save save)
    {
        //DeleteAllChildren();
        GameObject pointPrefab = Resources.Load<GameObject>(POINT_PREFAB_PATH);
        GameObject point;
        for (int i = 0; i < save.pointEntities.Count; i++)
        {
            if (FindPointChilds().Length > i)
            {
                FindPointChilds()[i].transform.position = save.pointEntities[i].position;
            }
            else
            {
                point = Instantiate(pointPrefab);
                point.transform.parent = transform;
                point.transform.position = save.pointEntities[i].position;
            }
            
        }

    }

    public override Save Serialize(Save save)
    {
        if (save.pointEntities == null)
        {
            save.pointEntities = new List<PointEntity>();
        }
        save.pointEntities.AddRange(CreatePointEntities());
        return save;
    }

    private PointEntity[] CreatePointEntities()
    {
        GameObject[] points = FindPointChilds();
        List<PointEntity> pointEntities = new List<PointEntity>();
        foreach (GameObject point in points)
        {
            pointEntities.Add(new PointEntity() { position = point.transform.position });
        }
        return pointEntities.ToArray();
    }

    private GameObject[] FindPointChilds()
    {
        List<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Point"))
            {
                childs.Add(transform.GetChild(i).gameObject);
            }
        }
        return childs.ToArray();
    }

    private void DeleteAllChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Point"))
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
