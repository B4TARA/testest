using Bank.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using IronXL;
using Npgsql;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Import(IFormFile file) //передается выбранный файл
                                                                //!!!Нормально ли разбивается файл эксель??? там есть нюансы - посмотреть!!!
        {
            //создаем копию файла //
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(location) + "\\files";  //путь где хранить эти файлы

            string filePath = Path.Combine(path, new DirectoryInfo(path).GetFiles().Length + 1 + "." + file.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            //создаем копию файла //


            //парсинг в БД//
            WorkBook workbook = WorkBook.Load(filePath);
            WorkSheet sheet = workbook.DefaultWorkSheet;
            int j = 2;
            foreach (var row in sheet.Rows) //для каждой строки 
            {
                int i = 0;
                Class temp = new Class();
                foreach(var column in sheet.Columns) //для каждого столбца
                {
                    if(!column.Hidden) //если не скрытый
                    {
                        switch (i)
                        {
                            case 2: temp.service_number = Convert.ToInt32(sheet.GetCellAt(j, i).Value); break;
                            case 3: temp.fullname = Convert.ToString(sheet.GetCellAt(j, i).Value); break;
                            case 6: temp.position_name = Convert.ToString(sheet.GetCellAt(j, i).Value); break;
                            case 7: temp.position_date = Convert.ToDateTime(sheet.GetCellAt(j, i).Value); break;
                            case 12: temp.hire_date = Convert.ToDateTime(sheet.GetCellAt(j, i).Value); break;
                            case 22: temp.department = Convert.ToString(sheet.GetCellAt(j, i).Value); break;
                            case 24: temp.division_name = Convert.ToString(sheet.GetCellAt(j, i).Value); break;
                            case 26: temp.sector_name = Convert.ToString(sheet.GetCellAt(j, i).Value); break;
                            case 27: temp.status = Convert.ToString(sheet.GetCellAt(j, i).Value); break;
                            case 28: temp.workday_balance = Convert.ToString(sheet.GetCellAt(j, i).Value); break;
                            case 30: temp.list_number = Convert.ToString(sheet.GetCellAt(j, i).Value); break;
                            case 57: temp.start_date = Convert.ToDateTime(sheet.GetCellAt(j, i).Value); break;
                            case 58: temp.end_date = Convert.ToDateTime(sheet.GetCellAt(j, i).Value); break;
                        }
                        //дозаполнить все поля Class
                        temp.password = "";
                        temp.email = "";
                    }
                    i++;
                }
                if (j== 3)break;
                j++;
                string connString = "Server=localhost; Database=postgres; User Id = postgres; Password = 12345";
                NpgsqlConnection nc = new NpgsqlConnection(connString);
                nc.Open();
                using (nc)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("call insert_data_in_Class" + "(:service_number, :fullname, :position_name," +
                        " :position_date, :hire_date, :dismiss_date, :department, :division_name, :sector_name, :status," +
                        " :workday_balance, :list_number, :start_date, :pass, :end_date, :user_role, :email)", nc);
                    cmd.Parameters.AddWithValue("service_number", DbType.Int32).Value = temp.service_number;
                    cmd.Parameters.AddWithValue("fullname", DbType.String).Value = temp.fullname;
                    cmd.Parameters.AddWithValue("position_name", DbType.String).Value = temp.position_name;
                    cmd.Parameters.AddWithValue("position_date", DbType.Date).Value = temp.position_date;
                    cmd.Parameters.AddWithValue("hire_date", DbType.Date).Value = temp.hire_date;
                    cmd.Parameters.AddWithValue("dismiss_date", DbType.Date).Value = temp.dismiss_date;
                    cmd.Parameters.AddWithValue("department", DbType.String).Value = temp.department;
                    cmd.Parameters.AddWithValue("division_name", DbType.String).Value = temp.division_name;
                    cmd.Parameters.AddWithValue("sector_name", DbType.String).Value = temp.sector_name;
                    cmd.Parameters.AddWithValue("status", DbType.String).Value = temp.status;
                    cmd.Parameters.AddWithValue("workday_balance", DbType.String).Value = temp.workday_balance;
                    cmd.Parameters.AddWithValue("list_number", DbType.String).Value = temp.list_number;
                    cmd.Parameters.AddWithValue("start_date", DbType.Date).Value = temp.start_date;
                    cmd.Parameters.AddWithValue("pass", DbType.String).Value = temp.password;
                    cmd.Parameters.AddWithValue("end_date", DbType.Date).Value = temp.end_date;
                    cmd.Parameters.AddWithValue("user_role", DbType.Int32).Value = temp.user_role;
                    cmd.Parameters.AddWithValue("email", DbType.String).Value = temp.email;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    nc.Close();
                }
            }

            
            return await Task.Run(() => View("Index"));
        }

        public IActionResult Excel()
        {
            return View();
        }

        public IActionResult Show(int file)
        {
            var list = new List<List<Class>>();
            string connString = "Server=localhost; Database=postgres; User Id = postgres; Password = 12345";
            NpgsqlConnection nc = new NpgsqlConnection(connString);
            nc.Open();
            using (nc)
            {
                for (int i = 1; i < 10; i++)
                {
                    List<Class> listtemp = new List<Class>();
                    NpgsqlCommand cmd = new NpgsqlCommand("Select * from Class" + i, nc); //where sservice_number == 
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Class temp = new Class();
                        temp.service_number = Convert.ToInt32(reader[i]); //reader ????
                        temp.fullname = Convert.ToString(reader[i]);
                        temp.position_name = Convert.ToString(reader[i]);
                        temp.position_date = (DateTime)reader[i];
                        temp.hire_date = (DateTime)reader[i];
                        temp.dismiss_date = (DateTime)reader[i];
                        temp.department = Convert.ToString(reader[i]);
                        temp.division_name = Convert.ToString(reader[i]);
                        temp.sector_name = Convert.ToString(reader[i]);
                        temp.status = Convert.ToString(reader[i]);
                        temp.workday_balance = Convert.ToString(reader[i]);
                        temp.list_number = Convert.ToString(reader[i]);
                        temp.start_date = (DateTime)reader[i];
                        temp.password = Convert.ToString(reader[i]);
                        temp.end_date = (DateTime)reader[i];
                        temp.user_role = Convert.ToInt32(reader[i]);
                        temp.email = Convert.ToString(reader[i]);
                        listtemp.Add(temp);
                    }
                    list.Add(listtemp);
                    reader.Close();
                }
                nc.Close();
            }
            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}