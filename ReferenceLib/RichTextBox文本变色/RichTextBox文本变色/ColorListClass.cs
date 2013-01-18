using System;
using System.Drawing;
using System.Collections;

namespace RichTextBox文本变色
{
	/// <summary>
	/// ColorListClass 的摘要说明。
	/// </summary>
	public class ColorListClass
	{
		public ColorListClass()
		{
			SetColorList();
		}	

		public Hashtable COLORLIST = new Hashtable();

		private void SetColorList()
		{
			DRandomColor myColor = new DRandomColor();
            COLORLIST.Add("顶峰", GetColorCollection(Color.GhostWhite, Color.Gray, myColor.CustomColor(207, 252, 152), myColor.CustomColor(102, 126, 82), Color.LightGray));
            COLORLIST.Add("穿越", GetColorCollection(myColor.CustomColor(81, 92, 106), Color.LightSteelBlue, myColor.CustomColor(133, 118, 129), Color.AliceBlue, myColor.CustomColor(159, 179, 145)));
            COLORLIST.Add("质朴", GetColorCollection(Color.OldLace, myColor.CustomColor(165, 159, 124), Color.Linen, myColor.CustomColor(120, 129, 180), myColor.CustomColor(181, 199, 210)));
            COLORLIST.Add("热情", GetColorCollection(myColor.CustomColor(234, 185, 153), Color.Linen, Color.Tan, myColor.CustomColor(171, 85, 78), myColor.CustomColor(237, 143, 143)));
            COLORLIST.Add("浪漫", GetColorCollection(myColor.CustomColor(233, 166, 238), myColor.CustomColor(252, 182, 209), Color.Thistle, myColor.CustomColor(189, 125, 223), myColor.CustomColor(214, 203, 216)));
            COLORLIST.Add("生态", GetColorCollection(myColor.CustomColor(147, 222, 5), myColor.CustomColor(205, 220, 122), myColor.CustomColor(234, 241, 203), myColor.CustomColor(168, 193, 21), Color.Olive));
            COLORLIST.Add("寒冷", GetColorCollection(myColor.CustomColor(151, 200, 244), myColor.CustomColor(53, 96, 134), myColor.CustomColor(218, 233, 235), myColor.CustomColor(124, 133, 146), myColor.CustomColor(122, 140, 203)));
		}

		private ColorObjCollection GetColorCollection(Color color,Color color1,Color color2,Color color3,Color color4)
		{
			ColorObjCollection hotColor = new ColorObjCollection(color,color1,color2,color3,color4);
			return hotColor;
		}
	}
}
