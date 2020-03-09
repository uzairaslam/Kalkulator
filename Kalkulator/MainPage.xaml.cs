using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Data;

namespace Kalkulator
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        DataTable dt = new DataTable();
        string[] operations = new string[] { "+", "-", "x", "÷" };
        public MainPage()
        {
            InitializeComponent();
            dt = new DataTable();
        }

        private void NumberButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var btnText = ((Button)sender).Text;
                entResult.Text = entResult.Text == "0" || string.IsNullOrEmpty(entResult.Text) ? btnText : entResult.Text + btnText;
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.Message);
            }
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            if (entResult.CursorPosition > 0)
                entResult.Text = entResult.Text.Remove(entResult.CursorPosition - 1, 1);
            else
                if (entResult.Text != "0")
                entResult.Text = entResult.Text.Remove(entResult.Text.Length - 1, 1);
        }

        private void OperationButton_Clicked(object sender, EventArgs e)
        {
            if(operations.Contains(lblQHist.Text.Substring(lblQHist.Text.Length - 1)) && (entResult.Text.Trim() == "0" || string.IsNullOrWhiteSpace(entResult.Text)))
                //lblQHist.Text.re

            if(lblQHist.Text.Any(u => u.Equals('+') || u.Equals('-') || u.Equals('÷') || u.Equals('x')))
            {
                if (lblQHist.Text.Split(new char[] { '+', '-', 'x', '÷'}).Count() > 1)
                {
                    entResult.Text = dt.Compute(lblQHist.Text, "").ToString();
                }
                else
                {
                    lblQHist.Text = lblQHist.Text.Remove(lblQHist.Text.Length - 1, 1);
                }
            }
            else
            {

            }
        }
    }
}
