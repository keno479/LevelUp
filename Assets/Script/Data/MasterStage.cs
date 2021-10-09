using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class MasterStageParam:CsvDataParam
{
    public int Stage_ID;
    public string Stage_Name;
    public int Boss_Pop_LV;
    public int Boss_Enemy_ID;
    public int Open_Stage_ID;
}

public class MasterStage : CsvData<MasterStageParam>
{

}