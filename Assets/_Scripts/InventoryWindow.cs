using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;


public class InventoryWindow : MonoBehaviour
{
    
    [SerializeField] Inventory[] targetsInventory;
    RectTransform itemPanel;
    readonly List<GameObject> drawIcons = new List<GameObject>();

    [SerializeField] GameObject itemPrefab;
    // Start is called before the first frame update
     async void Start()
    {
        itemPanel = GetComponent<RectTransform>();
        EventManager.instance.onItemChanged.AddListener(()=>Redraw_Inventory());
        
    }



    async Task  Redraw_Inventory()
    {
        print("Redraw_Inventory");
        ClearDraw();
        //await Task.Delay(50);
        
        foreach (var inventory in targetsInventory)
        {
            for (int i = 0; i < inventory.inventoryItems.Count; i++)
            {
                var _item = inventory.inventoryItems[i];
                var _icon = Instantiate(itemPrefab, inventory.myInventoryPanel.transform.GetChild(i).GetChild(0));
               
                DragableItem obj = _icon.GetComponent<DragableItem>();
                obj.cost = _item.Price;

                _icon.GetComponent<Image>().sprite = _item.Icon;

                inventory.inventoryItemsObject.Add(obj);
                
                drawIcons.Add(_icon);
                await Task.Yield();
            }

            //await Task.CompletedTask;
            inventory.IndexUpdate();
            
        }
        

    }
        void ClearDraw() 
    {
        for (int i = 0; i < drawIcons.Count; i++)
        {
            Destroy(drawIcons[i]);
        }
        drawIcons.Clear();
    }
}
