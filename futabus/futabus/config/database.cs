using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futabus.config
{
    internal class database
    {
        private IDriver _driver;
        string uri = "bolt://localhost:7687/FUTABUS"; // Thay bằng địa chỉ của Neo4j
        string user = "neo4j"; // Thay bằng tên người dùng của bạn
        string password = "12345678"; // Thay bằng mật khẩu lúc tạo FUTABUS của bạn

        //get data thì dùng hàm này
        public async Task<List<IRecord>> GetDataFromneo4j(string connectionString)
        {
            IResultCursor cursor;
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            IAsyncSession session = _driver.AsyncSession();
            List<IRecord> results = new List<IRecord>();

            try
            {
                cursor = await session.RunAsync(connectionString);
                results = await cursor.ToListAsync();
                await session.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return results;
        }
        //ghi xuống thì dùng hàm này
        public async Task<int> WriteDataToneo4j(string connectionString, object data)
        {
            int result = 0; //không thành công trả về 0

            IResultCursor cursor;
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            IAsyncSession session = _driver.AsyncSession();
            try
            {
                cursor = await session.RunAsync(connectionString);
                await cursor.ToListAsync();
                result = 1; //thành công thì trả về 1
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return result;
        }

        //ví dụ
        public async Task AddStudent(int svID, string name, int age)
        {
            IResultCursor cursor;
            using (IAsyncSession session = _driver.AsyncSession())
            {
                cursor = await session.RunAsync($"CREATE (s:Sinhvien {{ SVid:{svID}, Name: '{name}', age: {age} }})");
                await cursor.ConsumeAsync();
            }
        }
    }
}
