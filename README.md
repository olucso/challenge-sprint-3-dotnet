# 🏍️ Motorcycle Rental API

API REST desenvolvida em ASP.NET Core para gestão de clientes, motos e locações em uma empresa de aluguel de motocicletas.
O projeto utiliza **Entity Framework Core** com **Oracle Database** como banco de dados relacional.

## 👨‍💻 Autores
Desenvolvido pelo grupo LTAKN:
- RM: 557937  –  Enzo Prado Soddano
- RM: 556564  –  Lucas Resende Lima
- RM: 559183  –  Vinicius Prates Altafini

---

## 📦 Tecnologias utilizadas

- ✅ .NET 8 / ASP.NET Core
- ✅ Entity Framework Core (mapeamento Oracle)
- ✅ SSwagger / Swashbuckle
- ✅ Oracle Database
- ✅ Repository Pattern
- ✅ DTOs + Mappers
- ✅ Rate Limiting
- ✅ Paginação (PageResultModel)
- ✅ Links HATEOAS
- ✅ Autenticação JWT (JSON Web Token)
- ✅ Health Checks com Dashboard de Monitoramento
- ✅ Machine Learning (ML.NET) para previsão de valores de locação

---

## 🧱 Estrutura da API

```
motorcycle-rental-api/
│── Controllers/               # Controladores REST
│── Data/
│   ├── AppData/               # DbContext
│   ├── Repositories/          # Repositórios e Interfaces
│── Dtos/                      # Objetos de transferência de dados
│── HealthChecks/              # Verificação de componentes do sistema
│── Mappers/                   # Extensões para conversão DTO ↔ Entidade
│── Models/                    # Entidades mapeadas no banco
│── Services/                  # Serviços da aplicação
│── MachineLearning/           # Modelos e serviços de previsão (ML.NET)
│── Program.cs                 # Configuração inicial
```

---

## 📋 Funcionalidades

- Clientes
  - CRUD completo, com paginação.
- Motocicletas
  - CRUD completo, com controle de disponibilidade.
- Locações
  - Cadastro e gerenciamento de locações.
- Paginação
  - Implementada nos métodos `GetAll`.
- HATEOAS
  - Links de navegação retornados junto aos recursos.
- Rate Limiting
  - Controle de requisições configurado nos endpoints.
- Autenticação JWT
  - Proteção dos endpoints com geração e validação de tokens.
- Health Checks
  - Monitoramento de status da API, banco de dados e conectividade externa.
- Machine Learning
  - Predição automática do valor total de locações com base em dados históricos (dias, valor diário e fidelidade do cliente), utilizando ML.NET e modelo de regressão linear (SDCA).

---

## ⚙️ Configuração do Banco de Dados

