using FoodOrderBAL.Interfaces;
using FoodOrderBAL.Services;
using FoodOrderDAL.Repositories.IRepo;
using FoodOrderDAL.Repositories.ServiceRepo;
using FoodOrdering.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IApprovedUser, ApprovedUserSerivce>();
builder.Services.AddScoped<IApprovedUserRepo, ApprovedUserRepo>();
builder.Services.AddScoped<IPendingUser,PendingUserService>();
builder.Services.AddScoped<IPendingUserRepo, PendingUserRepo>();
builder.Services.AddScoped<IAddHotelBranch, AddHotelBranchService>();
builder.Services.AddScoped<IAddBranchRepo, AddBranchRepo>();
builder.Services.AddScoped<IAddMenu,AddMenuService>();
builder.Services.AddScoped<IAddMenuRepo, AddMenuRepo>();
builder.Services.AddTransient<IMailRepo, MailSer>();
builder.Services.AddScoped<IAddHotel, AddHotel>();
builder.Services.AddScoped<IAddHotelRepo,AddHotelServiceRepo>();
builder.Services.AddScoped<IUserViewMenu,UserViewMenu>();
builder.Services.AddScoped<IUserViewMenuRepo,UserViewMenusRepo>();
builder.Services.AddScoped<IOrder,OrderService>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddTransient<GlobalExceptionMiddleware>();



builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
