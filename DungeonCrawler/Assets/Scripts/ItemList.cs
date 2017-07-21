using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour {
    public GameObject healthPotion, manaPotion, chestArmor, legArmor, Gloves, Boots, Helm, one_H_Sword, kiteShield;
    public GameObject [] itemArray = new GameObject [9];
	// Use this for initialization
	void Start () {
        itemArray[0] = healthPotion;
        itemArray[1] = manaPotion;
        itemArray[2] = chestArmor;
        itemArray[3] = legArmor;
        itemArray[4] = Gloves;
        itemArray[5] = Boots;
        itemArray[6] = Helm;
        itemArray[7] = one_H_Sword;
        itemArray[8] = kiteShield;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
