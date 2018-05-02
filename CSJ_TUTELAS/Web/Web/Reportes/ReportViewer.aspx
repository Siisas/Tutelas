﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Web.Reportes.ListaUniversidades" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="scriptM" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="rptViewer" runat="server" Height="814px" Width="100%" ProcessingMode="Remote" AsyncRendering="False"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
