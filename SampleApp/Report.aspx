<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="SampleApp.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/dataTables.dataTables.min.css" rel="stylesheet" />
    <script src="Scripts/dataTables.min.js"></script>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default" style="margin-top: 15px;">
                <div class="panel-heading">
                    <label>Registrations</label>
                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-md btn-primary" Text="Export to Excel" OnClick="btnExport_Click" Style="float: right; margin-top: -3px;" />
                </div>
                <div class="panel-body">
                    <asp:Literal ID="ltrlDataTable" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
   
</asp:Content>
