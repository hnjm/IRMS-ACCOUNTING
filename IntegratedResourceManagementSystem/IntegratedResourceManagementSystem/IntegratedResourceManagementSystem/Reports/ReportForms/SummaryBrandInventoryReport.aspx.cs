﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using IntegratedResourceManagementSystem.Reports.ReportDocuments;
using IRMS.ObjectModel;
using IRMS.BusinessLogic.Manager;
using System.Data;
using System.Globalization;

namespace IntegratedResourceManagementSystem.Reports.ReportForms
{
    public partial class SummaryBrandInventoryReport  : System.Web.UI.Page
    {

        public UsersClass USER { get { return (UsersClass)Session["USER_ACCOUNT"]; } }

        #region "Book Qty"

        public double TotalBookQty;

        public double GetBookQty(double Qty)
        {
            TotalBookQty += Qty;
            return Qty;
        }

        public double GetTotalBookQty()
        {
            return TotalBookQty;
        }

        #endregion

        #region "Actual Pcount Qty"

        public double TotalPcountQty;

        public double GetPcountQty(double Qty)
        {
            TotalPcountQty += Qty;
            return Qty;
        }

        public double GetTotalPcountQty()
        {
            return TotalPcountQty;
        }

        #endregion

        #region "Qty (LKG / OVER)"

        public double GetTotalQtyLkgOver()
        {
            return TotalPcountQty - TotalBookQty;
        }

        public string GetTotalQtyLkgOverFormatted()
        {
            // return TotalPcountQty - TotalBookQty;
            double Result = 0;
            string ReturnResult = string.Empty;

            Result = TotalPcountQty - TotalBookQty;

            if (Result < 0)
            {
                Result = Result * (-1);

                ReturnResult = "(" + Result.ToString("0,000") + ")";
            }

            return ReturnResult;
        }

        #endregion

        #region "Percent Book Qty"

        public double GetTotalPrecentBookQty()
        {
            return GetTotalQtyLkgOver() / GetTotalBookQty();
        }

        public string GetTotalPrecentBookQtyFormatted()
        {
            double Result = 0;
            string ReturnResult = string.Empty;

            Result = GetTotalQtyLkgOver() / GetTotalBookQty();

            if (Result < 0)
            {
                Result = Result * (-1);

                ReturnResult = "(" + Result + ")";
            }

            return ReturnResult;
        }

        #endregion

        //#region "Ending Invty Prevailing"

        //public double TotalEndingInvtPrevailing;

        //public double GetEndingInvtPrevailing(double Price)
        //{
        //    TotalEndingInvtPrevailing += Price;
        //    return Price;
        //}

        //public double GetTotalEndingInvtPrevailing()
        //{
        //    return TotalEndingInvtPrevailing;
        //}

        //#endregion

        //#region "Actual Physical Count Prevailing"

        //public double TotalActualPcountPrevailing;

        //public double GetActualPcountPrevailing(double Price)
        //{
        //    TotalActualPcountPrevailing += Price;
        //    return Price;
        //}

        //public double GetTotalActualPcountPrevailing()
        //{
        //    return TotalActualPcountPrevailing;
        //}

        //#endregion

        //#region "Variance (Lkg/Over)"

        //public double GetTotalVariancePrevailing()
        //{
        //    return GetTotalActualPcountPrevailing() - GetTotalEndingInvtPrevailing();
        //}

        //#endregion

        #region "Ending Invty Cost"

        public double TotalEndingInvtCost;

        public double GetEndingInvtCost(double Price)
        {
            TotalEndingInvtCost += Price;
            return Price;
        }

        public double GetTotalEndingInvtCost()
        {
            return TotalEndingInvtCost;
        }

        #endregion

        #region "Actual Physical Count Cost"

        public double TotalActualPcountCost;

        public double GetActualPcountCost(double Price)
        {
            TotalActualPcountCost += Price;
            return Price;
        }

        public double GetTotalActualPcountCost()
        {
            return TotalActualPcountCost;
        }

        #endregion

        #region "Variance Cost"

        public double GetTotalVarianceCost()
        {
            return GetTotalActualPcountCost() - GetTotalEndingInvtCost();
        }

        public string GetTotalVarianceCostFormatted()
        {
             
            double Result = 0;
            string ReturnResult = string.Empty;

            Result = GetTotalActualPcountCost() - GetTotalEndingInvtCost();

            if (Result < 0)
            {
                Result = Result * (-1);

                ReturnResult = "(" + Result.ToString("0,000.00") + ")";
            }

            return ReturnResult;
        }

        #endregion

        #region "Average Per Pcount Cost"

        public double GetTotalAveragePerPcount()
        {
            return GetTotalActualPcountCost() / GetTotalBookQty();
        }

        #endregion

        #region "Percent Cost Lkg"

        public double GetTotalPercentCostLkg()
        {
            return GetTotalVarianceCost() / GetTotalEndingInvtCost();
        }

        public string GetTotalPercentCostLkgFormatted()
        {

            double Result = 0;
            string ReturnResult = string.Empty;

            Result = GetTotalVarianceCost() / GetTotalEndingInvtCost();

            if (Result < 0)
            {
                Result = Result * (-1);

                ReturnResult = "(" + Result + ")";
            }

            return ReturnResult;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            LoadGridView();
            //InitializeReport();
        }

