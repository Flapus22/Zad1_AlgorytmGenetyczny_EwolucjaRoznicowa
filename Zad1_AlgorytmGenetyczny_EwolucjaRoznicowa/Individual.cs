using System.Text;

public class Individual
{
    public double Phenotype { get => phenotype; }

    public double[] genotyp;

    private double phenotype;

    public event Func<double[], double> calculatePhenotype;

    public Individual(double[] genotyp, Func<double[], double> phenotypeFunction)
    {
        this.genotyp = genotyp;
        this.calculatePhenotype = phenotypeFunction;
        CalculatePhenotype();
    }

    public Individual(Individual individual)
    {
        genotyp = new double[individual.genotyp.Length];
        Array.Copy(individual.genotyp, genotyp, individual.genotyp.Length);
        //this.genotyp = individual.genotyp.CopyTo;
        this.calculatePhenotype = individual.calculatePhenotype;
        CalculatePhenotype();
    }

    public void CalculatePhenotype()
    {
        if (calculatePhenotype == null)
            return;

        phenotype = calculatePhenotype.Invoke(genotyp);
    }

    public static Individual operator -(Individual a, Individual b)
    {
        if (a.genotyp.Length != b.genotyp.Length)
            throw new System.Exception("Genotypy muszą być tej samej długości");

        var genotyp = new double[a.genotyp.Length];
        for (int i = 0; i < a.genotyp.Length; i++)
        {
            genotyp[i] = a.genotyp[i] - b.genotyp[i];
        }
        return new Individual(genotyp, a.calculatePhenotype);
    }

    public static Individual operator +(Individual a, Individual b)
    {
        if (a.genotyp.Length != b.genotyp.Length)
            throw new System.Exception("Genotypy muszą być tej samej długości");

        var genotyp = new double[a.genotyp.Length];
        for (int i = 0; i < a.genotyp.Length; i++)
        {
            genotyp[i] = a.genotyp[i] + b.genotyp[i];
        }
        return new Individual(genotyp, a.calculatePhenotype);
    }

    public static Individual operator *(Individual a, int b)
    {
        return b * a;
    }

    public static Individual operator *(int a, Individual b)
    {
        var genotyp = new double[b.genotyp.Length];
        for (int i = 0; i < b.genotyp.Length; i++)
        {
            genotyp[i] = a * b.genotyp[i];
        }
        return new Individual(genotyp, b.calculatePhenotype);
    }
    public static Individual operator *(Individual a, double b)
    {
        return b * a;
    }
    public static Individual operator *(double a, Individual b)
    {
        var genotyp = new double[b.genotyp.Length];
        for (int i = 0; i < b.genotyp.Length; i++)
        {
            //genotyp[i] = a * b.genotyp[i];
        }
        return new Individual(genotyp, b.calculatePhenotype);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("Genotyp: ");
        foreach (var item in genotyp)
        {
            sb.Append($"{item}, ");
        }
        sb.Append(" Fenotyp: ");
        sb.Append(Phenotype);
        return sb.ToString();
    }
}
