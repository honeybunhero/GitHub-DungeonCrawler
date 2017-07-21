using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    ItemList itemList;
    public int health = 100;
    int itemDropped;
    public GameObject[] enemyList = new GameObject[10];
	// Use this for initialization
	void Start () {
        GameObject itemListDatabase = GameObject.FindGameObjectWithTag("itemList");
        itemList = itemListDatabase.GetComponent<ItemList>();
    }
	
	// Update is called once per frame
	void Update () {
		if(health == 0)
        {
            gameObject.SetActive(false);
            itemDropped = Random.Range(0, itemList.itemArray.Length);
            Instantiate(itemList.itemArray[itemDropped], transform.position, Quaternion.identity);
        }
        Debug.Log(health);
	}
}
