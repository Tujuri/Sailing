using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CoinFlip : MonoBehaviour
{
    public Transform[] coins;

    private LootTable lootTable;
    private Transform coinRenderers;
    private List<Transform> coinFlips = new();

    public void Initialize(LootTable lootTable, Transform coinRenderers)
    {
        this.lootTable = lootTable;
        this.coinRenderers = coinRenderers;

        for (var i = 0; i < 5; i++)
            coinFlips.Add(coinRenderers.GetChild(i).GetChild(0));
    }

    public void FlipCoin(Transform coin)
    {
        var result = UnityEngine.Random.value > 0.5f;
        var coinFlip = coinFlips[Array.IndexOf(coins, coin)];

        const float duration = 0.6f;
        const int flips = 2;

        coinFlip.DOLocalMoveZ(0.8f, duration / 2).SetEase(Ease.OutQuad);
        coinFlip.DOLocalMoveZ(1, duration / 2).SetEase(Ease.InQuad).SetDelay(duration / 2);
        coinFlip.DOLocalRotate(new Vector3(0, 360 * flips, 0), duration, RotateMode.FastBeyond360).
            SetEase(Ease.Linear).OnComplete(() =>
            {
                coinFlip.localRotation = Quaternion.Euler(new Vector3(0, result ? 0 : 180, 0));
                Debug.Log(result ? "Heads" : "Tails");
            });

        Destroy(coin.GetComponent<EventTrigger>());
    }

    private void OnDestroy()
    {
        GameManager.LockPlayer(false);

        if (coinRenderers != null)
            Destroy(coinRenderers.gameObject);
    }
}