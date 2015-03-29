using ServiceBrokerCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ServiceBrokerReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string message;
            using (SqlConnection connection = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=ServiceBrokerIntegration;Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand("read_message", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    message = (string)command.ExecuteScalar();
                }
            }

            Notification notification;
            using (StringReader textReader = new StringReader(message))
            {
                notification = (Notification)new XmlSerializer(typeof(Notification)).Deserialize(textReader);
            }

            Console.WriteLine("Processing {0} ", notification);
        }
    }
}
