using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class DataQuestParam : CsvDataParam
{
    public int Mission_ID;
    public int Achievement_Degree;
    public bool Clear;
}

public class DataQuest : CsvData<DataQuestParam>
{

}