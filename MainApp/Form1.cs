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
                    foreach (var registration in Program.Container.ComponentRegistry.Registrations)
                    {
                        foreach (var service in registration.Services)
                        {
                            if (service is TypedService typedService && typedService.ServiceType.IsSubclassOf(typeof(Form)))
                            {
                                // Добавляем кнопку для каждой зарегистрированной формы
                                AddButtonToGroupBox(typedService.ServiceType, typedService.ServiceType.Name);
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

        private int buttonTop = 10; // Начальная позиция для первой кнопки

        private void AddButtonToGroupBox(Type formType, string buttonText)
        {
            Button button = new Button();
            button.Text = buttonText;
            button.Width = 200; 
            button.Height = 40; 
            button.Top = buttonTop; 
            button.Left = 10; 

            button.Click += (sender, e) =>
            {
                using (var scope = Program.Container.BeginLifetimeScope())
                {
                    var formFactory = scope.Resolve<FormFactory>();
                    Form form = formFactory.CreateForm(formType);
                    form.ShowDialog();
                }
            };

            groupBox1.Controls.Add(button);

            buttonTop += button.Height + 10;
        }

    }
}
