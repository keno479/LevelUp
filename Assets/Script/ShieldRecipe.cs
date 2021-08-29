﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using anogamelib;

public class ShieldRecipe : MonoBehaviour
{
    public TextMeshProUGUI Shield_Name;
    public TextMeshProUGUI TextCostGold;
    public Image ShieldIcon;
    public Transform areaItemCost;
    public GameObject Complete;
    public Button BtnCraft;

    public void CraftRecipe(MasterShieldParam _param)
    {
        DataShieldParam datashield = DataManager.Instance.datashield.list.Find
            (p => p.Shield_ID == _param.Shield_ID);
        ShieldIcon.sprite = SpriteManager.Instance.Get(_param.Sprite_Name);
        Shield_Name.text = $"{_param.Shield_Name}";
        TextCostGold.text = $"{_param.Craft_Gold_Cost}";
        
        int[] Craft_Item_IDs = new int[]
        {
            _param.Craft_Item_ID1,
            _param.Craft_Item_ID2,
            _param.Craft_Item_ID3,
        };

        for (int i = 0; i < Craft_Item_IDs.Length; i++)
        {
            //if (Craft_Item_IDs != null)
            {
                GameObject CraftItem =
                    Instantiate(PrefabHolder.Instance.CraftItem) as GameObject;
                CraftItem.transform.SetParent(areaItemCost);
                CraftItem.GetComponent<CraftItemCost>().ShowItemCost(_param, Craft_Item_IDs[i], i);
            }
        }

        if (datashield.Num > 0)
        {
            Complete.SetActive(true);
            BtnCraft.interactable = false;         
        }
    }
}