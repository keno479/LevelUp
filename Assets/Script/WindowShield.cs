using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using anogamelib;

public class WindowShield : MonoBehaviour
{
    public GameObject areaR;
    public GameObject areaL;
    public Image ImageShieldR;
    public Image ImageShieldL;
    public Image MainShieldImage;
    public Button ButtonEquip;
    public TextMeshProUGUI Shield_Name;
    public TextMeshProUGUI Defense;
    //private List<MasterShieldParam> ShieldList = new List<MasterShieldParam>();
    private int Shield_ID;
    //private int num ;

    private void OnEnable()
    {
        //Shield_ID = DataManager.Instance.GameInfo.GetInt(Define.KeyEquipShieldID);
        Shield_ID = 1;
        ShowShield(Shield_ID);
    }

    public void MoveR()
    {
        Shield_ID += 1;
        ShowShield(Shield_ID);
    }

    public void MoveL()
    {
        Shield_ID -= 1;
        ShowShield(Shield_ID);
    }

    public void ShowShield(int current_id)
    {
        MasterShieldParam mastershield = DataManager.Instance.mastershield.list.Find(p => p.Shield_ID == current_id);
        DataShieldParam datashield = DataManager.Instance.datashield.list.Find(p => p.Shield_ID == mastershield.Shield_ID);
        //ShieldList = DataManager.Instance.mastershield.list;
        //ShieldList[num] = ShieldList.Find(p => p.Shield_ID == Shield_ID);

        if (datashield != null)
        {
            MainShieldImage.sprite = SpriteManager.Instance.Get(mastershield.Sprite_Name);
            Defense.text = $"防御力:{mastershield.Defense}";
            Shield_Name.text = $"{mastershield.Shield_Name}";
            Debug.Log(mastershield.Sprite_Name);
        }
        else
        {
            MainShieldImage.sprite = SpriteManager.Instance.Get("Bonus_50");
            Defense.text = "防御力:??";
            Shield_Name.text = "???";
            Debug.Log(datashield);
        }
        ShowSideShield(areaR, ImageShieldR, Shield_ID + 1);
        ShowSideShield(areaL, ImageShieldL, Shield_ID - 1);
    }

    public void ShowSideShield(GameObject _area,Image _sideshield,int _shield_id)
    {
        MasterShieldParam mastershield = DataManager.Instance.mastershield.list.Find(p => p.Shield_ID == _shield_id);
        DataShieldParam datashield = DataManager.Instance.datashield.list.Find(p => p.Shield_ID == mastershield.Shield_ID);
        if (datashield != null)
        {
            _sideshield.sprite = SpriteManager.Instance.Get(mastershield.Sprite_Name);
        }
        else
        {
            _sideshield.sprite = SpriteManager.Instance.Get("Bonus_50");
        }
        _area.SetActive(mastershield != null);
    }
}
