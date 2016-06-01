// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;





namespace CsWpfBase.Themes.Controls.Containers
{
#pragma warning disable 1591

	[ContentProperty("DefaultItem")]
	public class NavigationContainer : Control
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof (DataTemplate), typeof (NavigationContainer), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty StackProperty = DependencyProperty.Register("Stack", typeof (HistoryStack), typeof (NavigationContainer), new FrameworkPropertyMetadata {DefaultValue = default(HistoryStack), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof (Object), typeof (NavigationContainer), new FrameworkPropertyMetadata {DefaultValue = default(Object), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof (DataTemplate), typeof (NavigationContainer), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty DefaultItemProperty = DependencyProperty.Register("DefaultItem", typeof (Object), typeof (NavigationContainer), new FrameworkPropertyMetadata
																																								{
																																									DefaultValue = default(Object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => { ((NavigationContainer) o).DefaultItemChanged(); }
																																								});
		private static readonly DependencyPropertyKey DisplayItemPropertyKey = DependencyProperty.RegisterReadOnly("DisplayItem", typeof (Object), typeof (NavigationContainer), new FrameworkPropertyMetadata {DefaultValue = default(Object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty DisplayItemProperty = DisplayItemPropertyKey.DependencyProperty;
		#endregion


		private RelayCommand _navigateBackCommand;
		private RelayCommand _navigateToRoot;


		static NavigationContainer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (NavigationContainer), new FrameworkPropertyMetadata(typeof (NavigationContainer)));
		}

		public NavigationContainer()
		{
			Stack = new HistoryStack();
		}

		public Object DefaultItem
		{
			get { return GetValue(DefaultItemProperty); }
			set { SetValue(DefaultItemProperty, value); }
		}
		public Object DisplayItem
		{
			get { return GetValue(DisplayItemProperty); }
			private set { SetValue(DisplayItemPropertyKey, value); }
		}
		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate) GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}
		public Object Header
		{
			get { return GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}
		public DataTemplate HeaderTemplate
		{
			get { return (DataTemplate) GetValue(HeaderTemplateProperty); }
			set { SetValue(HeaderTemplateProperty, value); }
		}
		public HistoryStack Stack
		{
			get { return (HistoryStack) GetValue(StackProperty); }
			set { SetValue(StackProperty, value); }
		}
		public RelayCommand NavigateBackCommand
		{
			get { return _navigateBackCommand ?? (_navigateBackCommand = new RelayCommand(() => NavigateBack())); }
		}
		public RelayCommand NavigateToRoot
		{
			get { return _navigateToRoot ?? (_navigateToRoot = new RelayCommand(() => NavigateToDefault())); }
		}
		private void DefaultItemChanged()
		{
			if (DefaultItem != null && DisplayItem == null)
			{
				DisplayItem = DefaultItem;
			}
		}


		[DebuggerStepThrough]
		public void Navigate(object ob)
		{
			if (Stack.ActualItem == ob)
				return;

			Unload(() =>
			{
				Stack.Push(ob);
				SuggestDisplayItem();
				Load();
			});
		}
		[DebuggerStepThrough]
		public void NavigateBack()
		{
			if (Stack.IsPopAvailable == false)
				return;

			Unload(() =>
			{
				Stack.Pop();
				SuggestDisplayItem();
				Load();
			});
		}
		[DebuggerStepThrough]
		public void NavigateToDefault()
		{
			if (Stack.IsPopAvailable == false)
				return;

			Unload(() =>
			{
				Stack.PopAll();
				SuggestDisplayItem();
				Load();
			});
		}

		[DebuggerStepThrough]
		private void SuggestDisplayItem()
		{
			if (Stack.ActualItem == null)
				DisplayItem = DefaultItem;
			else
				DisplayItem = Stack.ActualItem;
		}
		private void Unload(Action unloaded = null)
		{
			CsGlobal.Wpf.Animation.Opacity(ContentControl, 0, new Duration(TimeSpan.FromMilliseconds(500)), null, unloaded);
		}
		private void Load(Action loaded = null)
		{
			CsGlobal.Wpf.Animation.Opacity(ContentControl, 1, new Duration(TimeSpan.FromMilliseconds(500)), null, loaded);
		}


		#region TEMPLATE
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (Template != null)
			{
				ContentControl = (ContentControl) Template.FindName("PART_Content", this);
			}
		}
		private ContentControl ContentControl { get; set; }
		#endregion


		public class HistoryStack : BaseRegister<object>
		{
			private bool _isPopAvailable;

			public bool IsPopAvailable
			{
				get { return _isPopAvailable; }
				private set { SetProperty(ref _isPopAvailable, value); }
			}
			public object ActualItem
			{
				get { return Count == 0 ? null : this[0]; }
			}

			public void Push(object item)
			{
				if (Count != 0 && item == this[0])
					throw new InvalidOperationException("Das selbe Item kann nicht zweimal hintereinander dem Stack hinzugefügt werden.");


				Insert(0, item);
				Changed();
			}
			public void Pop()
			{
				if (Count == 0)
					return;

				RemoveAt(0);
				Changed();
			}
			public void PopAll()
			{
				if (Count == 0)
					return;
				Clear();
				Changed();
			}

			private void Changed()
			{
				OnPropertyChanged("ActualItem");
				IsPopAvailable = Count > 0;
			}
		}
	}
}