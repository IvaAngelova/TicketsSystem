namespace TicketSystem
{
    public class TicketSystemRoles
    {
        public const string Admin = "Администратор";

        public const string Manager = "Мениджър";

        public const string Employee = "Служител";

        public const string AdminOrManager = Admin + ", " + Manager;

        public const string AdminOrManagerOrEmp = Admin + ", " + Manager + ", " + Employee;
    }
}
