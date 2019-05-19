using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp
{
	public class User
	{
		private int id;

		private string name;

		private string login;
		private string password;

		private DateTime birthDate;
		private DateTime registrationDate;

		public User()
		{
			this.id = 0;
		}

		public User(int id, string name, string login, string password, DateTime birthDate, DateTime registrationDate)
		{
			this.id = id;
			this.name = name.Trim();
			this.login = login.Trim();
			this.password = password.Trim();
			this.birthDate = birthDate;
			this.registrationDate = registrationDate;
		}

		#region GettersSetters

		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public string Login
		{
			get
			{
				return this.login;
			}
		}

		public string Password
		{
			get
			{
				return this.password;
			}
		}

		public DateTime BirthDate
		{
			get
			{
				return this.birthDate;
			}
		}

		public DateTime RegistrationDate
		{
			get
			{
				return this.registrationDate;
			}
		}

		public string BirthDateText
		{
			get
			{
				return $"{this.birthDate.Day}.{this.birthDate.Month}.{this.birthDate.Year}";
			}
		}

		public string RegistrationDateText
		{
			get
			{
				return $"{this.registrationDate.Day}.{this.registrationDate.Month}.{this.registrationDate.Year}";
			}
		}

		#endregion


	}
}
