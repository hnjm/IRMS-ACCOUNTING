﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/Accounting.Master" AutoEventWireup="true" CodeBehind="TransactionReport.aspx.cs" Inherits="IntegratedResourceManagementSystem.Reports.ReportForms.TransactionReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style type ="text/css" >
    .backBtn
    {
        background :url('../../Resources/reply.png');
        background-repeat:no-repeat;
        font-family:Verdana;
        font-size:12px;
        height:25px;
        color:#FFFFCC;
        font-weight:bold;
        padding-left:20px;
        cursor:pointer;
    }
    .btnBrowse
    {
        background: rgb(252,234,187); /* Old browsers */
        /* IE9 SVG, needs conditional override of 'filter' to 'none' */
        background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2ZjZWFiYiIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjUwJSIgc3RvcC1jb2xvcj0iI2ZjY2Q0ZCIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjUxJSIgc3RvcC1jb2xvcj0iI2Y4YjUwMCIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiNmYmRmOTMiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
        background:  -moz-linear-gradient(top,  rgba(252,234,187,1) 0%, rgba(252,205,77,1) 50%, rgba(248,181,0,1) 51%, rgba(251,223,147,1) 100%); /* FF3.6+ */
        background:-webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(252,234,187,1)), color-stop(50%,rgba(252,205,77,1)), color-stop(51%,rgba(248,181,0,1)), color-stop(100%,rgba(251,223,147,1))); /* Chrome,Safari4+ */
        background:-webkit-linear-gradient(top,  rgba(252,234,187,1) 0%,rgba(252,205,77,1) 50%,rgba(248,181,0,1) 51%,rgba(251,223,147,1) 100%); /* Chrome10+,Safari5.1+ */
        background: -o-linear-gradient(top,  rgba(252,234,187,1) 0%,rgba(252,205,77,1) 50%,rgba(248,181,0,1) 51%,rgba(251,223,147,1) 100%); /* Opera 11.10+ */
        background: -ms-linear-gradient(top,  rgba(252,234,187,1) 0%,rgba(252,205,77,1) 50%,rgba(248,181,0,1) 51%,rgba(251,223,147,1) 100%); /* IE10+ */
        background: linear-gradient(to bottom,  rgba(252,234,187,1) 0%,rgba(252,205,77,1) 50%,rgba(248,181,0,1) 51%,rgba(251,223,147,1) 100%); /* W3C */

        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#fceabb', endColorstr='#fbdf93',GradientType=0 ); /* IE6-8 */
        color:#775c16;
        cursor:pointer;
        margin-top:2px;
        text-shadow: 1px 1px 1px #E0E0E0;
        border-radius:5px 3px 5px 3px;
        border:1px Solid #7F921C;
    }
    .btnBrowse:hover
    {
        background: rgb(249,198,103); /* Old browsers */
        /* IE9 SVG, needs conditional override of 'filter' to 'none' */
        background: url(data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/Pgo8c3ZnIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgd2lkdGg9IjEwMCUiIGhlaWdodD0iMTAwJSIgdmlld0JveD0iMCAwIDEgMSIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+CiAgPGxpbmVhckdyYWRpZW50IGlkPSJncmFkLXVjZ2ctZ2VuZXJhdGVkIiBncmFkaWVudFVuaXRzPSJ1c2VyU3BhY2VPblVzZSIgeDE9IjAlIiB5MT0iMCUiIHgyPSIwJSIgeTI9IjEwMCUiPgogICAgPHN0b3Agb2Zmc2V0PSIwJSIgc3RvcC1jb2xvcj0iI2Y5YzY2NyIgc3RvcC1vcGFjaXR5PSIxIi8+CiAgICA8c3RvcCBvZmZzZXQ9IjEwMCUiIHN0b3AtY29sb3I9IiNmNzk2MjEiIHN0b3Atb3BhY2l0eT0iMSIvPgogIDwvbGluZWFyR3JhZGllbnQ+CiAgPHJlY3QgeD0iMCIgeT0iMCIgd2lkdGg9IjEiIGhlaWdodD0iMSIgZmlsbD0idXJsKCNncmFkLXVjZ2ctZ2VuZXJhdGVkKSIgLz4KPC9zdmc+);
        background: -moz-linear-gradient(top,  rgba(249,198,103,1) 0%, rgba(247,150,33,1) 100%); /* FF3.6+ */
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(249,198,103,1)), color-stop(100%,rgba(247,150,33,1))); /* Chrome,Safari4+ */
        background: -webkit-linear-gradient(top,  rgba(249,198,103,1) 0%,rgba(247,150,33,1) 100%); /* Chrome10+,Safari5.1+ */
        background: -o-linear-gradient(top,  rgba(249,198,103,1) 0%,rgba(247,150,33,1) 100%); /* Opera 11.10+ */
        background: -ms-linear-gradient(top,  rgba(249,198,103,1) 0%,rgba(247,150,33,1) 100%); /* IE10+ */
        background: linear-gradient(to bottom,  rgba(249,198,103,1) 0%,rgba(247,150,33,1) 100%); /* W3C */
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#f9c667', endColorstr='#f79621',GradientType=0 ); /* IE6-8 */
    }
</style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <CR:crystalreportviewer ID="crvDailyReport" runat="server" 
                    AutoDataBind="True" GroupTreeImagesFolderUrl="" 
                    ToolbarImagesFolderUrl="" ToolPanelView="None" 
        ToolPanelWidth="200px" HasCrystalLogo="False" PrintMode="Pdf" />
              <%--  <CR:crystalreportsource ID="CrystalReportSource1" runat="server">
                    <Report FileName="Reports\ReportDocuments\CustSIIten.rpt">
                    </Report>
                </cr:crystalreportsource>--%>
</asp:Content>
