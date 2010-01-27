using System;
using System.Text;

namespace MongoDB.Driver
{
    public class MongoDBHost
    {
        public const string DEFAULTHOST = "localhost"; 
        public const int DEFAULTPORT = 27017;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBHost"/> class.
        /// </summary>
        public MongoDBHost() : this(DEFAULTHOST) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBHost"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        public MongoDBHost(string host) : this(host, DEFAULTPORT) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBHost"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        public MongoDBHost(string host, int port)
        {
            this.Host = host;
            this.Port = port;
        }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }
        
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (Port == DEFAULTPORT) 
            {
                return Host;
            }
            return String.Format("{0}:{1}", Host, Port);
        }
    }
}
