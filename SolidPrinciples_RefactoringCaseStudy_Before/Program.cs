Employee employee = new Developer
{
    FirstName = "Anna",
    LastName = "Lima",
    Position = "Developer",
    HoursThisMonth = 160,
    BugsFixed = 6
};
var employeeReportCreator = new EmployeeReportCreator(new File(), new EmployeeFileNameBuilder());
employeeReportCreator.CreateReportFor(employee);

Console.WriteLine("Done!");
Console.ReadKey();

public interface IEmployeeReportCreator
{
    void CreateReportFor(Employee employee);
}

public interface IFile
{
    public void SaveToFile(string path, string contents);
}

public class File : IFile {
    public void SaveToFile(string path, string contents) {
        System.IO.File.WriteAllText(path, contents);
    }
}

public class EmployeeReportCreator : IEmployeeReportCreator
{
    private IFile _file;
    private IEmployeeFileNameBuilder _employeeFileNameBuilder;

    public EmployeeReportCreator(IFile file, IEmployeeFileNameBuilder employeeFileNameBuilder) {
        _file = file;
        _employeeFileNameBuilder = employeeFileNameBuilder;
    }

    public void CreateReportFor(Employee employee)
    {
        var report = employee.GenerateReport();
        var fileName = _employeeFileNameBuilder.BuildFileName(employee.FirstName, employee.LastName);
        _file.SaveToFile(fileName, report);
    }
}

public interface IEmployeeFileNameBuilder {
    public string BuildFileName(string FirstName, string LastName);
}

public class EmployeeFileNameBuilder : IEmployeeFileNameBuilder {
    
    public string BuildFileName(string FirstName, string LastName) {
        return $"{FirstName}_{LastName}.txt";
    }    
}

public class Employee
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Position { get; init; }
    public int HoursThisMonth { get; init; }

    public virtual string GenerateReport()
    {
        return
            $"{FirstName} {LastName}," +
            $" working as {Position}," +
            $" worked {HoursThisMonth} hours this month.";
    }
}

public class Developer : Employee
{
    public int BugsFixed { get; init; }

    public override string GenerateReport() {
        return 
        base.GenerateReport()
        + $" and fixed {BugsFixed} bugs.";

    }
}

