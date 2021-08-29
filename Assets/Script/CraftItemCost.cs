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

    public void ShowItemCost(MasterShieldParam _param,int _item_id,int _item_cost)
    {
        MasterItemParam itemparam = DataManager.Instance.masteritem.list.Find
            (p => p.Item_ID == _item_id);
        

        ItemCostIcon.sprite = SpriteManager.Instance.Get(itemparam.Sprite_Name);
        TextCost.text = $"×{_item_cost}";
    }
    
    public bool CanCraft(MasterShieldParam _param, int _item_id, int _index)
    {
        bool ret = false;
        

        return ret;
    }
}
