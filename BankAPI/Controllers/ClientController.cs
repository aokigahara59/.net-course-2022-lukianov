using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("ClientController")]
    public class ClientController
    {
        private ClientService _clientService;

        public ClientController()
        {
            _clientService = new ClientService();
        }


        [HttpPost]
        public async void AddClient(Client client)
        {
            await _clientService.AddClientAsync(client);
        }


        [HttpGet]
        public Client GetClient(Guid id)
        {
            return _clientService.GetClient(id);
        }


        [HttpPut]
        public async void UpdateClient(Guid id, Client client)
        {
            await _clientService.UpdateClientAsync(id, client);
        }


        [HttpDelete]
        public async void DeleteClient(Guid id)
        {
            await _clientService.DeleteClientAsync(id);
        }
    }
}
