<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="ParticipantPage.aspx.cs" Inherits="WebLayer.Pages.ParticipantPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <div class="row">
            <div class="col-md-12">
                <h3>Participant List</h3>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 text-end">
                <asp:Button ID="BtnAddParticipant" runat="server" CssClass="btn btn-dark" Text="New Participant" OnClick="BtnAddParticipant_Click" />
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-12">
                <asp:GridView ID="GrdParticipants" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-light" >
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="Id" Visible="false" />
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                        <asp:BoundField HeaderText="Birth" DataField="Birth" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                        <asp:TemplateField HeaderText="Options" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Button ID="BtnAlterParticipant" runat="server" CssClass="btn btn-warning" Text="Alter" Width="30%" CommandArgument='<%#Eval("Id")%>' OnCommand="BtnAlterParticipant_Command" />
                                <asp:Button ID="BtnDelParticipant" runat="server" CssClass="btn btn-dark" Text="Delete" Width="30%" CommandArgument='<%#Eval("Id")%>' OnCommand="BtnDelParticipant_Command" />

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </section>
</asp:Content>
