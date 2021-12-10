using IronXL;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Parser
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Danger> dangers = new List<Danger>();
        int page = 0;

        string shablon = "https://drive.google.com/uc?export=download&id=1lrUZH0o6yZyPo33UdD9ONihCv6HNhEQ7";
        string fileUri = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        public MainWindow()
        {
            InitializeComponent();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Resource\\left.jpg"));
            Left.Content = image;
            image = new Image();
            image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Resource\\right.jpg"));
            Right.Content = image;
            try
            {
                Danger danger = AppDbContext.db.Danger.FirstOrDefault(t => t.Id == 0);
            }
            catch (SqliteException)
            {
                MessageBox.Show("Локальное хранилище не обнауженно, данные будут загружены");
                WebClient webClient = new WebClient();
                webClient.DownloadFile(new Uri(shablon), "DangerDB.db");
                try
                {
                    Danger danger = AppDbContext.db.Danger.FirstOrDefault(t => t.Id == 0);
                }
                catch (SqliteException)
                {
                }
                webClient = new WebClient();
                webClient.DownloadFile(new Uri(fileUri), "input.xlsx");
                WorkBook workbook = WorkBook.Load("input.xlsx");
                WorkSheet sheet = workbook.WorkSheets.First();
                for (int i = 3; sheet["A" + i].IntValue != 0; i++)
                {
                    AppDbContext.db.Danger.Add(new Danger()
                    {
                        UBID = sheet["A" + i].IntValue,
                        Name = sheet["B" + i].Value as string,
                        Description = sheet["C" + i].Value as string,
                        SourceOfThreat = sheet["D" + i].Value as string,
                        Object = sheet["E" + i].Value as string,
                        BreachOfConfidentiality = sheet["F" + i].IntValue == 1,
                        IntegrityViolation = sheet["G" + i].IntValue == 1,
                        AccessibilityViolation = sheet["H" + i].IntValue == 1
                    }); ;
                }
                AppDbContext.db.SaveChanges();
                File.Delete("input.xlsx");
            }
            Label_MouseLeftButtonDown(null, null);
        }

        private void AddBtn_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dictionary = new Dictionary<int, (Danger, Danger, bool)>();
            Result.Items.Clear();
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(new Uri(fileUri), "input.xlsx");
                WorkBook workbook = WorkBook.Load("input.xlsx");
                WorkSheet sheet = workbook.WorkSheets.First();
                for (int i = 3; sheet["A" + i].IntValue != 0; i++)
                {
                    Danger updateData = new Danger()
                    {
                        UBID = sheet["A" + i].IntValue,
                        Name = sheet["B" + i].Value as string,
                        Description = sheet["C" + i].Value as string,
                        SourceOfThreat = sheet["D" + i].Value as string,
                        Object = sheet["E" + i].Value as string,
                        BreachOfConfidentiality = sheet["F" + i].IntValue == 1,
                        IntegrityViolation = sheet["G" + i].IntValue == 1,
                        AccessibilityViolation = sheet["H" + i].IntValue == 1
                    };
                    Danger oldData = AppDbContext.db.Danger.FirstOrDefault(t => t.UBID == updateData.UBID);
                    if (oldData != null)
                    {
                        if (!oldData.Equals(updateData))
                        {
                            dictionary.Add(updateData.UBID, (oldData, updateData, true));
                            AppDbContext.db.Danger.Remove(oldData);
                            AppDbContext.db.Danger.Add(updateData);
                        }
                        else
                        {
                            dictionary.Add(updateData.UBID, (oldData, updateData, false));
                        }
                    }
                    else
                    {
                        dictionary.Add(updateData.UBID, (oldData, updateData, true));
                        AppDbContext.db.Danger.Add(updateData);
                    }
                }
                foreach (var value in AppDbContext.db.Danger)
                {
                    if (!dictionary.ContainsKey(value.UBID))
                    {
                        dictionary.Add(value.UBID, (value, null, true));
                        AppDbContext.db.Danger.Remove(value);
                    }
                }
                AppDbContext.db.SaveChanges();
                File.Delete("input.xlsx");
                Status.Content = $"Успешно: {dictionary.Where(t => t.Value.Item3).Count()}";
                Status.Foreground = new SolidColorBrush(Colors.Green);
                foreach (var value in dictionary.Where(t => t.Value.Item3))
                {
                    if (value.Value.Item1 == null)
                    {
                        Label label = new Label();
                        label.Content = value.Value.Item2;
                        label.Foreground = new SolidColorBrush(Colors.Green);
                        Result.Items.Add(label);
                    }
                    else
                    {
                        if (value.Value.Item2 == null)
                        {
                            Label label = new Label();
                            label.Content = value.Value.Item1;
                            label.Foreground = new SolidColorBrush(Colors.Red);
                            Result.Items.Add(label);
                        }
                        else
                        {
                            TreeViewItem item = new TreeViewItem();
                            item.Header = value.Value.Item2.ToString();
                            item.Foreground = new SolidColorBrush(Colors.Yellow);
                            Label label = new Label();
                            label.Content = Danger.Compare(value.Value.Item1, value.Value.Item2);
                            label.Foreground = new SolidColorBrush(Colors.Black);
                            item.Items.Add(label);
                            Result.Items.Add(item);
                        }
                    }
                }

            }
            catch (Exception exp)
            {
                Status.Content = $"Ошибка\r\n{exp.Message}";
                Status.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void Label_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dangers = AppDbContext.db.Danger.ToList();
            page = 0;
            Left.IsEnabled = false;
            if (page * 15 == dangers.Count || (dangers.Count - page * 15 < 15)) Right.IsEnabled = false;
            view.Items.Clear();
            Loader();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            page--;
            if (page == 0) Left.IsEnabled = false;
            if (page * 15 == dangers.Count || (dangers.Count - page * 15 < 15)) Right.IsEnabled = false;
            else Right.IsEnabled = true;
            view.Items.Clear();
            Loader();
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            page++;
            if (page * 15 == dangers.Count || (dangers.Count - page * 15 < 15)) Right.IsEnabled = false;
            if (page == 0) Left.IsEnabled = false;
            else Left.IsEnabled = true;
            view.Items.Clear();
            Loader();
        }

        private void Loader()
        {
            for (int i = page * 15; i < dangers.Count && i < page * 15 + 15; i++)
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = dangers[i];
                view.Items.Add(item);
                TreeViewItem description = new TreeViewItem();
                description.Header = "Описание угрозы";
                description.Items.Add(dangers[i].Description);
                TreeViewItem sourceOfThreat = new TreeViewItem();
                sourceOfThreat.Header = "Источник угрозы";
                sourceOfThreat.Items.Add(dangers[i].SourceOfThreat);
                TreeViewItem obj = new TreeViewItem();
                obj.Header = "Объект воздействия угрозы";
                obj.Items.Add(dangers[i].Object);
                TreeViewItem breachOfConfidentiality = new TreeViewItem();
                breachOfConfidentiality.Header = "Нарушение конфиденциальности";
                if (dangers[i].BreachOfConfidentiality)
                {
                    breachOfConfidentiality.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    breachOfConfidentiality.Foreground = new SolidColorBrush(Colors.Green);
                }
                TreeViewItem integrityViolation = new TreeViewItem();
                integrityViolation.Header = "Нарушение целостности";
                if (dangers[i].IntegrityViolation)
                {
                    integrityViolation.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    integrityViolation.Foreground = new SolidColorBrush(Colors.Green);
                }
                TreeViewItem accessibilityViolation = new TreeViewItem();
                accessibilityViolation.Header = "Нарушение доступности";
                if (dangers[i].AccessibilityViolation)
                {
                    accessibilityViolation.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    accessibilityViolation.Foreground = new SolidColorBrush(Colors.Green);
                }
                item.Items.Add(description);
                item.Items.Add(sourceOfThreat);
                item.Items.Add(obj);
                item.Items.Add(breachOfConfidentiality);
                item.Items.Add(integrityViolation);
                item.Items.Add(accessibilityViolation);
            }
        }
    }
}
