using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Seguranca.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger) => _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        // Declaramos a mesma fila que a API usa
        await channel.QueueDeclareAsync(queue: "fila_login", durable: false, exclusive: false, autoDelete: false);

        _logger.LogInformation(" [*] Worker aguardando mensagens. Para sair, pressione CTRL+C");

        var consumer = new AsyncEventingBasicConsumer(channel);
        
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var mensagem = Encoding.UTF8.GetString(body);

            _logger.LogInformation(" [x] MENSAGEM RECEBIDA: {0}", mensagem);
            _logger.LogInformation(" [!] Processando envio de 2FA...");
            
            await Task.Delay(2000); // Simulando o tempo de disparo do e-mail
            
            _logger.LogInformation(" [V] Alerta enviado com sucesso para o usuário!");
        };

        await channel.BasicConsumeAsync(queue: "fila_login", autoAck: true, consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}