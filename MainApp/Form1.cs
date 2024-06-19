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
            // Создаем кнопку
            Button button = new Button();
            button.Text = buttonText;
            button.Width = 200; // Задайте нужную ширину кнопки
            button.Height = 40; // Задайте нужную высоту кнопки
            button.Top = buttonTop; // Устанавливаем вертикальную позицию кнопки
            button.Left = 10; // Устанавливаем горизонтальную позицию кнопки

            // Подписываемся на событие Click
            button.Click += (sender, e) =>
            {
                using (var scope = Program.Container.BeginLifetimeScope())
                {
                    var formFactory = scope.Resolve<FormFactory>();
                    Form form = formFactory.CreateForm(formType);
                    form.ShowDialog();
                }
            };

            // Добавляем кнопку в GroupBox
            groupBox1.Controls.Add(button);

            // Обновляем позицию для следующей кнопки
            buttonTop += button.Height + 10; // 10 - отступ между кнопками
        }

    }
}
