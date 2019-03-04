using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProProxy
{
    public class ProxyCore : IProxyCore
    {
        private readonly string _defaultProxiesResourceName;
        private readonly Random _rndm;

        public ProxyCore()
        {
            _defaultProxiesResourceName = "ProProxy.default.txt";
            Proxies = new List<IProxy>();
            _rndm = new Random();
        }

        public ICollection<IProxy> Proxies { get; private set; }

        public void AddProxy(IProxy proxy) => Proxies.Add(proxy);

        public void ClearProxies() => Proxies.Clear();

        public void DeleteProxy(IProxy proxy) => Proxies.Remove(proxy);

        public void DisableProxy() => UnmanagedProxy.DisableProxy();

        public void LoadDefaultProxies()
        {
            using (var sRdr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(_defaultProxiesResourceName)))
            {
                while (sRdr.Peek() >= 0)
                {
                    string st = sRdr.ReadLine();
                    IProxy p = new Proxy() { ProxyHost = st };
                    Proxies.Add(p);
                }
            }
        }

        public void LoadProxiesFromFile(string path)
        {
            var proxies = File.ReadLines(path);
            Proxies = proxies.Select(p => (IProxy)new Proxy() { ProxyHost = p }).ToList();
            //foreach (var item in proxies)
            //{
            //    IProxy p = new Proxy() { ProxyHost = item };
            //    _proxies.Add(p);
            //}
        }

        //public IProxy RandomProxy()
        //{
        //    if (_proxies.Count > 0)
        //    {
        //        IProxy p = _proxies.ToList()[_rndm.Next(0, _proxies.Count())];
        //        UnmanagedProxy.ChangeProxy(p);
        //        return p;
        //    }
        //    return new Proxy();
        //}
        public IProxy RandomProxy() => Proxies.Count > 0 ? UnmanagedProxy.ChangeProxy(Proxies.ToList()[_rndm.Next(0, Proxies.Count())]) : new Proxy();
    }
}