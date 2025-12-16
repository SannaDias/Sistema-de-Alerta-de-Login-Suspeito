using Seguranca.Api;

var builder = WebApplication.CreateBuilder(args);

// Configura o Swagger e o nosso serviço
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MensagemService>();

var app = builder.Build();

// Ativa o Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/login", async (string email, MensagemService mensagemService) =>
{
    await mensagemService.EnviarParaFila(email);
    return Results.Ok($"Solicitação enviada para a fila: {email}");
});

app.Run();