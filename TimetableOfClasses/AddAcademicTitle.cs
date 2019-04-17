﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibOfTimetableOfClasses;

namespace TimetableOfClasses
{
	public partial class AddAcademicTitle : Form
	{
		private MTitle Lehrer;

		public AddAcademicTitle()
		{
			InitializeComponent();
		}

		public AddAcademicTitle(MTitle mTitle)
		{
			InitializeComponent();
			this.Text = "Изменение уч. звания";		

			FullName.Text = mTitle.FullName;
			Reduction.Text = mTitle.Reduction;

		}

		private void btCreateAndClose_Click(object sender, EventArgs e)
		{

			if (Add())
			{
				Close();
			}
			else MessageBox.Show("Новозможно добавить это уч. звание", "Попробуйте снова");
		}


		private bool Add()
		{
			if (Lehrer == null)
			{
				MTitle Title = new MTitle(FullName.Text, Reduction.Text);
				return Controllers.CTitle.Insert(Title);
			}
			else
			{
				
				Lehrer.FullName = FullName.Text;
				Lehrer.Reduction = Reduction.Text;
				return Controllers.CTeacher.Update(Lehrer);
			}
		}

		/// <summary>
		/// Здесь должны быть комментарии
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddTeacher_Load(object sender, EventArgs e)
		{

		}

		private void SelectionOfLetters1(object sender, EventArgs e)
		{
			TextBox R = sender as TextBox;
			R.Text = Regex.Replace(R.Text, "[^а-я]", "");
			R.Text = Regex.Replace(R.Text, "[, ]+", ", ");
			if (R.Text.Length > 2)
			{
				if (R.Text.IndexOf(", ") == 0)
					R.Text = R.Text.Substring(1);
				if (R.Text.LastIndexOf(", ") == R.Text.Length - 1)
					R.Text = R.Text.Remove(R.Text.Length - 1);
				R.Text = R.Text.ToLower();
				R.Text = PeriodLetterToUpper(R.Text);
			}

		}

		private void SelectionOfLetters2(object sender, EventArgs e)
		{
			TextBox R = sender as TextBox;
			R.Text = Regex.Replace(R.Text, "[^а-яА-Я ]", "");
			R.Text = Regex.Replace(R.Text, "[ ]+", " ");
			if (R.Text.Length > 2)
			{
				if (R.Text.IndexOf(" ") == 0)
					R.Text = R.Text.Substring(1);
				if (R.Text.LastIndexOf(" ") == R.Text.Length - 1)
					R.Text = R.Text.Remove(R.Text.Length - 1);
				R.Text = R.Text.ToLower();
				R.Text = FirstLetterToUpper(R.Text);
			}
		}

		private static string FirstLetterToUpper(string str)
		{
			if (str.Length > 0)
			{
				if (str.IndexOf("-") > 0)
				{
					return Char.ToUpper(str[0]) + str.Substring(1, str.IndexOf("-")) + Char.ToUpper(str[str.IndexOf("-") + 1]) + str.Substring(str.IndexOf("-") + 2);
				}
				else
					return Char.ToUpper(str[0]) + str.Substring(1);
			}
			return "";
		}

		private static string PeriodLetterToUpper(string str)
		{
			if (str.Length > 0)
			{
				if (str.IndexOf(",") > 0)
				{
					char p;
					str = Char.ToUpper(str[0]) + str.Substring(1);
					for (int i = 0; i < str.Length; i++)
					{
						if (str[i] == ',')
						{
							p = Char.ToUpper(str[i + 2]);
							str = str.Remove(i + 2, 1);
							str = str.Insert(i + 2, "" + p);
						}
					}
					return str;
				}
				else
					return Char.ToUpper(str[0]) + str.Substring(1);
			}
			return "";
		}

		private void btCreateAndClean_Click(object sender, EventArgs e)
		{
			
			if ((Reduction.Text.Length != 0) && (FullName.Text.Length != 0))
			{
				MTitle Title = new MTitle(FullName.Text, Reduction.Text);
				Controllers.CTitle.Insert(Title);
				FullName.Text = "";
				Reduction.Text = "";
			}
			else MessageBox.Show("Невозможно добавить это уч. звание!", "Попробуйте снова", MessageBoxButtons.OK);
		}

		private void btCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
