using System;
using AplicacaoCliente.Util;
using Newtonsoft.Json;

namespace AplicacaoCliente.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data_Cadastro { get; set; }
        public string Cpf_Cnpj { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string Tipo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

        public List<ClienteModel> ListarTodosClientes()
        {
            List<ClienteModel> retorno = new List<ClienteModel>();
            string json = WebAPI.RequestGET("listagem", string.Empty);

            Console.WriteLine("Resposta da API: " + json);

            if (!json.StartsWith("Erro"))
            {
                retorno = JsonConvert.DeserializeObject<List<ClienteModel>>(json);
            }

            return retorno;
        }

        public ClienteModel Carregar(int? id)
        {
            ClienteModel retorno = new ClienteModel();
            string json = WebAPI.RequestGET(string.Empty, id.ToString());

            Console.WriteLine("JSON recebido: " + json);

            if (!json.StartsWith("Erro"))
            {
                retorno = JsonConvert.DeserializeObject<ClienteModel>(json);
                Console.WriteLine("Cliente carregado: " + retorno.Nome);
            }
            else
            {
                Console.WriteLine("Erro ao carregar cliente!");
            }

            return retorno;
        }

        public void Atualizar()
        {
            string jsonData = JsonConvert.SerializeObject(this);
            string json = WebAPI.RequestPUT("Atualizar/" + Id, jsonData);
        }


        public void Inserir()
        {
            string jsonData = JsonConvert.SerializeObject(this);
            string json = WebAPI.RequestPOST("RegistrarCliente", jsonData);
        }

        public void Excluir(int id)
        {
            string json = WebAPI.RequestDELETE("excluir", id.ToString());
        }


    }
}
