using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    static float timeBetweenCoins = 0.5f;
    int amountOfCoins = 1;
    GameObject coin;
    Vector3 coinSpawnPosition;

    void LaunchCoin()
    {
        GameObject tempCoin = Instantiate(coin);
        tempCoin.AddComponent<CoinEffect>();
        tempCoin.transform.position = coinSpawnPosition;
        tempCoin.GetComponent<ParticleSystem>().Play();
        PlayerPrefs.SetInt("Coins", (PlayerPrefs.GetInt("Coins", 0) + 1));
        GetComponent<LEDManager>().UpdateCoinsText();
        amountOfCoins--;
        if (amountOfCoins > 0)
        {
            Invoke("LaunchCoin", timeBetweenCoins);
        }
        else
        {
            Destroy(GetComponent<CoinSpawn>());
        }
    }

    public void StartCoinLaunch(int n, Vector3 pos)
    {
        coin = Camera.main.GetComponent<ObjectManager>().Coin();
        amountOfCoins = n;
        coinSpawnPosition = pos;
        LaunchCoin();
    }
}
