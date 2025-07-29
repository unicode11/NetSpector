using System;
using System.Net.Sockets;
using NetSpector.util;

namespace NetSpector.Commands
{
    public static class Port
    {
        public static void Scan(string address, int port)
        {
            using(TcpClient client = new TcpClient())
            {
                try {
                    client.Connect(address, port);
                    Console.WriteLine($"{address}:{port} open");
                } catch (Exception) {
                    Console.WriteLine($"{address}:{port} closed");
                }
            }
        }
    }
}