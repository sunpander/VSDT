using System;
using System.Xml;

namespace SnippyLib
{
	public class Literal
	{
		// Fields
		private string _defaultValue;
		private bool _editable;
		private XmlElement _element;
		private string _function;
		private string _id;
		private bool _object;
		private string _toolTip;

		// Methods
		public Literal(XmlElement element, XmlNamespaceManager nsMgr, bool Object)
		{
			this._element = element;
			this._object = Object;
			this._id = Util.GetTextFromElement((XmlElement) this._element.SelectSingleNode("descendant::ns1:ID", nsMgr));
			this._toolTip = Util.GetTextFromElement((XmlElement) this._element.SelectSingleNode("descendant::ns1:ToolTip", nsMgr));
			this._function = Util.GetTextFromElement((XmlElement) this._element.SelectSingleNode("descendant::ns1:Function", nsMgr));
			this._defaultValue = Util.GetTextFromElement((XmlElement) this._element.SelectSingleNode("descendant::ns1:Default", nsMgr));
			string attribute = this._element.GetAttribute("Editable");
			if (attribute != string.Empty)
			{
				this._editable = bool.Parse(attribute);
			}
			else
			{
				this._editable = true;
			}
		}

		// Properties
		public string DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
			set
			{
				this._defaultValue = value;
				Util.SetTextInElement(this._element, "Default", this._defaultValue, null);
			}
		}

		public bool Editable
		{
			get
			{
				return this._editable;
			}
			set
			{
				this._editable = value;
				this._element.SetAttribute("Editable", this._editable.ToString());
			}
		}

		public string Function
		{
			get
			{
				return this._function;
			}
			set
			{
				this._function = value;
				Util.SetTextInElement(this._element, "Function", this._function, null);
			}
		}

		public string ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
				Util.SetTextInElement(this._element, "ID", this._id, null);
			}
		}

		public bool Object
		{
			get
			{
				return this._object;
			}
			set
			{
				this._object = value;
			}
		}

		public string ToolTip
		{
			get
			{
				return this._toolTip;
			}
			set
			{
				this._toolTip = value;
				Util.SetTextInElement(this._element, "ToolTip", this._toolTip, null);
			}
		}
	}

 
 
}
