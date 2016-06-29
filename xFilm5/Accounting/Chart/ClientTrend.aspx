﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientTrend.aspx.cs" Inherits="xFilm5.Accounting.Chart.ClientTrend" %>

<%@ Register assembly="DevExpress.Web.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v15.2, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>
<%@ Register assembly="DevExpress.XtraCharts.v15.2.Web, Version=15.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" width="800px" 
            ShowHeader="False">
            <PanelCollection>
<dx:PanelContent ID="PanelContent1" runat="server">
    <table border="0" cellpadding="2" cellspacing="2" >
        <tr>
            <td style="width: 60px; padding-left : 4px;">
    <dx:ASPxLabel ID="lblGroupBy" runat="server" Text="Group By:">
    </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxRadioButtonList ID="radGroupBy" runat="server" AutoPostBack="True" 
                    ItemSpacing="10px" OnSelectedIndexChanged="radGroupBy_SelectedIndexChanged" 
                    RepeatDirection="Horizontal" SelectedIndex="0">
                    <Items>
                        <dx:ListEditItem Text="Branch" Value="Branch" />
                        <dx:ListEditItem Text="Year" Value="Year" />
                    </Items>
                    <Border BorderStyle="None" />
                </dx:ASPxRadioButtonList>
            </td>
            <td style="width: 60px; text-align: right; padding-right : 4px;">
    <dx:ASPxLabel ID="lblFilter" runat="server" Text="Filter By:">
    </dx:ASPxLabel>
            </td>
            <td>
    <dx:ASPxComboBox ID="cboFilter" runat="server" Width="100px">
    </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxButtonEdit ID="cmdRefresh" runat="server" AutoPostBack="True" 
            Cursor="pointer" OnButtonClick="cmdRefresh_ButtonClick" Spacing="0" 
            Width="60px">
            <Buttons>
                <dx:EditButton Text="Draw">
                    <Image Url="~/Resources/Icons/16x16/chart16.png" />
                </dx:EditButton>
            </Buttons>
            <Border BorderStyle="None" />
        </dx:ASPxButtonEdit>
        </td>
        </tr>
    </table>
                </dx:PanelContent>
</PanelCollection>
        </dx:ASPxRoundPanel>
        
        <dxchartsui:WebChartControl ID="wccRevenue" runat="server" 
            DataSourceID="SqlDs"  Height="480px" 
            Width="800px">
            <DiagramSerializable>
<:XYDiagram>
                <axisx title-text="Month" title-visible="True">
<range sidemarginsenabled="True"></range>
</axisx>
                <axisy title-text="Revenue ($)" title-visible="True">
<range sidemarginsenabled="True"></range>

<numericoptions format="Number" precision="0"></numericoptions>
</axisy>
            </:XYDiagram>
</DiagramSerializable>
<FillStyle >
<OptionsSerializable>
<:SolidFillOptions HiddenSerializableString="to be serialized"></:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

<SeriesTemplate   
                >
<ViewSerializable>
<:LineSeriesView HiddenSerializableString="to be serialized"></:LineSeriesView>
</ViewSerializable>

<LabelSerializable>
<:PointSeriesLabel HiddenSerializableString="to be serialized">
<FillStyle >
<OptionsSerializable>
<:SolidFillOptions HiddenSerializableString="to be serialized"></:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
</:PointSeriesLabel>
</LabelSerializable>

<PointOptionsSerializable>
<:PointOptions HiddenSerializableString="to be serialized"></:PointOptions>
</PointOptionsSerializable>
</SeriesTemplate>
            <SeriesSerializable>
                <cc1:Series ArgumentDataMember="Month" ArgumentScaleType="Numerical" 
                     Name="2007" 
                      
                    ValueDataMembersSerializable="BranchYTD">
                    <Points>
                        <cc1:SeriesPoint ArgumentSerializable="1" Values="87">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="2" Values="90">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="3" Values="93">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="4" Values="99">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="5" Values="102">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="6" Values="107">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="7" Values="114">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="8" Values="122">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="9" Values="131">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="10" Values="134">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="11" Values="137">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="12" Values="139">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="1" Values="606">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="2" Values="609">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="3" Values="612">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="5" Values="614">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="6" Values="617">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="7" Values="621">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="8" Values="624">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="9" Values="628">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="11" Values="632">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="12" Values="634">
                        </cc1:SeriesPoint>
                    </Points>
                    <ViewSerializable>
