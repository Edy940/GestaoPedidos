using GestaoPedidos.Application.Clientes.UseCases;
using GestaoPedidos.Application.Produtos.UseCases;
using GestaoPedidos.Application.Pedidos.UseCases;
using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Infrastructure.Persistence.Contexts;
using GestaoPedidos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

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
