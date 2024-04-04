using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf12BC_Dinamikus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnFelveszWrap_Click(object sender, RoutedEventArgs e)
        {
            if (SzerepelMarIlyenNev(txtNev.Text))
            {
                MessageBox.Show("Ilyen nevet már használt korábban!");
                return;
            }
            if (rdoButton.IsChecked == true)
            {

                Button gomb = new();
                gomb.Content = txtNev.Text;
                gomb.Click += Gomb_Click;
                wpPanel.Children.Add(gomb);
            }
            else if (rdoTextBlock.IsChecked == true)
            {
                TextBlock szoveg = new();
                szoveg.Text = txtNev.Text;
                wpPanel.Children.Add(szoveg);

            }
        }

        private void Gomb_Click(object sender, RoutedEventArgs e)
        {
            //Megoldási módok

            if (sender is Button)
            {
                Button gomb = (Button)sender;  //Explicit típuskényszerítés. Csak akkor, ha biztosak vagyunk abban, hogy a sender Button példánya!!!
                MessageBox.Show($"Szia! {gomb.Content} vagyok.");
            }

            //Rövid írásmód. Jelentése: Ha a sender Button osztályba tartozik, akkor a gomb nevű objektumra úgy hivatkozhatunk mint Button obj-ra.
            //if (sender is Button gomb)  
            //{
            //    MessageBox.Show($"Szia! {gomb.Content} vagyok.");
            //}
        }

        private bool SzerepelMarIlyenNev(string nev)
        {
            foreach (object? elem in wpPanel.Children)  //object? -ben a ? azt jelenti, hogy az elem értéke null is lehet. Erre is fel kell készülni!
            {
                if (elem is Button)  //Ez Button példánya? FONTOS!
                {
                    Button gomb = (Button)elem;
                    if (gomb.Content.ToString() == nev)
                    {
                        return true;
                    }
                }

                if (elem is TextBlock)  //Ez TextBlock példánya? FONTOS!
                {
                    TextBlock szoveg = (TextBlock)elem;
                    if (szoveg.Text == nev)  //Itt miért nem kell a ToString()? Mivel String-et ad vissza, a Content pedig Object-et, ot azért kellett!
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        private void btnLetrehozGrid_Click(object sender, RoutedEventArgs e)
        {
            //Az előző rács eltávolítása, ha volt
            grdPanel.ColumnDefinitions.Clear();
            grdPanel.RowDefinitions.Clear();

            for (int oszlopIndex = 0; oszlopIndex < sliOszlop.Value; oszlopIndex++)
            {
                ColumnDefinition ujOszlop = new();
                grdPanel.ColumnDefinitions.Add(ujOszlop);

            }
            for (int sorIndex = 0; sorIndex < sliSor.Value; sorIndex++)
            {
                RowDefinition ujSor = new();
                grdPanel.RowDefinitions.Add(ujSor);
            }
        }

        private void btnFelveszGrid_Click(object sender, RoutedEventArgs e)
        {

            // Magyarázat: int.TryParse(txtOszlop.Text, out int oszlopIndex)
            // Olyan mintha előtte létrehoztunk volna egy int oszlopIndex változót, majd próbálnánk int oszlopIndex=int.Parse(txtOszlop.Text); értékadást.
            // A különbség az, hogy a TryParse tájékoztat az átalakítás sikerességéről. true - sikeres, false - nem, a sima Parse pedig nem, csak kivételt dob.
            
            //Három olyan eset, amivel nem tudunk továbblépni
            // 1) érvénytelen formátum
            // 2) 3) tartományon kivüli index
            if (!int.TryParse(txtOszlop.Text, out int oszlopIndex) || oszlopIndex < 1 || oszlopIndex > sliOszlop.Value)
            {
                MessageBox.Show("Érvénytelen érték az oszlop indexnél!");
                txtOszlop.Text = "";
                txtOszlop.Focus();
                return;
            }


            if (!int.TryParse(txtSor.Text, out int sorIndex) || sorIndex < 1 || sorIndex > sliSor.Value)
            {
                MessageBox.Show("Érvénytelen érték a sor indexnél!");
                txtSor.Text = "";
                txtSor.Focus();
                return;
            }

            //Itt most nem vizsgáljuk az ismétlődést, sem azt, hogy az adott cellában van-e már elem! Az utóbbit fontos lenne kezelni!
            if (rdoButton.IsChecked == true)
            {

                Button gomb = new();
                gomb.Content = txtNev.Text;
                gomb.Click += Gomb_Click;

                //Pozícionáljuk a Grid rácsban a vezérlőt, figyelve arra, hogy az indexelés 0-tól indul!
                Grid.SetColumn(gomb, oszlopIndex-1);
                Grid.SetRow(gomb, sorIndex-1);   

                grdPanel.Children.Add(gomb);
            }
            else if (rdoTextBlock.IsChecked == true)
            {
                TextBlock szoveg = new();
                szoveg.Text = txtNev.Text;
                Grid.SetColumn(szoveg, oszlopIndex - 1);
                Grid.SetRow(szoveg, sorIndex - 1);

                grdPanel.Children.Add(szoveg);

            }
        }
    }
}