namespace Cifra.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string CitizenID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int UserType { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
    }
}
