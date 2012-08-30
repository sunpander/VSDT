using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace demo
{
	public partial class Form1 : Form
	{
		public Form1()
		{
             
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			splitButton1.AddDropDownItemAndHandle("Test 1", Test1Handler);
			splitButton1.AddDropDownItemAndHandle("Testing Testing", Testing2Handler);
			splitButton1.AddDropDownItemAndHandle("Testing testing testing", Testing3Handle);

			AlwaysDropDown.DataBindings.Add("Checked", splitButton1, "AlwaysDropDown");
			AlwaysHoverChange.DataBindings.Add("Checked", splitButton1, "AlwaysHoverChange");
			PersistDropDownName.DataBindings.Add("Checked", splitButton1, "PersistDropDownName");
		}

		//
		// Handle drop down menu items.  These items are programmed without 
		// intellisense support.  You need to know that the signature of these
		// menu items is void <MethodCall>(object sender, EventArgs e)
		//

		private void Test1Handler(object sender, EventArgs e)
		{
			textBox1.Text += "Test 1 was fired" + Environment.NewLine;
		}

		private void Testing2Handler(object sender, EventArgs e)
		{
			textBox1.Text += "Testing Testing was fired" + Environment.NewLine;
		}

		private void Testing3Handle(object sender, EventArgs e)
		{
			textBox1.Text += "Testing testing testing was fired" + Environment.NewLine;
		}

		//
		// Handle other button events
		//

		private void splitButton1_Click(object sender, EventArgs e)
		{
			textBox1.Text += "SplitButton1 was clicked" + Environment.NewLine;
		}

		private void splitButton1_ButtonClick(object sender, EventArgs e)
		{
			textBox1.Text += "SplitButton1 ButtonClick was fired" + Environment.NewLine;
		}

		private void splitButton1_MouseDown(object sender, MouseEventArgs e)
		{
			textBox1.Text += "SplitButton1 MouseDown was fired" + Environment.NewLine;
		}

		private void splitButton1_MouseEnter(object sender, EventArgs e)
		{
			textBox1.Text = "SplitButton1 MouseEnter was fired" + Environment.NewLine;
		}

		private void splitButton1_MouseHover(object sender, EventArgs e)
		{
			textBox1.Text += "SplitButton1 MouseHover was fired" + Environment.NewLine;
		}

		private void splitButton1_MouseLeave(object sender, EventArgs e)
		{
			textBox1.Text += "SplitButton1 MouseLeave was fired" + Environment.NewLine;
		}

		private void splitButton1_MouseUp(object sender, MouseEventArgs e)
		{
			textBox1.Text += "SplitButton1 MouseUp was fired" + Environment.NewLine;
		}

		private void AlwaysDropDown_CheckedChanged(object sender, EventArgs e)
		{
			splitButton1.AlwaysDropDown = AlwaysDropDown.Checked;
		}

		private void AlwaysHoverChange_CheckedChanged(object sender, EventArgs e)
		{
			splitButton1.AlwaysHoverChange = AlwaysHoverChange.Checked;
		}

		private void PersistDropDownName_CheckedChanged(object sender, EventArgs e)
		{
			splitButton1.PersistDropDownName = PersistDropDownName.Checked;
		}
	}
}