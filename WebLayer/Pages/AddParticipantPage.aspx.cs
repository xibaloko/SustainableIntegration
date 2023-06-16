using IntegrationLayer.Entity;
using IntegrationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegrationLayer.Lib;
using IntegrationLayer.Request;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WebLayer.Pages
{
    public partial class AddParticipantPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected async void BtnSaveData_ClickAsync(object sender, EventArgs e)
        {
            await SaveParticipant();

            Response.Redirect("~/Pages/ParticipantPage");
        }

        private async Task SaveParticipant()
        {
            Client.Init(new ClientProperties()
            {
                ApiKey = "Teste"
            });

            string name = TxtParticipantName.Text;
            string lastName = TxtParticipantLastName.Text;
            string birth = TxtParticipantBirth.Text;

            ParticipantRequestMessage message = new ParticipantRequestMessage()
            {
                Name = name,
                LastName = lastName,
                Birth = DateTime.Parse(birth)
            };

            ParticipantModel myClient;

            using (var par = new Participant())
            {
                myClient = await par.CreateAsync(message,null);
            }
            
        }
    }
}