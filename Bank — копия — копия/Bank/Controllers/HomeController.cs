using Bank.Models;
using Bank.Service.Interfaces;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Diagnostics;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _accountService;

        public HomeController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) return View();
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Import(IFormFile file)
        {
            var workbook = new Aspose.Cells.Workbook("C:\\Users\\evgen\\OneDrive\\Рабочий стол\\testest-main\\2. ШР.xlsb");
            workbook.Save("C:\\Users\\evgen\\OneDrive\\Рабочий стол\\testest-main\\2. ШР.xlsx");

            //all files
            List<string> filesPathList = new List<string>();
            filesPathList.Add("C:\\Users\\evgen\\OneDrive\\Рабочий стол\\testest-main\\1. СЧ.xlsx");
            filesPathList.Add("C:\\Users\\evgen\\OneDrive\\Рабочий стол\\testest-main\\2. ШР.xlsx");
            filesPathList.Add("C:\\Users\\evgen\\OneDrive\\Рабочий стол\\testest-main\\3. Увольнения.xlsx");
            filesPathList.Add("C:\\Users\\evgen\\OneDrive\\Рабочий стол\\testest-main\\4. Матрица.xlsx");
            filesPathList.Add("C:\\Users\\evgen\\OneDrive\\Рабочий стол\\testest-main\\5. КПЭ.xlsx");
            filesPathList.Add("C:\\Users\\evgen\\OneDrive\\Рабочий стол\\testest-main\\7. ВД.xlsx");
            filesPathList.Add("C:\\Users\\evgen\\OneDrive\\Рабочий стол\\testest-main\\9. КК.xlsx");

            //парсинг в БД//
            string connString = "Server=localhost; Database=postgres; User Id = postgres; Password = 12345";
            NpgsqlConnection nc = new NpgsqlConnection(connString);
            nc.Open();
            using (var tx = nc.BeginTransaction())
            {
                foreach (string filePath in filesPathList)
                {
                    switch (filePath.Split("\\").Last())
                    {
                        case "1. СЧ.xlsx": ImportService.ImportUserInfo(filePath, nc); break;
                        case "2. ШР.xlsx": ImportService.ImportUserStructure(filePath, nc); break;
                        case "3. Увольнения.xlsx": ImportService.ImportUserDismiss(filePath, nc); break;
                        case "4. Матрица.xlsx": ImportService.ImportMatrix(filePath, nc); break;
                            //case "5. КПЭ.xlsx": ImportService.ImportAssessmentInfo(filePath, nc); break;
                            //case "7. ВД.xlsx": ImportService.ImportUserInfo(filePath, nc); break;
                            //case "9. КК.xlsx": ImportService.ImportUserInfo(filePath, nc); break;
                    }
                }
                tx.Commit();
            }
            //тут можно начинать транзакцию и если гдето в импорте выскочит ошибка там же ее заканчивать и возвращать чтото типа вью

            //тут комитить транзакцию
            nc.Close();
            return await Task.Run(() => View("Index"));
        }

        public IActionResult Excel()
        {
            return View();
        }


        public IActionResult UserStructure(int file)
        {
            var list = new List<List<UserStructure>>();
            string connString = "Server=localhost; Database=postgres; User Id = postgres; Password = 12345";
            NpgsqlConnection nc = new NpgsqlConnection(connString);
            nc.Open();
            using (nc)
            {
                List<UserStructure> listtemp = new List<UserStructure>();
                NpgsqlCommand cmd = new NpgsqlCommand("Select * from public.\"USER_STRUCTURE\"", nc); //where sservice_number == 
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserStructure temp = new UserStructure();
                    temp.service_number = Convert.ToInt32(reader[0]);
                    temp.grade_group = Convert.ToString(reader[1]);
                    temp.grade_number = Convert.ToString(reader[2]);
                    temp.structure_role = Convert.ToString(reader[3]);
                    temp.subrole = Convert.ToString(reader[4]);
                    if (reader[5] is DBNull) temp.subrole_date = null;
                    else temp.subrole_date = Convert.ToDateTime(reader[5]);
                    temp.subrole_reason = Convert.ToString(reader[6]);
                    temp.role_category = Convert.ToString(reader[7]);
                    temp.fot_mark = Convert.ToString(reader[8]);
                    temp.manager_service_number = Convert.ToInt32(reader[9]);
                    temp.manager_fullname = Convert.ToString(reader[10]);
                    temp.chief_service_number = Convert.ToInt32(reader[11]);
                    temp.chief_fullname = Convert.ToString(reader[12]);
                    temp.director_service_number = Convert.ToInt32(reader[13]);
                    temp.director_fullname = Convert.ToString(reader[14]);
                    temp.block_name = Convert.ToString(reader[15]);
                    if (reader[16] is DBNull) temp.curator_service_number = null;
                    else temp.curator_service_number = Convert.ToInt32(reader[16]);
                    temp.curator_fullname = Convert.ToString(reader[17]);
                    listtemp.Add(temp);
                }

                list.Add(listtemp);
                reader.Close();
            }
            nc.Close();
            return View(list);
        }

        public IActionResult Matrix(int file)
        {
            var list = new List<List<Matrix>>();
            string connString = "Server=localhost; Database=postgres; User Id = postgres; Password = 12345";
            NpgsqlConnection nc = new NpgsqlConnection(connString);
            nc.Open();
            using (nc)
            {
                List<Matrix> listtemp = new List<Matrix>();
                NpgsqlCommand cmd = new NpgsqlCommand("Select * from public.\"MATRIX\" where structure_type=\'ПОПА\'", nc);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Matrix temp = new Matrix();
                    temp.structure_type = Convert.ToString(reader[0]);
                    temp.subrole = Convert.ToString(reader[1]);
                    temp.position_name = Convert.ToString(reader[2]);
                    temp.grade_group = Convert.ToString(reader[3]);
                    temp.grade_number = Convert.ToString(reader[4]);
                    temp.fot_high = Convert.ToString(reader[5]);
                    temp.fot_middle = Convert.ToString(reader[6]);
                    temp.fot_region = Convert.ToString(reader[7]);
                    temp.fot_low = Convert.ToString(reader[8]);
                    listtemp.Add(temp);
                }

                list.Add(listtemp);
                reader.Close();
            }
            nc.Close();
            return View(list);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}