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
	public void Add(int _Shield_ID)
	{
		DataShieldParam param = list.Find(p => p.Shield_ID == _Shield_ID);
		if (param != null)
		{
			param.Num += 1;
		}
		else
		{
			param = new DataShieldParam()
			{
				Shield_ID = _Shield_ID,
				Num = 1,
			};
			list.Add(param);
		}
	}
}
