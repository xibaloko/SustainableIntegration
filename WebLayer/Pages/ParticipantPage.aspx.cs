using IntegrationLayer;
using IntegrationLayer.Entity;
using IntegrationLayer.Lib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebLayer.Pages
{
    public partial class ParticipantPage : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadGrid();
            }
        }

        private async Task LoadGrid()
        {

            Client.Init(new ClientProperties()
            {
                ApiKey = "Teste"
            });

            Participant p = new Participant();
            
            IEnumerable<ParticipantModel> lstParticipants = await p.GetAsync();

            if (lstParticipants != null)
            {
                GrdParticipants.DataSource = lstParticipants;
                GrdParticipants.DataBind();
            }
        }

        protected void BtnAddParticipant_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/AddParticipantPage.aspx");
        }

        protected void BtnAlterParticipant_Command(object sender, CommandEventArgs e)
        {
            string idParticipant = e.CommandArgument.ToString();
            Response.Redirect($"~/Pages/EditParticipantPage.aspx?id={idParticipant}");
        }

        protected void BtnDelParticipant_Command(object sender, CommandEventArgs e)
        {
            string idParticipant = e.CommandArgument.ToString();
            HdnIdParticipant.Value = idParticipant;

            ScriptManager.RegisterStartupScript(this,GetType(), "modal", "openModalDelete();", true);

        }

        protected async void BtnConfirmDeleteParticipant_Command(object sender, CommandEventArgs e)
        {
            string idParticipant = HdnIdParticipant.Value;

            await DeleteParticipant(idParticipant);

            HdnIdParticipant.Value = string.Empty;

            await LoadGrid();
        }

        private async Task DeleteParticipant(string idParticipant)
        {
            Client.Init(new ClientProperties()
            {
                ApiKey = "Teste"
            });

            Participant p = new Participant();

            ParticipantModel participant = await p.DeleteAsync(idParticipant);
        }
    }
}