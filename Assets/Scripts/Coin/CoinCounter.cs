using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText; 

    private int coinCount;
    [SerializeField] private AudioClip coinCollectSFX;

    public void AddCoin()
    {
        coinCount += 1;
        SoundFXManager.Instance.PlaySoundFXClip(coinCollectSFX, transform, 1f, "SFX");
        if (coinCount > 99)
        {
            coinCount -= 100;
        }
        coinText.text = coinCount.ToString("00");
    }
}
