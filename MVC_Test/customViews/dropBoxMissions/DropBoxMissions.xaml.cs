using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVC_Test.customViews.dropBoxMissions {
	/// <summary>
	/// Логика взаимодействия для DropBoxMissions.xaml
	/// </summary>
	public partial class DropBoxMissions : INotifyPropertyChanged {
		private MissionCategory SelectedCategory;
		private Mission SelectedMission;
		private bool isChanged;

		private RoutedEventHandler _click;
		public event RoutedEventHandler Click { add => _click += value; remove => _click -= value; }

		public static DependencyProperty SourceProperty =
			DependencyProperty.Register(nameof(Source), typeof(DropBoxModel), typeof(DropBoxMissions));

		internal DropBoxModel Source {
			get => (DropBoxModel) GetValue(SourceProperty);
			set {
				SetValue(SourceProperty, value);
				DropBox.ItemsSource = Source.MissionList;
				UpdateTitle();
			}
		}

		private string _title;

		public string Title {
			get => _title;
			set {
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}

		public string KeyList {
			get {
				var sb = new StringBuilder();
				SelectedMission.Model.CategoryList.ForEach(category => sb.Append($"-{category.Name}={category.SelectedItem.Name} "));
				return sb.ToString();
			}
		}

		public DropBoxMissions() {
			InitializeComponent();
			DataContext = this;
		}

		private void DropBoxMissions_OnLoaded(object sender, RoutedEventArgs e) {
			DropBox.DropDownOpened += DropDownOpened;
			DropBox.DropDownClosed += DropDownClosed;
		}

		private void EventButton_OnClick(object sender, RoutedEventArgs e) {
			_click?.Invoke(this, e);
			DropBox.IsDropDownOpen = false;
		}

		private void DropDownOpened(object sender, EventArgs e) { }

		private void DropDownClosed(object sender, EventArgs e) {
			if (isChanged) {
				isChanged = false;
				DropBox.IsDropDownOpen = true;
			} else { }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e) {
			if (isChanged) { return; } else { isChanged = true; }
			Console.WriteLine($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
			if (!(sender is ComboBoxItem cbItem)) { return; }
			var item = cbItem.Content as MissionDropBoxItem ?? throw new Exception("can't cast item to MissionDropBoxItem");
			//todo взять список категорий
			List<MissionDropBoxItem> itemList = new List<MissionDropBoxItem>();
			MissionCategory category;
			switch (item.Type) {
				case ItemType.Mission:
					SelectedMission = item as Mission ?? throw new Exception("can't cast item to MissionCategory");
					itemList.Add(new MissionDropBoxItem(ItemType.Back, "назад"));
					itemList.AddRange(SelectedMission.Model.CategoryList);
					break;
				case ItemType.Back:
					itemList.AddRange(Source.MissionList);
					break;
				case ItemType.Category:
					itemList.Add(new MissionDropBoxItem(ItemType.Back, "назад"));
					itemList.AddRange(SelectedMission.Model.CategoryList);
					category = item as MissionCategory ?? throw new Exception("can't cast item to MissionCategory");
					if (SelectedCategory?.Equals(category) ?? false) {
						SelectedCategory = null;
						break;
					}
					//todo найти индекс выбранной категории
					var selectedIndex = itemList.IndexOf(category);
					//todo вставить по индексу все элементы выбранной категории
					itemList.InsertRange(selectedIndex + 1, category.ItemList);
					SelectedCategory = category;
					break;
				case ItemType.Item:
					itemList.Add(new MissionDropBoxItem(ItemType.Back, "назад"));
					itemList.AddRange(SelectedMission.Model.CategoryList);
					//todo найти категорию выброного элемента
					category = SelectedMission.Model.CategoryList.Find(missionCategory =>
																															missionCategory.ItemList.Contains(item));
					//todo указать в поле SelectedItem выбранный элемент
					category.SelectedItem =
						item as MissionCategoryItem ?? throw new Exception("can't cast item to MissionCategory");
					//todo очистить 
					SelectedCategory = null;
					//todo update title
					UpdateTitle();
					break;
				default: throw new ArgumentOutOfRangeException();
			}
			//todo отобразить итоговый список элементов
			DropBox.ItemsSource = itemList;
		}

		private void UpdateTitle() {
			var sb = new StringBuilder();
			if (SelectedMission == null && Source != null && Source.MissionList.Count > 0) {
				SelectedMission = Source.MissionList[0];
			}
			sb.Append($"{SelectedMission?.Name}[");
			SelectedMission?.Model.CategoryList.ForEach(category => sb.Append($"{category.SelectedItem.Name}, "));
			if (sb.Length > 2) sb.Remove(sb.Length - 2, 2);
			Title = sb.Append("]").ToString();
		}

		private void ToolTip_OnOpened(object sender, RoutedEventArgs e) {
			TbMissionName.Text = SelectedMission.Name;
			var titles = "";
			var values = "";
			SelectedMission.Model.CategoryList.ForEach(category => titles += $"{category.Name}\n");
			SelectedMission.Model.CategoryList.ForEach(category => values += $"{category.SelectedItem.Name}\n");
			TbTitle.Text = titles;
			TbContent.Text = values;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void ToggleButton_OnClick(object sender, RoutedEventArgs e) { DropBox.ItemsSource = Source.MissionList; }
	}
}