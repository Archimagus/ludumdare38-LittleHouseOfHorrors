using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is used to persist data about selected player card from main menu into the
 * gameplay scene.
 */ 
public class SelectedPlayerCard : MonoBehaviour
{
    public string PlayerName;
    public int Health;
    public int Sanity;
    public int Movement;
    public int Strength;
    public int Intelligence;
    public int Essence;
    public Sprite CardSprite;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void DestroyThisObject()
    {
        DestroyImmediate(this.gameObject);
    }
}
