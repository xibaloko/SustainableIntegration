using IntegrationLayer.Entity;
using IntegrationLayer.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLayer.Lib
{
    public class Participant : APIResource
    {
        public Participant()
        {
            BaseURI = "/participant";
        }

        public async Task<IEnumerable<ParticipantModel>> GetAsync()
        {
            var retorno = await GetAsync<IEnumerable<ParticipantModel>>().ConfigureAwait(false);
            return retorno;
        }

        public async Task<ParticipantModel> GetAsync(string id)
        {
            var retorno = await GetAsync<ParticipantModel>(id).ConfigureAwait(false);
            return retorno;
        }

        public async Task<ParticipantModel> CreateAsync(ParticipantRequestMessage request, string customApiToken)
        {
            var retorno = await PostAsync<ParticipantModel>(request, null, customApiToken).ConfigureAwait(false);
            return retorno;
        }

        public async Task<ParticipantModel> PutAsync(string id, ParticipantModel model)
        {
            var retorno = await PutAsync<ParticipantModel>(id, model).ConfigureAwait(false);
            return retorno;
        }

        public async Task<ParticipantModel> DeleteAsync(string id)
        {
            var retorno = await DeleteAsync<ParticipantModel>(id).ConfigureAwait(false);
            return retorno;
        }
    }
}
