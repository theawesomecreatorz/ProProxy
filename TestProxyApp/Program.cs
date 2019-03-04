using ProProxy;
using System;
using System.Linq;

namespace TestProxyApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var prox = new ProxyCore();
            Console.WriteLine("Load Default Proxies ... ");
            prox.LoadDefaultProxies();
            Console.WriteLine($"Loaded: {prox.Proxies.Count()}\nPress Enter for random Proxy");
            Console.ReadLine();
            var rndmProx = prox.RandomProxy();
            Console.WriteLine($"Connected to: {rndmProx.ProxyHost} \nPress Any Key for Disable Proxy");
            Console.ReadKey();
            prox.DisableProxy();
        }
    }
}