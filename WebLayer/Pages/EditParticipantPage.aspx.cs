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
using System.Threading.Tasks;

namespace WebLayer.Pages
{
    public partial class EditParticipantPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadParticipantInformation();
            }
        }

        private async void LoadParticipantInformation()
        {
            if (Request.QueryString["id"] != null)
            {
                Client.Init(new ClientProperties()
                {
                    ApiKey = "Teste"
                });

                Participant p = new Participant();

                string idParticipant = Request.QueryString["id"];

                ParticipantModel participant = await p.GetAsync(idParticipant);

                if (participant != null)
                {
                    TxtParticipantName.Text = participant.Name;
                    TxtParticipantLastName.Text = participant.LastName;
                    TxtParticipantBirth.Text = participant.Birth.ToString("yyyy-MM-dd");
                }
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

            if (Request.QueryString["id"] != null)
            {
                int id = int.Parse(Request.QueryString["id"]);
                string name = TxtParticipantName.Text;
                string lastName = TxtParticipantLastName.Text;
                string birth = TxtParticipantBirth.Text;

                ParticipantModel message = new ParticipantModel()
                {
                    Id = id,
                    Name = name,
                    LastName = lastName,
                    Birth = DateTime.Parse(birth)
                };

                ParticipantModel myClient;

                using (var par = new Participant())
                {
                    myClient = await par.PutAsync(id.ToString(), message);
                }
            }
        }
    }
}