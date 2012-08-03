using System;
using System.Xml;
using System.Collections.Generic;

namespace SnippyLib
{
public class Snippet
{
    // Fields
    private string _author;
    private string _code;
    private string _codeKindAttribute;
    private string _codeLanguageAttribute;
    private XmlNode _codeSnippetNode;
    private string _description;
    private List<string> _imports = new List<string>();
    private List<Literal> _literals = new List<Literal>();
    private XmlNamespaceManager _nsMgr;
    private string _shortcut;
    private List<SnippetType> _snippetTypes = new List<SnippetType>();
    private string _title;

    // Methods
    public Snippet(XmlNode node, XmlNamespaceManager nsMgr)
    {
        this._nsMgr = nsMgr;
        this._codeSnippetNode = node;
        this.loadData(this._codeSnippetNode);
    }

    public void AddImport(string importString)
    {
        XmlDocument ownerDocument = this._codeSnippetNode.OwnerDocument;
        XmlElement newChild = ownerDocument.CreateElement("Import", this._nsMgr.LookupNamespace("ns1"));
        newChild.InnerText = importString;
        XmlNode node = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Snippet//ns1:Imports", this._nsMgr);
        if (node == null)
        {
            XmlNode node2 = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Snippet", this._nsMgr);
            node = ownerDocument.CreateElement("Imports", this._nsMgr.LookupNamespace("ns1"));
            node = node2.PrependChild(node);
        }
        node.AppendChild(newChild);
        this._imports.Add(importString);
    }

    public void AddLiteral(string id, string toolTip, string defaultVal, string function, bool editable, bool isObject)
    {
        XmlDocument ownerDocument = this._codeSnippetNode.OwnerDocument;
        XmlElement newChild = null;
        if (!isObject)
        {
            newChild = ownerDocument.CreateElement("Literal", this._nsMgr.LookupNamespace("ns1"));
        }
        else
        {
            newChild = ownerDocument.CreateElement("Object", this._nsMgr.LookupNamespace("ns1"));
        }
        newChild.SetAttribute("Editable", editable.ToString().ToLower());
        XmlElement element2 = ownerDocument.CreateElement("ID", this._nsMgr.LookupNamespace("ns1"));
        element2.InnerText = id;
        XmlElement element3 = ownerDocument.CreateElement("ToolTip", this._nsMgr.LookupNamespace("ns1"));
        element3.InnerText = toolTip;
        XmlElement element4 = ownerDocument.CreateElement("Default", this._nsMgr.LookupNamespace("ns1"));
        element4.InnerText = defaultVal;
        XmlElement element5 = ownerDocument.CreateElement("Function", this._nsMgr.LookupNamespace("ns1"));
        element5.InnerText = function;
        XmlNode node = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Snippet//ns1:Declarations", this._nsMgr);
        if (node == null)
        {
            XmlNode node2 = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Snippet", this._nsMgr);
            node = ownerDocument.CreateElement("Declarations", this._nsMgr.LookupNamespace("ns1"));
            XmlNode refChild = node2.SelectSingleNode("descendant::ns1:Code", this._nsMgr);
            if (refChild != null)
            {
                node = node2.InsertBefore(node, refChild);
            }
            else
            {
                node = node2.AppendChild(node);
            }
        }
        XmlElement element6 = (XmlElement) node.AppendChild(newChild);
        element6.AppendChild(element2);
        element6.AppendChild(element3);
        element6.AppendChild(element4);
        element6.AppendChild(element5);
        node.AppendChild(element6);
        this._literals.Add(new Literal(element6, this._nsMgr, isObject));
    }

    public void AddSnippetType(string snippetTypeString)
    {
        XmlElement parent = (XmlElement) this._codeSnippetNode.SelectSingleNode("descendant::ns1:Header//ns1:SnippetTypes", this._nsMgr);
        if (parent == null)
        {
            parent = Util.CreateElement((XmlElement) this._codeSnippetNode.SelectSingleNode("descendant::ns1:Header", this._nsMgr), "SnippetTypes", string.Empty, this._nsMgr);
        }
        XmlElement element = Util.CreateElement(parent, "SnippetType", snippetTypeString, this._nsMgr);
        this._snippetTypes.Add(new SnippetType(element));
    }

