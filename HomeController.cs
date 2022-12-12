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
            int j = 0;
            foreach (var row in sheet.Rows) //для каждой строки 
            {
                int i = 0;
                foreach(var column in sheet.Columns) //для каждого столбца
                {
                    Console.Write(sheet.GetCellAt(j, i).Value + " | ");
                    i++;
                }
                Console.WriteLine();
                j++;
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
                        temp.position_date = DateOnly.Parse((string)reader[i]);
                        temp.hire_date = DateOnly.Parse((string)reader[i]);
                        temp.dismiss_date = DateOnly.Parse((string)reader[i]);
                        temp.department = Convert.ToString(reader[i]);
                        temp.division_name = Convert.ToString(reader[i]);
                        temp.sector_name = Convert.ToString(reader[i]);
                        temp.status = Convert.ToString(reader[i]);
                        temp.workday_balance = Convert.ToString(reader[i]);
                        temp.list_number = Convert.ToString(reader[i]);
                        temp.start_date = DateOnly.Parse((string)reader[i]);
                        temp.password = Convert.ToString(reader[i]);
                        temp.end_date = DateOnly.Parse((string)reader[i]);
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