using Bank.Domain.Models;
using Bank.Models;
using Bank.Service.Interfaces;
using ExcelDataReader;
using Npgsql;
using System.Data;

namespace Bank.Services
{
    public class ImportService
    {
        private readonly IAccountService _accountService;

        public ImportService(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public static void ImportUserInfo(string filePath, NpgsqlConnection nc)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            FileStream fStream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fStream);
            DataSet resultDataSet = excelDataReader.AsDataSet();
            var table = resultDataSet.Tables[0];
            for (int rowCounter = 2; rowCounter <= 3; rowCounter++)
            {
                UserInfo temp = new UserInfo(); //Model
                for (int colCounter = 0; colCounter <= table.Columns.Count - 1; colCounter++)
                {
                    switch (colCounter)
                    {
                        case 2: temp.service_number = Convert.ToInt32(table.Rows[rowCounter][colCounter]); break;
                        case 3: temp.fullname = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 6: temp.position_name = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 7: temp.position_date = Convert.ToDateTime(table.Rows[rowCounter][colCounter]); break;
                        case 12: temp.hire_date = Convert.ToDateTime(table.Rows[rowCounter][colCounter]); break;
                        case 22: temp.department = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 24: temp.division_name = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 26: temp.sector_name = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 27: temp.status = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 28: temp.workday_balance = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 30: temp.list_number = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 57: temp.start_date = Convert.ToDateTime(table.Rows[rowCounter][colCounter]); break;
                        case 58: temp.end_date = Convert.ToDateTime(table.Rows[rowCounter][colCounter]); break;
                    }
                }

                excelDataReader.Close();


            }
        }

