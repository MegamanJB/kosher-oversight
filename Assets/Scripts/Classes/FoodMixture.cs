using System;
using System.Collections;
using System.Collections.Generic;

public class FoodMixture
{
	public List<string> Foods;
	public Const.KosherStatus kosherStatus; 

	public FoodMixture (string[] foods, Const.KosherStatus ks)
	{
		Foods = new List<string>(foods);
		kosherStatus = ks;
	}
		
}


