using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Services.Exeptions;
using Services.Filters;

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
        public async Task<ActionResult> AddClient(Client client)
        {
            try
            {
                await _clientService.AddClientAsync(client);
            }
            catch (AgeLimitException ex)
            {
                return new ForbidResult();
            }
            catch (ArgumentNullException ex)
            {
                return new ForbidResult();
            }

            return new OkResult();

        }

        [HttpGet]
        [Route("getClients")]
        public ActionResult<List<Client>> GetClients([FromQuery] ClientFilter filter)
        {
            return _clientService.GetClients(filter);
        }

        [HttpGet]
        public ActionResult<Client> GetClient(Guid id)
        {
            try
            {
                return _clientService.GetClient(id);
            }
            catch (NullReferenceException ex)
            {
                return new NotFoundResult();
            }
        }


        [HttpPut]
        public async Task<ActionResult> UpdateClient(Guid id, Client client)
        {
            try
            {
                await _clientService.UpdateClientAsync(id, client);
                return new OkResult();
            }
            catch (NullReferenceException ex)
            {
                return new NotFoundResult();
            }
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteClient(Guid id)
        {
            try
            {
                await _clientService.DeleteClientAsync(id);
                return new OkResult();
            }
            catch (NullReferenceException ex)
            {
                return new NotFoundResult();
            }
        }
    }
}