        public static void ImportUserDismiss(string filePath, NpgsqlConnection nc)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            FileStream fStream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fStream);
            DataSet resultDataSet = excelDataReader.AsDataSet();
            var table = resultDataSet.Tables[0];
            for (int rowCounter = 2; rowCounter <= 3; rowCounter++)
            {
                UserInfo temp = new UserInfo(); //Model
                for (int colCounter = 0; colCounter <= table.Columns.Count - 1; colCounter++)
                {
                    switch (colCounter)
                    {
                        case 0: temp.service_number = Convert.ToInt32(table.Rows[rowCounter][colCounter]); break;
                        case 3: temp.dismiss_date = Convert.ToDateTime(table.Rows[rowCounter][colCounter]); break;
                    }
                }

                excelDataReader.Close();


                //InsertToDB
                NpgsqlCommand cmd = new NpgsqlCommand("call insert_data_in_User_Dismiss" + "(:new_dismiss_date, :service_number)", nc);
                cmd.Parameters.AddWithValue("new_dismiss_date", DbType.DateTime).Value = temp.dismiss_date;
                cmd.Parameters.AddWithValue("service_number", DbType.Int32).Value = temp.service_number;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public static void ImportUserStructure(string filePath, NpgsqlConnection nc)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            FileStream fStream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fStream);
            DataSet resultDataSet = excelDataReader.AsDataSet();
            var table = resultDataSet.Tables[0];
            for (int rowCounter = 3; rowCounter <= 4; rowCounter++)
            {
                UserStructure temp = new UserStructure(); //Model
                for (int colCounter = 0; colCounter <= table.Columns.Count - 1; colCounter++)
                {
                    switch (colCounter)
                    {
                        case 41: temp.service_number = Convert.ToInt32(table.Rows[rowCounter][colCounter]); break;
                        case 42: temp.grade_group = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 43: temp.grade_number = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 44: temp.structure_role = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 45: temp.subrole = Convert.ToString(table.Rows[rowCounter][colCounter]); break;

                        case 46:
                            if (table.Rows[rowCounter][colCounter].ToString() == "") temp.subrole_date = null;
                            else temp.subrole_date = Convert.ToDateTime(table.Rows[rowCounter][colCounter]); break;

                        case 47: temp.subrole_reason = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 48: temp.role_category = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 49: temp.fot_mark = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 50: temp.manager_service_number = Convert.ToInt32(table.Rows[rowCounter][colCounter]); break;
                        case 51: temp.manager_fullname = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 52: temp.chief_service_number = Convert.ToInt32(table.Rows[rowCounter][colCounter]); break;
                        case 53: temp.chief_fullname = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 54: temp.director_service_number = Convert.ToInt32(table.Rows[rowCounter][colCounter]); break;
                        case 55: temp.director_fullname = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 56: temp.block_name = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 57:
                            if (table.Rows[rowCounter][colCounter].ToString() == "") temp.curator_service_number = null;
                            else temp.curator_service_number = Convert.ToInt32(table.Rows[rowCounter][colCounter]); break;

                        case 58: temp.curator_fullname = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                    }
                }

                excelDataReader.Close();

                //InsertToDB

                NpgsqlCommand cmd = new NpgsqlCommand("Select * from public.\"USER_STRUCTURE\" where service_number=\'" + temp.service_number + "\'", nc);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows) //пользователь с таким service_number нашелся
                {
                    NpgsqlCommand com = new NpgsqlCommand("call update_data_in_User_Structure" + "(:service_number, :grade_group, :grade_number," +
                    " :structure_role, :subrole, :subrole_date, :subrole_reason, :role_category, :fot_mark, :manager_service_number," +
                    " :manager_fullname, :chief_service_number, :chief_fullname, :director_service_number, :director_fullname, :block_name," +
                    " :curator_service_number, :curator_fullname)", nc);
                    com.Parameters.AddWithValue("service_number", DbType.Int32).Value = temp.service_number;
                    com.Parameters.AddWithValue("grade_group", DbType.String).Value = temp?.grade_group ?? "";
                    com.Parameters.AddWithValue("grade_number", DbType.String).Value = temp.grade_number;
                    com.Parameters.AddWithValue("structure_role", DbType.String).Value = temp.structure_role;
                    com.Parameters.AddWithValue("subrole", DbType.String).Value = temp.subrole;
                    com.Parameters.AddWithValue("subrole_date", DbType.Date).Value = temp.subrole_date == null ? DBNull.Value : temp.subrole_date;
                    com.Parameters.AddWithValue("subrole_reason", DbType.String).Value = temp.subrole_reason == null ? DBNull.Value : temp.subrole_reason;
                    com.Parameters.AddWithValue("role_category", DbType.String).Value = temp.role_category;
                    com.Parameters.AddWithValue("fot_mark", DbType.String).Value = temp.fot_mark;
                    com.Parameters.AddWithValue("manager_service_number", DbType.Int32).Value = temp.manager_service_number;
                    com.Parameters.AddWithValue("manager_fullname", DbType.String).Value = temp.manager_fullname;
                    com.Parameters.AddWithValue("chief_service_number", DbType.Int32).Value = temp.chief_service_number;
                    com.Parameters.AddWithValue("chief_fullname", DbType.String).Value = temp.chief_fullname;
                    com.Parameters.AddWithValue("director_service_number", DbType.Int32).Value = temp.director_service_number;
                    com.Parameters.AddWithValue("director_fullname", DbType.String).Value = temp.director_fullname;
                    com.Parameters.AddWithValue("block_name", DbType.String).Value = temp.block_name;
                    com.Parameters.AddWithValue("curator_service_number", DbType.Int32).Value = temp.curator_service_number == null ? DBNull.Value : temp.curator_service_number;
                    com.Parameters.AddWithValue("curator_fullname", DbType.String).Value = temp.curator_fullname;
                    com.CommandType = CommandType.Text;
                    cmd.Dispose(); //удаляем команду и закрываем чтение
                    reader.Close();
                    com.ExecuteNonQuery(); //выполнение процедуры
                }
                else //не нашелся
                {
                    NpgsqlCommand com = new NpgsqlCommand("call insert_data_in_User_Structure" + "(:service_number, :grade_group, :grade_number," +
                   " :structure_role, :subrole, :subrole_date, :subrole_reason, :role_category, :fot_mark, :manager_service_number," +
                   " :manager_fullname, :chief_service_number, :chief_fullname, :director_service_number, :director_fullname, :block_name," +
                   " :curator_service_number, :curator_fullname)", nc);
                    com.Parameters.AddWithValue("service_number", DbType.Int32).Value = temp.service_number;
                    com.Parameters.AddWithValue("grade_group", DbType.String).Value = temp.grade_group;
                    com.Parameters.AddWithValue("grade_number", DbType.String).Value = temp.grade_number;
                    com.Parameters.AddWithValue("structure_role", DbType.String).Value = temp.structure_role;
                    com.Parameters.AddWithValue("subrole", DbType.String).Value = temp.subrole;
                    com.Parameters.AddWithValue("subrole_date", DbType.Date).Value = temp.subrole_date == null ? DBNull.Value : temp.subrole_date;
                    com.Parameters.AddWithValue("subrole_reason", DbType.String).Value = temp.subrole_reason == null ? DBNull.Value : temp.subrole_reason;
                    com.Parameters.AddWithValue("role_category", DbType.String).Value = temp.role_category;
                    com.Parameters.AddWithValue("fot_mark", DbType.String).Value = temp.fot_mark;
                    com.Parameters.AddWithValue("manager_service_number", DbType.Int32).Value = temp.manager_service_number;
                    com.Parameters.AddWithValue("manager_fullname", DbType.String).Value = temp.manager_fullname;
                    com.Parameters.AddWithValue("chief_service_number", DbType.Int32).Value = temp.chief_service_number;
                    com.Parameters.AddWithValue("chief_fullname", DbType.String).Value = temp.chief_fullname;
                    com.Parameters.AddWithValue("director_service_number", DbType.Int32).Value = temp.director_service_number;
                    com.Parameters.AddWithValue("director_fullname", DbType.String).Value = temp.director_fullname;
                    com.Parameters.AddWithValue("block_name", DbType.String).Value = temp.block_name;
                    com.Parameters.AddWithValue("curator_service_number", DbType.Int32).Value = temp.curator_service_number == null ? DBNull.Value : temp.curator_service_number; ;
                    com.Parameters.AddWithValue("curator_fullname", DbType.String).Value = temp.curator_fullname;
                    com.CommandType = CommandType.Text;
                    cmd.Dispose(); //удаляем команду и закрываем чтение
                    reader.Close();
                    com.ExecuteNonQuery(); //выполнение процедуры
                }
            }
        }

        public static void ImportMatrix(string filePath, NpgsqlConnection nc)
        {

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            FileStream fStream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fStream);
            DataSet resultDataSet = excelDataReader.AsDataSet();
            var table = resultDataSet.Tables[0];
            for (int colCounter = 1; colCounter <= table.Columns.Count - 1; colCounter++)
            {
                Matrix temp = new Matrix(); //Model
                temp.structure_role = Convert.ToString(table.Rows[0][1]);
                temp.structure_type = Convert.ToString(table.Rows[0][1]).Split(" ").Last();
                for (int rowCounter = 0; rowCounter <= table.Rows.Count - 1; rowCounter++)
                {
                    switch (rowCounter)
                    {
                        case 1: temp.subrole = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 2: temp.position_name = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 3: temp.grade_group = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 4: temp.grade_number = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 5: temp.fot_high = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 6: temp.fot_middle = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 7: temp.fot_region = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                        case 8: temp.fot_low = Convert.ToString(table.Rows[rowCounter][colCounter]); break;
                    }
                }

                excelDataReader.Close();

                //InsertToDB
                NpgsqlCommand cmd = new NpgsqlCommand("Select * from public.\"MATRIX\" where structure_type=\'" + temp.structure_type + "\'", nc);
                var reader = cmd.ExecuteReader();
                int RowNumber = 0;
                while (reader.Read())
                {
                    RowNumber++;
                }
                if (RowNumber == 5) //матрица такого типа есть
                {
                    NpgsqlCommand com = new NpgsqlCommand("call update_data_in_Matrix" + "(:structure_role, :subrole, :position_name," +
                    " :grade_group, :grade_number, :fot_high, :fot_middle, :fot_region, :fot_low, :structure_type, :new_id)", nc);
                    com.Parameters.AddWithValue("structure_role", DbType.Int32).Value = temp.structure_role;
                    com.Parameters.AddWithValue("subrole", DbType.String).Value = temp?.subrole ?? "";
                    com.Parameters.AddWithValue("position_name", DbType.String).Value = temp.position_name;
                    com.Parameters.AddWithValue("grade_group", DbType.String).Value = temp.grade_group;
                    com.Parameters.AddWithValue("grade_number", DbType.String).Value = temp.grade_number;
                    com.Parameters.AddWithValue("fot_high", DbType.Date).Value = temp.fot_high == null ? DBNull.Value : temp.fot_high;
                    com.Parameters.AddWithValue("fot_middle", DbType.String).Value = temp.fot_middle == null ? DBNull.Value : temp.fot_middle;
                    com.Parameters.AddWithValue("fot_region", DbType.String).Value = temp.fot_region;
                    com.Parameters.AddWithValue("fot_low", DbType.String).Value = temp.fot_low;
                    com.Parameters.AddWithValue("structure_type", DbType.String).Value = temp.structure_type;
                    com.Parameters.AddWithValue("new_id", DbType.Int32).Value = colCounter;
                    com.CommandType = CommandType.Text;
                    cmd.Dispose(); //удаляем команду и закрываем чтение
                    reader.Close();
                    com.ExecuteNonQuery(); //выполнение процедуры
                }
                else //не нашелся
                {
                    NpgsqlCommand com = new NpgsqlCommand("call insert_data_in_Matrix" + "(:structure_role, :subrole, :position_name," +
                     " :grade_group, :grade_number, :fot_high, :fot_middle, :fot_region, :fot_low, :structure_type)", nc);
                    com.Parameters.AddWithValue("structure_role", DbType.Int32).Value = temp.structure_role;
                    com.Parameters.AddWithValue("subrole", DbType.String).Value = temp?.subrole ?? "";
                    com.Parameters.AddWithValue("position_name", DbType.String).Value = temp.position_name;
                    com.Parameters.AddWithValue("grade_group", DbType.String).Value = temp.grade_group;
                    com.Parameters.AddWithValue("grade_number", DbType.String).Value = temp.grade_number;
                    com.Parameters.AddWithValue("fot_high", DbType.Date).Value = temp.fot_high == null ? DBNull.Value : temp.fot_high;
                    com.Parameters.AddWithValue("fot_middle", DbType.String).Value = temp.fot_middle == null ? DBNull.Value : temp.fot_middle;
                    com.Parameters.AddWithValue("fot_region", DbType.String).Value = temp.fot_region;
                    com.Parameters.AddWithValue("fot_low", DbType.String).Value = temp.fot_low;
                    com.Parameters.AddWithValue("structure_type", DbType.String).Value = temp.structure_type;
                    com.CommandType = CommandType.Text;
                    cmd.Dispose(); //удаляем команду и закрываем чтение
                    reader.Close();
                    com.ExecuteNonQuery(); //выполнение процедуры
                }

            }
        }
        public static void ImportAssessmentInfo(string filePath, NpgsqlConnection nc)
        {


        }

        public static void ImportAssessmentResult(string filePath, NpgsqlConnection nc)
        {

        }

        public static void ImportKKResult(string filePath, NpgsqlConnection nc)
        {


        }
    }
}

