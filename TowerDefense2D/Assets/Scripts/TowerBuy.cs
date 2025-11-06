using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBuy : MonoBehaviour
{
    public static event Action<Transform, GameObject> placedTowers;
    public static event Action<float> BuyTowerMana;
    [SerializeField] public float manaCost;
    [SerializeField] private GameObject tower;
    private Vector2 difference = Vector2.zero;
    private Vector2 startPosition;
    private bool onPath = false;
    private SpriteRenderer spriteRenderer;
    private float colliderTimer;
    private bool closed = false;
    private Sprite defaultSprite;
    private float mana;
    
    private void Start()
    {
        startPosition = gameObject.transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        BaseUI.ListChange += OnListChange;
        defaultSprite = spriteRenderer.sprite;
        ManaSystem.OnManaChange += CheckMana;
    }
    private void Update()
    {
        colliderTimer += Time.deltaTime;
    }
    private void OnMouseDown()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseDrag()
    {
        spriteRenderer.sprite = tower.GetComponent<SpriteRenderer>().sprite;
        gameObject.transform.localScale = tower.transform.localScale;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2,2);
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        if (onPath && spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
        if (colliderTimer >= 0.1f)
        {
            onPath = false;
        }
    }
    private void OnMouseUp()
    {
        if (!onPath && manaCost <= mana)
        {
            BuyTowerMana?.Invoke(-manaCost);
            placedTowers?.Invoke(gameObject.transform,tower);
        }
        transform.position = startPosition;
        spriteRenderer.sprite = defaultSprite;
        spriteRenderer.color = Color.white;
        gameObject.transform.localScale = new Vector2(1, 1);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onPath = true;
        colliderTimer = 0f;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        onPath = true;
        colliderTimer = 0f;
    }
    private void OnListChange()
    {
        if (closed)
        {
            gameObject.SetActive(true);
            closed = false;
        }
        else
        {
            gameObject.SetActive(false);
            closed = true;
        }
    }
    private void CheckMana(float manaCount)
    {
        mana = manaCount;
    }
}
