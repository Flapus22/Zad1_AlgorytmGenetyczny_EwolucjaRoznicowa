using AlgorytmGenetyczny;
using System.Text;
using Zad1_AlgorytmGenetyczny_EwolucjaRoznicowa;

class GeneticAlgoritm
{
    public Statistics statistics;
    public double maxChanceToSelection = 0.9;

    private Settings setting;

    private double percentPopulactionSelection;
    private double mutationChance;

    private Random rnd = new Random();

    public event Func<double[], double> fenotypeFunction;

    public List<Individual> population;

    public GeneticAlgoritm(Settings setting, double percentPopulactionSelection, double mutationChance)
    {
        this.setting = setting;

        this.percentPopulactionSelection = percentPopulactionSelection;
        this.mutationChance = mutationChance;

        statistics = new Statistics(setting.extremum);

        if (setting.seed != null)
            rnd = new Random((int)setting.seed);
    }

    public void Start()
    {
        population = new List<Individual>();

        population = GeneratePopulation();
        //Console.WriteLine(ToString());

        var generation = 0;
        while (generation < setting.maxGeneration)
        {
            statistics.CalculateStatistics(population);
            generation++;
            //Selekcja z uzupełnieniem tablicy
            var tempIndividualList = Selection(population);
            if (tempIndividualList.Count < setting.populationCount)
            {
                var quantityToFill = setting.populationCount - tempIndividualList.Count;
                tempIndividualList.AddRange(GeneratePopulation(quantityToFill));
            }

            //Krzyżowanie
            for (int i = 0; i < tempIndividualList.Count; i += 2)
            {
                if (i + 1 >= tempIndividualList.Count)
                {
                    break;
                }
                Crossing(tempIndividualList[i], tempIndividualList[i + 1]);
            }

            //Mutacja 
            Mutation(tempIndividualList);

            //Sukcesja
            population = tempIndividualList;

            RatePopulation();
            //Console.WriteLine($"\nGeneracja: {generation}\n {ToString()}");
        }
    }

    private void Mutation(List<Individual> population)
    {
        for (int i = 0; i < population.Count; i++)
        {
            var temp = rnd.NextDouble();
            if (temp < mutationChance)
            {
                var genotypeIndexHashSet = RandomIndexHashSet();
                foreach (var item in genotypeIndexHashSet)
                {
                    double newGenotype = rnd.Next(setting.minX, setting.maxX);
                    newGenotype += rnd.NextDouble();
                    population[i].genotyp[item] = Math.Clamp(newGenotype, setting.minX, setting.maxX);
                }
            }
        }
    }

    private void Crossing(Individual first, Individual second)
    {
        HashSet<int> crossingIndexHashSet = RandomIndexHashSet();

        foreach (var item in crossingIndexHashSet)
        {
            (first.genotyp[item], second.genotyp[item]) = (second.genotyp[item], first.genotyp[item]);
        }
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

    private List<Individual> Selection(List<Individual> population)
    {
        return RouletteSelection(population);
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

    private List<Individual> RouletteSelection(List<Individual> population)
    {
        var result = new List<Individual>();

        var minPhenotype = population.Min(x => x.Phenotype);
        var maxPhenotype = population.Max(x => x.Phenotype);
        var temp = minPhenotype;
        switch (setting.extremum)
        {
            case ExtremumEnum.Minimum: // promuj minimalne wartości
                if(minPhenotype < 0)
                {
                    minPhenotype = (minPhenotype * 100) / maxChanceToSelection;
                }
                else
                {
                    minPhenotype = (minPhenotype * maxChanceToSelection) / 100;
                }
                break;
            case ExtremumEnum.Maximum: // promuj maksymalne wartości
                if (maxPhenotype < 0)
                {
                    maxPhenotype = (maxPhenotype * maxChanceToSelection) / 100;
                }
                else
                {
                    maxPhenotype = (maxPhenotype * 100) / maxChanceToSelection;
                }
                break;
        }

        var i = 0;
        int resultPopulationCount = (int)(population.Count() * percentPopulactionSelection);

        while (result.Count != resultPopulationCount)
        {
            var individualChance = Helper.InverseLerp(minPhenotype, maxPhenotype, population[i].Phenotype);
            var randomTemp = rnd.NextDouble();
            switch (setting.extremum)
            {
                case ExtremumEnum.Minimum: // promuj minimalne wartości
                    if (individualChance < randomTemp)
                        result.Add(population[i]);
                    //Console.WriteLine($"{i}: ===> {temp} > {randomTemp}");
                    break;
                case ExtremumEnum.Maximum: // promuj maksymalne wartości
                    if (individualChance > randomTemp)
                        result.Add(population[i]);
                    break;
            }
            i++;
            i %= population.Count;
        }

        return result;
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