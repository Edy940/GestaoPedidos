# GestaoPedidos — API .NET + AWS RDS + Terraform

API RESTful para gestão de **Clientes**, **Produtos** e **Pedidos**, construída em **.NET** com foco em:

- Clean Architecture / DDD
- Boas práticas (SOLID, separação de camadas, use cases)
- Infraestrutura como código com **Terraform**
- Banco relacional em **AWS RDS (PostgreSQL)**
- CI com **GitHub Actions** (build, testes e `terraform plan`)

> Projeto criado para estudo e portfólio focado em vagas .NET + AWS + DevOps.

---

## Arquitetura

Camadas principais:

- **Domain**  
  Entidades, regras de negócio e testes de domínio.

- **Application**  
  Casos de uso (use cases), DTOs, interfaces de repositório.

- **Infrastructure**  
  Implementação de repositórios, DbContext (EF Core + PostgreSQL), Migrations.

- **API**  
  Controllers REST (Clientes, Produtos, Pedidos), exposição via Swagger.

- **Infra (Terraform)**  
  Infraestrutura de rede + banco na AWS:
  - VPC
  - Subnets públicas
  - Internet Gateway
  - Route Table
  - Security Group
  - RDS PostgreSQL

---

## Tecnologias

- **Backend**
  - .NET / C#
  - ASP.NET Core Web API
  - Entity Framework Core + Npgsql (PostgreSQL)
  - xUnit (testes de domínio)

- **Cloud / DevOps**
  - AWS RDS (PostgreSQL)
  - AWS VPC, Subnet, Security Group
  - Terraform (Infra as Code)
  - GitHub Actions (CI: build + test + terraform plan)

---

## Estrutura de pastas (simplificada)

```text
src/
  GestaoPedidos.Api/
  GestaoPedidos.Application/
  GestaoPedidos.Domain/
  GestaoPedidos.Infrastructure/

tests/
  GestaoPedidos.Domain.Tests/

infra/
  rds/
    provider.tf
    variables.tf
    vpc.tf
    security-group.tf
    subnet-group.tf
    rds.tf
    outputs.tf
