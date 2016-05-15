// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.wpf.Storage
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public class CsgWpfStorageListView : Base
	{
		private static CsgWpfStorageListView _instance;
		/// <summary>Returns the singleton instance</summary>
		internal static CsgWpfStorageListView I
		{
			get { return _instance ?? (_instance = CsGlobal.Storage.Private.Handle("Configuration-Listviews", () => new CsgWpfStorageListView())); }
		}
		private List<ListViewHandle> _handles;

		private CsgWpfStorageListView()
		{
		}

		private List<ListViewHandle> Handles
		{
			get { return _handles ?? (_handles = new List<ListViewHandle>()); }
		}

		/// <summary>Adds a new handle</summary>
		public void Handle(ListView listView, string name)
		{
			var handle = Handles.FirstOrDefault(x => x.Name == name);

			if (handle != null)
			{
				handle.ListView = listView;
				if (handle.ColumnWidths == null)
					return;
				var columnCollection = ((GridView)handle.ListView.View).Columns;

				for (var i = 0; i < handle.ColumnWidths.Length && i< columnCollection.Count; i++)
				{
					columnCollection[i].Width = handle.ColumnWidths[i];
				}
			}
			else
			{
				handle = new ListViewHandle(listView, name);
				Handles.Add(handle);
			}
		}



		/// <summary>Handles the automated save.</summary>
		[Serializable]
		private class ListViewHandle : Base
		{
			private readonly string _name;
			private double[] _columnWidths;
			[field: NonSerialized] private ListView _listView;

			internal ListViewHandle(ListView view, string name)
			{
				_name = name;
				ListView = view;
			}

			/// <summary>Associated column name.</summary>
			public string Name
			{
				get { return _name; }
			}
			public ListView ListView
			{
				get { return _listView; }
				internal set
				{
					if (_listView != null)
						_listView.IsVisibleChanged -= ListViewOnIsVisibleChanged;
					SetProperty(ref _listView, value);
					if (_listView != null)
						_listView.IsVisibleChanged += ListViewOnIsVisibleChanged;
				}
			}
			/// <summary>The widths of the columns.</summary>
			public double[] ColumnWidths
			{
				get { return _columnWidths; }
				internal set { SetProperty(ref _columnWidths, value); }
			}

			private void ListViewOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
			{
				ColumnWidths = ((GridView) ListView.View).Columns.Select(x => x.Width).ToArray();
			}
		}
	}
}