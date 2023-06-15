using System;
using System.Configuration;
using System.Data.SqlClient;

namespace 測試資料庫連線
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 測試資料庫開啟");
            TestDatabaseConnection();

            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 測試指令執行");
            TestDatabaseSelect();

            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 按任意鍵結束");
            Console.ReadKey();
        }

        /// <summary>
        /// 測試資料庫連線
        /// </summary>
        static void TestDatabaseConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Database connection successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Failed to connect to the database: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 測試資料庫指令
        /// </summary>
        static void TestDatabaseSelect()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string selectCommand = ConfigurationManager.AppSettings["TestCommand"];

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Executing SELECT query...");

                    using (var command = new SqlCommand(selectCommand, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Assuming the first column is of type string, change the data type accordingly
                                string firstColumnValue = reader.GetString(0);
                                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} First column value: " + firstColumnValue);
                            }
                            else
                            {
                                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} No data found.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Error executing SELECT query: " + ex.Message);
                }
            }
        }
    }
}
