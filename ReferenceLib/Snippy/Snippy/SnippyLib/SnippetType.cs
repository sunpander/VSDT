using System;
using System.Xml;

namespace SnippyLib
{
	public class SnippetType
	{
		// Fields
		private XmlElement _element;
		private string _value;

		// Methods
		public SnippetType(XmlElement element)
		{
			this._element = element;
			this._value = Util.GetTextFromElement(this._element);
		}

		// Properties
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				this._element.InnerText = this._value;
			}
		}
	}

 
}
