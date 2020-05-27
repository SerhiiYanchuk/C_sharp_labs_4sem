using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlExpression;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
   
                while (true)
                {
                    connection.Open();
                    Console.Clear();
                    Console.WriteLine("1. Вывести все товары");
                    Console.WriteLine("2. Вывести торговые точки, где в продаются >= 7 товаров");
                    Console.WriteLine("3. Вывести количество торговых точек");
                    Console.WriteLine("4. Вывести торговые точки и название товаров, которые продаются");
                    Console.WriteLine("5. Вывести самые дорогие товары ");
                    Console.WriteLine("6. Вывести количество проданых товаров по каждому виду в общем");
                    Console.WriteLine("7. Вывести самые популярные товары");
                    Console.WriteLine("8. Посчитать заработок каждой торговой точки");
                    Console.WriteLine("9. Выбрать торг точки, где можно купить рубашку, костюм, брюки");
                    Console.WriteLine("10. Вывести торг точки и количество проданого товара");
                    Console.WriteLine(@"11. Insert \ Update продажи. Ввести номер торговой точки, название товара и объем продаж");
                    Console.WriteLine("12. Delete продажи. Ввести номер торговой точки, название товар");
                    Console.WriteLine(@"13. DataAdapter \ DataSet");
                    Console.WriteLine("0. Exit");
                    int choosen;
                    Console.Write("Choose: ");
                    choosen = int.Parse(Console.ReadLine());
                    Console.Clear();

                    switch (choosen)
                    {
                        case 1:
                            #region 1. Вывести все товары
                            sqlExpression = "SELECT ProductName FROM cProducts;";
                            SqlCommand command1 = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader1 = command1.ExecuteReader();
                            
                            if (reader1.HasRows)
                            {
                                Console.WriteLine($"{reader1.GetName(0)}");
                                while (reader1.Read())
                                {
                                    Console.WriteLine($"{reader1.GetString(0)}");
                                }
                            }
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 2:
                            #region 2. Вывести торговые точки, где в продаются >= 7 товаров
                            sqlExpression = "SELECT StoreNumber, StoreAddress FROM cStores WHERE Product7 IS NOT NULL;";
                            SqlCommand command2 = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader2 = command2.ExecuteReader();

                            if (reader2.HasRows)
                            {
                                Console.WriteLine("{0}\t{1}", reader2.GetName(0), reader2.GetName(1));
                                while (reader2.Read())
                                {
                                    int number = reader2.GetInt32(0);
                                    string address = reader2.GetString(1);                               
                                    Console.WriteLine("{0} \t{1}", number, address);
                                }
                            }
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 3:
                            #region 3. Вывести количество торговых точек
                            sqlExpression = "SELECT COUNT(*) AS QuantityStores FROM cStores;";
                            SqlCommand command3 = new SqlCommand(sqlExpression, connection);
                            object reader3 = command3.ExecuteScalar();
                            Console.WriteLine("Количество торговых точек: {0}", reader3);
                           
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 4:
                            #region 4. Вывести торговые точки и название товаров, которые продаются
                            sqlExpression = "SELECT cStores.StoreAddress, P1.ProductName, P2.ProductName, P3.ProductName, P4.ProductName, P5.ProductName, " +
                                   "P6.ProductName, P7.ProductName, P8.ProductName, P9.ProductName, P10.ProductName " +
                                   "FROM cStores " +
                                    "LEFT JOIN cProducts AS P1 ON cStores.Product1 = P1.ProductId " +
                                    "LEFT JOIN cProducts AS P2 ON cStores.Product2 = P2.ProductId " +
                                    "LEFT JOIN cProducts AS P3 ON cStores.Product3 = P3.ProductId " +
                                    "LEFT JOIN cProducts AS P4 ON cStores.Product4 = P4.ProductId " +
                                    "LEFT JOIN cProducts AS P5 ON cStores.Product5 = P5.ProductId " +
                                    "LEFT JOIN cProducts AS P6 ON cStores.Product6 = P6.ProductId " +
                                    "LEFT JOIN cProducts AS P7 ON cStores.Product7 = P7.ProductId " +
                                    "LEFT JOIN cProducts AS P8 ON cStores.Product8 = P8.ProductId " +
                                    "LEFT JOIN cProducts AS P9 ON cStores.Product9 = P9.ProductId " +
                                    "LEFT JOIN cProducts AS P10 ON cStores.Product10 = P10.ProductId;";
                            SqlCommand command4 = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader4 = command4.ExecuteReader();

                            if (reader4.HasRows)
                            {
                                //Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}", reader4.GetName(0), reader4.GetName(1), reader4.GetName(2), reader4.GetName(3),
                                //              reader4.GetName(4), reader4.GetName(5), reader4.GetName(6), reader4.GetName(7), reader4.GetName(8), reader4.GetName(9), reader4.GetName(10));

                                while (reader4.Read())
                                {
                                    Console.WriteLine("{0}: {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}", reader4.GetValue(0), reader4.GetValue(1), reader4.GetValue(2), reader4.GetValue(3),
                                              reader4.GetValue(4), reader4.GetValue(5), reader4.GetValue(6), reader4.GetValue(7), reader4.GetValue(8), reader4.GetValue(9), reader4.GetValue(10));
                                }
                            }
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 5:
                            #region 5. Вывести самые дорогие товары 
                            sqlExpression = "SELECT ProductName, ProductPrice FROM cProducts " +
                                               "WHERE ProductPrice = (SELECT MAX(ProductPrice) FROM cProducts); ";
                            SqlCommand command5 = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader5 = command5.ExecuteReader();

                            if (reader5.HasRows)
                            {
                                Console.WriteLine("{0, -5}\t{1}", reader5.GetName(0), reader5.GetName(1));
                                while (reader5.Read())
                                {
                                    Console.WriteLine("{0, -10} \t{1}", reader5.GetValue(0), reader5.GetValue(1));
                                }
                            }
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 6:
                            #region 6. Вывести количество проданых товаров по каждому виду в общем
                            sqlExpression = "SELECT cProducts.ProductName, SUM(vSelling.Quantity) AS Quantity " +
                                                "FROM vSelling " +
                                                "INNER JOIN cProducts ON vSelling.ProductId = cProducts.ProductId " +
                                                "GROUP BY cProducts.ProductName;";
                            SqlCommand command6 = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader6 = command6.ExecuteReader();

                            if (reader6.HasRows)
                            {
                                Console.WriteLine("{0, -5} {1}", reader6.GetName(0), reader6.GetName(1));
                                while (reader6.Read())
                                {
                                    Console.WriteLine("{0, -12} {1}", reader6.GetString(0), reader6.GetInt32(1));
                                }
                            }
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 7:
                            #region 7. Вывести самые популярные товары
                            sqlExpression = "SELECT cProducts.ProductName, SUM(vSelling.Quantity) AS Quantity " +
                                            "FROM vSelling " +
                                            "INNER JOIN cProducts ON vSelling.ProductId = cProducts.ProductId " +
                                            "GROUP BY cProducts.ProductName " +
                                            "HAVING SUM(vSelling.Quantity) = " +
                                                "(SELECT MAX(S) FROM(SELECT SUM(vSelling.Quantity) AS S FROM vSelling GROUP BY vSelling.ProductId) AS M);";
                            SqlCommand command7 = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader7 = command7.ExecuteReader();

                            if (reader7.HasRows)
                            {
                                Console.WriteLine("{0} {1}", reader7.GetName(0), reader7.GetName(1));
                                while (reader7.Read())
                                {
                                    Console.WriteLine("{0, -12} {1}", reader7.GetValue(0), reader7.GetValue(1));
                                }
                            }
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 8:
                            #region 8. Посчитать заработок каждой торговой точки
                            sqlExpression = "SELECT cStores.StoreAddress, SUM(vSelling.Quantity*cProducts.ProductPrice) AS '$' FROM vSelling " +
                                            "INNER JOIN cProducts ON vSelling.ProductId = cProducts.ProductId " +
                                            "INNER JOIN cStores ON vSelling.StoreId = cStores.StoreId " +
                                            "GROUP BY cStores.StoreAddress " +
                                            "ORDER BY '$' DESC;";
                            SqlCommand command8 = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader8 = command8.ExecuteReader();

                            if (reader8.HasRows)
                            {
                                Console.WriteLine("{0, -25} {1}", reader8.GetName(0), reader8.GetName(1));
                                while (reader8.Read())
                                {
                                    Console.WriteLine("{0, -25} {1}", reader8.GetValue(0), reader8.GetValue(1));
                                }
                            }
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 9:
                            #region 9. Выбрать торг точки, где можно купить рубашку, костюм, брюки
                            sqlExpression = "SELECT StoreAddress FROM cStores " +
                                             "WHERE 1 IN(Product1, Product2, Product3, Product4, Product5, Product6, Product7, Product8, Product9, Product10) AND " +
                                                  "2 IN(Product1, Product2, Product3, Product4, Product5, Product6, Product7, Product8, Product9, Product10) AND " +
                                                  "3 IN(Product1, Product2, Product3, Product4, Product5, Product6, Product7, Product8, Product9, Product10);";
                            SqlCommand command9 = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader9 = command9.ExecuteReader();

                            if (reader9.HasRows)
                            {
                                Console.WriteLine("{0}", reader9.GetName(0));
                                while (reader9.Read())
                                {
                                    Console.WriteLine("{0}", reader9.GetValue(0));
                                }
                            }
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 10:
                            #region 10. Вывести торг точки и количество проданого товара
                            sqlExpression = "SELECT cStores.StoreAddress, ISNULL(V1.Quantity, 0), ISNULL(V2.Quantity, 0), ISNULL(V3.Quantity, 0), ISNULL(V4.Quantity, 0), ISNULL(V5.Quantity, 0), " +
                                               "ISNULL(V6.Quantity, 0), ISNULL(V7.Quantity, 0), ISNULL(V8.Quantity, 0), ISNULL(V9.Quantity, 0), ISNULL(V10.Quantity, 0) " +
                                               "FROM cStores " +
                                        "LEFT JOIN vSelling AS V1 ON cStores.Product1 = V1.ProductId AND cStores.StoreId = V1.StoreId " +
                                        "LEFT JOIN vSelling AS V2 ON cStores.Product2 = V2.ProductId AND cStores.StoreId = V2.StoreId " +
                                        "LEFT JOIN vSelling AS V3 ON cStores.Product3 = V3.ProductId AND cStores.StoreId = V3.StoreId " +
                                        "LEFT JOIN vSelling AS V4 ON cStores.Product4 = V4.ProductId AND cStores.StoreId = V4.StoreId " +
                                        "LEFT JOIN vSelling AS V5 ON cStores.Product5 = V5.ProductId AND cStores.StoreId = V5.StoreId " +
                                        "LEFT JOIN vSelling AS V6 ON cStores.Product6 = V6.ProductId AND cStores.StoreId = V6.StoreId " +
                                        "LEFT JOIN vSelling AS V7 ON cStores.Product7 = V7.ProductId AND cStores.StoreId = V7.StoreId " +
                                        "LEFT JOIN vSelling AS V8 ON cStores.Product8 = V8.ProductId AND cStores.StoreId = V8.StoreId " +
                                        "LEFT JOIN vSelling AS V9 ON cStores.Product9 = V9.ProductId AND cStores.StoreId = V9.StoreId " +
                                        "LEFT JOIN vSelling AS V10 ON cStores.Product10 = V10.ProductId AND cStores.StoreId = V10.StoreId; ";
                            SqlCommand command10 = new SqlCommand(sqlExpression, connection);
                            SqlDataReader reader10 = command10.ExecuteReader();

                            if (reader10.HasRows)
                            {
                                //Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}", reader10.GetName(0), reader10.GetName(1), reader10.GetName(2), reader10.GetName(3),
                                //              reader10.GetName(4), reader10.GetName(5), reader10.GetName(6), reader10.GetName(7), reader10.GetName(8), reader10.GetName(9), reader10.GetName(10));

                                while (reader10.Read())
                                {
                                    Console.WriteLine("{0}: {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}", reader10.GetValue(0), reader10.GetValue(1), reader10.GetValue(2), reader10.GetValue(3),
                                              reader10.GetValue(4), reader10.GetValue(5), reader10.GetValue(6), reader10.GetValue(7), reader10.GetValue(8), reader10.GetValue(9), reader10.GetValue(10));
                                }
                            }
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 11:
                            #region 11. Insert \ Update продажи. Ввести номер торговой точки, название товара и объем продаж
                            sqlExpression = "sp_UpdateSelling";
                            Console.Write("Название товара: ");
                            string productName = Console.ReadLine();
                            Console.Write("Номер торговой точки: ");
                            int storeNumber = Int32.Parse(Console.ReadLine());
                            Console.Write("Количество продаж: ");
                            int quantity = Int32.Parse(Console.ReadLine());

                            SqlCommand command11 = new SqlCommand(sqlExpression, connection);
                            command11.CommandType = System.Data.CommandType.StoredProcedure;

                            SqlParameter p1 = new SqlParameter("@produnctName", productName);
                            SqlParameter p2 = new SqlParameter
                            {
                                ParameterName = "@storeNumber",
                                Value = storeNumber,
                                SqlDbType = System.Data.SqlDbType.Int
                            };
                            SqlParameter p3 = new SqlParameter
                            {
                                ParameterName = "@quantity",
                                Value = quantity,
                                SqlDbType = System.Data.SqlDbType.Int
                            };
                            SqlParameter p4 = new SqlParameter()
                            {
                                ParameterName = "@result",
                                Direction = System.Data.ParameterDirection.Output,
                                SqlDbType = System.Data.SqlDbType.NVarChar,
                                Size = 50
                            };
                            command11.Parameters.AddRange(new SqlParameter[] { p1, p2, p3, p4 });
                            command11.ExecuteNonQuery();
                            Console.WriteLine("{0}", p4.Value);
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 12:
                            #region 12. Delete продажи. Ввести номер торговой точки, название товар
                            sqlExpression = "sp_DeleteSelling";
                            Console.Write("Название товара: ");
                            string productName2 = Console.ReadLine();
                            Console.Write("Номер торговой точки: ");
                            int storeNumber2 = Int32.Parse(Console.ReadLine());

                            SqlCommand command12 = new SqlCommand(sqlExpression, connection);
                            command12.CommandType = System.Data.CommandType.StoredProcedure;

                            SqlParameter p21 = new SqlParameter("@produnctName", productName2);
                            SqlParameter p22 = new SqlParameter
                            {
                                ParameterName = "@storeNumber",
                                Value = storeNumber2,
                                SqlDbType = System.Data.SqlDbType.Int
                            };
                            SqlParameter p23 = new SqlParameter()
                            {
                                ParameterName = "@result",
                                Direction = System.Data.ParameterDirection.Output,
                                SqlDbType = System.Data.SqlDbType.NVarChar,
                                Size = 50
                            };
                            command12.Parameters.AddRange(new SqlParameter[] { p21, p22, p23});
                            command12.ExecuteNonQuery();
                            Console.WriteLine("{0}", p23.Value);
                            connection.Close();
                            Console.ReadKey();
                            #endregion
                            break;
                        case 13:
                            #region 13. DataAdapter \ DataSet
                            sqlExpression = "SELECT * FROM cProducts;";
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            DataTable dt = ds.Tables[0];
                            foreach (DataColumn column in dt.Columns)
                                Console.Write("{0, -10}\t", column.ColumnName);
                            Console.WriteLine();
                            // перебор всех строк таблицы
                            foreach (DataRow row in dt.Rows)
                            {
                                // получаем все ячейки строки
                                var cells = row.ItemArray;
                                foreach (object cell in cells)
                                    Console.Write("{0, -10}\t", cell);
                                Console.WriteLine();
                            }
                            Console.ReadKey();
                            connection.Close();
                            #endregion
                            break;
                        case 0:
                            return;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
