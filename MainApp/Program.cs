using Autofac;
using FZFarm.Core.Constants.Enums;
using FZFarm.Core.Exceptions;
using FZFarm.Core.Helpers;
using FZFarm.Core.Models.Exceptions;
using FZFarm.Core.Services.Loaders;
using FZFarm.MainApp.Forms;
using Serilog;
using Serilog.Events;

namespace FZFarm.MainApp
{
    public static class Program
    {
        public static IContainer Container { get; private set; }

        [STAThread]
        static void Main()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(
                    "Logs\\log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information)
                .CreateLogger();

            Log.Logger = logger;

            string basePath = @"D:\Git\AdminPortal\AdminPortal\bin\Debug\net8.0-windows";

            // ������ ������ ����������� ������
            DependencyLoader.PrintLoadedAssemblies();

            // ��������� ���������� � ��������� ��������� ��� ���������� ������
            var containerConfig = new ContainerConfig(basePath,  "AdminPortal");
            Container = containerConfig.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (sender, args) => GlobalExceptionHandler(args.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => GlobalExceptionHandler(args.ExceptionObject as Exception);
            Application.Run(new MainForm());
        }
        public static void GlobalExceptionHandler(Exception e)
        {
            ErrorInfo errorInfo = ProcessException(e);

            MessageBox.Show($"" +
                            $"�������: {errorInfo.Message}\n" +
                            $"���: {errorInfo.Type}\n", "���", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Log.Logger.Error(e, e.Message);

        }
        private static ErrorInfo ProcessException(Exception ex)
        {
            switch (ex)
            {
                case CustomException customException:
                    return new ErrorInfo
                    {
                        Type = customException.ErrorType,
                        Message = customException.Message,
                    };
                case BadRequestException badRequestException:
                    return new ErrorInfo
                    {
                        Type = badRequestException.ErrorType,
                        Message = badRequestException.Message,
                    };
                default:
                    return new ErrorInfo
                    {
                        Type = ErrorType.General,
                        Message = ex.Message,
                    };
            }
        }
    }
}