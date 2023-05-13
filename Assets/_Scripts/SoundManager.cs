using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] generalLibrary;
    public AudioClip[] interactTraderLibraryStart;
    public AudioClip[] interactTraderLibraryFinish;
    public AudioClip[] interactTraderMakeTrade;

    public AudioClip[] interactTraderCanceledTrade;
    AudioSource generalSource;
    AudioSource interactSource;

    // Start is called before the first frame update
    void Start()
    {
        generalSource = transform.GetChild(1).GetComponent<AudioSource>();
        interactSource = transform.GetChild(0).GetComponent<AudioSource>();
        EventManager.instance.startTradeEvent.AddListener(()=> PlaySoundInteractTrader(true));
        EventManager.instance.finishTradeEvent.AddListener(() => PlaySoundInteractTrader(false));
        EventManager.instance.tradeEvent.AddListener(PlaySoundTraderMadeTrade);
        EventManager.instance.tradeCanceledEvent.AddListener(PlaySoundTraderCanceledTrade);

        //generalSource.PlayOneShot(generalLibrary[Random.Range(0,generalLibrary.Length)]);
    }

    public void PlaySoundInteractTrader(bool start_finish) 
    {
        if (start_finish)
        {
            if (interactSource.isPlaying == false) 
            {
                interactSource.PlayOneShot(interactTraderLibraryStart[Random.Range(0, interactTraderLibraryStart.Length)]);
            }
            
        }
        else
        {
            if (interactSource.isPlaying == false)
            {
                interactSource.PlayOneShot(interactTraderLibraryFinish[Random.Range(0, interactTraderLibraryFinish.Length)]);
            }
        }
    }
    public void PlaySoundTraderMadeTrade()
    {
        if (interactSource.isPlaying == false)
        {
            interactSource.PlayOneShot(interactTraderMakeTrade[Random.Range(0, interactTraderMakeTrade.Length)]);
        }
       
    }
    public void PlaySoundTraderCanceledTrade()
    {
        if (interactSource.isPlaying == false)
        {

            interactSource.PlayOneShot(interactTraderCanceledTrade[Random.Range(0, interactTraderCanceledTrade.Length)]);
        }
    }
}
