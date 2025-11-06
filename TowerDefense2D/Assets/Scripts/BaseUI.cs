using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    public static event Action ListChange;
    [SerializeField] private TMP_Text health;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private TMP_Text waves;
    [SerializeField] private float hp;
    [SerializeField] private TMP_Text closeButtonText;
    [SerializeField] private GameObject speedUpButton;
    [SerializeField] private List<Sprite> pauseSprites;
    private GameObject[] enemyScripts;
    [SerializeField] private Spawner waveSpawner;
    [SerializeField] private GameObject list;
    [SerializeField] private Image pause;
    private bool listOpen = true;
    public bool isPaused = false;
    private bool spedup= false;
    [SerializeField] private List<TowerBuy> buylist;
    private float manaCount;
    [SerializeField] private List<TMP_Text> manaCostText;

    void Start()
    {
        Enemies.OnEndpointReached += Health;
        Spawner.OnWaveIncrease += WavesUI;
        ManaSystem.OnManaChange += Mana;
        health.text = hp.ToString();
    }
    private void Update()
    {
        enemyScripts = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < buylist.Count; i++)
        {
            manaCostText[i].text = buylist[i].manaCost.ToString();
            if (buylist[i].manaCost > manaCount)
            {
                manaCostText[i].color = Color.red;
            }
            else
            {
                manaCostText[i].color = new Color(0.6196079f, 0.5843138f, 0.5333334f);
            }
        }
    }
    private void WavesUI(string wavecount)
    {
        waves.text = "Waves: " + wavecount;
    }
    private void Health(float dmg)
    {
        hp -= dmg;
        health.text = hp.ToString();
        if (hp <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
    private void Mana(float manaCout)
    {
        manaCount = manaCout;
        mana.text = manaCout.ToString();
    }
    public void OnPauseButtonClick()
    {
        if (!isPaused)
        {
            pause.sprite = pauseSprites[1];
            waveSpawner.enabled = false;
            for (int i = 0; i < enemyScripts.Length; i++)
            {
                enemyScripts[i].GetComponent<Enemies>().enabled = false;
            }
            isPaused = true;
        }
        else
        {
            pause.sprite = pauseSprites[0];
            waveSpawner.enabled = true; 
            for (int i = 0;i < enemyScripts.Length; i++)
            {
                enemyScripts[i].GetComponent<Enemies>().enabled = true;
            }
            isPaused = false;
        }
    }
    public void OnSpeedUpButtonClick()
    {
        if (!spedup)
        {
            speedUpButton.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            Time.timeScale = 2f;
            spedup = true;
        }
        else
        {
            speedUpButton.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Time.timeScale = 1f;
            spedup = false;
        }
    }

    public void OnCloseButtonClick()
    {
        if (listOpen)
        {
            closeButtonText.text = "←";
            list.SetActive(false);
            listOpen = false;
        }
        else
        {
            closeButtonText.text = "x";
            list.SetActive(true);
            listOpen = true;
        }
        ListChange?.Invoke();
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
