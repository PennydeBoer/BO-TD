using System.Collections.Generic;
using UnityEngine;

public class TowerSwitchAttack : MonoBehaviour
{
    [SerializeField] protected List<GameObject> projectiles;
    private int projectilenumber;
    private bool Clicked;
    private SpriteRenderer Sr;

    protected GameObject ProjectileSelect()
    {
        if (Clicked)
        {
            switch (Input.inputString)
            {
                case "1":
                    projectilenumber = 0;
                    break;
                case "2":
                    projectilenumber = 1;
                    break;
                case "3":
                    projectilenumber = 2;
                    break;

            }
        }
        return projectiles[projectilenumber];
    }
    protected int getProjectileNumber()
    {
        return projectilenumber;
    }
    private void OnMouseEnter()
    {
        Sr = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        Sr = GetComponent<SpriteRenderer>(); 
        Sr.color = Color.magenta;
        Clicked = true;
    }
    private void OnMouseExit()
    {
        Sr.color = Color.white;
        Clicked = false;
    }

}
