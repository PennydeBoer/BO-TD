using System;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    public static event Action<float> OnManaChange;
    [SerializeField]private float manaCounter;
    void Start()
    {
        TowerBuy.BuyTowerMana += ManaChange;
        Spawner.ManaPerEnemy += ManaChange;
        Invoke("AfterStart", 0.1f);
    }
    private void AfterStart() 
    {
        OnManaChange?.Invoke(manaCounter);
    }
    private void ManaChange(float mana)
    {
        manaCounter += Mathf.Floor(mana);
        OnManaChange?.Invoke(manaCounter);
    }
}