    public void ClearImports()
    {
        XmlNode node = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Snippet//ns1:Imports", this._nsMgr);
        if (node != null)
        {
            node.RemoveAll();
        }
        this._imports.Clear();
    }

    public void ClearLiterals()
    {
        XmlNode node = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Snippet//ns1:Declarations", this._nsMgr);
        if (node != null)
        {
            node.RemoveAll();
        }
        this._literals.Clear();
    }

    public void ClearSnippetTypes()
    {
        XmlNode node = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Header//ns1:SnippetTypes", this._nsMgr);
        if (node != null)
        {
            node.RemoveAll();
        }
        this._snippetTypes.Clear();
    }

    private void extractDeclarations(XmlNode node)
    {
        if (node != null)
        {
            XmlNodeList list = node.SelectNodes("descendant::ns1:Literal", this._nsMgr);
            if (list != null)
            {
                foreach (XmlElement element in list)
                {
                    this._literals.Add(new Literal(element, this._nsMgr, false));
                }
            }
            XmlNodeList list2 = node.SelectNodes("descendant::ns1:Object", this._nsMgr);
            if (list2 != null)
            {
                foreach (XmlElement element2 in list2)
                {
                    this._literals.Add(new Literal(element2, this._nsMgr, true));
                }
            }
        }
    }

    private void extractHeader(XmlNode node)
    {
        if (node == null)
        {
            this._title = string.Empty;
            this._shortcut = string.Empty;
            this._description = string.Empty;
            this._author = string.Empty;
        }
        else
        {
            this._title = Util.GetTextFromElement((XmlElement) node.SelectSingleNode("descendant::ns1:Title", this._nsMgr));
            this._shortcut = Util.GetTextFromElement((XmlElement) node.SelectSingleNode("descendant::ns1:Shortcut", this._nsMgr));
            this._description = Util.GetTextFromElement((XmlElement) node.SelectSingleNode("descendant::ns1:Description", this._nsMgr));
            this._author = Util.GetTextFromElement((XmlElement) node.SelectSingleNode("descendant::ns1:Author", this._nsMgr));
            this.extractSnippetTypes(node.SelectSingleNode("descendant::ns1:SnippetTypes", this._nsMgr));
        }
    }

    private void extractImports(XmlNode node)
    {
        if (node != null)
        {
            XmlNodeList list = node.SelectNodes("descendant::ns1:Import", this._nsMgr);
            if (list != null)
            {
                foreach (XmlElement element in list)
                {
                    this._imports.Add(element.InnerText);
                }
            }
        }
    }

    private void extractSnippet(XmlNode node)
    {
        if (node == null)
        {
            this._code = string.Empty;
        }
        else
        {
            XmlNode node2 = node.SelectSingleNode("descendant::ns1:Code", this._nsMgr);
            this._code = Util.GetTextFromElement((XmlElement) node2);
            if ((node2 != null) && (node2.Attributes.Count > 0))
            {
                if (node2.Attributes["Language"] != null)
                {
                    this.CodeLanguageAttribute = node2.Attributes["Language"].Value.ToString();
                }
                if (node2.Attributes["Kind"] != null)
                {
                    this.CodeKindAttribute = node2.Attributes["Kind"].Value.ToString();
                }
            }
            this.extractDeclarations(node.SelectSingleNode("descendant::ns1:Declarations", this._nsMgr));
            this.extractImports(node.SelectSingleNode("descendant::ns1:Imports", this._nsMgr));
        }
    }

    private void extractSnippetTypes(XmlNode node)
    {
        if (node != null)
        {
            foreach (XmlElement element in node.SelectNodes("descendant::ns1:SnippetType", this._nsMgr))
            {
                this._snippetTypes.Add(new SnippetType(element));
            }
        }
    }

