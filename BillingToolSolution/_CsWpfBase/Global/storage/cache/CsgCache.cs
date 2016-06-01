using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.storage.cache
{
	///<summary>Collapses the CSG file storage cache functions</summary>
	public class CsgCache : Base
	{
		private DirectoryInfo _name;
		///<summary>Gets or sets the Name.</summary>
		public DirectoryInfo Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
	}
}
