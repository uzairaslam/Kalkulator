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
        bool enableEditEntResult = false;
        bool entHasResult = false;
        public MainPage()
        {
            InitializeComponent();
            dt = new DataTable();
        }

        private void NumberButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (enableEditEntResult)
                    entResult.Text = "0";
                if (lblQHist.Text.Substring(lblQHist.Text.Length - 1,1) == "=")
                {
                    lblQHist.Text = string.Empty;
                    entResult.Text = "0";
                }
                var btnText = ((Button)sender).Text;
                entResult.Text = entResult.Text == "0" || string.IsNullOrEmpty(entResult.Text) ? btnText : entResult.Text + btnText;
                enableEditEntResult = false;
                entHasResult = false;
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.Message);
            }
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            if (!entHasResult && entResult.Text.Length > 0)
            {
                if (entResult.CursorPosition > 0)
                    entResult.Text = entResult.Text.Remove(entResult.CursorPosition - 1, 1);
                else
                if (entResult.Text != "0")
                    entResult.Text = entResult.Text.Remove(entResult.Text.Length - 1, 1);
            }
        }

        private void OperationButton_Clicked(object sender, EventArgs e)
        {
            if (lblQHist.Text.Substring(lblQHist.Text.Length - 1) == "=")
                lblQHist.Text = entResult.Text;
            else
            {
                if (entResult.Text == "Cannot divide by zero‬")
                    entResult.Text = "0";
                if (entResult.Text.Substring(0, 1) == ".")
                    entResult.Text = "0" + entResult.Text;
                if (entResult.Text.Substring(entResult.Text.Length - 1, 1) == ".")
                    entResult.Text = entResult.Text + "0";

                if (string.IsNullOrWhiteSpace(lblQHist.Text))
                {
                    if (string.IsNullOrWhiteSpace(entResult.Text) || entResult.Text == "0")
                    {
                        lblQHist.Text = "0" + ((Button)sender).Text;
                    }
                    else
                    {
                        lblQHist.Text = entResult.Text + ((Button)sender).Text;
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(entResult.Text) || entResult.Text == "0")
                    {
                        if (entHasResult)
                        {
                            if (operations.Contains(lblQHist.Text.Substring(lblQHist.Text.Length - 1)))
                                lblQHist.Text = lblQHist.Text.Remove(lblQHist.Text.Length - 1, 1).Insert(lblQHist.Text.Length - 1, ((Button)sender).Text);
                        }
                        else
                            CalculateResult(sender);
                    }
                    else
                    {
                        if (!entHasResult)
                        {
                            CalculateResult(sender);
                        }
                        else
                        {
                            if (operations.Contains(lblQHist.Text.Substring(lblQHist.Text.Length - 1)))
                                lblQHist.Text = lblQHist.Text.Remove(lblQHist.Text.Length - 1, 1).Insert(lblQHist.Text.Length - 1, ((Button)sender).Text);
                        }
                    }
                }
            }
            enableEditEntResult = true;
        }

        private void CalculateResult(object sender)
        {
            lblQHist.Text = lblQHist.Text + entResult.Text;
            string expression = lblQHist.Text.Replace('x', '*').Replace('÷', '/');
            try
            {

                entResult.Text = dt.Compute(expression, "").ToString();
                if (entResult.Text == "Infinity")
                {
                    throw new DivideByZeroException();
                }
                else
                {
                    lblQHist.Text = lblQHist.Text + ((Button)sender).Text;
                }
            }
            catch (DivideByZeroException ex)
            {
                lblQHist.Text = string.Empty;
                entResult.FontSize = 40;
                entResult.Text = "Cannot divide by zero‬";
            }
            entHasResult = true;
        }

       

        private void Reset_Clicked(object sender, EventArgs e)
        {
            lblQHist.Text = string.Empty;
            entResult.Text = "0";
        }

        private void EqualButton_Clicked(object sender, EventArgs e)
        {
            if (lblQHist.Text.Any(l => l.Equals('+') || l.Equals('-') || l.Equals('x') || l.Equals('÷')))
            {
                CalculateResult(sender);
                enableEditEntResult = true;
            }
        }
    }
}
