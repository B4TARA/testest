namespace Bank.Domain.ViewModels.UserStructure
{
    internal class UserStructureViewModel
    {
        public int service_number { get; set; }
        public string grade_group { get; set; }
        public string grade_number { get; set; }
        public string structure_role { get; set; }
        public string subrole { get; set; }
        public DateTime? subrole_date { get; set; } = null;
        public string subrole_reason { get; set; }
        public string role_category { get; set; }
        public string fot_mark { get; set; }
        public int manager_service_number { get; set; }
        public string manager_fullname { get; set; }
        public int chief_service_number { get; set; }
        public string chief_fullname { get; set; }
        public int director_service_number { get; set; }
        public string director_fullname { get; set; }
        public string block_name { get; set; }
        public int? curator_service_number { get; set; } = null;
        public string curator_fullname { get; set; }
    }
}
