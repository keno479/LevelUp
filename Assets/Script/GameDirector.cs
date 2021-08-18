﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using UnityEngine.UI;
using TMPro;

public class GameDirector : Singleton<GameDirector>
{
    public ParamGauge HP_Gauge;
    public ParamGauge EXP_Gauge;
    public Button EscButton;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI StoneText;
    public TextMeshProUGUI LVText;
    public Image Badge_StatusUp;
    public Image areaDrop;
    public Image GoldIcon;

    public void Init()
    {
        HP_Gauge.Init(DataManager.Instance.UnitPlayer.HP, DataManager.Instance.UnitPlayer.HP_max);
        EXP_Gauge.Init(DataManager.Instance.UnitPlayer.EXP, DataManager.Instance.UnitPlayer.EXP_max);
        ShowGold(DataManager.Instance.GameInfo.GetInt(Define.KeyGold));
        ShowStone(DataManager.Instance.GameInfo.GetInt(Define.KeyStone));
        ShowLV();
    }

    public void Damage(int damage)
    {
        DataManager.Instance.UnitPlayer.HP -= damage;
        if (DataManager.Instance.UnitPlayer.HP < 0)
        {
            DataManager.Instance.UnitPlayer.HP = 0;
        }
        HP_Gauge.Set(DataManager.Instance.UnitPlayer.HP);
        DataManager.Instance.dataunit.Save();
    }

    public void EscOff()
    {
        EscButton.interactable = false;
    }
    public void EscOn()
    {
        EscButton.interactable = true;
    }

    public void Heal()
    {
        DataItemParam param = DataManager.Instance.dataItem.list.Find(p => p.Item_ID == 101);
        if (param.Num > 0)
        {
            DataManager.Instance.UnitPlayer.HP += DataManager.Instance.UnitPlayer.HP_max / 5;
            if (DataManager.Instance.UnitPlayer.HP > DataManager.Instance.UnitPlayer.HP_max)
            {
                DataManager.Instance.UnitPlayer.HP = DataManager.Instance.UnitPlayer.HP_max;
            }
            param.Num -= 1;
        }
        HP_Gauge.Set(DataManager.Instance.UnitPlayer.HP);
        if (ItemWindow.Instance != null)
        {
            ItemWindow.Instance.ShowItem();
        }
        DataManager.Instance.dataItem.Save();
        DataManager.Instance.dataunit.Save();
    }

    public void ShowGold(int Gold)
    {
        GoldText.text = $"金:{Gold}";
    }

    public void AddGold(int Add)
    {
        DataManager.Instance.GameInfo.AddInt(Define.KeyGold, Add);
        ShowGold(DataManager.Instance.GameInfo.GetInt(Define.KeyGold));
        DataManager.Instance.GameInfo.Save();
    }

    public void ShowStone(int Stone)
    {
        StoneText.text = $"石:{Stone}";
    }

    public void ShowLV()
    {
        LVText.text = $"LV:{DataManager.Instance.UnitPlayer.LV}";
        Badge_StatusUp.gameObject.SetActive(DataManager.Instance.UnitPlayer.StatusPoint > 0);
    }

    public void GetEXP(int exp)
    {
        DataManager.Instance.UnitPlayer.EXP += exp;
        while (DataManager.Instance.UnitPlayer.EXP >= DataManager.Instance.UnitPlayer.EXP_max)
        {
            DataManager.Instance.UnitPlayer.LV += 1;
            DataManager.Instance.UnitPlayer.EXP -= DataManager.Instance.UnitPlayer.EXP_max;
            DataManager.Instance.UnitPlayer.EXP_max += 10;
            ShowLV();
            DataManager.Instance.UnitPlayer.StatusPoint += 3;
            DataManager.Instance.UnitPlayer.Attack += 1;
            DataManager.Instance.UnitPlayer.Defense += 1;
            DataManager.Instance.UnitPlayer.HP_max += 10;
        }
        EXP_Gauge.Init(DataManager.Instance.UnitPlayer.EXP,DataManager.Instance.UnitPlayer.EXP_max);
        HP_Gauge.Init(DataManager.Instance.UnitPlayer.HP, DataManager.Instance.UnitPlayer.HP_max);
        DataManager.Instance.dataunit.Save();
    }

    public void DropItem(int _enemy_id)
    {
        MasterEnemyParam masterenemy = DataManager.Instance.masterenemy.list.Find(p => p.Enemy_ID == _enemy_id);
        MasterItemParam masteritem = DataManager.Instance.masteritem.list.Find(p => p.Item_ID == masterenemy.Drop_Item_ID1);
        DataManager.Instance.dataItem.Add(masterenemy.Drop_Item_ID1);
        GameObject drop = Instantiate(PrefabHolder.Instance.ShowDrop) as GameObject;
        drop.transform.SetParent(areaDrop.transform);
        drop.GetComponent<ShowDrop>().ImageDrop.sprite = SpriteManager.Instance.Get(masteritem.Sprite_Name);
        drop.GetComponent<ShowDrop>().TextValue.text = "×1";
    }

    public void DropGold(int _enemy_id)
    {
        MasterEnemyParam masterenemy = DataManager.Instance.masterenemy.list.Find(p => p.Enemy_ID == _enemy_id);
        GameDirector.Instance.AddGold(masterenemy.Base_Gold);
        GameObject drop = Instantiate(PrefabHolder.Instance.ShowDrop) as GameObject;
        drop.transform.SetParent(areaDrop.transform);
        drop.GetComponent<ShowDrop>().ImageDrop.sprite = GoldIcon.sprite;
        drop.GetComponent<ShowDrop>().TextValue.text = $"×{masterenemy.Base_Gold}";
    }
}