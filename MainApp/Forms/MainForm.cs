using Autofac;
using Autofac.Core;
using FZFarm.Core.Services.Factories;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace FZFarm.MainApp.Forms
{
    public partial class MainForm : Form
    {
        private HashSet<string> addedForms;

        public MainForm()
        {
            InitializeComponent();
            addedForms = new HashSet<string>();
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
                            if (service is TypedService typedService && typedService.ServiceType.IsSubclassOf(typeof(Form)) && typedService.ServiceType.Name == "MainForm")
                            {
                                // Проверяем, была ли форма уже добавлена
                                if (!addedForms.Contains(typedService.ServiceType.Name))
                                {
                                    // Получаем имя сборки
                                    string assemblyName = typedService.ServiceType.Assembly.GetName().Name;
                                    // Получаем логотип сборки
                                    // Добавляем кнопку для каждой зарегистрированной формы
                                    AddButtonToGroupBox(typedService.ServiceType, assemblyName);
                                    addedForms.Add(typedService.ServiceType.Name);
                                }
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

        private int buttonTop = 20; // Начальная позиция для первой кнопки

        private void AddButtonToGroupBox(Type formType, string buttonText)
        {
            // Создаем ImageList для управления размерами изображений
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(32, 32); // Устанавливаем фиксированный размер изображения
            Image formIcon = GetFormIcon(formType);
            if (formIcon != null)
            {
                imageList.Images.Add(formIcon);
            }

            Button button = new Button
            {
                Text = "    " + buttonText, // Добавляем отступ для изображения
                Width = 200,
                Height = 40,
                Top = buttonTop,
                Left = 10,
                TextAlign = ContentAlignment.MiddleCenter, // Текст слева
                ImageAlign = ContentAlignment.MiddleLeft, // Изображение слева
                ImageList = imageList,
                ImageIndex = 0 // Устанавливаем индекс изображения в ImageList
            };
            button.Click += (sender, e) =>
            {
                using (var scope = Program.Container.BeginLifetimeScope())
                {
                    var formFactory = scope.Resolve<FormFactory>();
                    Form form = formFactory.CreateForm(formType);
                    form.ShowDialog();
                }
            };

            GroupBoxModul.Controls.Add(button);

            // Увеличиваем значение buttonTop на высоту кнопки плюс отступ
            buttonTop += button.Height + 10;
        }

        private Image GetFormIcon(Type formType)
        {
            try
            {
                // Получаем конструкторы формы
                var constructors = formType.GetConstructors();

                // Находим конструктор с минимальным количеством параметров
                var constructor = constructors.OrderBy(c => c.GetParameters().Length).FirstOrDefault();

                if (constructor != null)
                {
                    // Создаем параметры для конструктора
                    var parameters = constructor.GetParameters().Select(p => GetDefault(p.ParameterType)).ToArray();

                    // Создаем экземпляр формы
                    using (var formInstance = (Form)constructor.Invoke(parameters))
                    {
                        if (formInstance.Icon != null)
                        {
                            return formInstance.Icon.ToBitmap();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении иконки из формы {formType.Name}: {ex.Message}");
            }

            // Если иконка не найдена, вернуть null или изображение по умолчанию
            return null;
        }

        // Метод для получения значения по умолчанию для типа
        private object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }



    }
}
