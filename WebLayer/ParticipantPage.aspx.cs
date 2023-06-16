using IntegrationLayer;
using IntegrationLayer.Entity;
using IntegrationLayer.Lib;
using System;
using System.Collections.Generic;

namespace WebLayer
{
    public partial class ParticipantPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
            }
        }

        private async void LoadGrid()
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

        }
    }
}