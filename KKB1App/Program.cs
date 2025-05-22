using KKB1App.Components;
using KKB1App.Data;
using KKB1App.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthStateService>();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<ProgramService>();
builder.Services.AddScoped<ShowService>();
builder.Services.AddScoped<StatisticService>();
builder.Services.AddScoped<TicketHolderService>();
builder.Services.AddScoped<TicketService>();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();



app.MapGet("/statisticsapi/programs", async (AppDbContext db) =>
{
    var data = await db.Programs
        .Include(p => p.Artist)
        .Include(p => p.Shows)
        .Select(p => new
        {
            ProgramTitle = p.Title,
            Artist = p.Artist.ArtistName,
            Shows = p.Shows
                .OrderBy(s => s.DateStartTime)
                .Select(s => new { s.DateStartTime, s.TicketPrice })
        })
        .ToListAsync();

    return Results.Ok(data);
});

app.MapGet("/statisticsapi/topseller", async (AppDbContext db) =>
{
    var result = await db.Shows
        .Include(s => s.Program)
        .ThenInclude(p => p.Artist)
        .Include(s => s.Tickets)
        .GroupBy(s => new
        {
            s.ProgramId,
            ProgramTitle = s.Program.Title,
            Artist = s.Program.Artist.ArtistName
        })
        .Select(g => new
        {
            g.Key.ProgramTitle,
            g.Key.Artist,
            TotalTickets = g.SelectMany(s => s.Tickets).Count()
        })
        .OrderByDescending(x => x.TotalTickets)
        .FirstOrDefaultAsync();

    return Results.Ok(result);
});


app.Run();