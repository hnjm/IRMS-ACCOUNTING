﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SOIGrossSalesPrintPreview.aspx.cs"
    Inherits="IntegratedResourceManagementSystem.Reports.ReportForms.SOIGrossSalesPrintPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GROSS SALES BREAK DOWN</title>
    <link href="../../Styles/toolbar-controls.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #gvGrossSales th
        {
            padding: 3px 5px 3px 5px;
        }
        #gvGrossSales tr td
        {
            padding: 3px 5px 3px 5px;
        }
    </style>
    <style type="text/css" media="print">
        #btnPrint
        {
            visibility:hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-weight: bold;">
        GROSS SALES BREAK DOWN
    </div>
    <div >
        <asp:GridView ID="gvGrossSales" align="center" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="GROSS_QUANTITY" HeaderText="QUANTITY">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="GROSS_AMOUNT" HeaderText="AMOUNT" 
                    DataFormatString="{0:###,###.00}">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="GROSS_MONTH" HeaderText="MONTH" />
                <asp:BoundField DataField="GROSS_YEAR" HeaderText="YEAR">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <div style="text-align: center; margin: 5px;">
            <input id="btnPrint" type="button" class="btnPrint" onclick="window.print()" value="PRINT" />
             <input id="Button1" type="button" class="btnDelete" onclick="window.close()" value="CLOSE" />
    </div>
    </form>
</body>
</html>
