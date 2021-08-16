using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class MasterShieldParam : CsvDataParam
{
    public int Shield_ID;
    public string Shield_Name;
    public int Defense;
    public int Craft_Item_ID1;
    public int Craft_Item_ID2;
    public int Craft_Item_ID3;
}

public class MasterShield : CsvData<MasterShieldParam>
{

}