        private void InitializeReport()
        {
            try
            {
                DateTime From = Convert.ToDateTime(Session["From"].ToString());
                DateTime To = Convert.ToDateTime(Session["To"].ToString());

                ReportDocument REPORT_DOC = new ReportDocument();
                string reportCacheSummaryBrandInventory = string.Concat("SummaryBrandInventory", USER.FullName, From, To);               
                
                ParameterField prmUser = new ParameterField();
                ParameterField prmFrom = new ParameterField();
                ParameterField prmTo = new ParameterField();

                ParameterFields prmList = new ParameterFields();

                prmUser.ParameterFieldName = "username";
                prmFrom.ParameterFieldName = "datefrom";
                prmTo.ParameterFieldName = "dateto";

                ParameterDiscreteValue prmUserValue = new ParameterDiscreteValue();
                ParameterDiscreteValue prmFromValue = new ParameterDiscreteValue();
                ParameterDiscreteValue prmToValue = new ParameterDiscreteValue();
                
                prmUserValue.Value = USER.FullName;
                prmFromValue.Value = From;
                prmToValue.Value = To;

                prmUser.CurrentValues.Add(prmUserValue);
                prmFrom.CurrentValues.Add(prmFromValue);
                prmTo.CurrentValues.Add(prmToValue);

              
                prmList.Add(prmUser);
                prmList.Add(prmFrom);
                prmList.Add(prmTo);

                //if (Status == 0)
                //{
                    REPORT_DOC = new RptSummBrandInventory();
                    Cache.Insert(reportCacheSummaryBrandInventory, REPORT_DOC);
                //}
              
                DataBaseLogIn(REPORT_DOC);
                this.CrystalReportViewer1.ParameterFieldInfo = prmList;
                CrystalReportViewer1.ReportSource = REPORT_DOC;

            }
            catch
            {
                throw;
            }

        }

        private static SqlConnectionStringBuilder Connection()
        {
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["IRMSConnectionString"].ConnectionString;
            return con;
        }

        private static void DataBaseLogIn(ReportDocument rpt)
        {
            ConnectionInfo con_info = new ConnectionInfo();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            Tables crTables;

            con_info.ServerName = Connection().DataSource;
            con_info.DatabaseName = Connection().InitialCatalog;
            con_info.UserID = Connection().UserID;
            con_info.Password = Connection().Password;

            crTables = rpt.Database.Tables;

            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
            {
                crtableLogoninfo = crTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = con_info;
                crTable.ApplyLogOnInfo(crtableLogoninfo);
            }
        }

        protected void LoadGridView()
        {

            string Date = string.Empty;
            DateTime From;
            DateTime To;
            


            Date = Session["From"].ToString() + " TO " + Session["To"].ToString();
            From = DateTime.Parse(Session["From"].ToString()).Date;
            To = DateTime.Parse(Session["To"].ToString()).Date;
           


            lblDate.Text = Date;
          

            StoreOutStandingInventoryManager SOI = new StoreOutStandingInventoryManager();

            TotalBookQty = 0;
            TotalPcountQty = 0;
            //TotalEndingInvtPrevailing = 0;
            //TotalActualPcountPrevailing = 0;
            TotalEndingInvtCost = 0;
            TotalActualPcountCost = 0;

            GridView1.DataSource = SOI.GetSoiByBrandMMDS(From, To);
            GridView1.DataBind();

            TotalBookQty = 0;
            TotalPcountQty = 0;
            TotalEndingInvtCost = 0;
            TotalActualPcountCost = 0;

            GridView2.DataSource = SOI.GetSoiByBrandLuzon(From, To);
            GridView2.DataBind();

            TotalBookQty = 0;
            TotalPcountQty = 0;
            TotalEndingInvtCost = 0;
            TotalActualPcountCost = 0;

            GridView3.DataSource = SOI.GetSoiByBrandVisayas(From, To);
            GridView3.DataBind();

            TotalBookQty = 0;
            TotalPcountQty = 0;
            TotalEndingInvtCost = 0;
            TotalActualPcountCost = 0;

            GridView4.DataSource = SOI.GetSoiByBrandMindanao(From, To);
            GridView4.DataBind();

        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                Label lblLkgOverQty = (Label)e.Row.FindControl("lblLkgOverQty");
                Label lblPercentageQty = (Label)e.Row.FindControl("lblPercentageQty");
                Label lblVarianceCost = (Label)e.Row.FindControl("lblVarianceCost");
                Label lblCostLkgBook = (Label)e.Row.FindControl("lblCostLkgBook");
                
                int Counter = 0;
                if (Convert.ToDouble(lblLkgOverQty.Text) < Counter || Convert.ToInt16(lblPercentageQty.Text) < Counter || Convert.ToDouble(lblVarianceCost.Text) < Counter || Convert.ToInt16(lblCostLkgBook.Text) < Counter)
                {
                    var OutputLkgOverQty = Convert.ToDouble(lblLkgOverQty.Text) * (-1);
                    lblLkgOverQty.Text = OutputLkgOverQty.ToString("(0,000)");

                    var OutputPercentageQty = Convert.ToDouble(lblPercentageQty.Text) * (-1);
                    lblPercentageQty.Text = "(" + OutputPercentageQty + ")";

                    var OutputVarianceCost = Convert.ToDouble(lblVarianceCost.Text) * (-1);
                    lblVarianceCost.Text = OutputVarianceCost.ToString("(0,000.00)");

                    var OutputCostLkgBook = Convert.ToDouble(lblCostLkgBook.Text) * (-1);
                    lblCostLkgBook.Text = "(" + OutputCostLkgBook + ")";
                }
                else
                {
                    lblLkgOverQty.Text = lblLkgOverQty.Text;
                    lblPercentageQty.Text = lblPercentageQty.Text;
                    lblVarianceCost.Text = lblVarianceCost.Text;
                    lblCostLkgBook.Text = lblCostLkgBook.Text;
                } 
            }
        }

      
    }
}