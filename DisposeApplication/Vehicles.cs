using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DisposeApplication
{
    /// <summary>
    /// Приклад застосування паттерну Disposable
    /// </summary>
    public class Vehicles: IDisposable
    {
        private string dbConnection = "Server=localhost,1433;User Id=sa;Password=Admin123!;Database=Vehicles;";
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;
        
        private List<Car> cars;
        
        public List<Car> Cars { 
            get => this.cars;
            set => this.cars = value;
        } // GET ONLY
        
        public Vehicles()
        {
            cars = new List<Car>();
            connection = new SqlConnection(dbConnection);
            connection.Open(); 
            command = new SqlCommand("SELECT * FROM Cars", connection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                string mark = (string) reader[1];
                string color = (string) reader[2];
                cars.Add(new Car() { Mark = mark, Color = color });
            }
        }
        
        public void Dispose()
        {
            // Передаємо true, таким чином даємо знати що метод викликав безпосередньо сам користувач,
            // і тому нам потрібно вивільнити не тільки нерекровані ресурси, а й керовані теж.
            Dispose(true);
            // Оскільки ми самостійно звільнили ресурси, повторна утилізація при проходження GC нам не потрібна.
            GC.SuppressFinalize(this);
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            // В тому випадку,
            // якщо метод Dispose вже був викликаний для даного об'єкта,
            // тоді для уникнення помилок повторного використання підряд задаємо відповідну перевірку.
            if (this.disposed) return;
    
            // Перевірка на утилізацію керованих ресурсів
            // (виконується при виклику Dispose безпосередньо користувачем)
            if (disposing)
            {
                // TODO: Логіка звільнення керованих ресурсів
                this.cars.Clear();
            }
            // TODO: Логіка звільнення некерованих ресурсів
            try
            {
                connection.Dispose();
                command.Dispose();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                // Присвоюєм true змінній яка дає знати, чи був уже застосований метод Dispose (вивільнення сміття),
                // для уникнення помилок в тому разі, якщо метод Dispose буде викликаний декілька разів підряд.
                this.disposed = true;
            }
        }
        
        // Гарантує утилізацію ресурсів,
        // навіть якщо користувач забув вручну це зробити через метод Dispose.
        ~Vehicles()
        {
            // Передаємо false,
            // оскільки деструктор сам подбає про автоматичне звільнення керованих ресурсів
            Console.WriteLine($"The {nameof(Vehicles)} destructor is executing.");
            Dispose(false);
        }
    }
}