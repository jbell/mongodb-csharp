using System;
using System.Collections.Generic;

namespace MongoDB.Driver
{
    public class MongoDBConnectionStringBuilder
    {

        /// <summary>
        /// Holds a collection of the current values
        /// </summary>
        private IDictionary<string, object> currentValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBConnectionStringBuilder"/> class.
        /// </summary>
        public MongoDBConnectionStringBuilder()
        {
            currentValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MongoDBConnectionStringBuilder(string connectionString)
            : this()
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                return;
            }

            string[] keyValuePairs = connectionString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < keyValuePairs.Length; i++)
            {

                string[] parts = keyValuePairs[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    
                    string keyword = parts[0].Trim();
                    string value = parts[1].Trim();

                    if (ConnectionStringKeyword.Host.Equals(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        currentValues.Add(keyword, new MongoDBHostCollection(value));
                    }
                    else
                    {
                        currentValues.Add(keyword, value);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public MongoDBConnectionStringBuilder(IDictionary<string, object> values)
            : this()
        {
            foreach (KeyValuePair<string, object> kvp in values)
            {
                currentValues.Add(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified keyword.
        /// </summary>
        /// <value></value>
        public object this[string keyword]
        {
            get
            {
                if (currentValues.ContainsKey(keyword))
                {
                    return currentValues[keyword];
                }
                return null;
            }
            set
            {
                currentValues[keyword] = value;
            }
        }

        protected void SetKeyword(string keyword, object value)
        {
            if (currentValues.ContainsKey(keyword))
            {
                currentValues[keyword] = value;
            }
            else
            {
                currentValues.Add(keyword, value);
            }
        }

        /// <summary>
        /// Removes the specified keyword.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns></returns>
        public bool Remove(string keyword)
        {
            return currentValues.Remove(keyword);
        }

        public virtual MongoDBHostCollection Host
        {
            get
            {
                if (this[ConnectionStringKeyword.Host] == null)
                {
                    SetKeyword(ConnectionStringKeyword.Host, new MongoDBHostCollection());
                }
                return this[ConnectionStringKeyword.Host] as MongoDBHostCollection;
            }
        }

        public virtual Boolean SlaveOk
        {
            get
            {
                if (this[ConnectionStringKeyword.SlaveOk] == null) {
                    return false;
                }
                return "True".Equals((string)this[ConnectionStringKeyword.SlaveOk], StringComparison.OrdinalIgnoreCase);
            }
            set
            {
                if (value)
                {
                    SetKeyword(ConnectionStringKeyword.SlaveOk, "True");
                }
                else
                {
                    SetKeyword(ConnectionStringKeyword.SlaveOk, "False");
                }
            }
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public virtual string UserId
        {
            get
            {
                return (string)this[ConnectionStringKeyword.UserId];
            }
            set
            {
                SetKeyword(ConnectionStringKeyword.UserId, value);
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public virtual string Password
        {
            get
            {
                return (string)this[ConnectionStringKeyword.Password];
            }
            set
            {
                SetKeyword(ConnectionStringKeyword.Password, value);
            }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public virtual string ConnectionString
        {
            get
            {
                if (!currentValues.ContainsKey(ConnectionStringKeyword.Host))
                {
                    SetKeyword(ConnectionStringKeyword.Host, new MongoDBHostCollection());
                }
                List<string> valuePairs = new List<string>();
                foreach (KeyValuePair<string, object> kvp in currentValues)
                {
                    valuePairs.Add(String.Format("{0}={1}", kvp.Key, kvp.Value.ToString()));
                }
                return String.Join(";", valuePairs.ToArray());
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
            return ConnectionString;
        }

    }
}
