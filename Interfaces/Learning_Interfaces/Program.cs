using Learning_Interfaces.ServiceImplementations;
using Learning_Interfaces.ServiceInterfaces;
using Learning_Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


// Build the host, useful for DI in console applications
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register CraneOperator and interfaces
        services.AddSingleton<IEmployee, CraneOperator>();
        services.AddSingleton<ICraneOperatorResponsibilities, CraneOperator>();
        services.AddSingleton<IContractEmployee, CraneOperator>();

        services.AddSingleton<IEmployee, HumanResource>();
        services.AddSingleton<IManagerialResponsibilities, HumanResource>();
        services.AddSingleton<IContractEmployee, HumanResource>();

        // Register CraneOperatorService
        services.AddTransient<RoleServices>();

        // If EmployeeProgram or EmployeesProgram need to be registered, do it here
        ////services.AddTransient<EmployeeProgram>();
        ////services.AddTransient<EmployeesProgram>();
        ///we cant inject static class ??
    })
    .Build();

// Now resolve and run your services
var OperationService = host.Services.GetRequiredService<RoleServices>();
OperationService.ProcessCraneOperator();
OperationService.ProcessEmployee();

// Directly call static methods
////EmployeeProgram.EmployeeProgramMain();
////EmployeesProgram.EmployeesProgramMain();
////CraneOperatorProgram.CraneOperatorProgramMain();
////CraneOperatorGPTProgram.CraneOperatorGPTProgramMain();
