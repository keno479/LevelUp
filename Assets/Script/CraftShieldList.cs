using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftShieldList : MonoBehaviour
{
    private List<MasterShieldParam> Shieldlist = new List<MasterShieldParam>();
    public Transform areaCraftShield;


    private void OnEnable()
    {
        Shieldlist = DataManager.Instance.mastershield.list;

        for (int i = 0; i < Shieldlist.Count; i++)
        {
            //Debug.Log(GameDirector.Instance.CraftRecipe[i]);
            MasterShieldParam param = Shieldlist[i];

            DataManager.Instance.datashield.CraftRecipe(param.Shield_ID);
            if (!GameDirector.Instance.CraftRecipe[i])
            {
                GameObject CraftShield =
                    Instantiate(PrefabHolder.Instance.CraftShield) as GameObject;
                CraftShield.transform.SetParent(areaCraftShield);
                CraftShield.GetComponent<ShieldRecipe>().CraftRecipe(param);
                GameDirector.Instance.CraftRecipe[i] = true;
            }
        }
    }
}
