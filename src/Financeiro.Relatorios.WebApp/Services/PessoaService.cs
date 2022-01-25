using Financeiro.Relatorios.WebApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using Newtonsoft.Json;

namespace Financeiro.Relatorios.WebApp.Services
{
    public class PessoaService
    {
        private HttpClient _client;

        public PessoaService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlApi"]);
        }

        public async Task<IEnumerable<PessoaDto>> ObterTodos()
        {
            HttpResponseMessage response = await _client.GetAsync("/Pessoa/obterTodos");

            if (!response.IsSuccessStatusCode)
                return null;

            var results = JsonConvert.DeserializeObject<IEnumerable<PessoaDto>>(await response.RequestMessage.Content.ReadAsStringAsync());

            return results;
        }

    }
}