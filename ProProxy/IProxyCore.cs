using System.Collections.Generic;

namespace ProProxy
{
    public interface IProxyCore
    {
        /// <summary>
        /// Load Default HTTP Proxies
        /// </summary>
        void LoadDefaultProxies();

        /// <summary>
        /// Load Proxies from a .txt File seperated with \n
        /// </summary>
        /// <param name="path">The path for the file</param>
        void LoadProxiesFromFile(string path);

        /// <summary>
        /// Adds the Proxy into to the Proxy-Collection
        /// </summary>
        /// <param name="proxy">The proxy for adding</param>
        void AddProxy(IProxy proxy);

        /// <summary>
        /// Deletes the Proxy from the Proxy-Collection
        /// </summary>
        /// <param name="proxy">The proxy which should be deleted</param>
        void DeleteProxy(IProxy proxy);

        /// <summary>
        /// Clear all Proxies from the Collection
        /// </summary>
        void ClearProxies();

        /// <summary>
        /// Disable Proxy Settings of your OS
        /// </summary>
        void DisableProxy();

        /// <summary>
        /// All availbale Proxies
        /// </summary>
        ICollection<IProxy> Proxies { get; }

        /// <summary>
        /// Select a Random Proxy from your collection
        /// </summary>
        /// <returns></returns>
        IProxy RandomProxy();
    }
}