<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditParticipantPage.aspx.cs" Async="true" Inherits="WebLayer.Pages.EditParticipantPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section>
                <div class="row mt-4">
                    <div class="col-md-12">
                        <h3>Fill the fields with the participant information.</h3>
                    </div>
                </div>
                <div class="row mt-4">
                    <div class="col-md-3">
                        <label for="TxtParticipantName" class="form-label">Name:</label>
                        <asp:TextBox ID="TxtParticipantName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label for="TxtParticipantLastName" class="form-label">Last Name:</label>
                        <asp:TextBox ID="TxtParticipantLastName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label for="TxtParticipantBirth" class="form-label">Birth:</label>
                        <asp:TextBox ID="TxtParticipantBirth" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mt-4">
                    <div class="col-md-12 text-end">
                        <asp:Button ID="BtnSaveData" runat="server" CssClass="btn btn-dark" Text="Save" Width="10%" OnClick="BtnSaveData_ClickAsync" />
                        <a href="ParticipantPage.aspx" class="btn btn-secondary" style="width: 10%">Back</a>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
