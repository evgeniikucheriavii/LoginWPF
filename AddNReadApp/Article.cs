using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp
{
	public class Article
	{
		private int id;

		private string title;
		private string text;
		private int views;

		private User user;

		private DateTime creationDate;

		public Article()
		{
			this.id = 0;
		}

		public Article(int id, string title, string text, int views, DateTime creationDate, User user)
		{
			this.id = id;
			this.title = Cut(title, 50);
			this.text = text.Trim();
			this.views = views;
			this.creationDate = creationDate;
			this.user = user;
		}

		private string Cut(string text, int length)
		{

			text = text.Trim();

			if (text.Length > length)
			{
				text = text.Substring(0, 140);
			}

			return text;
		}

		#region GettersSetters

		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public User User
		{
			get
			{
				return this.user;
			}
		}

		public int Views
		{
			get
			{ 
				return this.views;
			}
		}


		public string Title
		{
			get
			{
				return this.title;
			}
		}

		public string Text
		{
			get
			{
				return this.text;
			}
		}

		public string ShortText
		{
			get
			{
				return Cut(this.text, 140);
			}
		}

		public DateTime CreationDate
		{
			get
			{
				return this.creationDate;
			}
		}

		public string CreationDateString
		{
			get
			{
				string text = $"{this.creationDate.Day}.{this.creationDate.Month}.{this.creationDate.Year}";

				return text != "1.1.1" ? text : "";
			}
		}

		#endregion

	}
}
