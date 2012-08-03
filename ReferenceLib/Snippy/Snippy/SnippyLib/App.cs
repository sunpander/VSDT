using System;

namespace SnippyLib
{
	public class App
	{
		// Fields
		private SnippetFile _currentFile = new SnippetFile();
		private int _currentSnippetIndex;

		// Methods
		public void AppendNewSnippet()
		{
			this._currentSnippetIndex = this._currentFile.AppendNewSnippet();
		}

		public void CreateNewFile()
		{
			this._currentFile = new SnippetFile();
			this._currentSnippetIndex = 0;
		}

		public string[] GetSnippetTitles()
		{
			string[] strArray = new string[this._currentFile.Snippets.Count];
			int index = 0;
			foreach (Snippet snippet in this._currentFile.Snippets)
			{
				strArray[index] = snippet.Title;
				index++;
			}
			return strArray;
		}

		public void LoadFile(string fileName)
		{
			this._currentFile = new SnippetFile(fileName);
		}

		public void Save()
		{
			this._currentFile.Save();
		}

		public void SaveAs(string fileName)
		{
			this._currentFile.SaveAs(fileName);
		}

		public void SetCurrentSnippet(int index)
		{
			if ((index >= this._currentFile.Snippets.Count) || (index < 0))
			{
				throw new Exception("The 'index' argument is outside the valid range");
			}
			this._currentSnippetIndex = index;
		}

		// Properties
		public string CurrentFile
		{
			get
			{
				return this._currentFile.FileName;
			}
		}

		public Snippet CurrentSnippet
		{
			get
			{
				if (this._currentFile.Snippets.Count > 0)
				{
					return this._currentFile.Snippets[this._currentSnippetIndex];
				}
				return null;
			}
		}

		public int CurrentSnippetIndex
		{
			get
			{
				return this._currentSnippetIndex;
			}
		}
	}

 
}
