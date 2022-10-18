using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Services.Filters;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("ClientServiceController")]
    public class ClientServiceController
    {
        private ClientService _clientService;

        public ClientServiceController()
        {
            _clientService = new ClientService();
        }


        [HttpPost(Name = "AddClient")]
        public async void AddClient(Client client)
        {
            await _clientService.AddClientAsync(client);
        }


        [HttpGet(Name = "GetClient")]
        public Client GetClient(Guid id)
        {
            return _clientService.GetClient(id);
        }


        [HttpPut(Name = "UpdateClient")]
        public async void UpdateClient(Guid id, Client client)
        {
            await _clientService.UpdateClientAsync(id, client);
        }


        [HttpDelete(Name = "DeleteClient")]
        public async void DeleteClient(Guid id)
        {
            await _clientService.DeleteClientAsync(id);
        }
    }
}
