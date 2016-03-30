// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;





namespace CsWpfBase.Utilitys
{
#pragma warning disable 1591
	/// <summary>DateDiff allows to create strings which calculates the amount of years months or days between two dates.</summary>
	public class DateDiff
	{
		public DateDiff(DateTime first, DateTime second)
		{
			if (first < second)
			{
				Lower = first;
				Upper = second;
			}
			else
			{
				Lower = second;
				Upper = first;
			}
			RunConvesion();
		}


		#region Overrides
		public override string ToString()
		{
			return ToString(true, true);
		}
		#endregion


		public DateTime Lower { get; set; }
		public DateTime Upper { get; set; }


		public int Years { get; private set; }
		public int Months { get; private set; }
		public int Days { get; private set; }
		public int Hours { get; private set; }
		public int Minutes { get; private set; }
		public int Seconds { get; private set; }
		public int Milliseconds { get; private set; }
		private void RunConvesion()
		{
			int years = Upper.Year - Lower.Year;
			DateTime stopReminder = Upper;
			if (years > 0)
			{
				if (Lower.AddYears(years) > Upper)
					years--;
				Lower = Lower.AddYears(years);
			}
			int months = 0;
			while (Lower.AddMonths(++months) < stopReminder)
			{
			}
			months--;
			if (months > 0)
			{
				if (Lower.AddMonths(months) > stopReminder)
					months--;
				Lower = Lower.AddMonths(months);
			}
			int days = 0;
			while (Lower.AddDays(++days) <= Upper)
			{
			}
			days--;

			Lower = Lower.AddDays(days);
			int hours = 0;
			while (Lower.AddHours(++hours) <= Upper)
			{
			}
			hours--;

			Lower = Lower.AddDays(hours);
			int minutes = 0;
			while (Lower.AddMinutes(++minutes) <= Upper)
			{
			}
			minutes--;

			Lower = Lower.AddMinutes(minutes);
			int seconds = 0;
			while (Lower.AddSeconds(++seconds) <= Upper)
			{
			}
			seconds--;

			Lower = Lower.AddSeconds(seconds);
			int milliseconds = 0;
			while (Lower.AddMilliseconds(++milliseconds) <= Upper)
			{
			}
			milliseconds--;

			Years = years;
			Months = months;
			Days = days;
			Hours = hours;
			Minutes = minutes;
			Seconds = seconds;
			Milliseconds = milliseconds;
		}

		public string ToString(bool plural, bool approximate)
		{
			if (Years != 0)
			{
				if (approximate && Months >= 8)
					return "knapp " + (Years + 1) + " Jahre" + (plural ? "n" : "");
				if (Years == 1)
					return "1 Jahr";
				return Years + " Jahre" + (plural ? "n" : "");
			}
			if (Months != 0)
			{
				if (approximate && Days >= 22)
					return "knapp " + (Months + 1) + " Monate" + (plural ? "n" : "");
				if (Months == 1)
					return "1 Monat";
				return Months + " Monate" + (plural ? "n" : "");
			}
			if (Days != 0)
			{
				if (Days >= 7)
				{
					var weeks = Days/7;
					if (approximate && Days%7 > 5)
						return "knapp " + (weeks + 1) + " Wochen";
					if (weeks == 1)
						return "1 Woche";
					return weeks + " Wochen";
				}
				if (approximate && Hours >= 20)
					return "knapp " + (Days + 1) + " Tage" + (plural ? "n" : "");
				if (Days == 1)
					return "1 Tag";
				return Days + " Tage" + (plural ? "n" : "");
			}
			if (Hours != 0)
			{
				if (approximate && Minutes >= 50)
					return "knapp " + (Hours + 1) + " Stunde" + (plural ? "n" : "");
				if (Hours == 1)
					return "1 Stunde" + (plural ? "n" : "");
				return Hours + " Stunde" + (plural ? "n" : "");
			}
			if (Minutes != 0)
			{
				if (approximate && Seconds >= 50)
					return "knapp " + (Minutes + 1) + " Minuten";
				if (Minutes == 1)
					return "1 Minute";
				return Minutes + " Minuten";
			}
			if (Seconds != 0)
			{
				if (approximate && Milliseconds >= 900)
					return "knapp " + (Seconds + 1) + " Sekunden";
				if (Seconds == 1)
					return "1 Sekunde";
				return Seconds + " Sekunden";
			}
			if (Milliseconds != 0)
			{
				if (Milliseconds == 1)
					return "1 Millisekunde";
				return Milliseconds + " Millisekunden";
			}
			return "Jetzt";
		}


		#region CascadedCalculation
		private void CorrectYear(ref int year)
		{
			if (Upper.Month == Lower.Month)
				CorrectMonth(ref year);
			else if (Upper.Month < Lower.Month)
				year--;
		}
		private void CorrectMonth(ref int month)
		{
			if (Upper.Day == Lower.Day)
				CorrectDay(ref month);
			else if (Upper.Day < Lower.Day)
				month--;
		}
		private void CorrectDay(ref int day)
		{
			if (Upper.Hour == Lower.Hour)
				CorrectHour(ref day);
			else if (Upper.Hour < Lower.Hour)
				day--;
		}
		private void CorrectHour(ref int hour)
		{
			if (Upper.Minute == Lower.Minute)
				CorrectMinute(ref hour);
			else if (Upper.Minute < Lower.Minute)
				hour--;
		}
		private void CorrectMinute(ref int minute)
		{
			if (Upper.Second == Lower.Second)
				CorrectSecond(ref minute);
			else if (Upper.Second < Lower.Second)
				minute--;
		}
		private void CorrectSecond(ref int second)
		{
			if (Upper.Millisecond == Lower.Millisecond)
				return;
			if (Upper.Millisecond < Lower.Millisecond)
				second--;
		}
		#endregion
	}
}