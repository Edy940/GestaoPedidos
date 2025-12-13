using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using GestaoPedidos.Application.Interfaces.Storage;
using GestaoPedidos.Infrastructure.Storage;
using GestaoPedidos.Application.Clientes.UseCases;
using GestaoPedidos.Application.Produtos.UseCases;
using GestaoPedidos.Application.Pedidos.UseCases;
using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Infrastructure.Persistence.Contexts;
using GestaoPedidos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// AWS SDK
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var regionName = config["AWS:Region"] ?? "us-east-1";
    var region = RegionEndpoint.GetBySystemName(regionName);

    return new AmazonS3Client(region);
});

builder.Services.AddScoped<IProductImageStorageService, S3ProductImageStorageService>();

// 🔹 DbContext
builder.Services.AddDbContext<GestaoPedidosDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 🔹 Repositórios (Domain → Infra)
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

// 🔹 UseCases de Cliente
builder.Services.AddScoped<CriarClienteUseCase>();
builder.Services.AddScoped<AtualizarClienteUseCase>();
builder.Services.AddScoped<ListarClientesUseCase>();
builder.Services.AddScoped<ObterClientePorIdUseCase>();
builder.Services.AddScoped<RemoverClienteUseCase>();

// 🔹 UseCases de Produto
builder.Services.AddScoped<CriarProdutoUseCase>();
builder.Services.AddScoped<AtualizarProdutoUseCase>();
builder.Services.AddScoped<ListarProdutosUseCase>();
builder.Services.AddScoped<ObterProdutoPorIdUseCase>();
builder.Services.AddScoped<RemoverProdutoUseCase>();

// 🔹 UseCases de Pedido
builder.Services.AddScoped<CriarPedidoUseCase>();
builder.Services.AddScoped<AtualizarPedidoUseCase>();
builder.Services.AddScoped<ListarPedidosUseCase>();
builder.Services.AddScoped<ObterPedidoPorIdUseCase>();
builder.Services.AddScoped<RemoverPedidoUseCase>();

// 🔹 Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
