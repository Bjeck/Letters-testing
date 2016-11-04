using UnityEngine;
using System.Collections.Generic;

public class TextObject : UIOBject {

	public TextContent a,b;
	public UIOBject link;

	public void SetLink(UIOBject l){
		link = l;
		authorMan.orderPanel.DrawLine(this,l);
	}

}
