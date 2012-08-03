using System;
using System.Xml;

namespace SnippyLib
{
	public class Util
	{
		// Methods
		private Util()
		{
 
		}

		public static XmlElement CreateElement(XmlElement parent, string name, string innerText, XmlNamespaceManager nsMgr)
		{
			if (parent == null)
			{
				throw new Exception("Passed in a null node, which should never happen.  Tell Gus!");
			}
			XmlElement newChild = parent.OwnerDocument.CreateElement(name, nsMgr.LookupNamespace("ns1"));
			XmlElement element2 = (XmlElement) parent.AppendChild(newChild);
			element2.InnerText = innerText;
			return (XmlElement) parent.AppendChild(element2);
		}

		public static string GetTextFromElement(XmlElement element)
		{
			if (element == null)
			{
				return string.Empty;
			}
			return element.InnerText;
		}

		public static XmlNode SetTextInElement(XmlElement element, string name, string text, XmlNamespaceManager nsMgr)
		{
			if (element == null)
			{
				throw new Exception("Passed in a null node, which should never happen.");
			}
			XmlElement newChild = (XmlElement) element.SelectSingleNode("descendant::ns1:" + name, nsMgr);
			if (newChild == null)
			{
				newChild = (XmlElement) element.AppendChild(element.OwnerDocument.CreateElement(name, nsMgr.LookupNamespace("ns1")));
			}
			newChild.InnerText = text;
			return element.AppendChild(newChild);
		}
	}

 
 
}