No arquivo `appsettings.json` configure sua conexão com o Oracle:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=seu_usuario;Password=sua_senha;Data Source=localhost:1521/XEPDB1;"
  }
}
```

---

## 🧪 Testes Automatizados

A solução contém testes automatizados em xUnit, divididos em:
- 🧱 Model Tests: Testam as entidades (`ClientEntity`, `MotorcycleEntity`, `RentalEntity`) e suas validações (`[Required]`, `[StringLength]`, etc.).
- 🌐 Integration Tests: Utilizam `WebApplicationFactory<ApiMarker>` para testar endpoints reais da API (exemplo: `GET /api/v1/Motorcycle`).

## 🧰 Estrutura dos Arquivos de Teste
```
motorcycle_rental_api.Tests/
├── App/
│   ├── ClientEntityTest.cs
│   ├── MotorcycleEntityTest.cs
│   ├── RentalEntityTest.cs
│
├── ControllerTests.cs
```

## ▶️ Como Executar os Testes

1️⃣ Pré-requisitos
- SDK .NET 8.0+ instalado
  Verifique com:
```
dotnet --version
```
- A API deve compilar corretamente (sem erros no projeto principal `motorcycle_rental_api`).

2️⃣ Restaurar Dependências
- No diretório raiz da solução (`.sln`):
```
dotnet restore
```

3️⃣ Executar Todos os Testes
- Use o comando abaixo para rodar todos os testes (unitários e de integração):
```
dotnet test
```

📋 O que acontece:
- O .NET compila a solução.
- Executa automaticamente todos os testes [Fact] e [Theory] com o xUnit.
- Mostra no console os resultados de sucesso/falha.

4️⃣ Executar Apenas um Conjunto de Testes
👉 Testes de Entidades (Validações)
```
dotnet test --filter "FullyQualifiedName~ClientEntityTest"
dotnet test --filter "FullyQualifiedName~MotorcycleEntityTest"
dotnet test --filter "FullyQualifiedName~RentalEntityTest"
```

👉 Teste de Controller (Integração)
```
dotnet test --filter "FullyQualifiedName~ControllerTests"
```

5️⃣ Resultado Esperado
Se tudo estiver configurado corretamente, o terminal mostrará algo como:
```
Starting test execution, please wait...
Passed!  - Failed: 0, Passed: 25, Skipped: 0, Total: 25, Duration: 2 s
```

---

## 🛡️ Autenticação JWT (JSON Web Token)

A API utiliza autenticação baseada em tokens JWT para garantir segurança e controle de acesso aos endpoints protegidos.
Somente usuários autenticados podem realizar operações como criação, atualização ou exclusão de recursos.

🔧 Configuração
No arquivo `appsettings.json`, adicione as configurações do JWT:
```
"Jwt": {
  "Key": "sua_chave_secreta_super_segura_aqui",
  "Issuer": "MotorcycleRentalAPI",
  "Audience": "MotorcycleRentalClient",
  "ExpireMinutes": 60
}
```
Essas informações são utilizadas para assinar e validar os tokens gerados.

---

## 👤 Endpoint de Login

O endpoint de login (`/api/Auth/login`) é responsável por autenticar o usuário e gerar o token JWT.

Requisição:
```
POST /api/Auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "123456"
}
```

Resposta:
```
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiration": "2025-11-07T22:00:00Z"
}
```

---

## 🔐 Utilizando o Token no Swagger

1. Após rodar a aplicação e acessar o Swagger (`http://localhost:5166/swagger`), clique no botão "Authorize" (ícone de cadeado).

2. No campo exibido, insira o token obtido no login, precedido de `Bearer `.

```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

3. Clique em Authorize para autenticar.
   Agora todos os endpoints protegidos poderão ser acessados.

---

## 🔒 Proteção dos Endpoints

Os controladores ou métodos que requerem autenticação possuem o atributo [Authorize].
Exemplo:

```
[Authorize]
[HttpPost]
public async Task<IActionResult> Create(MotorcycleDto dto)
{
    ...
}
```

Endpoints públicos (como `/api/Auth/login`) permanecem acessíveis sem token, marcados com `[AllowAnonymous]`.

---

## 🧩 Vantagens do JWT

- Tokens stateless: não exigem sessão no servidor.
- Assinatura digital garante integridade dos dados.
- Fácil integração com clientes web e mobile.
- Suporte nativo no ASP.NET Core.

---

## 🔑 Usuário padrão para testes

Durante o desenvolvimento, um usuário padrão é utilizado para login de testes:

```
Usuário: admin  
Senha: 123456
```
Esse usuário é criado em memória (mock) apenas para fins de autenticação e não é armazenado no banco de dados.

---

## 🩺 Health Checks e Dashboard de Monitoramento

A API possui um sistema de monitoramento de saúde (Health Checks) integrado, que verifica continuamente o funcionamento dos principais componentes do sistema — incluindo banco de dados, API e conectividade externa (FIAP).
Essa funcionalidade permite detectar falhas de forma proativa e visualizar o status da aplicação em tempo real através de um dashboard gráfico interativo.

## 🔍 Componentes Monitorados

- Oracle Database
  - Verifica se a conexão com o banco está ativa e responsiva.
- FIAP Health Check
  - Testa a conectividade externa com o site oficial da FIAP para avaliar conectividade de rede.
- API Health Check
  - Avalia se a própria aplicação está ativa e processando requisições corretamente.

## ⚙️ Implementação Técnica
Os Health Checks são configurados no `Program.cs`:
```
builder.Services.AddHealthChecks()
    .AddOracle(builder.Configuration.GetConnectionString("Oracle"), name: "Health Check Database")
    .AddCheck<FIAPHealthCheck>("FIAP Health Check");

builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(5);
    options.MaximumHistoryEntriesPerEndpoint(5);
    options.AddHealthCheckEndpoint("API Health Check", "/health");
}).AddInMemoryStorage();
```

A classe `FIAPHealthCheck` está localizada em `HealthChecks/FIAPHealthCheck.cs` e executa uma requisição HTTP para validar a resposta do site da FIAP:
```
using Microsoft.Extensions.Diagnostics.HealthChecks;

