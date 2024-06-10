using Zad1_AlgorytmGenetyczny_EwolucjaRoznicowa;

Console.WriteLine("Bocha");
Start_Bocha();
Console.WriteLine("Ackley");
Start_Ackley();
Console.WriteLine("Scwefel");
Start_Schwefel();
Console.WriteLine("Levy");
Start_Levy();
Console.WriteLine("Styblinski");
Start_Styblinski();


void Start_Bocha()
{
    Settings set = new Settings(50, -100, 100, 100, 2);

    GeneticAlgoritm geneticAlgoritm = new GeneticAlgoritm(set, 0.8, 0.1);
    DifferentialEvolution difAlgoritm = new DifferentialEvolution(set, 0.8, 2);

    geneticAlgoritm.fenotypeFunction += BohachevskyFunction;
    difAlgoritm.fenotypeFunction += BohachevskyFunction;

    difAlgoritm.Start();
    geneticAlgoritm.Start();

    Console.WriteLine(geneticAlgoritm.statistics.ToString());
    Console.WriteLine(difAlgoritm.statistics.ToString());

    var converter = new ConvertToCSV();
    converter.ConvertStatisticsToCSV(geneticAlgoritm.statistics, "./gen_Basic_Bocha.csv", ConvertToCSV.SaveType.Basic);// 
    converter.ConvertStatisticsToCSV(difAlgoritm.statistics, "./diff_Basic_Bocha.csv", ConvertToCSV.SaveType.Basic);
    converter.ConvertStatisticsToCSV(geneticAlgoritm.statistics, "./gen_Gene_Bocha.csv", ConvertToCSV.SaveType.Generations);//do wizualizacji
    converter.ConvertStatisticsToCSV(difAlgoritm.statistics, "./diff_Gene_Bocha.csv", ConvertToCSV.SaveType.Generations);//do wizualizacji
}

void Start_Ackley()
{
    Settings set = new Settings(1000, -35, 35, 100, 50);

    GeneticAlgoritm geneticAlgoritm = new GeneticAlgoritm(set, 0.8, 0.1);
    DifferentialEvolution difAlgoritm = new DifferentialEvolution(set, 0.8, 2);

    geneticAlgoritm.fenotypeFunction += AckleyFunction;
    difAlgoritm.fenotypeFunction += AckleyFunction;

    difAlgoritm.Start();
    geneticAlgoritm.Start();

    //Console.WriteLine(geneticAlgoritm.statistics.ToString());
    //Console.WriteLine(difAlgoritm.statistics.ToString());

    var converter = new ConvertToCSV();
    converter.ConvertStatisticsToCSV(geneticAlgoritm.statistics, "./gen_Basic_Ackley.csv", ConvertToCSV.SaveType.Basic);
    converter.ConvertStatisticsToCSV(difAlgoritm.statistics, "./diff_Basic_Ackley.csv", ConvertToCSV.SaveType.Basic);
    //converter.ConvertStatisticsToCSV(geneticAlgoritm.statistics, "./gen_Gene_Ackley.csv", ConvertToCSV.SaveType.Generations);
    //converter.ConvertStatisticsToCSV(difAlgoritm.statistics, "./diff_Gene_Ackley.csv", ConvertToCSV.SaveType.Generations);
}

void Start_Schwefel()
{
    Settings set = new Settings(1000, -500, 500, 100, 50);

    GeneticAlgoritm geneticAlgoritm = new GeneticAlgoritm(set, 0.8, 0.1);
    DifferentialEvolution difAlgoritm = new DifferentialEvolution(set, 0.8, 2);

    geneticAlgoritm.fenotypeFunction += SchwefelFunction;
    difAlgoritm.fenotypeFunction += SchwefelFunction;

    geneticAlgoritm.Start();
    difAlgoritm.Start();

    //Console.WriteLine(geneticAlgoritm.statistics.ToString());
    //Console.WriteLine(difAlgoritm.statistics.ToString());

    var converter = new ConvertToCSV();
    converter.ConvertStatisticsToCSV(geneticAlgoritm.statistics, "./gen_Basic_Schwefel.csv", ConvertToCSV.SaveType.Basic);
    converter.ConvertStatisticsToCSV(difAlgoritm.statistics, "./diff_Basic_Schwefel.csv", ConvertToCSV.SaveType.Basic);
    //converter.ConvertStatisticsToCSV(geneticAlgoritm.statistics, "./gen_Gene_Schwefel.csv", ConvertToCSV.SaveType.Generations);
    //converter.ConvertStatisticsToCSV(difAlgoritm.statistics, "./diff_Gene_Schwefel.csv", ConvertToCSV.SaveType.Generations);
}

