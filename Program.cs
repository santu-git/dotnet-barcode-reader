using System.Net;
using System.Net.Sockets;
using System.Text;
using BarCodeReader.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<TCPListenerClient>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//TCPListenerClient tcpListenerService = new TCPListenerClient();
//tcpListenerService.StartListener("127.0.0.1", 3001);

TcpListenerService tcpListenerService = new TcpListenerService();
tcpListenerService.StartListener(3000);

//var ipEndPoint = new IPEndPoint(IPAddress.Any, 3001);
//TcpListener listener = new(ipEndPoint);
//try
//{
//    listener.Start();

//    using TcpClient handler = await listener.AcceptTcpClientAsync();
//    await using NetworkStream stream = handler.GetStream();

//    var message = $"{DateTime.Now} ";
//    var dateTimeBytes = Encoding.UTF8.GetBytes(message);
//    await stream.WriteAsync(dateTimeBytes);

//    Console.WriteLine($"Sent message: \"{message}\"");
//    // Sample output:
//    //     Sent message: "📅 8/22/2022 9:07:17 AM 🕛"
//}
//catch (Exception e)
//{
//    Console.WriteLine("Error in TCP");
//    Console.WriteLine(e.Message);
//}
//finally
//{
//    Console.WriteLine("Listener Stops");
//    listener.Stop();
//}
app.Run();

