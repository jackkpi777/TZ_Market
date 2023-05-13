using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderAnimController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        EventManager.instance.startTradeEvent.AddListener(TraderReact);
        EventManager.instance.finishTradeEvent.AddListener(TraderReact);
    }

    void TraderReact() 
    {
        anim.SetTrigger("welcome");
    }
}
