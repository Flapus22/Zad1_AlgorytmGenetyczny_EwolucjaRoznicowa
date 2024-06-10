using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad1_AlgorytmGenetyczny_EwolucjaRoznicowa;

public class Statistics
{
    public List<Individual> bestIndividualInIteration;
    public List<double> average;
    public List<Individual> worst;

    private ExtremumEnum extremum;

    public List<IndividualsInGeneretionCollection> individualsInGeneretions;

    public Statistics(ExtremumEnum extremum = ExtremumEnum.Minimum)
    {
        bestIndividualInIteration = new List<Individual>();
        average = new List<double>();
        worst = new List<Individual>();
        this.extremum = extremum;
        individualsInGeneretions = new List<IndividualsInGeneretionCollection>();
    }

    public void CalculateStatistics(List<Individual> population)
    {
        var potentialBest = population.OrderByDescending(x => x.Phenotype);
        var lastBest = bestIndividualInIteration.LastOrDefault();

        switch (extremum)
        {
            case ExtremumEnum.Minimum:
                CompareMinimum(potentialBest.Last(), lastBest);

                if (population[0].genotyp.Length == 2)// zbieraj te dane tylko dla 2 wymiarów (do symulacji)
                    SaveGenerationMin(population);
                break;

            case ExtremumEnum.Maximum:
                CompareMaximum(potentialBest.First(), lastBest);

                if (population[0].genotyp.Length == 2)// zbieraj te dane tylko dla 2 wymiarów (do symulacji)
                    SaveGenerationMax(population);
                break;
        };

        average.Add(population.Average(x => x.Phenotype));
        worst.Add(new Individual(population.OrderBy(x => x.Phenotype).First()));
    }

    private void SaveGenerationMin(List<Individual> population)
    {
        var temp = new IndividualsInGeneretionCollection();
        temp.bestIndividualInIteration = population.OrderBy(x => x.Phenotype).Take(10).ToList();
        individualsInGeneretions.Add(temp);
    }
    private void SaveGenerationMax(List<Individual> population)
    {
        var temp = new IndividualsInGeneretionCollection();
        temp.bestIndividualInIteration = population.OrderByDescending(x => x.Phenotype).Take(10).ToList();
        individualsInGeneretions.Add(temp);
    }

    private void CompareMinimum(Individual potentialBest, Individual? lastBest)
    {
        if (lastBest == null || lastBest.Phenotype > potentialBest.Phenotype)
        {
            bestIndividualInIteration.Add(new Individual(potentialBest));
        }
        else
        {
            bestIndividualInIteration.Add(new Individual(lastBest));
        }
    }

    private void CompareMaximum(Individual potentialBest, Individual? lastBest)
    {
        if (lastBest == null || lastBest.Phenotype < potentialBest.Phenotype)
        {
            bestIndividualInIteration.Add(new Individual(potentialBest));
        }
        else
        {
            bestIndividualInIteration.Add(new Individual(lastBest));
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Best individual in iteration:");
        foreach (var item in bestIndividualInIteration)
        {
            sb.AppendLine($"\t{item.ToString()}");
        }

        sb.AppendLine("\nAverage:");
        foreach (var item in average)
        {
            sb.AppendLine($"\t{item.ToString()}");
        }

        sb.AppendLine("\nWorst:");
        foreach (var item in worst)
        {
            sb.AppendLine($"\t{item.ToString()}");
        }

        return sb.ToString();
    }

    public class IndividualsInGeneretionCollection
    {
        public List<Individual> bestIndividualInIteration;

        public IndividualsInGeneretionCollection()
        {
            bestIndividualInIteration = new List<Individual>();
        }
    }
}
