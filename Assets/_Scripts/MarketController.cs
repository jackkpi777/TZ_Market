using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

public class MarketController : MonoBehaviour
{
    
    [SerializeField] Inventory playerInventory;
    [SerializeField] Inventory traderInventory;


    [SerializeField] List<GameObject> sellItems = new List<GameObject>();
    [SerializeField] List<GameObject> buyItems = new List<GameObject>();

     public GameObject sellPanel;

     public GameObject buyPanel;


    
    public static MarketController instance;


    DragableItem obj;
    item item;
    int index;
    bool canMakeTrade;


    List<DragableItem> objectsToTradeList = new List<DragableItem>();
    // Start is called before the first frame update

    void Awake() 
    {
        instance = this;
    }

    async void Start() 
    {
        await CalculateTradeItems(sellPanel, sellItems);
        await CalculateTradeItems(buyPanel, buyItems);
        EventManager.instance.onItemChanged.Invoke();

    }

    async Task CalculateTradeItems(GameObject _object,List<GameObject> _list)
    {
        for (int i = 0; i < _object.transform.childCount; i++)
        {
            _list.Add(_object.transform.GetChild(i).transform.GetChild(0).gameObject);
            await Task.Yield();
           
        }
        await Task.CompletedTask;
    }

    void TradeItemInInventory(Inventory _targetInventoryLost, Inventory _targetInventoryAdd, item _item,int index, DragableItem _obj)
    {
        _targetInventoryAdd.AddItem(_item, _obj);
       

        _targetInventoryLost.LostItem(index);

        _targetInventoryAdd.IndexUpdate();

        _targetInventoryLost.IndexUpdate();

        CalculateMoney(_targetInventoryLost, _targetInventoryAdd, _item);
    }


    bool CheckMoney(Inventory _targetInventoryAdd, List<DragableItem> obj) 
    {
        int money = _targetInventoryAdd.myMoney;
        for (int i = 0; i < obj.Count; i++)
        {
            money -= obj[i].cost;
        }
        if (money >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
     
    void CalculateMoney(Inventory _targetInventoryLost, Inventory _targetInventoryAdd, item _item) 
    {
        _targetInventoryLost.myMoney = _targetInventoryLost.myMoney + _item.Price;
        _targetInventoryAdd.myMoney = _targetInventoryAdd.myMoney - _item.Price;
    }

    public void MakeSell() 
    {
        objectsToTradeList.Clear();
        canMakeTrade = false;
        for (int i = 0; i < sellItems.Count; i++)
        {
            if (sellItems[i].GetComponentInChildren<DragableItem>() != null)
            {
                objectsToTradeList.Add(sellItems[i].GetComponentInChildren<DragableItem>());
            }
        }
        canMakeTrade = CheckMoney(traderInventory, objectsToTradeList);
        if (canMakeTrade == true)
        {
            for (int i = 0; i < objectsToTradeList.Count; i++)
            {
                obj = objectsToTradeList[i];
                index = obj.index;
                item = playerInventory.inventoryItems[index];


                TradeItemInInventory(playerInventory, traderInventory, item, index, obj);
                Debug.Log("MakeSell");

                EventManager.instance.tradeEvent.Invoke();
            }
            EventManager.instance.onItemChanged.Invoke();
        }
        else
        {
            EventManager.instance.tradeCanceledEvent.Invoke();
        }
           
    }

    public void MakeBuy()
    {

        objectsToTradeList.Clear();
        canMakeTrade = false;

        for (int i = 0; i < buyItems.Count; i++)
        {
            if (buyItems[i].GetComponentInChildren<DragableItem>() != null)
            {
                objectsToTradeList.Add(buyItems[i].GetComponentInChildren<DragableItem>());

            }
        }
        canMakeTrade = CheckMoney(playerInventory, objectsToTradeList);

        if (canMakeTrade == true)
        {
            for (int i = 0; i < objectsToTradeList.Count; i++)
            {
                obj = objectsToTradeList[i];
                index = obj.index;
                item = traderInventory.inventoryItems[index];


                TradeItemInInventory(traderInventory, playerInventory, item, index, obj);

                Debug.Log("MakeBuy_" + item.Name.ToString());
                EventManager.instance.tradeEvent.Invoke();
            }
            EventManager.instance.onItemChanged.Invoke();
        }
        else
        {
            EventManager.instance.tradeCanceledEvent.Invoke();
        }
    }

}