public class FIAPHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = "https://www.fiap.com.br";
            using HttpClient client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = true });
            using var response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
                return Task.FromResult(HealthCheckResult.Healthy("Sistema Funcionando."));
            else
                return Task.FromResult(HealthCheckResult.Degraded("O sistema não está funcionando."));
        }
        catch
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("Sistema fora do ar."));
        }
    }
}
```

## 🧭 Endpoints Disponíveis
`/health`	- Retorna o status detalhado da API e dos serviços monitorados em formato JSON.
`/dashboard`	- Interface visual (HealthChecks UI) com histórico e status gráfico.

## 🧠 Exemplo de resposta do endpoint /health
```
{
  "status": "Healthy",
  "totalDuration": "00:00:01.032",
  "entries": {
    "Health Check Database": {
      "status": "Healthy",
      "description": "Conexão com o Oracle estável"
    },
    "FIAP Health Check": {
      "status": "Healthy",
      "description": "Sistema Funcionando."
    }
  }
}
```

## 🖥️ Acessando o Dashboard
1. Inicie a aplicação:
```
dotnet run
```
2. Acesse no navegador:
```
http://localhost:5166/dashboard
```
3. O painel exibirá em tempo real:
- Status geral da API
- Conexão com o Oracle
- Verificação da FIAP
- Histórico das últimas verificações

## 💡 Benefícios
- Monitoramento em tempo real do estado da API.
- Integração com o HealthChecks.UI, que permite visualizar status e histórico.
- Detecção rápida de falhas no banco de dados ou serviços externos.
- Base para integração futura com sistemas de observabilidade como Grafana, Prometheus ou Azure Application Insights.

---

## 🧠 Machine Learning (Previsão de Valor de Locação)

A API conta com um módulo de Machine Learning integrado, desenvolvido com ML.NET, capaz de prever o valor total estimado de uma locação com base em variáveis históricas como dias de aluguel, valor diário e fidelidade do cliente.
Essa funcionalidade demonstra a integração de IA preditiva diretamente na camada de negócios da aplicação, tornando o sistema mais inteligente e responsivo.

## ⚙️ Funcionamento Técnico

O modelo utiliza regressão linear (SDCA Regression Trainer) para aprender padrões a partir de dados de locações anteriores.
O pipeline é montado e treinado automaticamente na inicialização da aplicação, sem necessidade de intervenção manual.

🔹 Estrutura dos Arquivos
```
MachineLearning/
│── RentalPredictionService.cs   # Serviço principal de ML.NET (treino e predição)
│── Models/
│   └── RentalPredictionModel.cs # Classes de entrada (input) e saída (prediction)
```

🔹 Classe de Entrada (RentalInputModel)
Representa os dados utilizados como base para o cálculo:
```
public class RentalInputModel
{
    public float Days { get; set; }
    public float DailyValue { get; set; }
    public float ClientFidelity { get; set; }

    // Valor total real (usado apenas no treinamento)
    public float TotalValue { get; set; }
}
```

🔹 Classe de Saída (RentalPrediction)
Retorna o valor estimado da locação:
```
public class RentalPrediction
{
    public float Score { get; set; } // Valor total previsto
}
```

🔹 Serviço de Predição
O serviço RentalPredictionService é responsável por:
- Criar e treinar o modelo com dados de exemplo;
- Definir o pipeline de features (Days, DailyValue, ClientFidelity);
- Executar a predição com base em novos valores de entrada.

```
var pipeline = _mlContext.Transforms.Concatenate(
                    "Features",
                    nameof(RentalInputModel.Days),
                    nameof(RentalInputModel.DailyValue),
                    nameof(RentalInputModel.ClientFidelity))
               .Append(_mlContext.Regression.Trainers.Sdca(
                    labelColumnName: nameof(RentalInputModel.TotalValue),
                    maximumNumberOfIterations: 100));
```

## 🔍 Endpoint de Predição
A API expõe um endpoint para realizar a previsão diretamente pelo Swagger:
```
POST /api/Prediction
Content-Type: application/json
```

Exemplo de requisição:
```
{
  "days": 3,
  "dailyValue": 90,
  "clientFidelity": 1
}
```

Resposta:
```
{
  "predictedTotalValue": 275.45
}
```

## 🧩 Lógica de Cálculo
Durante o treinamento, o modelo aprende padrões como:
- Maior número de dias → aumento linear no total.
- Descontos aplicados a clientes fiéis (ClientFidelity = 1).
- Variação de tarifas diárias por tipo de moto ou data.
Essas relações são modeladas por uma regressão linear multivariável, permitindo que o sistema generalize novas situações de locação com boa precisão.

---

## ▶️ Como executar o projeto

1. Clone o repositório:
- `git clone https://github.com/seu-repositorio/motorcycle-rental-api.git`
- `cd motorcycle-rental-api`

