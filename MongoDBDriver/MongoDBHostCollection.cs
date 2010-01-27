using System;
using System.Collections.Generic;

namespace MongoDB.Driver
{
    public class MongoDBHostCollection : List<MongoDBHost>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBHostCollection"/> class.
        /// </summary>
        public MongoDBHostCollection() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBHostCollection"/> class.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="collection"/> is null.
        /// </exception>
        public MongoDBHostCollection(IEnumerable<MongoDBHost> collection) : base(collection) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBHostCollection"/> class.
        /// </summary>
        /// <param name="hostPart">The host part.</param>
        public MongoDBHostCollection(string hostPart)
            : this()
        {
            if (String.IsNullOrEmpty(hostPart))
            {
                return;
            }

            string[] hostPairs = hostPart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < hostPairs.Length; i++)
            {
                string[] parts = hostPairs[i].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 1)
                {
                    this.Add(parts[0].Trim());
                }
                else if (parts.Length == 2)
                {
                    this.Add(parts[0].Trim(), Convert.ToInt32(parts[1]));
                }
            }
        }

        /// <summary>
        /// Gets the primary host.
        /// </summary>
        /// <value>The host.</value>
        public MongoDBHost Host
        {
            get
            {
                return Left;
            }
        }

        /// <summary>
        /// Gets the left host in a paired connection.
        /// </summary>
        /// <value>The left.</value>
        public MongoDBHost Left
        {
            get
            {
                if (this.Count == 0)
                {
                    return new MongoDBHost();
                }
                else {
                    return this[0];
                }
            }
        }

        /// <summary>
        /// Gets the right host in a paired connection.
        /// </summary>
        /// <value>The right.</value>
        public MongoDBHost Right
        {
            get
            {
                if (this.Count > 1)
                {
                    return this[1];
                }
                return null;
            }
        }

        /// <summary>
        /// Adds the specified host.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        public void Add(string host, int port)
        {
            this.Add(new MongoDBHost(host, port));
        }

        /// <summary>
        /// Adds the specified host.
        /// </summary>
        /// <param name="host">The host.</param>
        public void Add(string host)
        {
            this.Add(new MongoDBHost(host));
        }

        /// <summary>
        /// Gets a value indicating whether this collection represents a paired connection.
        /// </summary>
        /// <value><c>true</c> if this instance is a paired connection; otherwise, <c>false</c>.</value>
        public bool IsPaired
        {
            get
            {
                return this.Count > 1;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (!this.IsPaired)
            {
                return Host.ToString();
            }
            List<string> hostPairs = new List<string>();
            foreach (MongoDBHost host in this)
            {
                hostPairs.Add(host.ToString());
            }
            return String.Join(",", hostPairs.ToArray());
        }
    }
}
