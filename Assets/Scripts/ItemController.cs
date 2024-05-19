using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int ID;
    public int Quantity;
    public TMP_Text qtyText;
    public bool Clicked = false;
    private LevelEditorManager editor;
    private void Start()
    {
        qtyText.text = Quantity.ToString();
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    public void ButtonClicked()
    {
        if (Quantity > 0)
        {
            Clicked = true;
            Quantity--;
            qtyText.text = Quantity.ToString();
            editor.CurrentButtonPressed = ID;
        }
    }
}
