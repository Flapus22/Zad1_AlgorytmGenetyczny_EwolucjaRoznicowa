namespace Zad1_AlgorytmGenetyczny_EwolucjaRoznicowa;

internal class Settings
{
    public int maxGeneration = 50;

    public int minX;
    public int maxX;

    public int populationCount;
    public int genotypeLength;

    public int? seed;
    public ExtremumEnum extremum;

    public Settings(int maxGeneration, int minX, int maxX, int populationCount, int genotypeLength, int? seed = null, ExtremumEnum extremum = ExtremumEnum.Minimum)
    {
        this.maxGeneration = maxGeneration;
        this.minX = minX;
        this.maxX = maxX;
        this.populationCount = populationCount;
        this.genotypeLength = genotypeLength;
        this.seed = seed;
        this.extremum = extremum;
    }
}
