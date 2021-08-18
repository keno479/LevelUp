using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class MasterEnemyParam : CsvDataParam
{
    public int Enemy_ID;
    public string Enemy_Name;
    public int Attack;
    public int HP;
    public int Base_EXP;
    public int Base_Gold;
    public int Drop_Item_ID1;
    public int Drop_Item_ID2;
    public int Drop_Item_ID3;
}
public class MasterEnemy : CsvData<MasterEnemyParam>
{
    
}
