using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProProxy
{
    public class ProxyCore : IProxyCore
    {
        private string _defaultProxiesResourceName;
        private ICollection<IProxy> _proxies;
        private Random _rndm;

        public ProxyCore()
        {
            _defaultProxiesResourceName = "ProProxy.default.txt";
            _proxies = new List<IProxy>();
            _rndm = new Random();
        }

        public ICollection<IProxy> Proxies => _proxies;

        public void AddProxy(IProxy proxy) => _proxies.Add(proxy);

        public void ClearProxies() => _proxies.Clear();

        public void DeleteProxy(IProxy proxy) => _proxies.Remove(proxy);

        public void DisableProxy() => UnmanagedProxy.DisableProxy();

        public void LoadDefaultProxies()
        {
            using (Stream rsrcStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_defaultProxiesResourceName))
            {
                using (var sRdr = new StreamReader(rsrcStream))
                {
                    while (sRdr.Peek() >= 0)
                    {
                        string st = sRdr.ReadLine();
                        IProxy p = new Proxy() { ProxyHost = st };
                        _proxies.Add(p);
                    }
                }
            }
        }

        public void LoadProxiesFromFile(string path)
        {
            var proxies = File.ReadLines(path);
            foreach (var item in proxies)
            {
                IProxy p = new Proxy() { ProxyHost = item };
                _proxies.Add(p);
            }
        }

        public IProxy RandomProxy()
        {
         
            if (_proxies.Count > 0)
            {
                IProxy  p = _proxies.ToList()[_rndm.Next(0, _proxies.Count())];
                UnmanagedProxy.ChangeProxy(p);
                return p;
            }
            return new Proxy();
        }
    }
}
