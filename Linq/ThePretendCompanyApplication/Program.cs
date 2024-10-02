using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TCPData;
using ThePretendCompanyApplication;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<Data>();
    })
    .Build();

List<Department> departmentList = Data._departments;
List<Employee> employeeList = Data._employees;

LinqProgram.LinqProgramMain(departmentList, employeeList);
LinqProgramLesson2.LinqProgramLesson2Main(departmentList, employeeList);
LinqProgramLesson3.LinqProgramLesson3Main(departmentList, employeeList);
LinqProgramLesson4.LinqProgramLesson4Main(departmentList, employeeList);
