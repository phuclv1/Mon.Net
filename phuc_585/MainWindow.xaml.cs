using phuc_585.dulieu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace phuc_585
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QlbanHangContext dl = new QlbanHangContext();
        public MainWindow()
        {
            InitializeComponent();
        }

       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hienthi();
            var cb = from a in dl.LoaiSps
                     select a;
            cbl.ItemsSource = cb.ToList();
            cbl.DisplayMemberPath = "TenLoai";
            cbl.SelectedValuePath = "MaLoai";
        }
        private void hienthi()
        {
            var ht = from a in dl.SanPhams
                     orderby a.DonGia descending
                     select new
                     {
                         a.MaSp,
                         a.TenSp,
                         a.MaLoaiNavigation.TenLoai,
                         a.SoLuongCo,
                         a.DonGia,
                         ThanhTien = a.DonGia * a.SoLuongCo,
                     };
            data.ItemsSource = ht.ToList();
        }
        
  private bool kt()
        {
            try
            {
                int n = int.Parse(txslc.Text);
                if (n <= 0)
                {
                    MessageBox.Show("so luong co phai lon hon 0","kiem tra", MessageBoxButton.OK,MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("so luong co phai la so nguyen !", "kiem tra", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                int m = int.Parse(txdg.Text);

                if (m <= 0)
                {
                    MessageBox.Show("don gia phai lon hon 0", "kiem tra", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("don gia phai la so nguyen", "kiem tra", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void them_Click(object sender, RoutedEventArgs e)
        {
            if (kt()==true)
            {
                SanPham a = new SanPham();
                a.MaSp = txm.Text;
                a.TenSp = txten.Text;
                a.MaLoai = cbl.SelectedValue.ToString();
                a.SoLuongCo = int.Parse(txslc.Text);
                a.DonGia = int.Parse(txdg.Text);

                dl.SanPhams.Add(a);
                dl.SaveChanges();
                hienthi(); 
            }
        }
       
        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var d = data.SelectedItem;
            if (d != null)
            {
                Type t = d.GetType();
                PropertyInfo[] dong = t.GetProperties();
                txm.Text = dong[0].GetValue(d).ToString();
                txten.Text = dong[1].GetValue(d).ToString();
                cbl.Text = dong[2].GetValue(d).ToString();
                txslc.Text = dong[3].GetValue(d).ToString();
                txdg.Text = dong[4].GetValue(d).ToString();
            }
        }

        private void sua_Click(object sender, RoutedEventArgs e)
        {
            var s = from a in dl.SanPhams 
                    where a.MaSp==txm.Text
                    select a;
            if (s.Count() > 0)
            {
               SanPham  a = s.SingleOrDefault();
                a.TenSp = txten.Text;
                a.MaLoai = cbl.SelectedValue.ToString();
                a.SoLuongCo = int.Parse(txslc.Text);
                a.DonGia = int.Parse(txdg.Text);
                dl.SanPhams.Add(a);
                dl.SaveChanges();
            }
        }
    }
}
