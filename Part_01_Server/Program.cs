using System.Net;
using System.Net.Sockets;
using System.Text;
using Part_01_Server.sourse;


public class Program
{
    async public static Task Main()
    {
        var tcpListener = new TcpListener(IPAddress.Any, 8888);

        tcpListener.Start();
        Console.WriteLine("Сервер запущен. Ожидание подключений... ");
        try
        {
            while (true)
            {
                using var tcpClient = await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine($"Входящее подключение: {tcpClient.Client.RemoteEndPoint}");
            
            var stream = tcpClient.GetStream();
                byte[] data = new byte[256];
                await stream.ReadAsync(data);
                var s = Encoding.UTF8.GetString(data);
                using (var context = new StockContext())
                {
                    var ticker = context.Tickers.FirstOrDefault(t => t.TickerSymbol == s);
                    var price = context.Prices.FirstOrDefault(p => p.TickerId == ticker.Id).PriceAfter;
                    await stream.WriteAsync(Encoding.UTF8.GetBytes(Convert.ToString(price)));
                }
            }
        }
        finally
        {
            tcpListener.Stop();
        }

    }
}