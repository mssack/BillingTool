// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;





namespace CsWpfBase.Ev.Objects
{
	/// <summary>Base, serialize- and bind- able, super base type. Provides different methods.</summary>
	[Serializable]
	[DebuggerStepThrough]
	public class Base : INotifyPropertyChanged, IDisposable
	{
		/// <summary>
		///     All references to this object are freed by setting <see cref="PropertyChanged" /> to null. In inherited
		///     classes override this method to free all other events.
		/// </summary>
		public virtual void Dispose()
		{
			PropertyChanged = null;
		}
		/// <summary>Thread comprehensive property changed implementation.</summary>
		[field: NonSerialized]
		public virtual event PropertyChangedEventHandler PropertyChanged;


		#region Overrides
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return GetType().Name;
		}
		#endregion


		/// <summary>Invokes the <see cref="PropertyChanged" /> event. Equivalent for protected <see cref="OnPropertyChanged" />.</summary>
		public static void RaisePropertyChange(Base @base, string propertyname)
		{
			@base.OnPropertyChanged(propertyname);
		}
		/// <summary>Invokes the <see cref="PropertyChanged" /> event for all property's in a <see cref="Base" /> using
		///     <see cref="System.Reflection" />
		/// </summary>
		public static void RaiseAllPropertyChanged(Base @base)
		{
			@base.GetType().GetProperties().ToList().ForEach(pi => RaisePropertyChange(@base, pi.Name));
		}


		#region Virtuals
		/// <summary>
		///     Implementation of the <see cref="INotifyPropertyChanged" /> interface. Checks whether the backing field
		///     already provides this reference or value. If backing field provides the same value, no notification is sent, if you
		///     want to force a notification use <see cref="OnPropertyChanged" />
		/// </summary>
		/// <typeparam name="T">the generic controller, used to provide intellisense feature.</typeparam>
		/// <param name="backingField">The backing field where the property stores the reference</param>
		/// <param name="value">The new value.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns></returns>
		protected virtual bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] String propertyName = null)
		{
			if (Equals(backingField, value))
				return false;

			backingField = value;
			OnPropertyChanged(propertyName);
			return true;
		}
		/// <summary>Sends a notification to <see cref="PropertyChanged" /> subscriber.</summary>
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler onPropertyChanged = PropertyChanged;
			if (onPropertyChanged != null)
			{
				onPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion
	}
}