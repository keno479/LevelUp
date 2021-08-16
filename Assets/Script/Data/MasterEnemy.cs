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
}
public class MasterEnemy : CsvData<MasterEnemyParam>
{
    
}