    private void loadData(XmlNode node)
    {
        this.extractHeader(node.SelectSingleNode("descendant::ns1:Header", this._nsMgr));
        this.extractSnippet(node.SelectSingleNode("descendant::ns1:Snippet", this._nsMgr));
    }

    // Properties
    public string Author
    {
        get
        {
            return this._author;
        }
        set
        {
            this._author = value;
            Util.SetTextInElement((XmlElement) this._codeSnippetNode.SelectSingleNode("descendant::ns1:Header", this._nsMgr), "Author", this._author, this._nsMgr);
        }
    }

    public string Code
    {
        get
        {
            return this._code;
        }
        set
        {
            this._code = value;
            XmlNode node = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Snippet//ns1:Code", this._nsMgr);
            if (node == null)
            {
                node = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Snippet", this._nsMgr).AppendChild(this._codeSnippetNode.OwnerDocument.CreateElement("Code", this._nsMgr.LookupNamespace("ns1")));
            }
            XmlCDataSection newChild = node.OwnerDocument.CreateCDataSection(this._code);
            if (node.ChildNodes.Count > 0)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    node.RemoveChild(node.ChildNodes[i]);
                }
            }
            node.AppendChild(newChild);
        }
    }

    public string CodeKindAttribute
    {
        get
        {
            return this._codeKindAttribute;
        }
        set
        {
            this._codeKindAttribute = value;
            XmlNode node = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Snippet//ns1:Code", this._nsMgr);
            if (node != null)
            {
                if (value != null)
                {
                    XmlNode node2 = this._codeSnippetNode.OwnerDocument.CreateAttribute("Kind");
                    node2.Value = this._codeKindAttribute;
                    node.Attributes.SetNamedItem(node2);
                }
                else
                {
                    XmlNode node3 = this._codeSnippetNode.OwnerDocument.CreateAttribute("Kind");
                    node3.Value = this._codeKindAttribute;
                    node.Attributes.SetNamedItem(node3);
                    if ((node.Attributes.Count > 0) && (node.Attributes["Kind"] != null))
                    {
                        node.Attributes.Remove(node.Attributes["Kind"]);
                    }
                }
            }
        }
    }

    public string CodeLanguageAttribute
    {
        get
        {
            return this._codeLanguageAttribute;
        }
        set
        {
            this._codeLanguageAttribute = value;
            XmlNode node = this._codeSnippetNode.SelectSingleNode("descendant::ns1:Snippet//ns1:Code", this._nsMgr);
            if (node != null)
            {
                XmlNode node2 = this._codeSnippetNode.OwnerDocument.CreateAttribute("Language");
                node2.Value = this._codeLanguageAttribute;
                node.Attributes.SetNamedItem(node2);
            }
        }
    }

    public string Description
    {
        get
        {
            return this._description;
        }
        set
        {
            this._description = value;
            Util.SetTextInElement((XmlElement) this._codeSnippetNode.SelectSingleNode("descendant::ns1:Header", this._nsMgr), "Description", this._description, this._nsMgr);
        }
    }

    public List<string> Imports
    {
        get
        {
            return this._imports;
        }
    }

    public List<Literal> Literals
    {
        get
        {
            return this._literals;
        }
    }

    public string Shortcut
    {
        get
        {
            return this._shortcut;
        }
        set
        {
            this._shortcut = value;
            Util.SetTextInElement((XmlElement) this._codeSnippetNode.SelectSingleNode("descendant::ns1:Header", this._nsMgr), "Shortcut", this._shortcut, this._nsMgr);
        }
    }

    public List<SnippetType> SnippetTypes
    {
        get
        {
            return this._snippetTypes;
        }
    }

    public string Title
    {
        get
        {
            return this._title;
        }
        set
        {
            this._title = value;
            Util.SetTextInElement((XmlElement) this._codeSnippetNode.SelectSingleNode("descendant::ns1:Header", this._nsMgr), "Title", this._title, this._nsMgr);
        }
    }
}

 
 
}
