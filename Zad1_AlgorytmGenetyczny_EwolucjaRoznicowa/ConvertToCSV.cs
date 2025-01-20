using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad1_AlgorytmGenetyczny_EwolucjaRoznicowa;

internal class ConvertToCSV
{
    public void ConvertStatisticsToCSV(Statistics statisticsList, string filePath, SaveType saveType = SaveType.Basic)
    {
        switch (saveType)
        {
            case SaveType.Basic:
                ConvertBasicStatics(statisticsList, filePath);
                break;
            case SaveType.Generations:
                ConvertGenerations(statisticsList, filePath);
                break;
        }
    }

    private void ConvertBasicStatics(Statistics statisticsList, string filePath)
    {
        StringBuilder csvContent = new StringBuilder();

        // Add header row
        csvContent.AppendLine("Best;Average;Worst");

        // Add data rows
        for (int i = 0; i < statisticsList.bestIndividualInIteration.Count; i++)
        {
            csvContent.AppendLine($"{statisticsList.bestIndividualInIteration[i].Phenotype};{statisticsList.average[i]};{statisticsList.worst[i].Phenotype}");
          
        }
        //Console.WriteLine(csvContent.ToString());
        // Write CSV content to file

        File.WriteAllText(filePath, csvContent.ToString());
    }

    private void ConvertGenerations(Statistics statisticsList, string filePath)
    {
        StringBuilder csvContent = new StringBuilder();

        // Add header row
        csvContent.AppendLine("X1;X2;Y;Generation");

        // Add data rows
        for (int i = 0; i < statisticsList.individualsInGeneretions.Count; i++)
        {
            var temp = statisticsList.individualsInGeneretions[i];

            for (int j = 0; j < temp.bestIndividualInIteration.Count; j++)
            {
                csvContent.AppendLine($"{temp.bestIndividualInIteration[j].genotyp[0]};{temp.bestIndividualInIteration[j].genotyp[1]};{temp.bestIndividualInIteration[j].Phenotype};{i}");
            }
        }

        // Write CSV content to file
        File.WriteAllText(filePath, csvContent.ToString());
    }

    public enum SaveType
    {
        Basic,
        Generations
    }
}
