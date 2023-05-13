using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    [Header("purpose")]
    public bool traderTrigger;
    public bool moneyTrigger;
    int moneyToAdd = 1000;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Inventory>() != null)
        {
            if (traderTrigger)
            {
                EventManager.instance.startTradeEvent.Invoke();
            }
            if (moneyTrigger)
            {
                other.GetComponent<Inventory>().GetMoney(moneyToAdd);
                Destroy(gameObject);
            }
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<Inventory>() != null)
    //    {
    //        if (traderTrigger)
    //        {
    //            EventManager.instance.finishTradeEvent.Invoke();
    //        }
           
    //    }
    //}
}
