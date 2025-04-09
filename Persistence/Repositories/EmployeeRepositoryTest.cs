using CleanArchTask.Application.Features.Employee.Commands.AddEditCmd;
using CleanArchTask.Application.Features.Employee.Queries.GetEmployeesQuery;
using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Entities;


namespace CleanArchTask.Persistence.Repositories
{
    public class EmployeeRepositoryTest : IEmployeeRepository
    {
        #region Data
        public List<Employee> Employees { get; } = new List<Employee>
        {
            new Employee { Id = 1, FullNameAr = "أحمد محمد", FullNameEn = "Ahmed Mohamed", DepartmentAr = "الموارد البشرية", DepartmentEn = "HR" },
            new Employee { Id = 2, FullNameAr = "منى علي", FullNameEn = "Mona Ali", DepartmentAr = "المبيعات", DepartmentEn = "Sales" },
            new Employee { Id = 3, FullNameAr = "كريم حسن", FullNameEn = "Karim Hassan", DepartmentAr = "تكنولوجيا المعلومات", DepartmentEn = "IT" },
            new Employee { Id = 4, FullNameAr = "سارة محمود", FullNameEn = "Sara Mahmoud", DepartmentAr = "المحاسبة", DepartmentEn = "Accounting" },
            new Employee { Id = 5, FullNameAr = "علي إبراهيم", FullNameEn = "Ali Ibrahim", DepartmentAr = "التسويق", DepartmentEn = "Marketing" },
            new Employee { Id = 6, FullNameAr = "نهى عبد الله", FullNameEn = "Noha Abdallah", DepartmentAr = "الموارد البشرية", DepartmentEn = "HR" },
            new Employee { Id = 7, FullNameAr = "يوسف فؤاد", FullNameEn = "Youssef Fouad", DepartmentAr = "تكنولوجيا المعلومات", DepartmentEn = "IT" },
            new Employee { Id = 8, FullNameAr = "رحاب سعيد", FullNameEn = "Rehab Saeed", DepartmentAr = "المبيعات", DepartmentEn = "Sales" },
            new Employee { Id = 9, FullNameAr = "هاني عبد الرحمن", FullNameEn = "Hany Abdelrahman", DepartmentAr = "التسويق", DepartmentEn = "Marketing" },
            new Employee { Id = 10, FullNameAr = "دينا أشرف", FullNameEn = "Dina Ashraf", DepartmentAr = "المحاسبة", DepartmentEn = "Accounting" },
            new Employee { Id = 11, FullNameAr = "مازن طارق", FullNameEn = "Mazen Tarek", DepartmentAr = "تكنولوجيا المعلومات", DepartmentEn = "IT" },
            new Employee { Id = 12, FullNameAr = "لبنى عادل", FullNameEn = "Lubna Adel", DepartmentAr = "المبيعات", DepartmentEn = "Sales" },
            new Employee { Id = 13, FullNameAr = "عبد الرحمن سامي", FullNameEn = "Abdelrahman Sami", DepartmentAr = "الموارد البشرية", DepartmentEn = "HR" },
            new Employee { Id = 14, FullNameAr = "فاطمة الزهراء", FullNameEn = "Fatma Elzahraa", DepartmentAr = "التسويق", DepartmentEn = "Marketing" },
            new Employee { Id = 15, FullNameAr = "محمد شعبان", FullNameEn = "Mohamed Shaaban", DepartmentAr = "المحاسبة", DepartmentEn = "Accounting" },
            new Employee { Id = 16, FullNameAr = "ياسمين حسين", FullNameEn = "Yasmin Hussein", DepartmentAr = "المبيعات", DepartmentEn = "Sales" },
            new Employee { Id = 17, FullNameAr = "طارق منصور", FullNameEn = "Tarek Mansour", DepartmentAr = "تكنولوجيا المعلومات", DepartmentEn = "IT" },
            new Employee { Id = 18, FullNameAr = "إيمان أحمد", FullNameEn = "Eman Ahmed", DepartmentAr = "الموارد البشرية", DepartmentEn = "HR" },
            new Employee { Id = 19, FullNameAr = "جمال حمدي", FullNameEn = "Gamal Hamdy", DepartmentAr = "التسويق", DepartmentEn = "Marketing" },
            new Employee { Id = 20, FullNameAr = "هبة علي", FullNameEn = "Heba Ali", DepartmentAr = "المحاسبة", DepartmentEn = "Accounting" },
        };

        #endregion
        public async Task<int> AddEmployeeAsync(AddEditEmployeeCmd request)
        {
            int currentId = Employees
                .OrderByDescending(e => e.Id)
                .FirstOrDefault()?.Id ?? 0;

            var employee = new Employee()
            {
                Id = currentId + 1,
                FullNameAr = request.FullNameAr,
                FullNameEn = request.FullNameEn,
                DepartmentAr = request.DepartmentAr,
                DepartmentEn = request.DepartmentEn,
            };
            Employees.Add(employee);
            return employee.Id;
        }

        public async Task<int> DeleteEmployeesAsync(int[] ids)
        {
            var count  = 0;
            foreach (var id in ids)
            {
                var employee = Employees.FirstOrDefault(e => e.Id == id);
                if (employee is null)
                    continue;
                count++;
                Employees.Remove(employee);
            }
            return count;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = Employees.FirstOrDefault(x => x.Id == id);
            return employee;
        }

        public async Task<(IEnumerable<Employee>, int)> GetEmployeesAsync(GetEmployeesQuery request)
        {
            int count = Employees.Count;
            var employees = Employees;
            if (request.Mode == WorkingMode.Client)
            {
                return (employees, count);
            }
            else
            {
                // Sorting
                #region Sorting
                switch (request.SortColumn?.ToLower())
                {
                    case "fullnamear":
                        employees = request.SortDirection.ToLower() == "desc"
                            ? employees.OrderByDescending(x => x.FullNameAr).ToList()
                            : employees.OrderBy(x=>x.FullNameAr).ToList();
                        break;
                    case "fullnameen":
                        employees = request.SortDirection.ToLower() == "desc"
                            ? employees.OrderByDescending(x => x.FullNameEn).ToList()
                            : employees.OrderBy(x => x.FullNameEn).ToList();
                        break;
                    case "departmentar":
                        employees = request.SortDirection.ToLower() == "desc"
                            ? employees.OrderByDescending(x => x.DepartmentAr).ToList()
                            : employees.OrderBy(x => x.DepartmentAr).ToList();
                        break;
                    case "departmenten":
                        employees = request.SortDirection.ToLower() == "desc"
                            ? employees.OrderByDescending(x => x.DepartmentEn).ToList()
                            : employees.OrderBy(x => x.DepartmentEn).ToList();
                        break;
                    case "id":
                    default:
                        employees = request.SortDirection.ToLower() == "desc"
                            ? employees.OrderByDescending(x => x.Id).ToList()
                            : employees.OrderBy(x => x.Id).ToList();
                        break;
                }
                #endregion

                // Pagination
                var result = employees.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize).ToList();

                return (result, count);
            }
        }

        public async Task<int> UpdateEmployeeAsync(AddEditEmployeeCmd request)
        {
            var employee = Employees.Find(e=>e.Id == request.Id);
            if (employee is null)
                return -1;
            employee.FullNameAr = request.FullNameAr;
            employee.FullNameEn = request.FullNameEn;
            employee.DepartmentAr = request.DepartmentAr;
            employee.DepartmentEn = request.DepartmentEn;
            return 1;
        }
    }
}
