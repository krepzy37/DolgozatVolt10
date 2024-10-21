using System.IO;
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



namespace MuveltsegiVerseny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Feladat> feladatok = new();
        public MainWindow()
        {
            InitializeComponent();
            //1. feladat
            using StreamReader sr = new(@"..\..\..\src\feladatok.txt", Encoding.UTF8);
            //_= sr.ReadLine();

            while (!sr.EndOfStream) feladatok.Add(new(sr.ReadLine()));

            foreach (var feladat in feladatok)
            {
                kerdesLB.Items.Add(feladat.Kerdes);
            }



        }
        //2. feladat
        private void f2BTN_Click(object sender, RoutedEventArgs e)
        {

            int f2Szamlalo = 0;
            foreach (var f in feladatok)
            {
                if (f.Kerdes.Contains('?'))
                {
                    f2Szamlalo++;
                }
            }
            f2TBlock.Text = $"A kérdőjeles feladatok száma: {f2Szamlalo}";
        }
        //3. feladat
        private void f3BTN_Click(object sender, RoutedEventArgs e)
        {
            int f3Szamlalo = 0;

            foreach (var f in feladatok)
            {
                if (f.Pontszam.Equals(3) && f.Teamkor.Equals("kemia")) { f3Szamlalo++; }
            }
            f3TBlock.Text = $"{f3Szamlalo}db 3 pontos kémia feladat van.";
        }
        //4. feladat
        private void f4BTN_Click(object sender, RoutedEventArgs e)
        {
            int f4Min = feladatok.Min(x => x.Megoldas);
            int f4Max = feladatok.Max(x => x.Megoldas);
            f4TBlock.Text = $"A válaszok számértéke {f4Min} és {f4Max} között tejred";
        }
        //5. feladat
        private void f5BTN_Click(object sender, RoutedEventArgs e)
        {
            List<string> teamkors = new();

            foreach (var f in feladatok)
            {
                if (!teamkors.Contains(f.Teamkor)) teamkors.Add(f.Teamkor);
            }

            teamkors.Sort();

            f5LB.Items.Clear();
            foreach (var teamkor in teamkors)
            {
                f5LB.Items.Add(teamkor);
            } //mostmár ABC sorrend
        }
        //6. feladat
        private void f6BTN_Click(object sender, RoutedEventArgs e)
        {
            string f6Input = f6IN.Text;
            foreach (var f in feladatok)
            {
                if (f.Kerdes.Contains(f6Input))
                {
                    f6LB.Items.Add(f.Kerdes);
                }
            }
            if (f6LB.Items.Count.Equals(0)) MessageBox.Show("Nem található ilyen kérdés a listában!");

            Random rnd = new();
            int f6index = rnd.Next(0, f6LB.Items.Count);


            f6KivalaszottQ.Text = f6LB.Items[f6index].ToString();

        }

        private void f6BBTN_Click(object sender, RoutedEventArgs e)
        {
            int f6A = int.Parse(f6Answer.Text);
            int f6Points = 0;
            Feladat currentFeladat = null;
            
            foreach (var f in feladatok)
            {
                if (f.Kerdes.Equals(f6KivalaszottQ.Text))
                {
                    currentFeladat = f;
                    f6Points = f.Pontszam;
                    break;
                }
            }

            if (f6A == currentFeladat.Megoldas)
            {
                elertPont.Text = $"A válasz {f6Points} pontot ér.";
            }
            else
            {
                elertPont.Text = $"Nem jó a válasz. 0 pontot ért.";
                MessageBox.Show($"A helyes válasz: {currentFeladat.Megoldas}");
            }
        }
        //7. feladat
        private void f7BTN_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            List<int> usedQuestions = new List<int>();
            int totalPoints = 0;

            using StreamWriter sw = new(
                path: @"..\..\..\src\15_feladat.txt",
                encoding: Encoding.UTF8,
                append: false);

            

            for (int i = 0; i < 15; i++)
            {
                int questionIndex;
                do
                {
                    questionIndex = rnd.Next(feladatok.Count);
                } while (usedQuestions.Contains(questionIndex));

                usedQuestions.Add(questionIndex);
                totalPoints += feladatok[questionIndex].Pontszam;

                sw.Write(feladatok[questionIndex].Kerdes + "@");
            }

            sw.WriteLine("\nMax adható pont: " + totalPoints);
        }
    }
}