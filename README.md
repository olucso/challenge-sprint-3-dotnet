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

---

## 🧱 Estrutura da API

```sql
motorcycle-rental-api/
│── Controllers/        # Controladores REST
│── Data/
│   ├── AppData/        # DbContext
│   ├── Repositories/   # Repositórios e Interfaces
│── Dtos/               # Objetos de transferência de dados
│── Mappers/            # Extensões para conversão DTO ↔ Entidade
│── Models/             # Entidades mapeadas no banco
│── Program.cs          # Configuração inicial
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
