using Bank.Domain.Enum;

namespace Bank.Domain.ViewModels.UserInfo
{
    public class UserInfoViewModel
    {
        public int service_number { get; set; }
        public string fullname { get; set; }
        public string position_name { get; set; }
        public DateTime position_date { get; set; }
        public DateTime hire_date { get; set; }
        public DateTime? dismiss_date { get; set; } = null;
        public string department { get; set; }
        public string division_name { get; set; }
        public string sector_name { get; set; }
        public string status { get; set; }
        public string workday_balance { get; set; }
        public string list_number { get; set; }
        public DateTime start_date { get; set; }
        public string pass { get; set; }
        public DateTime end_date { get; set; }
        public Role user_role { get; set; }
        public string login { get; set; }
    }
}
