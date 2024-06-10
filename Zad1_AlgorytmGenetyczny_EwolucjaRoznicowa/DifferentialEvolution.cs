using System.Text;

namespace Zad1_AlgorytmGenetyczny_EwolucjaRoznicowa;

internal class DifferentialEvolution
{
    public Statistics statistics;
    public int maxGeneration = 50;

    private Settings setting;

    private double crossChance;
    private int mutationRate;

    private Random rnd = new Random();

    public event Func<double[], double> fenotypeFunction;

    public List<Individual> population;


    public DifferentialEvolution(Settings setting, double crossChance, int mutationRate)
    {
        this.setting = setting;
        this.crossChance = crossChance;
        this.mutationRate = mutationRate;

        statistics = new Statistics(setting.extremum);

        if (setting.seed != null)
            rnd = new Random((int)setting.seed);
    }


    public void Start()
    {
        population = GeneratePopulation();

        var generation = 0;
        while (generation < maxGeneration)
        {
            statistics.CalculateStatistics(population);
            generation++;

            var mutatedPopulation = Mutation(population);
            var crossedPopulation = Crossing(mutatedPopulation);

            //RatePopulation(mutatedPopulation);
            RatePopulation(crossedPopulation);

            var selectedPopulation = Selection(population, crossedPopulation);

            //Console.WriteLine($"Generacja {generation} \n{ToString()}\n");
            population = selectedPopulation;
        }
    }

    private List<Individual> Mutation(List<Individual> population)
    {
        var result = new List<Individual>(population.Count);

        for (int i = 0; i < setting.populationCount; i++)
        {
            var tempIndividual = new Individual(population[rnd.Next(0, setting.populationCount)] + mutationRate *
                (population[rnd.Next(0, setting.populationCount)] - population[rnd.Next(0, setting.populationCount)]));
            result.Add(tempIndividual);
        }
        return result;
    }

    private List<Individual> Crossing(List<Individual> individualList)
    {
        var result = new List<Individual>();

        for (int i = 0; i < setting.populationCount; i++)
        {
            var genotypeLenght = individualList[i].genotyp.Length;
            var newGenotype = new double[genotypeLenght];
            for (int j = 0; j < genotypeLenght; j++)
            {
                var randomChance = rnd.NextDouble();

                if (randomChance < crossChance)
                {
                    newGenotype[j] = individualList[i].genotyp[j];
                }
                else
                {
                    newGenotype[j] = population[rnd.Next(0, setting.populationCount)].genotyp[j];
                }
            }
            result.Add(new Individual(newGenotype, fenotypeFunction));
        }
        return result;
    }

    private HashSet<int> RandomIndexHashSet()
    {
        HashSet<int> result = new HashSet<int>();

        for (int i = 0; i < rnd.Next(setting.genotypeLength); i++)
        {
            result.Add(rnd.Next(setting.genotypeLength));
        }
        return result;
    }

    private List<Individual> Selection(List<Individual> populationA, List<Individual> populationB)
    {
        var result = new List<Individual>(setting.populationCount);
        //Select Better
        for (int i = 0; i < setting.populationCount; i++)
        {
            switch (setting.extremum)
            {
                case ExtremumEnum.Minimum:
                    ChooseMinimum(populationA[i], populationB[i], result);
                    break;
                case ExtremumEnum.Maximum:
                    ChooseMaximum(populationA[i], populationB[i], result);
                    break;
            }

        }
        return result;
    }

    private static void ChooseMaximum(Individual populationA, Individual populationB, List<Individual> result)
    {
        if (populationA.Phenotype > populationB.Phenotype)
        {
            result.Add(populationA);
        }
        else
        {
            result.Add(populationB);
        }
    }

    private static void ChooseMinimum(Individual populationA, Individual populationB, List<Individual> result)
    {
        if (populationA.Phenotype < populationB.Phenotype)
        {
            result.Add(populationA);
        }
        else
        {
            result.Add(populationB);
        }
    }

    private List<Individual> GeneratePopulation()
    {
        return GeneratePopulation(setting.populationCount);
    }

    private List<Individual> GeneratePopulation(int populationQuantity)
    {
        var result = new List<Individual>();

        for (int i = 0; i < populationQuantity; i++)
        {
            result.Add(new Individual(GenerateGenotype(), fenotypeFunction));
            result[i].CalculatePhenotype();
        }

        return result;
    }

    private double[] GenerateGenotype()
    {
        var result = new double[setting.genotypeLength];

        for (int i = 0; i < setting.genotypeLength; i++)
        {
            double temp = rnd.Next(setting.minX, setting.maxX);
            temp += rnd.NextDouble();
            result[i] = Math.Clamp(temp, setting.minX, setting.maxX);
        }

        return result;
    }

    private void RatePopulation()
    {
        foreach (var item in population)
        {
            item.CalculatePhenotype();
        }
    }

    private void RatePopulation(List<Individual> individualList)
    {
        foreach (var item in individualList)
        {
            item.CalculatePhenotype();
        }
    }

    private void Print(List<Individual> populationList, string? txt = null)
    {
        var msg = new StringBuilder();

        foreach (var item in populationList)
        {
            msg.Append($"{item}\n");
        }
        msg.AppendLine();
        Console.WriteLine(msg.ToString());
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        foreach (var item in population.OrderByDescending(x => x.Phenotype).ToList())
        {
            result.Append($"{item}\n");
        }
        result.AppendLine();
        return result.ToString();
    }
}
