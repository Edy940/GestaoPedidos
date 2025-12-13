📦 GestaoPedidos

Projeto backend desenvolvido em .NET com foco em boas práticas de arquitetura, integração com AWS, Infraestrutura como Código (Terraform) e CI/CD com GitHub Actions.

O objetivo do projeto é demonstrar domínio técnico em cenários reais de mercado, servindo como case de portfólio e base para entrevistas técnicas.

🎯 Visão Geral

O GestaoPedidos é uma API REST responsável pela gestão de:

Clientes

Produtos (com upload de imagem)

Pedidos

A aplicação foi construída seguindo princípios de Clean Architecture, SOLID e separação de responsabilidades, integrando serviços da AWS como RDS (PostgreSQL) e S3.

🧱 Arquitetura

O projeto segue a Clean Architecture, com as seguintes camadas:

src/
 ├── GestaoPedidos.Domain        → Entidades e regras de negócio
 ├── GestaoPedidos.Application   → Casos de uso (UseCases) e interfaces
 ├── GestaoPedidos.Infrastructure→ Repositórios, EF Core, AWS (S3)
 └── GestaoPedidos.Api           → Controllers e configuração da API

Responsabilidades por camada

Domain

Entidades

Regras de negócio

Validações de domínio

Application

Casos de uso (ex: CriarPedido, CriarProduto)

Interfaces de repositórios e serviços externos

DTOs de entrada e saída

Infrastructure

Implementação dos repositórios

EF Core (PostgreSQL)

Integração com AWS S3

API

Endpoints REST

Upload de arquivos

Injeção de dependências

Swagger

⚙️ Tecnologias Utilizadas
Backend

.NET

ASP.NET Core Web API

Entity Framework Core

PostgreSQL

Clean Architecture

SOLID

Cloud (AWS)

RDS (PostgreSQL)

S3 (armazenamento de imagens)

IAM (credenciais)

VPC / Security Groups

Infraestrutura como Código

Terraform

CI/CD

GitHub Actions

Build

Testes

Terraform Plan

Terraform Apply manual (workflow_dispatch)

☁️ Infraestrutura AWS

A infraestrutura é criada via Terraform, incluindo:

VPC

Subnets públicas

Security Group

RDS PostgreSQL

Bucket S3 para imagens de produtos

📁 Infra localizada em:

infra/rds

Criar infraestrutura
cd infra/rds
terraform init
terraform plan
terraform apply

🖼 Upload de Imagens (S3)

As imagens dos produtos são enviadas via multipart/form-data

A API realiza o upload no S3

Apenas a chave do arquivo (FotoKey) é persistida no banco

A API utiliza uma abstração (IProductImageStorageService), permitindo troca futura do provider

📌 Endpoints Principais
Produtos

POST /api/Produtos → Criar produto (com upload de imagem)

GET /api/Produtos → Listar produtos

GET /api/Produtos/{id} → Obter produto por ID

PUT /api/Produtos/{id} → Atualizar produto

DELETE /api/Produtos/{id} → Remover produto

Clientes

CRUD completo

Pedidos

CRUD completo

Relacionamento N produtos para 1 pedido

🧪 Testes

Testes unitários na camada Domain

Validação de regras de negócio

Executar testes:

dotnet test

🔄 CI/CD

O pipeline do GitHub Actions executa:

Build da aplicação

Testes automatizados

Terraform Plan

Terraform Apply manual

O apply é feito apenas via workflow_dispatch, garantindo controle da infraestrutura.

🔐 Segurança

Credenciais não versionadas

Uso de .gitignore

Secrets configurados no GitHub Actions

GitGuardian utilizado para detecção de secrets

▶️ Como rodar localmente
dotnet restore
dotnet build
dotnet run --project src/GestaoPedidos.Api


Acesse:

http://localhost:5080/swagger

📚 Objetivo do Projeto

Este projeto foi desenvolvido com foco em:

Prática real de mercado

Preparação para entrevistas técnicas

Demonstração de domínio em:

Backend .NET

Arquitetura limpa

AWS

Terraform

CI/CD

🚀 Próximos Passos (possíveis evoluções)

Arquitetura orientada a eventos (SNS / SQS)

Microsserviços

Autenticação (JWT / Cognito)

Observabilidade (logs, métricas)

👤 Autor

Ben (Edy940)
Projeto desenvolvido para fins de estudo, portfólio e evolução profissional.