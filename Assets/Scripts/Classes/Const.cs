using System;
using System.Collections;
using System.Collections.Generic;

public static class Const
{
	public enum KosherStatus { 
		Kosher,
		Nonkosher,
		Cannot_benefit
	};

	public static Dictionary<string, KosherStatus> SelectedAnswerKosherStatus = new Dictionary<string, KosherStatus>
	{ 
		{"jew", KosherStatus.Kosher},
		{"dog", KosherStatus.Nonkosher},
		{"trashcan", KosherStatus.Cannot_benefit}
	};
}


