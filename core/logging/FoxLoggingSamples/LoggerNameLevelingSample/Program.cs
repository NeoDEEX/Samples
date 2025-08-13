// See https://aka.ms/new-console-template for more information
Console.WriteLine("Fox Logging Logger Name Leveling Sample...");

var salaryService = new HR.Salary.SalaryService();
var batchService = new HR.Salary.SalaryBatch();
var hrEtcService = new HR.Etc.EtcService();
var hrCommonService = new HR.CommonService();

var closingService = new FI.Closing.ClosingService();
var fiEtcService = new FI.Etc.EtcService();
var fiCommonService = new FI.CommonService();

salaryService.Calc("327856");
batchService.BatchCalculate();
hrEtcService.DoSomething();
hrCommonService.GetCommonData();

closingService.DoClosing();
fiEtcService.DoSomething();
fiCommonService.GetCommonData();

