using System;
using System.Drawing;
using System.Collections;

namespace RichTextBox文本变色
{
	/// <summary>
	/// ColorObjCollection 的摘要说明。
	/// </summary>
	public class ColorObjCollection
	{
		public ColorObjCollection()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public ColorObjCollection(Color color,Color color1,Color color2,Color color3,Color color4)
		{
			COLORLIST = new Color[]{color,color1,color2,color3,color4};
		}
		public Color[] COLORLIST;
	}
}
