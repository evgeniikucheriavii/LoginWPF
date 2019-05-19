using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp
{
	public class MainApp : INotifyPropertyChanged
	{

		private User user;
		private User currUser;
		private List<User> users;

		private Article currArticle;
		private List<Article> articles;

		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyPropertyChanged(string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		public MainApp()
		{
			this.users = new List<User>();
			this.user = new User();
			this.currUser = new User();

			this.articles = new List<Article>();
			this.currArticle = new Article();
		}

		public void LogOut()
		{
			this.User = new User();
		}


		#region GettersSetters
		public User User
		{
			get
			{
				return this.user;
			}

			set
			{
				this.user = value;
				NotifyPropertyChanged();
			}
		}

		public User CurrUser
		{
			get
			{
				return this.currUser;
			}

			set
			{
				this.currUser = value;
				NotifyPropertyChanged();
			}
		}

		public List<User> Users
		{
			get
			{
				return this.users;
			}

			set
			{
				this.users = value;
				NotifyPropertyChanged();
			}
		}

		public Article CurrArticle
		{
			get
			{
				return this.currArticle;
			}

			set
			{
				this.currArticle = value;
				NotifyPropertyChanged();
			}
		}

		public List<Article> Articles
		{
			get
			{
				return this.articles;
			}

			set
			{
				this.articles = value;
				NotifyPropertyChanged();
			}
		}

		#endregion

	}
}
