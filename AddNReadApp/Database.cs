using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddNReadApp
{
	public static class Database
	{
		private static string cs = @"Data Source=.\SQLEXPRESS;Initial Catalog=AddNRead;Integrated Security=True;";


		private static string Cut(string text, int length)
		{
			text = text.Trim();

			if (text.Length > length)
			{
				text = text.Substring(0, length);
			}

			return text;
		}

		#region Login and Registration

		public static User Login(string login, string password) //
		{
			User user = new User();


			login = Cut(login, 50);
			password = Cut(password, 50);

			using (SqlConnection connection = new SqlConnection(cs))
			{
				try
				{
					connection.Open();

					SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE login = @login AND password = @password", connection);

					command.Parameters.Add(new SqlParameter("@login", login));
					command.Parameters.Add(new SqlParameter("@password", password));

					try
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								if (reader.Read())
								{
									user = new User
										(
											reader.GetInt32(0),
											reader.GetString(1),
											reader.GetString(2),
											reader.GetString(3),
											reader.GetDateTime(4),
											reader.GetDateTime(5)
										);
								}
							}
						}
					}
					catch (Exception ex)
					{

					}

				}
				catch (Exception exc)
				{

				}

			}


			return user;
		}


		public static bool Add(User user)
		{
			bool success = false;

			using (SqlConnection connection = new SqlConnection(cs))
			{
				try
				{
					connection.Open();

					SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM Users WHERE login = @login", connection);

					check.Parameters.Add(new SqlParameter("@login", user.Login));

					int count = (int)check.ExecuteScalar();

					if (count == 0)
					{
						SqlCommand command = new SqlCommand("INSERT INTO Users (name, login, password, birth_date, registration_date) VALUES (@name, @login, @password, @birth_date, @registration_date)", connection);

						command.Parameters.Add(new SqlParameter("@name", user.Name));
						command.Parameters.Add(new SqlParameter("@login", user.Login));
						command.Parameters.Add(new SqlParameter("@password", user.Password));
						command.Parameters.Add(new SqlParameter("@birth_date", user.BirthDate));
						command.Parameters.Add(new SqlParameter("@registration_date", user.RegistrationDate));

						int result = command.ExecuteNonQuery();

						if (result == 1)
						{
							success = true;
						}
					}
				}
				catch (Exception exc)
				{

				}
			}

			return success;
		}

		#endregion

		#region Users

		public static List<User> GetUsers()
		{
			List<User> users = new List<User>();

			using (SqlConnection connection = new SqlConnection(cs))
			{
				try
				{
					connection.Open();

					SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								users.Add(new User
									(
										reader.GetInt32(0),
										reader.GetString(1),
										reader.GetString(2),
										reader.GetString(3),
										reader.GetDateTime(4),
										reader.GetDateTime(5)
									));
							}
						}
					}
				}
				catch (Exception exc)
				{

				}
			}

			return users;
		}

		#endregion


		#region Articles

		public static List<Article> GetArticles() //Получить статьи
		{
			List<Article> articles = new List<Article>();
			List<User> users = GetUsers();

			using (SqlConnection connection = new SqlConnection(cs))
			{
				try
				{
					connection.Open();

					

					SqlCommand command = new SqlCommand("SELECT * FROM Articles", connection);

					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								User author = new User();
								foreach (User user in users)
								{
									if (user.ID == reader.GetInt32(5))
									{
										author = user;
										break;
									}
								}

								articles.Add(new Article
									(
										reader.GetInt32(0),
										reader.GetString(1),
										reader.GetString(2),
										reader.GetInt32(3),
										reader.GetDateTime(4),
										author
									));
							}
						}
					}
				}
				catch (Exception exc)
				{

				}
			}


			return articles;
		}

		public static bool Add(Article article) //Добавить статью
		{
			bool success = false;

			using (SqlConnection connection = new SqlConnection(cs))
			{
				try
				{
					connection.Open();

					SqlCommand command = new SqlCommand("INSERT INTO Articles (title, text, creation_date, views, user_id) VALUES (@title, @text, @creation_date, @views, @user_id)", connection);

					command.Parameters.Add(new SqlParameter("@title", article.Title));
					command.Parameters.Add(new SqlParameter("@text", article.Text));
					command.Parameters.Add(new SqlParameter("@creation_date", article.CreationDate));
					command.Parameters.Add(new SqlParameter("@views", article.Views));
					command.Parameters.Add(new SqlParameter("@user_id", article.User.ID));

					int result = command.ExecuteNonQuery();

					if (result == 1)
					{
						success = true;
					}
				}
				catch(Exception exc)
				{

				}
			}


			return success;
		}

		#endregion

	}
}
