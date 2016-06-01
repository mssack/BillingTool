// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using CsWpfBase.Db.codegen.architecture.parts.bases;






namespace CsWpfBase.Db.codegen.architecture.parts
{
	/// <summary>Used to represent views.</summary>
	public class CsDbArcView : CsDbArcTableViewBase
	{
		private CsDbArcView(CsDbArcDatabase owner, string name) : base(owner, name)
		{
		}

		/// <summary>This initialize method should only be called by the <see cref="CsDbArcDatabase" /> class.</summary>
		internal static CsDbArcView Create(CsDbArcDatabase owner, string name)
		{
			return new CsDbArcView(owner, name);
		}

		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return $"View[{Name}]";
		}
	}
}