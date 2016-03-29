// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using System.IO;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingDataAccessGenerator.DatabaseCreation
{
	[Serializable]
	public sealed class DatabaseInstaller : Base
	{
		private string _databaseFilePath;



		public DatabaseInstaller(string databaseFilePath)
		{
			DatabaseFilePath = databaseFilePath;
		}



		///<summary>The file path to SQLCE 4.0 Database.</summary>
		public string DatabaseFilePath
		{
			get { return _databaseFilePath; }
			private set { SetProperty(ref _databaseFilePath, value); }
		}


		public void Install()
		{
			var dbFile = new FileInfo(DatabaseFilePath);
			if (dbFile.Exists)
				throw new InvalidOperationException("The database file already exist. You have to delete it before you can install a new one.");
			dbFile.CreateDirectory_IfNotExists();



		}
	}
}