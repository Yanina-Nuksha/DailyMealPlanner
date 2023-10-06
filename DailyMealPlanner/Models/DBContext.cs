using Microsoft.CodeAnalysis;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyMealPlanner.Models
{
    public class DBContext
    {
        private string connectionString = "server = localhost; port=3306;database=productsdb;user=root;password=5F108q~12";
        public DBContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM products", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Product()
                        {
                            ProductId = Convert.ToInt32(reader["id"]),
                            Name = reader["Name"].ToString(),
                            Calories = Convert.ToDouble(reader["Calories"]),
                            Carbs = Convert.ToDouble(reader["Carbs"]),
                            Fats = Convert.ToDouble(reader["Fats"]),
                            Gramms = Convert.ToInt32(reader["Gramms"]),
                            Protein = Convert.ToDouble(reader["Protein"]), 
                            Category = reader["Category"].ToString()
                        });
                    }
                }
                connection.Close();
            }
            return list;
        }

        public List<Product> GetAllUserProducts(long userId) 
        {
            List<Product> products = new List<Product>();
            List<long> productsId = new List<long>();

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT ProductId FROM userproducts WHERE UserId = " + userId, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productsId.Add(Convert.ToInt32(reader["ProductId"]));
                    }
                }
                connection.Close();
            }
            foreach (long id in productsId)
                products.Add(GetProductById(id));
            return products;
        }

        public void AddUserProduct(long userId, long productId)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT userproducts(ProductId, UserId)" +
                   " VALUES (" + productId + ", " + userId + ")", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AddAllUserProducts(long userId)
        {
            List<Product> products = GetAllProducts();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                foreach (Product product in products)
                    AddUserProduct(userId, product.ProductId);
                connection.Close();
            }
        }

        public void DeleteUserProduct(long userId, long productId)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM userproducts WHERE ProductId = " + productId + " and UserId = " + userId, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Product> AddAllProducts(List <Product> products)
        {
            List<Product> list = new List<Product>();
            foreach (Product product in products)
               AddProductToDB(product);
            return list;
        }
        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM users", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User()
                        {
                            UserId = Convert.ToInt32(reader["id"]),
                            Login = reader["Login"].ToString(),
                            Password = reader["Password"].ToString(),
                            Activity = reader["Activity"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Height = Convert.ToInt32(reader["Height"]),
                            Weight = Convert.ToInt32(reader["Weight"])
                        });
                    }
                }
                connection.Close();
            }
            return list;
        }
        public User GetUserByLogAndPass(string login, string password) 
        {
            User user = null;
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE Login = '" + login + "' AND Password = '" + password + "'", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user.UserId = Convert.ToInt32(reader["id"]);
                        user.Height = Convert.ToInt32(reader["Height"]);
                        user.Login = reader["Login"].ToString();
                        user.Weight = Convert.ToInt32(reader["Weight"]);
                        user.Activity = reader["Activity"].ToString();
                        user.Age = Convert.ToInt32(reader["Age"]);
                    }
                }
                connection.Close();
            }
            return user;
        }

        public User GetUserByLog(string login) 
        {
            User user = null;
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE Login = '" + login + "'", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user.UserId = Convert.ToInt32(reader["id"]);
                        user.Height = Convert.ToInt32(reader["Height"]);
                        user.Login = reader["Login"].ToString();
                        user.Weight = Convert.ToInt32(reader["Weight"]);
                        user.Activity = reader["Activity"].ToString();
                        user.Age = Convert.ToInt32(reader["Age"]);
                    }
                }
                connection.Close();
            }
            return user;
        }

        public User GetUserById(long id)
        {
            User user = new User();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE id = " + id, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.UserId = Convert.ToInt32(reader["id"]);
                        user.Height = Convert.ToInt32(reader["Height"]);
                        user.Login = reader["Login"].ToString();
                        user.Weight = Convert.ToInt32(reader["Weight"]);
                        user.Activity = reader["Activity"].ToString();
                        user.Age = Convert.ToInt32(reader["Age"]);
                    }
                }
                connection.Close();
            }
            return user;
        }
        public Product GetProductById(long id)
        {
            Product product = new Product();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM products WHERE id = " + id, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.ProductId = Convert.ToInt32(reader["id"]);
                        product.Protein = Convert.ToDouble(reader["Protein"]);
                        product.Gramms = Convert.ToInt32(reader["Gramms"]);
                        product.Calories = Convert.ToDouble(reader["Calories"]);
                        product.Carbs = Convert.ToDouble(reader["Carbs"]);
                        product.Fats = Convert.ToDouble(reader["Fats"]);
                        product.Category = reader["Category"].ToString();
                        product.Name = reader["Name"].ToString();
                    }
                }
                connection.Close();
            }
            return product;
        }

        public List<Product> GetProductsByCategory(string category) 
        {
            List<Product> list = new List<Product>();

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM products WHERE Category = '" + category + "'", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Product()
                        {
                            ProductId = Convert.ToInt32(reader["id"]),
                            Name = reader["Name"].ToString(),
                            Calories = Convert.ToDouble(reader["Calories"]),
                            Carbs = Convert.ToDouble(reader["Carbs"]),
                            Fats = Convert.ToDouble(reader["Fats"]),
                            Gramms = Convert.ToInt32(reader["Gramms"]),
                            Protein = Convert.ToDouble(reader["Protein"]),
                            Category = reader["Category"].ToString()
                        });
                    }
                }
                connection.Close();
            }
            return list;
        }

        public List<Product> GetProductsByName(string name)
        {
            List<Product> list = new List<Product>();

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM products WHERE Name LIKE '" + name + "%'", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Product()
                        {
                            ProductId = Convert.ToInt32(reader["id"]),
                            Name = reader["Name"].ToString(),
                            Calories = Convert.ToDouble(reader["Calories"]),
                            Carbs = Convert.ToDouble(reader["Carbs"]),
                            Fats = Convert.ToDouble(reader["Fats"]),
                            Gramms = Convert.ToInt32(reader["Gramms"]),
                            Protein = Convert.ToDouble(reader["Protein"]),
                            Category = reader["Category"].ToString()
                        });
                    }
                }
                connection.Close();
            }
            return list;
        }

        public List<Product> GetProductsByNameCategory(string name, string category)
        {
            List<Product> list = new List<Product>();

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM products WHERE Name LIKE '" + name + "%' AND Category = '" + category + "'", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Product()
                        {
                            ProductId = Convert.ToInt32(reader["id"]),
                            Name = reader["Name"].ToString(),
                            Calories = Convert.ToDouble(reader["Calories"]),
                            Carbs = Convert.ToDouble(reader["Carbs"]),
                            Fats = Convert.ToDouble(reader["Fats"]),
                            Gramms = Convert.ToInt32(reader["Gramms"]),
                            Protein = Convert.ToDouble(reader["Protein"]),
                            Category = reader["Category"].ToString()
                        });
                    }
                }
                connection.Close();
            }
            return list;
        }

        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();
            List<Product> products = GetAllProducts();
            foreach(Product product in products)
            {
                if (!categories.Contains(product.Category))
                    categories.Add(product.Category);
            }          
            return categories;
        }

        public void AddProductToDB(Product product)
        {

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT products(Name, Calories, Carbs, Category, Fats, Gramms, Protein)" +
                " VALUES ('" + product.Name + "', " + product.Calories + ", " + product.Carbs + ", '" + product.Category + "', " + product.Fats + ", " + product.Gramms + ", " + product.Protein + ")", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void AddUserToDB(User user)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT users(Login, Password, Height, Weight, Activity, Age)" +
                " VALUES ('" + user.Login + "', '" + user.Password + "', " + user.Height + ", '" + user.Weight + "', '" + user.Activity + "', " + user.Age + ")", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            AddAllUserProducts(GetUserByLog(user.Login).UserId);
        }
        public void AddMealProductToDB(string type, long productId, long userId)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT mealproducts(ProductId, UserId, Type)" +
                " VALUES (" + productId + ", " + userId + ", '" + type + "')", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public List<MealProduct> GetMealProductsByUser(long userId)
        {
            List<MealProduct> list = new List<MealProduct>();

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM mealproducts WHERE UserId = " + userId, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MealProduct()
                        {
                            MealProductId = Convert.ToInt32(reader["id"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            Type = reader["Type"].ToString(),
                            Gramms = Convert.ToInt32(reader["Gramms"])
                        });
                    }
                }
                connection.Close();
            }
            return list;
        }
        public MealProduct GetMealProduct(long productId, long userId, string type)
        {
            MealProduct product = new MealProduct();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open(); 
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM mealproducts WHERE UserId = " + userId + " and ProductId = " + productId + " and Type = '" + type + "'", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.MealProductId = Convert.ToInt32(reader["id"]);
                        product.UserId = Convert.ToInt32(reader["UserId"]);
                        product.ProductId = Convert.ToInt32(reader["ProductId"]);
                        product.Type = reader["Type"].ToString();
                        product.Gramms = Convert.ToInt32(reader["Gramms"]);
                    }
                }
                connection.Close();
            }
            return product;
        }
        public MealProduct GetMealProductById(long productId)
        {
            MealProduct product = new MealProduct();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM mealproducts WHERE id = " + productId, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.MealProductId = Convert.ToInt32(reader["id"]);
                        product.UserId = Convert.ToInt32(reader["UserId"]);
                        product.ProductId = Convert.ToInt32(reader["ProductId"]);
                        product.Type = reader["Type"].ToString();
                        product.Gramms = Convert.ToInt32(reader["Gramms"]);
                    }
                }
                connection.Close();
            }
            return product;
        }
        public void UpdateMealProduct(long id, int gramms)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE mealproducts SET Gramms = " + gramms + " WHERE id = " + id, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteMealProductFromDB(long id)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM mealproducts WHERE id = " + id, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteUserFromDB(long id)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM users WHERE id = " + id, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdateUser(User user, long userId)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE users SET Activity = '" + user.Activity + "', Age = " + user.Age + ", Height = " + user.Height + ", Weight = " + user.Weight + " WHERE id = " + userId, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdateMealProduct(int gramms, long productId) 
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE mealproducts SET Gramms = " + gramms + " WHERE id = " + productId, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

