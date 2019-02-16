using Assets.Scripts.GameInformation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Item {

    public string itemName;

    public string Name
    {
        get
        {
            return itemName;
        }
        set
        {
            this.itemName = value;
        }
    }

    public int stack;

    public Texture2D sprite;

    public Item()
    {

    }

    public Item(string Name)
    {
        this.itemName = Name;
        stack = 1;
    }

    public Item(string Name, int StackSize)
    {
        this.itemName = Name;
        stack = StackSize;
    }

	// Use this for initialization
	void Start () {
        //this.sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void addToStack(int amount)
    {
        this.stack += amount;
    }

    public void removeFromStack(int amount)
    {
        this.stack -= amount;
    }


    public virtual Item clone()
    {
        return new Item(this.Name);
    }


    protected virtual void loadSpriteFromDisk()
    {
       
    }
}
