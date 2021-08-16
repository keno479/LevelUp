using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class DataEnemyParam : CsvDataParam
{
    public int Enemy_ID;
    public int Enemy_LV;
}

public class DataEnemy : CsvData<DataEnemyParam>
{

}