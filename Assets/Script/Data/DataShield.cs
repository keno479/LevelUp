using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;

public class DataShieldParam : CsvDataParam 
{
    public int Shield_ID;
	public int Num;
}

public class DataShild : CsvData<DataShieldParam>
{
	public void Add(int _iWeaponId)
	{
		DataShieldParam param = list.Find(p => p.Shield_ID == _iWeaponId);
		if (param != null)
		{
			param.Num += 1;
		}
		else
		{
			param = new DataShieldParam()
			{
				Shield_ID = _iWeaponId,
				Num = 1,
			};
			list.Add(param);
		}
	}
}
