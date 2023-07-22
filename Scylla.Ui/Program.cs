using Serilog;
using Scylla.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((ctx, lc) => lc
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));


builder.Services.AddControllers();

builder.Services.AddServices(builder.Configuration);


try
{
    var app = builder.Build();

    app.UseSwagger();

    app.UseSwaggerUI();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception err)
{
    Log.Error("the application has not been launched and exited with folloing error : {0}", err);
}
finally
{
    Log.CloseAndFlush();
}





