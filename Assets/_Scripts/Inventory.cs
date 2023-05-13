using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class Inventory : MonoBehaviour
{

    public string myOwner;
    
    public List<item> inventoryItems = new List<item>();

    public List<DragableItem> inventoryItemsObject = new List<DragableItem>();

    public GameObject myInventoryPanel;

    public int myMoney;
    // Update is called once per frame
    public void AddItem(item _item,DragableItem _obj)
    {
        inventoryItems.Add(_item);
        inventoryItemsObject.Add(_obj);

       // IndexUpdate();
    }
    public void LostItem(int _index)
    {
        inventoryItemsObject.RemoveAt(_index);
        inventoryItems.RemoveAt(_index);
        //Dynamicly!!!
       // IndexUpdate();
    }
    public void IndexUpdate()
    {
        for (int i = 0; i < inventoryItemsObject.Count; i++)
        {
            inventoryItemsObject.RemoveAll(x => x == null);
            inventoryItemsObject[i].index = i;
            inventoryItemsObject[i].Owner = myOwner;
            
        }
        Debug.Log("IndexUpdate in " + myOwner+ " is Completed");
    }

    public void GetMoney(int points) 
    {
        myMoney += points;
        EventManager.instance.tradeEvent.Invoke();
    }

}
