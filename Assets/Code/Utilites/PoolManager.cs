using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PoolManager : MonoBehaviourService<PoolManager>
{
    [Serializable]
    public class PoolItem
    {
        public int index;
        public string name;
        public GameObject matherObject;
        public List<GameObject> pool = new List<GameObject>();
        [Header("")]
        [Header("")]
        public List<GameObject> items;
        public int count;
    }

    public List<PoolItem> poolItems;


    protected override void OnCreateService()
    {
        
    }

    protected override void OnDestroyService()
    {

    }

    private void AddPool(List<GameObject> objects, GameObject gameObject, Transform mather)
    {
        GameObject inst = Instantiate(gameObject);
        inst.name = gameObject.name;
        objects.Add(inst);
        inst.transform.parent = mather;
        inst.SetActive(false);
    }

    private void InjectPool(PoolItem item)
    {
        GameObject gameObject = item.pool[0];
        gameObject.SetActive(true);
        gameObject.transform.parent = item.matherObject.transform;
        //gameObject.transform.localPosition = new Vector3(xpos + worldLong, 0, 0);
        item.pool.Remove(gameObject);
    }

}
