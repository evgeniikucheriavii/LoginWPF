using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddNReadApp
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	/// 

	public partial class MainWindow : Window
	{

		private List<Border> borders = new List<Border>();

		private MainApp app = new MainApp();

		public MainWindow()
		{
			InitializeComponent();

			borders = GetBorders();

			this.DataContext = app;

			app.Articles = Database.GetArticles();

		}

		#region ContentBorders

		public List<Border> GetBorders()
		{
			List<Border> borders = new List<Border>();

			borders.Add(LogInBorder);
			borders.Add(SignUpBorder);
			borders.Add(UserBorder);

			borders.Add(ArticleBorder);
			borders.Add(HomeBorder);
			borders.Add(AddBorder);

			return borders;
		}


		public void Show(Border border)
		{
			foreach (Border b in borders)
			{
				b.Visibility = b.Equals(border) ? Visibility.Visible : Visibility.Hidden;
			}
		}

		public void ShowUserPanels()
		{
			NotAuthPanel.Visibility = Visibility.Hidden;
			AuthPanel.Visibility = Visibility.Visible;
		}
		public void HideUserPanels()
		{
			NotAuthPanel.Visibility = Visibility.Visible;
			AuthPanel.Visibility = Visibility.Hidden;
		}

		#endregion

		private void LogInButton_Click(object sender, RoutedEventArgs e)
		{
			Show(LogInBorder);
		}

		private void SignUpButton_Click(object sender, RoutedEventArgs e)
		{
			Show(SignUpBorder);
		}

		private void AccountButton_Click(object sender, RoutedEventArgs e)
		{
			Show(UserBorder);
		}

		private void LogOutButton_Click(object sender, RoutedEventArgs e) //Log Out
		{
			MessageBoxResult result = MessageBox.Show("Do you really want to log out?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

			if (result == MessageBoxResult.Yes)
			{
				app.LogOut();

				HideUserPanels();
				Show(HomeBorder);
			}
		}

		private void ArticlesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int index = ArticlesList.SelectedIndex;

			if (index != 0)
			{

				app.CurrArticle = app.Articles[index];

				Show(ArticleBorder);

				ArticlesList.UnselectAll();
			}
		}

		private void RegistrationButton_Click(object sender, RoutedEventArgs e)
		{

			int id = 0;

			string name = RegNameBox.Text.Trim();

			string login = RegLoginBox.Text.Trim();

			string password = RegPasswordBox.Password.Trim();
			string passwordConfirm = RegPasswordConfirmBox.Password.Trim();

			DateTime birthDate = new DateTime(1800, 1, 1);

			try
			{
				birthDate = (DateTime)RegDateBox.SelectedDate;
			}
			catch (Exception exc)
			{
				
			}

			if (!string.IsNullOrEmpty(login))
			{
				if (!string.IsNullOrEmpty(name))
				{
					if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(passwordConfirm))
					{
						if (password == passwordConfirm)
						{
							if (!birthDate.Equals(new DateTime(1800, 1, 1)))
							{
								bool success = Database.Add(new User(id, name, login, password, birthDate, DateTime.Now));
								if (success)
								{
									MessageBox.Show("Congradulations! You can now log in!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
									Show(LogInBorder);
								}
								else
								{
									MessageBox.Show("Something went wrong! Maybe user with such data allready exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
								}
							}
							else
							{
								MessageBox.Show("The date field is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
							}
						}
						else
						{
							MessageBox.Show("Passwords are different!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
						}
					}
					else
					{
						MessageBox.Show("One or both password fields are empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
					}
				}
				else
				{
					MessageBox.Show("The name field is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			else
			{
				MessageBox.Show("The login field is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

		private void EnterButton_Click(object sender, RoutedEventArgs e)
		{
			string login = LogLoginBox.Text.Trim();
			string password = LogPasswordBox.Password.Trim();

			if (!string.IsNullOrEmpty(login))
			{
				if (!string.IsNullOrEmpty(password))
				{
					User user = Database.Login(login, password);
					if (user.ID != 0)
					{
						app.User = user;
						app.CurrUser = user;

						ShowUserPanels();
						Show(HomeBorder);
					}
					else
					{
						MessageBox.Show("Cannot log in! Maybe you've entered wrong login or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
					}
				}
				else
				{
					MessageBox.Show("The password field is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			else
			{
				MessageBox.Show("The login field is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

		private void NewArticleButton_Click(object sender, RoutedEventArgs e)
		{
			Show(AddBorder);
		}

		private void AddArticleButton_Click(object sender, RoutedEventArgs e)
		{
			int id = 0;

			string title = ArtTitleBox.Text.Trim();
			string text = ArtTextBox.Text.Trim();


			if (!string.IsNullOrEmpty(title))
			{
				if (!string.IsNullOrEmpty(text))
				{
					if (app.User.ID != 0)
					{
						Article article = new Article(id, title, text, 0, DateTime.Now, app.User);
						bool success = Database.Add(article);

						if (success)
						{
							ArtTitleBox.Text = "";
							ArtTextBox.Text = "";
							app.CurrArticle = article;
							Show(ArticleBorder);
						}
						else
						{
							MessageBox.Show("The login field is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
						}
					}
					else
					{
						MessageBox.Show("You must log in to add an article!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
					}
				}
				else
				{
					MessageBox.Show("The text field is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			else
			{
				MessageBox.Show("The title field is empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

		}

		private void ArticlesButton_Click(object sender, RoutedEventArgs e)
		{
			Show(HomeBorder);
		}

		private void AuthorsButton_Click(object sender, RoutedEventArgs e)
		{
			Show(AuthorsBorder);
		}
	}
}
