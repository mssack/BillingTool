// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Windows.Input;






namespace CsWpfBase.Ev.Objects
{
	/// <summary>The relay command.</summary>
	public class RelayCommand : ICommand
	{
		private readonly Func<bool> _canExecuteEvaluator;
		private readonly Action _methodToExecute;
		private readonly Action<object> _methodToExecute1;

		/// <summary>ctor</summary>
		public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
		{
			_methodToExecute = methodToExecute;
			_canExecuteEvaluator = canExecuteEvaluator;
		}

		/// <summary>ctor</summary>
		public RelayCommand(Action<object> methodToExecute)
		{
			_methodToExecute1 = methodToExecute;
		}

		/// <summary>ctor</summary>
		public RelayCommand(Action methodToExecute)
			: this(methodToExecute, null)
		{
		}


		#region Overrides/Interfaces
		/// <summary>event</summary>
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		/// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
		/// <returns>true if this command can be executed; otherwise, false.</returns>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		public bool CanExecute(object parameter)
		{
			if (_canExecuteEvaluator == null)
			{
				return true;
			}
			var result = _canExecuteEvaluator.Invoke();
			return result;
		}

		/// <summary>Defines the method to be called when the command is invoked.</summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		public void Execute(object parameter)
		{
			if (_methodToExecute != null)
				_methodToExecute.Invoke();
			if (_methodToExecute1 != null)
				_methodToExecute1.Invoke(parameter);
		}
		#endregion


		/// <summary>Executes the command</summary>
		public void Execute()
		{
			if (_methodToExecute != null)
				_methodToExecute.Invoke();
			if (_methodToExecute1 != null)
				_methodToExecute1.Invoke(null);
		}
	}
}