using System.IO;
using System.Net;
using System.Text;

namespace AplicacaoCliente.Util
{
    public class WebAPI
    {
        public static string URI = "https://localhost:7050/api/Clientes/";
        public static string TOKEN = "123";

        private static string RequestGET_DELETE(string metodo, string parametro, string tipo)
        {
            try
            {
                string url = URI;

                if (!string.IsNullOrEmpty(metodo))
                    url += metodo + "/";

                url += parametro;

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("Token", TOKEN);
                request.Method = tipo;
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    var responseString = stream.ReadToEnd();
                    return responseString;
                }
            }
            catch (WebException ex)
            {
                return "Erro ao consumir API: " + ex.Message;
            }
        }

        public static string RequestGET(string metodo, string parametro)
        {
            try
            {
                string url = URI;

                if (!string.IsNullOrEmpty(metodo))
                    url += metodo;

                if (!string.IsNullOrEmpty(parametro))
                {
                    if (!url.EndsWith("/"))
                        url += "/";
                    url += parametro;
                }

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("Token", TOKEN);
                request.Method = "GET";

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    var responseString = stream.ReadToEnd();
                    return responseString;
                }
            }
            catch (WebException ex)
            {
                using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string errorResponse = reader.ReadToEnd();
                    Console.WriteLine("Erro da API: " + errorResponse);
                    return "Erro da API: " + errorResponse;
                }
            }
        }




        //DELETE
        public static string RequestDELETE(string metodo, string parametro)
        {
            return RequestGET_DELETE(metodo, parametro, "DELETE");
        }


        //PUT
        public static string RequestPUT(string metodo, string jsonData)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(jsonData);

                var request = (HttpWebRequest)WebRequest.Create(URI + metodo);
                request.Method = "PUT";
                request.Headers.Add("Token", TOKEN);
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                using (var responseStream = new StreamReader(response.GetResponseStream()))
                {
                    var responseString = responseStream.ReadToEnd();
                    return responseString;
                }
            }
            catch (WebException ex)
            {
                using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string errorResponse = reader.ReadToEnd();
                    return "Erro da API: " + errorResponse;
                }
            }
        }


        public static string RequestPOST(string metodo, string jsonData)
        {
            //POST
            try
            {
                //var json = "\"valor1\"";  // Apenas o valor como string, sem chave JSON

                var data = Encoding.UTF8.GetBytes(jsonData);

                var request = (HttpWebRequest)WebRequest.Create(URI + metodo);
                request.Method = "POST";
                request.Headers.Add("Token", TOKEN);
                request.ContentType = "application/json";  // Certifique-se de que o Content-Type seja application/json
                request.ContentLength = data.Length;

                // Enviando os dados para a API
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }

                // Obtendo a resposta da API
                var response = (HttpWebResponse)request.GetResponse();
                using (var responseStream = new StreamReader(response.GetResponseStream()))
                {
                    var responseString = responseStream.ReadToEnd();
                    return responseString;
                }
            }
            catch (WebException ex)
            {
                using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string errorResponse = reader.ReadToEnd();
                    return "Erro da API: " + errorResponse;
                }
            }
        }
    }

}