<:LineSeriesView HiddenSerializableString="to be serialized">
                    </:LineSeriesView>
</ViewSerializable>
                    <DataFilters>
                        <cc1:DataFilter ColumnName="Year" DataType="System.Int32" 
                            ValueSerializable="2007" />
                    </DataFilters>
                    <LabelSerializable>
<:PointSeriesLabel HiddenSerializableString="to be serialized" BackColor="255, 255, 255">
                        <FillStyle >
                            <OptionsSerializable>
<:SolidFillOptions HiddenSerializableString="to be serialized" />
</OptionsSerializable>
                        </FillStyle>
                    </:PointSeriesLabel>
</LabelSerializable>
                    <PointOptionsSerializable>
<:PointOptions HiddenSerializableString="to be serialized">
                        <ValueNumericOptions Format="Number" Precision="0" />
                    </:PointOptions>
</PointOptionsSerializable>
                </cc1:Series>
                <cc1:Series ArgumentDataMember="Month" ArgumentScaleType="Numerical" 
                     Name="2008" 
                      
                    ValueDataMembersSerializable="BranchYTD">
                    <Points>
                        <cc1:SeriesPoint ArgumentSerializable="1" Values="140">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="2" Values="141">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="3" Values="144">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="4" Values="147">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="5" Values="151">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="6" Values="155">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="7" Values="162">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="8" Values="168">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="9" Values="169">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="10" Values="174">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="11" Values="181">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="12" Values="183">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="1" Values="636">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="2" Values="638">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="3" Values="639">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="4" Values="641">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="7" Values="643">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="8" Values="644">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="9" Values="645">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="10" Values="646">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="11" Values="649">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="12" Values="651">
                        </cc1:SeriesPoint>
                    </Points>
                    <ViewSerializable>
<:LineSeriesView HiddenSerializableString="to be serialized">
                    </:LineSeriesView>
</ViewSerializable>
                    <DataFilters>
                        <cc1:DataFilter ColumnName="Year" DataType="System.Int32" 
                            ValueSerializable="2008" />
                    </DataFilters>
                    <LabelSerializable>
<:PointSeriesLabel HiddenSerializableString="to be serialized">
                        <FillStyle >
                            <OptionsSerializable>
<:SolidFillOptions HiddenSerializableString="to be serialized" />
</OptionsSerializable>
                        </FillStyle>
                    </:PointSeriesLabel>
</LabelSerializable>
                    <PointOptionsSerializable>
<:PointOptions HiddenSerializableString="to be serialized">
                        <ValueNumericOptions Format="Number" Precision="0" />
                    </:PointOptions>
</PointOptionsSerializable>
                </cc1:Series>
                <cc1:Series ArgumentDataMember="Month" ArgumentScaleType="Numerical" 
                     Name="2009" 
                      
                    ValueDataMembersSerializable="BranchYTD">
                    <Points>
                        <cc1:SeriesPoint ArgumentSerializable="1" Values="185">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="2" Values="187">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="3" Values="190">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="4" Values="193">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="5" Values="196">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="1" Values="653">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="2" Values="658">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="3" Values="661">
                        </cc1:SeriesPoint>
                        <cc1:SeriesPoint ArgumentSerializable="4" Values="666">
                        </cc1:SeriesPoint>
                    </Points>
                    <ViewSerializable>
<:LineSeriesView HiddenSerializableString="to be serialized">
                    </:LineSeriesView>
</ViewSerializable>
                    <DataFilters>
                        <cc1:DataFilter ColumnName="Year" DataType="System.Int32" 
                            ValueSerializable="2009" />
                    </DataFilters>
                    <LabelSerializable>
<:PointSeriesLabel HiddenSerializableString="to be serialized">
                        <FillStyle >
                            <OptionsSerializable>
<:SolidFillOptions HiddenSerializableString="to be serialized" />
</OptionsSerializable>
                        </FillStyle>
                    </:PointSeriesLabel>
</LabelSerializable>
                    <PointOptionsSerializable>
<:PointOptions HiddenSerializableString="to be serialized">
                        <ValueNumericOptions Format="Number" Precision="0" />
                    </:PointOptions>
</PointOptionsSerializable>
                </cc1:Series>
            </SeriesSerializable>
            <Border Visible="False" />
        </dxchartsui:WebChartControl>
        <asp:SqlDataSource ID="SqlDs" runat="server" >
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