2. Restaure dependências:
- `dotnet restore`

3. Rode a aplicação:
- `dotnet run`

Acesse o Swagger em:

```
http://localhost:5166/swagger
```

---

## 📖 Exemplos de Uso dos Endpoints

🔹 Clientes

Cadastrar Cliente
```
POST /api/Client
Content-Type: application/json

{
  "name": "João Silva",
  "cpf": "12345678901",
  "street": "Rua das Flores",
  "houseNumber": 123,
  "district": "Centro",
  "city": "São Paulo",
  "state": "SP"
}
```

Resposta
```
{
  "data": {
    "id": 1,
    "name": "João Silva",
    "cpf": "12345678901",
    "street": "Rua das Flores",
    "houseNumber": 123,
    "district": "Centro",
    "city": "São Paulo",
    "state": "SP"
  },
  "links": {
    "self": "http://localhost:5000/api/Client/1",
    "getAll": "http://localhost:5000/api/Client",
    "put": "http://localhost:5000/api/Client/1",
    "delete": "http://localhost:5000/api/Client/1"
  }
}
```

##

🔹 Motocicletas

Cadastrar Moto
```
POST /api/Motorcycle
Content-Type: application/json

{
  "brand": "Honda",
  "model": "CG 160 Start",
  "plate": "ELH6668",
  "manufacturingYear": 2019,
  "dailyValue": 50.20,
  "availability": true
}

```

Resposta
```
{
  "data": {
    "id": 2,
    "brand": "Honda",
    "model": "CG 160 Start",
    "plate": "ELH6668",
    "manufacturingYear": 2019,
    "dailyValue": 50.20,
    "availability": true
  },
  "links": {
    "self": "http://localhost:5000/api/Motorcycle/2",
    "getAll": "http://localhost:5000/api/Motorcycle",
    "put": "http://localhost:5000/api/Motorcycle/2",
    "delete": "http://localhost:5000/api/Motorcycle/2"
  }
}
```

##

🔹 Locações

Criar Locação
```
POST /api/Rental
Content-Type: application/json

{
  "clientId": 1,
  "motorcycleId": 2,
  "startDate": "2025-10-01T10:00:00",
  "endDate": "2025-10-05T10:00:00"
}
```

Resposta
```
{
  "data": {
    "id": 3,
    "clientId": 1,
    "motorcycleId": 2,
    "startDate": "2025-10-01T10:00:00",
    "endDate": "2025-10-05T10:00:00"
  },
  "links": {
    "self": "http://localhost:5000/api/Rental/3",
    "getAll": "http://localhost:5000/api/Rental",
    "put": "http://localhost:5000/api/Rental/3",
    "delete": "http://localhost:5000/api/Rental/3"
  }
}
```

---

## 🏗️ Justificativa da Arquitetura

A arquitetura foi desenhada seguindo boas práticas de APIs REST e separação de responsabilidades:

1. Repository Pattern
    - Separa a lógica de acesso ao banco da lógica de negócio.
    - Facilita manutenção, testes unitários e futuras trocas de banco (ex: Oracle → SQL Server).

2. DTOs + Mappers
    - Garante segurança e desacoplamento entre a entidade de banco e os dados expostos.
    - Permite controlar exatamente quais informações trafegam na API.

3. Entity Framework Core + Oracle
    - Simplifica o mapeamento objeto-relacional.
    - Aproveita recursos do Oracle (performance, escalabilidade).
    - Configuração de decimal com HasPrecision e bool convertido para NUMBER garante compatibilidade.

4. Swagger
    - Documentação automática dos endpoints, facilitando testes e integração.

5. HATEOAS
    - Fornece links de navegação junto às respostas, seguindo princípios RESTful.
    - Melhora a experiência do consumidor da API.

6. Rate Limiting
    - Evita sobrecarga e abuso da API.
    - Mantém segurança e controle de uso.

Essa abordagem torna o sistema modular, escalável e fácil de evoluir, podendo futuramente receber autenticação (JWT), logging, cache distribuído e deploy com Docker/Kubernetes.

---
