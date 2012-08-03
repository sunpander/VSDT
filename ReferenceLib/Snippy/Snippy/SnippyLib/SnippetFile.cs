using System;
using System.Collections.Generic;
using System.Xml;

namespace SnippyLib
{
public class SnippetFile
{
    // Fields
    private XmlDocument _doc;
    private string _fileName;
    private XmlNamespaceManager _nsMgr;
    private List<Snippet> _snippets;

    // Methods
    public SnippetFile()
    {
        this._snippets = new List<Snippet>();
        this.initialize();
    }

    public SnippetFile(string fileName)
    {
        this._snippets = new List<Snippet>();
        this._fileName = fileName;
        this.loadData();
    }

    public int AppendNewSnippet()
    {
        XmlElement newChild = this._doc.CreateElement("CodeSnippet", this._nsMgr.LookupNamespace("ns1"));
        newChild.SetAttribute("Format", "1.0.0");
        newChild.AppendChild(this._doc.CreateElement("Header", this._nsMgr.LookupNamespace("ns1")));
        newChild.AppendChild(this._doc.CreateElement("Snippet", this._nsMgr.LookupNamespace("ns1")));
        XmlNode node = this._doc.SelectSingleNode("//ns1:CodeSnippets", this._nsMgr).AppendChild(newChild);
        this._snippets.Add(new Snippet(node, this._nsMgr));
        return (this._snippets.Count - 1);
    }

    private void initialize()
    {
        this._doc = new XmlDocument();
        this._doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><CodeSnippets xmlns=\"http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet\"><CodeSnippet Format=\"1.0.0\"><Header></Header><Snippet></Snippet></CodeSnippet></CodeSnippets>");
        this._snippets = new List<Snippet>();
        this._nsMgr = new XmlNamespaceManager(this._doc.NameTable);
        this._nsMgr.AddNamespace("ns1", "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet");
        XmlNode node = this._doc.SelectSingleNode("//ns1:CodeSnippets//ns1:CodeSnippet", this._nsMgr);
        this._snippets.Add(new Snippet(node, this._nsMgr));
        this._fileName = "Untitled.snippet";
    }

    private void loadData()
    {
        this._doc = new XmlDocument();
        this._doc.Load(this._fileName);
        this._nsMgr = new XmlNamespaceManager(this._doc.NameTable);
        this._nsMgr.AddNamespace("ns1", "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet");
        if (this._doc.FirstChild.NodeType != XmlNodeType.XmlDeclaration)
        {
            XmlDeclaration newChild = this._doc.CreateXmlDeclaration("1.0", "utf-8", string.Empty);
            this._doc.InsertBefore(newChild, this._doc.DocumentElement);
        }
        this._doc.SelectSingleNode("//ns1:CodeSnippets", this._nsMgr);
        if (this._doc.DocumentElement.Name == "CodeSnippet")
        {
            this._snippets.Add(new Snippet(this._doc.DocumentElement, this._nsMgr));
        }
        else
        {
            foreach (XmlNode node in this._doc.DocumentElement.SelectNodes("//ns1:CodeSnippet", this._nsMgr))
            {
                this._snippets.Add(new Snippet(node, this._nsMgr));
            }
        }
    }

    public void Save()
    {
        this._doc.Save(this._fileName);
    }

    public void SaveAs(string fileName)
    {
        this._doc.Save(fileName);
        this._fileName = fileName;
    }

    // Properties
    public string FileName
    {
        get
        {
            return this._fileName;
        }
    }

    public List<Snippet> Snippets
    {
        get
        {
            return this._snippets;
        }
    }
}

 
}