void Start_Levy()
{
    Settings set = new Settings(1000, -10, 10, 100, 50);

    GeneticAlgoritm geneticAlgoritm = new GeneticAlgoritm(set, 0.8, 0.1);
    DifferentialEvolution difAlgoritm = new DifferentialEvolution(set, 0.8, 2);

    geneticAlgoritm.fenotypeFunction += LevyFunction;
    difAlgoritm.fenotypeFunction += LevyFunction;

    geneticAlgoritm.Start();
    difAlgoritm.Start();

    //Console.WriteLine(geneticAlgoritm.statistics.ToString());
    //Console.WriteLine(difAlgoritm.statistics.ToString());

    var converter = new ConvertToCSV();
    converter.ConvertStatisticsToCSV(geneticAlgoritm.statistics, "./gen_Basic_Levy.csv", ConvertToCSV.SaveType.Basic);
    converter.ConvertStatisticsToCSV(difAlgoritm.statistics, "./diff_Basic_Levy.csv", ConvertToCSV.SaveType.Basic);
    //converter.ConvertStatisticsToCSV(geneticAlgoritm.statistics, "./gen_Gene_Levy.csv", ConvertToCSV.SaveType.Generations);
    //converter.ConvertStatisticsToCSV(difAlgoritm.statistics, "./diff_Gene_Levy.csv", ConvertToCSV.SaveType.Generations);
}

void Start_Styblinski()
{
    Settings set = new Settings(1000, -5, 5, 100, 50);

    GeneticAlgoritm geneticAlgoritm = new GeneticAlgoritm(set, 0.8, 0.1);
    DifferentialEvolution difAlgoritm = new DifferentialEvolution(set, 0.8, 2);

    geneticAlgoritm.fenotypeFunction += StyblinskiFunction;
    difAlgoritm.fenotypeFunction += StyblinskiFunction;

    geneticAlgoritm.Start();
    difAlgoritm.Start();

    //Console.WriteLine(geneticAlgoritm.statistics.ToString());
    //Console.WriteLine(difAlgoritm.statistics.ToString());

    var converter = new ConvertToCSV();
    converter.ConvertStatisticsToCSV(geneticAlgoritm.statistics, "./gen_Basic_Styblinski.csv", ConvertToCSV.SaveType.Basic);
    converter.ConvertStatisticsToCSV(difAlgoritm.statistics, "./diff_Basic_Styblinski.csv", ConvertToCSV.SaveType.Basic);
    //converter.ConvertStatisticsToCSV(geneticAlgoritm.statistics, "./gen_Gene_Styblinski.csv", ConvertToCSV.SaveType.Generations);
    //converter.ConvertStatisticsToCSV(difAlgoritm.statistics, "./diff_Gene_Styblinski.csv", ConvertToCSV.SaveType.Generations);
}


// ----------------------
static double BohachevskyFunction(double[] genotype)
{
    double x1 = genotype[0];
    double x2 = genotype[1];

    double term1 = Math.Pow(x1, 2);
    double term2 = 2 * Math.Pow(x2, 2);
    double term3 = -0.3 * Math.Cos(3 * Math.PI * x1);
    double term4 = -0.4 * Math.Cos(4 * Math.PI * x2);

    double y = term1 + term2 + term3 + term4 + 0.7;
    return y;
}

static double AckleyFunction(double[] genotype)
{
    double a = 20;
    double b = 0.2;
    double c = 2 * Math.PI;

    int d = genotype.Length;

    double sum1 = 0;
    double sum2 = 0;
    for (int ii = 0; ii < d; ii++)
    {
        double xi = genotype[ii];
        sum1 = sum1 + Math.Pow(xi, 2);
        sum2 = sum2 + Math.Cos(c * xi);
    }

    double term1 = -a * Math.Exp(-b * Math.Sqrt(sum1 / d));
    double term2 = -Math.Exp(sum2 / d);

    double y = term1 + term2 + a + Math.Exp(1);
    return y;
}

static double SchwefelFunction(double[] genotype)
{
    int d = genotype.Length;
    double sum = 0;
    for (int ii = 0; ii < d; ii++)
    {
        double xi = genotype[ii];
        sum = sum + xi * Math.Sin(Math.Sqrt(Math.Abs(xi)));
    }

    double y = 418.9829 * d - sum;
    return y;
}

static double LevyFunction(double[] genotype)
{
    int d = genotype.Length;
    List<double> w = genotype.Select(x => 1 + (x - 1) / 4).ToList();

    double term1 = Math.Pow(Math.Sin(Math.PI * w[0]), 2);
    double term3 = Math.Pow(w[d - 1] - 1, 2) * (1 + Math.Pow(Math.Sin(2 * Math.PI * w[d - 1]), 2));

    double sum = 0;
    for (int ii = 0; ii < d - 1; ii++)
    {
        double wi = w[ii];
        double newTerm = Math.Pow(wi - 1, 2) * (1 + 10 * Math.Pow(Math.Sin(Math.PI * wi + 1), 2));
        sum = sum + newTerm;
    }

    double y = term1 + sum + term3;
    return y;
}

static double StyblinskiFunction(double[] genotype)
{
    int d = genotype.Length;
    double sum = 0;
    for (int ii = 0; ii < d; ii++)
    {
        double xi = genotype[ii];
        double newTerm = Math.Pow(xi, 4) - 16 * Math.Pow(xi, 2) + 5 * xi;
        sum = sum + newTerm;
    }

    double y = sum / 2;
    return y;
}