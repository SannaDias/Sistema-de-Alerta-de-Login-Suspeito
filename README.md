# 🛡️ Sistema de Alerta de Login Suspeito com Mensageria

Este projeto simula um sistema de segurança de alta performance que monitora acessos. Quando um login é detectado, o sistema utiliza **Mensageria (RabbitMQ)** para processar o envio de alertas/2FA de forma assíncrona, garantindo que a aplicação principal nunca fique lenta ou travada.

## 🚀 Tecnologias Utilizadas

**C# .NET 8/9** (Web API & Worker Service)
**RabbitMQ** (Message Broker)
**Docker** (Containerização do Broker)
**Swagger/OpenAPI** (Documentação e Testes da API)

## 🏗️ Arquitetura e Conceitos Aplicados

O projeto foi construído seguindo princípios de **Arquitetura Baseada em Eventos (EDA)** e boas práticas de desenvolvimento:

**Desacoplamento:** A API de Login não conhece os detalhes de envio de e-mail, apenas publica uma mensagem na fila.
**Escalabilidade:** O Worker Service pode ser escalado independentemente para processar grandes volumes de mensagens.
**Resiliência:** Se o serviço de envio falhar, as mensagens permanecem seguras no RabbitMQ para reprocessamento.
**Injeção de Dependência:** Utilizada para manter o código limpo e facilitar testes unitários.
**Programação Assíncrona:** Uso intensivo de async/await para melhor aproveitamento dos recursos do servidor.



## 🛠️ Como Executar o Projeto

### 1. Pré-requisitos
Docker Desktop instalado.
SDK do .NET instalado.

### 2. Rodar o RabbitMQ
No terminal, execute o comando para subir o container do broker:
bash

docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
