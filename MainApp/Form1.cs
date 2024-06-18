using System.Reflection;
using Autofac;
using Autofac.Core;

namespace MainApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadFormsFromContainer();
        }

        private void LoadFormsFromContainer()
        {
            try
            {
                using (var scope = Program.Container.BeginLifetimeScope())
                {
                    var formFactory = scope.Resolve<FormFactory>();

                    foreach (var registration in Program.Container.ComponentRegistry.Registrations)
                    {
                        foreach (var service in registration.Services)
                        {
                            if (service is TypedService typedService && typedService.ServiceType.IsSubclassOf(typeof(Form)))
                            {
                                Form form = formFactory.CreateForm(typedService.ServiceType);
                                AddFormToGroupBox(form);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сборки: {ex.Message}");
            }
        }

        private void AddFormToGroupBox(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Top;
            form.Visible = true;
            groupBox1.Controls.Add(form); // Предполагается, что ваш GroupBox называется groupBox1
        }
    }
}
