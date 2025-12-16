using RabbitMQ.Client;
using System.Text;

namespace Seguranca.Api;

public class MensagemService 
{
    public async Task EnviarParaFila(string email) 
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

    //criando a fila de login
        await channel.QueueDeclareAsync(queue: "fila_login", durable: false, exclusive: false, autoDelete: false);

        string mensagem = $"Alerta: Login suspeito para o e-mail {email}";
        var body = Encoding.UTF8.GetBytes(mensagem);

        await channel.BasicPublishAsync(exchange: "", routingKey: "fila_login", body: body);
        Console.WriteLine(" [x] Mensagem enviada!");
    }
}