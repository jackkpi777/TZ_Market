using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [Header("UI_Elements")]
    public GameObject tradeWindow;
    public GameObject inputWindow;

    public TextMeshProUGUI playermoneyCounter;
    public TextMeshProUGUI tradermoneyCounter;

    public TextMeshProUGUI playermoneyCounterTotal;

    [SerializeField] Inventory trader;
    [SerializeField] Inventory player;

    [SerializeField] LeoLuz.PlugAndPlayJoystick.AnalogicKnob knob;
    // Start is called before the first frame update
    void Start()
    {
        tradeWindow.SetActive(false);
        EventManager.instance.startTradeEvent.AddListener(TradeWindowOpen);
        EventManager.instance.finishTradeEvent.AddListener(TradeWindowClose);
        EventManager.instance.tradeEvent.AddListener(UI_Update_Money);
        UI_Update_Money();
    }


    void UI_Update_Money() 
    {
        
        playermoneyCounter.text = (player.myMoney.ToString()+"$");
        tradermoneyCounter.text = (trader.myMoney.ToString() + "$");
        playermoneyCounterTotal.DOCounter(0, player.myMoney,0.3f);
    }

    public void StartTrade() 
    {
        EventManager.instance.startTradeEvent.Invoke();
    }
    public void FinishTrade()
    {
        EventManager.instance.finishTradeEvent.Invoke();
    }

    void TradeWindowOpen()
    {
        player.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = false;
        player.gameObject.GetComponent<Animator>().SetFloat("Forward", 0);
        Input.ResetInputAxes();
        inputWindow.SetActive(false);
        tradeWindow.SetActive(true);
        
    }
    void TradeWindowClose()
    {
        player.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled = true;
        player.gameObject.GetComponent<Animator>().SetFloat("Forward", 0);
        knob.NormalizedAxis = Vector2.zero;
        tradeWindow.SetActive(false);
        inputWindow.SetActive(true);

    }
}
