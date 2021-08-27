using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using anogamelib;

public class CraftItemCost : MonoBehaviour
{
    public Image ItemCostIcon;
    public TextMeshProUGUI TextCost;

    public void ShowItemCost(MasterShieldParam _param,int _item_id,int _index)
    {
        MasterItemParam itemparam = DataManager.Instance.masteritem.list.Find
            (p => p.Item_ID == _item_id);
        int[] Craft_Item_Cost = new int[]
        {
            _param.Craft_Item1_Value,
            _param.Craft_Item2_Value,
            _param.Craft_Item3_Value,
        };

        ItemCostIcon.sprite = SpriteManager.Instance.Get(itemparam.Sprite_Name);
        TextCost.text = $"×{Craft_Item_Cost[_index]}";
    }          
}
