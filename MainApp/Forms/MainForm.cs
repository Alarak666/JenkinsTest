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
                                // ���������, ���� �� ����� ��� ���������
                                if (!addedForms.Contains(typedService.ServiceType.Name))
                                {
                                    // �������� ��� ������
                                    string assemblyName = typedService.ServiceType.Assembly.GetName().Name;
                                    // �������� ������� ������
                                    // ��������� ������ ��� ������ ������������������ �����
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
                MessageBox.Show($"������ �������� ������: {ex.Message}");
            }
        }

        private int buttonTop = 20; // ��������� ������� ��� ������ ������

        private void AddButtonToGroupBox(Type formType, string buttonText)
        {
            // ������� ImageList ��� ���������� ��������� �����������
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(32, 32); // ������������� ������������� ������ �����������
            Image formIcon = GetFormIcon(formType);
            if (formIcon != null)
            {
                imageList.Images.Add(formIcon);
            }

            Button button = new Button
            {
                Text = "    " + buttonText, // ��������� ������ ��� �����������
                Width = 200,
                Height = 40,
                Top = buttonTop,
                Left = 10,
                TextAlign = ContentAlignment.MiddleCenter, // ����� �����
                ImageAlign = ContentAlignment.MiddleLeft, // ����������� �����
                ImageList = imageList,
                ImageIndex = 0 // ������������� ������ ����������� � ImageList
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

            // ����������� �������� buttonTop �� ������ ������ ���� ������
            buttonTop += button.Height + 10;
        }

        private Image GetFormIcon(Type formType)
        {
            try
            {
                // �������� ������������ �����
                var constructors = formType.GetConstructors();

                // ������� ����������� � ����������� ����������� ����������
                var constructor = constructors.OrderBy(c => c.GetParameters().Length).FirstOrDefault();

                if (constructor != null)
                {
                    // ������� ��������� ��� ������������
                    var parameters = constructor.GetParameters().Select(p => GetDefault(p.ParameterType)).ToArray();

                    // ������� ��������� �����
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
                Console.WriteLine($"������ ��� ��������� ������ �� ����� {formType.Name}: {ex.Message}");
            }

            // ���� ������ �� �������, ������� null ��� ����������� �� ���������
            return null;
        }

        // ����� ��� ��������� �������� �� ��������� ��� ����
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
