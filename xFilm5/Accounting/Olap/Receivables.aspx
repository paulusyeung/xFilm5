<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Receivables.aspx.cs" Inherits="xFilm5.Accounting.Olap.Receivables" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraPivotGrid.Web" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0">
    <form id="form1" runat="server">
    <div style="font: 9pt Tahoma; border-right: 0; border-top: 0; border-left: 0; padding: 0px 0px 0px 0px;" runat="server" id="divPivotGrid">
        <table border="0" cellpadding="3" cellspacing="0" style="width :100%; background-color : #BFDBFF;">
            <tr>
                <td style="vertical-align: middle;">
                    <strong>Export to:</strong>
                </td>
                <td style="vertical-align: middle;">
                    <asp:DropDownList ID="listExportFormat" runat="server" Style="vertical-align: middle; font-size : 9pt; font-family: Arial;">
                        <asp:ListItem Selected="True">Pdf</asp:ListItem>
                        <asp:ListItem>Excel</asp:ListItem>
                        <asp:ListItem>Rtf</asp:ListItem>
                        <asp:ListItem>Text</asp:ListItem>
                    </asp:DropDownList>
                    <asp:ImageButton ID="btnSaveAs" runat="server" ImageUrl="~/Resources/Icons/16x16/export.png"
                        ToolTip="Export and save" Style="vertical-align: middle" OnClick="btnSaveAs_Click"
                        AlternateText="Save" />
                    <asp:ImageButton ID="btnOpen" runat="server" 
                        ImageUrl="~/Resources/Icons/16x16/16_L_saveOpen.gif" ToolTip="Export and open"
                        Style="vertical-align: middle" OnClick="btOpen_Click" AlternateText="Open" 
                        Visible="False" />
                </td>
                <td style="vertical-align: middle;">
                    <strong>Options: </strong>
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintHeadersOnEveryPage" runat="server" 
                        Text="Headers on every page" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintFilterHeaders" runat="server" 
                        Text="Filter Headers" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintColumnHeaders" runat="server" Text="Column Headers"
                        Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintRowHeaders" runat="server" Text="Row Headers" 
                        Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="checkPrintDataHeaders" runat="server" Text="Data Headers"
                        Checked="True" />
                </td>
            </tr>
        </table>
        <dx:ASPxPivotGridExporter ID="olapPivotGridExporter" runat="server" ASPxPivotGridID="olapPivotGrid" />
        <dx:ASPxPivotGrid ID="olapPivotGrid" runat="server" 
            DataSourceID="olapSQLSource">
            <Styles CssFilePath="~/App_Themes/Glass/{0}/styles.css" CssPostfix="Glass">
                <HeaderStyle>
                    <HoverStyle>
                        <BackgroundImage ImageUrl="~/App_Themes/Glass/PivotGrid/pgHeaderBackHot.gif" Repeat="RepeatX" />
                    </HoverStyle>
                    <BackgroundImage ImageUrl="~/App_Themes/Glass/PivotGrid/pgHeaderBack.gif" Repeat="RepeatX" />
                </HeaderStyle>
                <FilterAreaStyle>
                    <BackgroundImage ImageUrl="~/App_Themes/Glass/PivotGrid/pgFilterAreaBack.gif" Repeat="RepeatX" />
                </FilterAreaStyle>
                <FilterButtonPanelStyle>
                    <BackgroundImage ImageUrl="~/App_Themes/Glass/PivotGrid/pgFilterPanelBack.gif" Repeat="RepeatX" />
                </FilterButtonPanelStyle>
                <MenuStyle GutterWidth="0px" />
            </Styles>
            <OptionsLoadingPanel Text="Loading&amp;hellip;">
            </OptionsLoadingPanel>
            <OptionsPager RowsPerPage="100">
            </OptionsPager>
            <OptionsView ShowColumnTotals="true" ShowRowTotals="true" ShowColumnGrandTotals="true"
                ShowRowGrandTotals="true" ShowGrandTotalsForSingleValues="true" ShowTotalsForSingleValues="true" />
            <Images ImageFolder="~/App_Themes/Glass/{0}/">
            </Images>
        </dx:ASPxPivotGrid>
        <asp:SqlDataSource ID="olapSQLSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SysDb %>" 
            
            SelectCommand="SELECT [Workshop], [ClientName], [Year], [Month], [Day], CONVERT(NVARCHAR(10), [InvoiceDate], 120) AS InvoiceDate, [InvoiceNumber], [InvoiceAmount], [OrderNumber] FROM [vwOlapAR] WHERE [Year] >= 2008">
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
